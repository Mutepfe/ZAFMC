using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.PDF417;
using ZXing.PDF417.Internal;

namespace ZAFMC
{
    // PDF417 barcode of a Congregation member. No UI and no DB, so it can be tested without the page.
    public static class CongregationBarcode
    {
        // Symbol geometry. The PNG is shown at its native pixel size (never scaled by the page), so every
        // module stays a whole number of screen pixels and a phone / scanner can resolve it.
        // X = 2 px module width; each row is 4 modules high (Y/X = 4, spec minimum 3);
        // 5-module white quiet zone on every side (spec minimum 2).
        // 4 data columns = 137 modules wide -> (137 + 2 x 5) x 2 = 294 px, which fits the 300 px form column.
        private const int ModulePixels = 2;
        private const int QuietZoneModules = 5;
        private const int MinDataColumns = 4;
        private const int MaxDataColumns = 30;
        private const int RowModules = 4;

        // "MEMNO:..|TITLE:..|FIRSTNAME:..|SURNAME:..|DOB:yyyy-MM-dd|GENDER:..|ID:.." (UTF-8 text).
        // A '\' or '|' inside a value is escaped as "\\" / "\|". Empty / unselected values are encoded as empty.
        public static string BuildPayload(string membershipNumber, string title, string firstname, string surname,
            DateTime? dateOfBirth, string gender, string passportId)
        {
            StringBuilder sb = new StringBuilder();
            Append(sb, "MEMNO", membershipNumber);
            Append(sb, "TITLE", title == "-1" ? string.Empty : title);
            Append(sb, "FIRSTNAME", firstname);
            Append(sb, "SURNAME", surname);
            Append(sb, "DOB", dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty);
            Append(sb, "GENDER", gender);
            Append(sb, "ID", passportId);
            return sb.ToString();
        }

        private static void Append(StringBuilder sb, string label, string value)
        {
            if (sb.Length > 0)
            {
                sb.Append('|');
            }
            sb.Append(label).Append(':').Append(Escape(value));
        }

        private static string Escape(string value)
        {
            return (value ?? string.Empty).Replace("\\", "\\\\").Replace("|", "\\|");
        }

        // PDF417 PNG of the payload: error correction level 4, pure black / white 1-bit image,
        // integer module size (nearest-neighbour, no smoothing). Plain-ASCII payloads carry no ECI
        // (some phone apps mis-handle it); a payload with non-ASCII characters is encoded as UTF-8 with an ECI.
        public static byte[] RenderPng(string payload)
        {
            // Fewest columns that fit (4 for normal member details). Only an unusually dense payload
            // gets more columns, i.e. a wider image, instead of failing.
            for (int columns = MinDataColumns; ; columns++)
            {
                // Rows are capped so the symbol stays wider than tall (17 x columns + 69 modules wide):
                // ZXing would otherwise turn a taller-than-wide symbol sideways.
                int maxRows = Math.Min(90, (17 * columns + 69 - 1) / RowModules);
                try
                {
                    return ToPng(EncodeSymbol(payload, new Dimensions(columns, columns, 3, maxRows)), ModulePixels);
                }
                catch (WriterException)
                {
                    if (columns == MaxDataColumns)
                    {
                        throw;
                    }
                }
            }
        }

        // One matrix cell per module horizontally and 4 cells per row vertically, quiet zone included.
        private static BitMatrix EncodeSymbol(string payload, Dimensions dimensions)
        {
            bool ascii = true;
            foreach (char c in payload)
            {
                if (c > 0x7F)
                {
                    ascii = false;
                    break;
                }
            }
            PDF417EncodingOptions options = new PDF417EncodingOptions
            {
                Margin = QuietZoneModules,
                ErrorCorrection = PDF417ErrorCorrectionLevel.L4,
                Compact = false,
                Compaction = Compaction.AUTO,
                AspectRatio = PDF417AspectRatio.A4,
                Dimensions = dimensions,
                DisableECI = ascii
            };
            if (!ascii)
            {
                options.CharacterSet = "UTF-8";
            }
            // Width / Height 0: no scaling by ZXing (it would scale the bars but not the margin); scaled below.
            return new PDF417Writer().encode(payload, BarcodeFormat.PDF_417, 0, 0, options.Hints);
        }

        // 1-bit PNG, each matrix cell drawn as a scale x scale block of pure black or white pixels.
        private static byte[] ToPng(BitMatrix matrix, int scale)
        {
            int width = matrix.Width * scale;
            int height = matrix.Height * scale;
            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed))
            {
                bitmap.SetResolution(96, 96);
                // Default 1-bit palette: index 0 = black, index 1 = white
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
                try
                {
                    byte[] row = new byte[data.Stride];
                    for (int y = 0; y < height; y++)
                    {
                        for (int i = 0; i < row.Length; i++)
                        {
                            row[i] = 0xFF; // white
                        }
                        int my = y / scale;
                        for (int x = 0; x < width; x++)
                        {
                            if (matrix[x / scale, my])
                            {
                                row[x >> 3] &= (byte)~(0x80 >> (x & 7)); // black
                            }
                        }
                        Marshal.Copy(row, 0, data.Scan0 + y * data.Stride, row.Length);
                    }
                }
                finally
                {
                    bitmap.UnlockBits(data);
                }
                using (MemoryStream png = new MemoryStream())
                {
                    bitmap.Save(png, ImageFormat.Png);
                    return png.ToArray();
                }
            }
        }

        // Pixel size of a PNG (read from its IHDR header), or null if the bytes are not a PNG.
        // Lets the page show a stored barcode at exactly its native size.
        public static Size? PngSize(byte[] png)
        {
            if (png == null || png.Length < 24 || png[0] != 0x89 || png[1] != 0x50 || png[2] != 0x4E || png[3] != 0x47)
            {
                return null;
            }
            int width = (png[16] << 24) | (png[17] << 16) | (png[18] << 8) | png[19];
            int height = (png[20] << 24) | (png[21] << 16) | (png[22] << 8) | png[23];
            return width > 0 && height > 0 ? new Size(width, height) : (Size?)null;
        }
    }
}
