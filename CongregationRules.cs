using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ZAFMC
{
    // Validation rules for the Congregation screen. Pure logic (no UI, no DB) so it can be tested
    // without the page. Every Validate* method returns null when valid, otherwise a user-facing message.
    public static class CongregationRules
    {
        public const int SearchMaxLength = 20;

        // Format used by <input type="date"> when posting.
        private const string DateFormat = "yyyy-MM-dd";

        private static readonly Regex MembershipNumberPattern = new Regex("^ZAFMC[0-9]{8}$", RegexOptions.CultureInvariant);

        // Date Of Birth is optional, but when given it must be a valid date and not later than today.
        public static string ValidateDateOfBirth(string text, DateTime today, out DateTime? dob)
        {
            string error = ValidateOptionalDate(text, "Date Of Birth", out dob);
            if (error != null)
            {
                return error;
            }
            if (dob.HasValue && dob.Value.Date > today.Date)
            {
                dob = null;
                return "Date Of Birth cannot be later than today.";
            }
            return null;
        }

        // Empty -> valid with a null value; otherwise must be a valid yyyy-MM-dd date.
        public static string ValidateOptionalDate(string text, string label, out DateTime? value)
        {
            value = null;
            string trimmed = (text ?? string.Empty).Trim();
            if (trimmed.Length == 0)
            {
                return null;
            }
            DateTime parsed;
            if (!DateTime.TryParseExact(trimmed, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return label + " is not a valid date.";
            }
            value = parsed.Date;
            return null;
        }

        // Exactly one of Male / Female must be selected.
        public static string ValidateGender(string selectedValue)
        {
            if (string.Equals(selectedValue, "Male", StringComparison.Ordinal) ||
                string.Equals(selectedValue, "Female", StringComparison.Ordinal))
            {
                return null;
            }
            return "Please select Male or Female.";
        }

        public static string ValidateRequired(string value, string label)
        {
            return string.IsNullOrWhiteSpace(value) ? label + " is required." : null;
        }

        public static string ValidateMaxLength(string value, int maxLength, string label)
        {
            return (value ?? string.Empty).Length > maxLength
                ? label + " cannot be longer than " + maxLength + " characters."
                : null;
        }

        // Largest photo / ID document that is stored (each).
        public const int MaxUploadBytes = 5 * 1024 * 1024;

        // Content type from the file's first bytes (signature), not from its name: JPEG, PNG or PDF, else null.
        public static string DetectFileType(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            if (data.Length >= 3 && data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            {
                return "image/jpeg";
            }
            if (data.Length >= 8 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47
                && data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A)
            {
                return "image/png";
            }
            if (data.Length >= 5 && data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46 && data[4] == 0x2D)
            {
                return "application/pdf"; // "%PDF-"
            }
            return null;
        }

        // An uploaded photo (JPEG / PNG) or ID document (JPEG / PNG / PDF when allowPdf), at most 5 MB.
        // No file -> valid with contentType null.
        public static string ValidateUpload(byte[] data, string label, bool allowPdf, out string contentType)
        {
            contentType = null;
            if (data == null || data.Length == 0)
            {
                return null;
            }
            if (data.Length > MaxUploadBytes)
            {
                return label + " is larger than 5 MB.";
            }
            string type = DetectFileType(data);
            if (type == null || (type == "application/pdf" && !allowPdf))
            {
                return allowPdf
                    ? label + " must be a JPEG, PNG or PDF file."
                    : label + " must be a JPEG or PNG image.";
            }
            contentType = type;
            return null;
        }

        // Fields encoded in the barcode that must be filled before "Generate Barcode".
        // Returns null when all are filled, otherwise a message listing the missing ones.
        public static string ValidateBarcodeFields(string title, string firstname, string surname, string dateOfBirth, string gender, string passportId)
        {
            List<string> missing = MissingBarcodeFields(title, firstname, surname, dateOfBirth, gender, passportId);
            return missing.Count == 0
                ? null
                : "Fill in these details before generating the barcode:\n\n" + string.Join("\n", missing);
        }

        // Same fields, checked by Save of a new member (Save generates the barcode when it was not generated yet).
        // Returns null when all are filled, otherwise one message listing every missing field.
        public static string ValidateSaveFields(string title, string firstname, string surname, string dateOfBirth, string gender, string passportId)
        {
            List<string> missing = MissingBarcodeFields(title, firstname, surname, dateOfBirth, gender, passportId);
            return missing.Count == 0
                ? null
                : "Please capture all details before saving: " + string.Join(", ", missing);
        }

        // Labels of the barcode fields that are empty / not selected, in form order.
        public static List<string> MissingBarcodeFields(string title, string firstname, string surname, string dateOfBirth, string gender, string passportId)
        {
            List<string> missing = new List<string>();
            if (string.IsNullOrWhiteSpace(title) || title == "-1") missing.Add("Title");
            if (string.IsNullOrWhiteSpace(firstname)) missing.Add("Firstname");
            if (string.IsNullOrWhiteSpace(surname)) missing.Add("Surname");
            if (string.IsNullOrWhiteSpace(dateOfBirth)) missing.Add("Date Of Birth");
            if (ValidateGender(gender) != null) missing.Add("Male / Female");
            if (string.IsNullOrWhiteSpace(passportId)) missing.Add("ID Number / Passport");
            return missing;
        }

        // "ZAFMC" followed by exactly 8 digits, e.g. ZAFMC04718263.
        public static bool IsMembershipNumber(string value)
        {
            return value != null && MembershipNumberPattern.IsMatch(value);
        }

        // Turns a search term into a LIKE "starts with" pattern, escaping LIKE wildcards.
        public static string ToLikePrefix(string term)
        {
            string t = (term ?? string.Empty).Trim();
            t = t.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
            return t + "%";
        }
    }
}
