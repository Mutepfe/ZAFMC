using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity;
using System.Data.Sql;
using System.Configuration; //For DBase connection in the Web.Config
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Web.ModelBinding;
using ZAFMC.Models;

namespace ZAFMC
{
    public partial class Congregation : System.Web.UI.Page
    {
        // Passport / ID of the member loaded from the grid (null = new-member mode).
        // Kept in the Page's ViewState: the form panels have ViewState disabled.
        private string EditPassportID
        {
            get { return ViewState["EditPassportID"] as string; }
            set { ViewState["EditPassportID"] = value; }
        }

        // Membership Number shown in the read-only box while that member is current.
        private string CurrentMembershipNumber
        {
            get { return ViewState["CurrentMembershipNumber"] as string; }
            set { ViewState["CurrentMembershipNumber"] = value; }
        }

        // PDF417 barcode (PNG, Base64) shown in the Barcode Image box while that member / preview is current.
        private string CurrentBarcodePng
        {
            get { return ViewState["CurrentBarcodePng"] as string; }
            set { ViewState["CurrentBarcodePng"] = value; }
        }

        // New member: Membership Number reserved by Generate Barcode / Save (dbo.ReserveMembershipNumber).
        // Server-side only (MAC-protected ViewState); the read-only box is never read back. Reused until the
        // member is saved, and cleared by Reset / Select (the number stays burned in the registry).
        private string ReservedMembershipNumber
        {
            get { return ViewState["ReservedMembershipNumber"] as string; }
            set { ViewState["ReservedMembershipNumber"] = value; }
        }

        // New member: payload of the barcode shown in CurrentBarcodePng (Save regenerates it only when this changed)
        private string GeneratedBarcodePayload
        {
            get { return ViewState["GeneratedBarcodePayload"] as string; }
            set { ViewState["GeneratedBarcodePayload"] = value; }
        }

        // Barcode payload built from the loaded member's saved values (null = new-member mode).
        // Used to tell whether Update changed an encoded field, and to refuse encoding unsaved edits.
        private string LoadedBarcodePayload
        {
            get { return ViewState["LoadedBarcodePayload"] as string; }
            set { ViewState["LoadedBarcodePayload"] = value; }
        }

        // Stored dropdown values of the loaded member that are not in the dropdown's list
        // (control ID -> value). Re-added on every request so Update does not lose them.
        private Hashtable LegacyDropDowns
        {
            get { return ViewState["LegacyDropDowns"] as Hashtable ?? new Hashtable(); }
        }

        // Every dropdown of the member form
        private DropDownList[] MemberDropDowns
        {
            get { return new[] { Titles, MaritalStatus, RankPosition, ManagerialPost, Province, District, Zones, Sect, SeniorLeader, ViceLeader }; }
        }

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            //Block future Dates Of Birth in the browser (panel ViewState is disabled, so set on every request)
            DOB.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            //Reset only on first load; on postbacks the click handlers need the posted values
            if (!IsPostBack)
            {
                ResetForm(); //Resets Web Controls
            }


        }

        // Re-applies the current member state on every request (the form panels have ViewState disabled)
        protected void Page_PreRender(object sender, EventArgs e)
        {
            MembershipNumber.Text = CurrentMembershipNumber ?? string.Empty;

            string barcode = CurrentBarcodePng;
            Barcode.ImageUrl = barcode == null ? string.Empty : "data:image/png;base64," + barcode;
            Barcode.AlternateText = barcode == null ? string.Empty : "PDF417 barcode of the member";
            // Show the barcode at its exact pixel size: a scaled PDF417 (e.g. squeezed into the 150 x 70 box)
            // blurs its narrow bars and cannot be scanned. The empty box keeps its 150 x 70 placeholder size.
            var barcodeSize = barcode == null ? null : CongregationBarcode.PngSize(Convert.FromBase64String(barcode));
            Barcode.Width = Unit.Pixel(barcodeSize.HasValue ? barcodeSize.Value.Width : 150);
            Barcode.Height = Unit.Pixel(barcodeSize.HasValue ? barcodeSize.Value.Height : 70);

            ShowStoredFiles(EditPassportID);

            if (EditPassportID != null)
            {
                PassportID.Text = EditPassportID;
                PassportID.ReadOnly = true;
            }

            foreach (DropDownList ddl in MemberDropDowns)
            {
                string legacy = LegacyDropDowns[ddl.ID] as string;
                if (legacy == null || ddl.Items.FindByValue(legacy) != null)
                {
                    continue; // nothing remembered, or the option is still there (load request)
                }
                bool reposted = GetDropDownValue(ddl) == legacy;
                ListItem item = new ListItem(legacy, legacy);
                ddl.Items.Insert(1, item);
                if (reposted)
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }

        //Delete  Congregation (Event)
        protected void DeleteCongregation_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            string key = EditPassportID ?? PassportID.Text;

            string error = CongregationRules.ValidateRequired(key, "Passport / ID")
                ?? CongregationRules.ValidateMaxLength(key, 15, "Passport / ID");
            if (error != null)
            {
                ShowValidationError(error, "ZAFMC - Delete Record");
                return;
            }

            DialogResult MPJ = TopMostDialogs.ShowTopMost("Do you want to Delete this Record..!!", "ZAFMC - Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MPJ == DialogResult.Yes)
            {
                int? rows = CongregationDelete(key);
                if (rows == null)
                {
                    return; // error already shown
                }
                if (rows == 0)
                {
                    TopMostDialogs.ShowTopMost("No member with Passport / ID '" + key + "' was found. Nothing was deleted.", "ZAFMC - Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ResetForm(); //Resets Web Controls
                CongregationConnection(); // Refresh the Database
                TopMostDialogs.ShowTopMost("Member " + key + " deleted.", "ZAFMC - Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
               if (MPJ == DialogResult.No)
            {
                ResetForm(); //Resets Web Controls (also clears the search box)
                CongregationConnection(); //Refresh the DBase
            }

        }

        //Delete  Congregation(Method): returns the number of deleted rows, or null on error
        private int? CongregationDelete(string passportId)
        {
            //Using SQL Stored Procedure

            string CongDel = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CD = new SqlConnection(CongDel);
            SqlCommand Cong = new SqlCommand("DeleteCongregation", CD);

            try
            {
                Cong.Parameters.AddWithValue("@DelCONG", passportId); //@DelCONG StoredProcedure actual parameter passed in the DB
                SqlParameter rows = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
                Cong.Parameters.Add(rows);
                Cong.CommandType = System.Data.CommandType.StoredProcedure;
                CD.Open();
                Cong.ExecuteNonQuery();
                return rows.Value == DBNull.Value ? 0 : Convert.ToInt32(rows.Value);

            }
            catch (Exception ex)
            {
                ShowDbError(ex, "deleted", "ZAFMC - Delete Error");
                return null;
            }
            finally
            {
                Cong.Dispose(); //Clean up memory
                CD.Close(); //Close DBase  connection
            }



        }

        //AddNew Congregation (Event)
        protected void SaveCongregation_Click(object sender, EventArgs e)
        {
            if (EditPassportID != null)
            {
                TopMostDialogs.ShowTopMost("You are editing an existing member. Use Edit to update it, or Reset for a new member.", "ZAFMC - Save Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Every barcode detail is required: Save generates the barcode when it was not generated yet
            string missing = CongregationRules.ValidateSaveFields(GetDropDownValue(Titles), Firstname.Text, Surname.Text,
                DOB.Text, Gender.SelectedValue, PassportID.Text);
            if (missing != null)
            {
                ShowValidationError(missing, "ZAFMC - Save Record");
                return; // nothing saved, no number reserved
            }
            DateTime? dob, appointed, elected;
            if (!TryReadMemberForm("ZAFMC - Save Record", out dob, out appointed, out elected))
            {
                return;
            }
            MemberUploads files = ReadUploads("ZAFMC - Save Record");
            if (files == null)
            {
                return;
            }

            // Same as ticking Generate Barcode: the page's reserved number (reserved now if not yet) and its barcode
            string reserved = EnsureReservedMembershipNumber();
            if (reserved == null)
            {
                TopMostDialogs.ShowTopMost("The Membership Number could not be generated. Please contact the IT Department.", "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] barcode = NewMemberBarcode(reserved);
            if (barcode != null)
            {
                GenerateBarcode.Checked = true;
            }

            string number = CongregationAddNew(dob, appointed, elected, files, reserved, barcode); // Add or Save Record (barcode in the same INSERT)
            if (number == null)
            {
                return; // Insert failed: keep the entered values and the reservation (a corrected Save reuses it)
            }
            bool newNumber = number != reserved;
            if (newNumber)
            {
                // The reserved number was taken meanwhile: AddNewCongregation saved the member WITHOUT a barcode
                // (CongBarcode NULL, migration 006), because the one passed in encodes the reserved number.
                // Store a barcode with the number actually saved. A failure here never undoes the save: the member
                // is kept without a barcode, the on-screen barcode (reserved number) is cleared and a warning is shown.
                barcode = GenerateFormBarcode(number);
                if (barcode == null || StoreBarcode(PassportID.Text, barcode) != 1)
                {
                    barcode = null;
                }
            }
            string uploads = UpLoadsFiles();       //Save Photo
            ResetForm(); //Resets Web Controls
            CongregationConnection(); // Refresh Database
            CurrentMembershipNumber = number; // Show the saved Membership Number (re-applied in Page_PreRender)
            CurrentBarcodePng = barcode == null ? null : Convert.ToBase64String(barcode); // Show the stored barcode (none when the member has none)
            if (newNumber)
            {
                TopMostDialogs.ShowTopMost("The reserved Membership Number " + reserved + " was already used, so the member was saved with a new Membership Number: " + number + ".", "ZAFMC - Save Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            TopMostDialogs.ShowTopMost("Member saved. Membership Number: " + number + "." + uploads, "ZAFMC - Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (barcode == null)
            {
                // The row has no barcode (CongBarcode NULL): generating or storing it failed
                TopMostDialogs.ShowTopMost("Member " + number + " was saved without a barcode, because the barcode could not be generated or saved. Select the member and tick Generate Barcode to try again.", "ZAFMC - Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            MembershipNumber.Text = "ZAFMC Membership";


        }
        //AddNew Congregation (Method): inserts the validated form with its barcode (CongBarcode).
        //reserved: the page's reserved number, used when still free; otherwise the database issues a fresh one.
        //Returns the Membership Number actually saved, or null when nothing was saved.
        private string CongregationAddNew(DateTime? dob, DateTime? appointed, DateTime? elected, MemberUploads files, string reserved, byte[] barcode)
        {
            string CongInsert = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CID = new SqlConnection(CongInsert);
            SqlCommand Prosper = new SqlCommand("AddNewCongregation", CID);

            try
            {

                Prosper.CommandType = CommandType.StoredProcedure;

                AddMemberParameters(Prosper, dob, appointed, elected, barcode, files);
                // Server-side reservation only (never the posted box); the database checks it is still free
                Prosper.Parameters.Add(new SqlParameter("@ReservedMembershipNumber", SqlDbType.NVarChar, 20) { Value = (object)reserved ?? DBNull.Value });
                // Number actually saved (the reserved one, or a fresh one); any value typed or posted for it is ignored
                SqlParameter membership = new SqlParameter("@CongMembershipNumber", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output };
                Prosper.Parameters.Add(membership);

                CID.Open();
                Prosper.ExecuteNonQuery();

                string number = Convert.ToString(membership.Value);
                if (!CongregationRules.IsMembershipNumber(number))
                {
                    TopMostDialogs.ShowTopMost("The Membership Number could not be generated. Please contact the IT Department.", "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                return number;

            }
            catch (Exception P)
            {
                ShowDbError(P, "Saved", "ZAFMC-Insert Error");
                return null;
            }
            finally
            {
                Prosper.Dispose(); //Clean up memory
                CID.Close(); //Close DBase  connection

            }
        }

        // Uploaded photo / ID document of this request, checked by file signature (stored as bytes on the member's row)
        private sealed class MemberUploads
        {
            public byte[] Photo;
            public string PhotoType;
            public byte[] PassID;
            public string PassIDType;
            public string PassIDFileName;
        }

        // Reads and checks the uploads; shows a Warning (title = the operation) and returns null when a file is rejected
        private MemberUploads ReadUploads(string title)
        {
            MemberUploads files = new MemberUploads();
            if (PhotoUpload.HasFile)
            {
                files.Photo = PhotoUpload.FileBytes;
            }
            if (IDPass.HasFile)
            {
                files.PassID = IDPass.FileBytes;
                files.PassIDFileName = System.IO.Path.GetFileName(IDPass.FileName);
            }

            string error = CongregationRules.ValidateUpload(files.Photo, "The photo", false, out files.PhotoType)
                ?? CongregationRules.ValidateUpload(files.PassID, "The ID / Passport document", true, out files.PassIDType)
                ?? CongregationRules.ValidateMaxLength(files.PassIDFileName, 260, "The ID / Passport file name");
            if (error != null)
            {
                ShowValidationError(error + "\n\nNothing was saved.", title);
                return null;
            }
            return files;
        }

        // Validates the member form; shows the first problem (title = the operation) and returns false when invalid
        private bool TryReadMemberForm(string title, out DateTime? dob, out DateTime? appointed, out DateTime? elected)
        {
            dob = null;
            appointed = null;
            elected = null;

            string key = EditPassportID ?? PassportID.Text;

            string error = CongregationRules.ValidateRequired(key, "Passport / ID")
                ?? CongregationRules.ValidateMaxLength(Firstname.Text, 15, "Firstname")
                ?? CongregationRules.ValidateMaxLength(Surname.Text, 20, "Surname")
                ?? CongregationRules.ValidateMaxLength(key, 15, "Passport / ID")
                ?? CongregationRules.ValidateMaxLength(Profession.Text, 20, "Profession")
                ?? CongregationRules.ValidateMaxLength(NextOfKin.Text, 20, "Next Of Kin")
                ?? CongregationRules.ValidateMaxLength(KinContact.Text, 20, "Kin Contact")
                ?? CongregationRules.ValidateMaxLength(CellNumber.Text, 20, "Cell Number")
                ?? CongregationRules.ValidateMaxLength(PhysAddress.Text, 50, "Physical Address")
                ?? CongregationRules.ValidateMaxLength(EmailAdd.Text, 50, "Email Address")
                // Dropdown values must fit their proc parameters (no silent truncation)
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(Titles), 5, "Title")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(MaritalStatus), 10, "Marital Status")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(RankPosition), 50, "Rank / Position")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(ManagerialPost), 50, "Managerial Post")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(Province), 30, "Province")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(District), 30, "District")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(Zones), 30, "Zone")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(Sect), 30, "Section")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(SeniorLeader), 20, "Snr. Leader")
                ?? CongregationRules.ValidateMaxLength(GetDropDownValue(ViceLeader), 20, "Vice-Leader")
                ?? CongregationRules.ValidateGender(Gender.SelectedValue)
                ?? CongregationRules.ValidateDateOfBirth(DOB.Text, DateTime.Today, out dob)
                ?? CongregationRules.ValidateOptionalDate(DateAppointed.Text, "Date Appointed", out appointed)
                ?? CongregationRules.ValidateOptionalDate(DateElected.Text, "Date Elected", out elected);

            if (error != null)
            {
                ShowValidationError(error, title);
                return false;
            }
            return true;
        }

        private static void ShowValidationError(string message, string title)
        {
            TopMostDialogs.ShowTopMost(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Logs the full error on the server and shows a friendly message (no SQL details)
        private static void ShowDbError(Exception ex, string action, string title)
        {
            System.Diagnostics.Trace.TraceError(ex.ToString());
            SqlException sql = ex as SqlException;
            if (sql != null && (sql.Number == 2627 || sql.Number == 2601))
            {
                TopMostDialogs.ShowTopMost("A member with this Passport / ID already exists.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TopMostDialogs.ShowTopMost("The record could not be " + action + ". Please contact the IT Department.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Value of a member dropdown, including a remembered legacy value that was posted back
        private string GetDropDownValue(DropDownList ddl)
        {
            string value = ddl.SelectedValue;
            if (value != "-1")
            {
                return value;
            }
            string legacy = LegacyDropDowns[ddl.ID] as string;
            // Event validation has already checked that the posted value was rendered as an option
            if (legacy != null && Request.Form[ddl.UniqueID] == legacy)
            {
                return legacy;
            }
            return "-1";
        }

        // Selects a stored value; a value that is not in the list is added as an extra option and remembered
        private void SetDropDown(DropDownList ddl, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "-1";
            }
            ddl.ClearSelection();
            ListItem item = ddl.Items.FindByValue(value);
            if (item == null)
            {
                item = new ListItem(value, value);
                ddl.Items.Insert(1, item);
                Hashtable legacy = LegacyDropDowns;
                legacy[ddl.ID] = value;
                ViewState["LegacyDropDowns"] = legacy; // re-assign so the change is saved
            }
            item.Selected = true;
        }

        // Parameters shared by AddNewCongregation and UpdateCongregation (names match the procs)
        // barcode: PNG for @CongBarcode, or null (Add: set after the insert; Update: keeps the stored barcode)
        // files: uploaded photo / ID bytes; a file that was not uploaded is sent as NULL (Update keeps the stored one)
        private void AddMemberParameters(SqlCommand cmd, DateTime? dob, DateTime? appointed, DateTime? elected, byte[] barcode, MemberUploads files)
        {
            cmd.Parameters.AddWithValue("@CongTitle", GetDropDownValue(Titles));
            cmd.Parameters.AddWithValue("@CongName", Firstname.Text);
            cmd.Parameters.AddWithValue("@CongSurname", Surname.Text);
            cmd.Parameters.Add(new SqlParameter("@CongDOB", SqlDbType.Date) { Value = (object)dob ?? DBNull.Value });
            cmd.Parameters.AddWithValue("@CongGender", Gender.SelectedValue);
            cmd.Parameters.AddWithValue("@CongPassportID", EditPassportID ?? PassportID.Text); // a loaded member is always keyed by its own ID
            cmd.Parameters.AddWithValue("@CongStatus", GetDropDownValue(MaritalStatus));
            cmd.Parameters.AddWithValue("@CongProfession", Profession.Text);
            cmd.Parameters.AddWithValue("@CongKin", NextOfKin.Text);
            cmd.Parameters.AddWithValue("@CongKinContact", KinContact.Text);
            cmd.Parameters.AddWithValue("@CongCell", CellNumber.Text);
            cmd.Parameters.AddWithValue("@CongAddress", PhysAddress.Text);
            cmd.Parameters.AddWithValue("@CongEmail", EmailAdd.Text);
            cmd.Parameters.AddWithValue("@CongPosition", GetDropDownValue(RankPosition));
            cmd.Parameters.Add(new SqlParameter("@CongDateAppointed", SqlDbType.Date) { Value = (object)appointed ?? DBNull.Value });
            cmd.Parameters.AddWithValue("@CongManagerial", GetDropDownValue(ManagerialPost));
            cmd.Parameters.Add(new SqlParameter("@CongDateElected", SqlDbType.Date) { Value = (object)elected ?? DBNull.Value });
            cmd.Parameters.AddWithValue("@CongProvince", GetDropDownValue(Province));
            cmd.Parameters.AddWithValue("@CongDistrict", GetDropDownValue(District));
            cmd.Parameters.AddWithValue("@CongZone", GetDropDownValue(Zones));
            cmd.Parameters.AddWithValue("@CongSection", GetDropDownValue(Sect));
            cmd.Parameters.AddWithValue("@CongSnrLeader", GetDropDownValue(SeniorLeader));
            cmd.Parameters.AddWithValue("@CongViceLeader", GetDropDownValue(ViceLeader));
            // The photo box may show the stored photo as an inline data: URL; that is never a path to store
            cmd.Parameters.AddWithValue("@CongPhoto", PersonPhoto.ImageUrl.StartsWith("data:", StringComparison.Ordinal) ? string.Empty : PersonPhoto.ImageUrl);
            cmd.Parameters.AddWithValue("@CongPassID", PassID.ImageUrl);
            cmd.Parameters.AddWithValue("@CongregationID", DBNull.Value);
            // image columns: must be typed as Image (an nvarchar value clashes with image)
            cmd.Parameters.Add(new SqlParameter("@CongFingerprint", SqlDbType.Image) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongBarcode", SqlDbType.Image) { Value = (object)barcode ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongPhotoData", SqlDbType.VarBinary, -1) { Value = (object)files.Photo ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongPhotoContentType", SqlDbType.NVarChar, 100) { Value = (object)files.PhotoType ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongPassIDData", SqlDbType.VarBinary, -1) { Value = (object)files.PassID ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongPassIDContentType", SqlDbType.NVarChar, 100) { Value = (object)files.PassIDType ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongPassIDFileName", SqlDbType.NVarChar, 260) { Value = (object)files.PassIDFileName ?? DBNull.Value });
        }

        //Refresh Congregation(Event): clear the form and the search box first, then rebind the full list
        protected void RefreshCongregation_Click(object sender, EventArgs e)
        {
            ResetForm(); //Resets Web Controls
            CongregationConnection();
        }
        //Refresh Congregation(Method): rebinds the Congregants grid through the ZAFMCCong data source
        public void CongregationConnection()
        {
            try
            {
                CongregantsList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the Database");
            }

        }

        //Update Congregation(Event): validates first, then asks Yes / No
        protected void EditCongregation_Click(object sender, EventArgs e)
        {
            DateTime? dob, appointed, elected;
            if (!TryReadMemberForm("ZAFMC - Update Record", out dob, out appointed, out elected))
            {
                return;
            }
            MemberUploads files = ReadUploads("ZAFMC - Update Record");
            if (files == null)
            {
                return;
            }

            DialogResult MPJ = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MPJ == DialogResult.Yes)
            {
                string key = EditPassportID ?? PassportID.Text;
                // Keep the barcode in sync: regenerate it (saved in the same UPDATE) when an encoded field
                // of the loaded member changed. Membership Number and Passport / ID never change.
                byte[] barcode = null;
                bool barcodeFailed = false;
                // A member without a stored barcode also gets one.
                if (EditPassportID != null && (BuildFormPayload(CurrentMembershipNumber) != LoadedBarcodePayload || CurrentBarcodePng == null))
                {
                    barcode = GenerateFormBarcode(CurrentMembershipNumber);
                    barcodeFailed = barcode == null;
                }
                int? rows = UpdateEditedCongregation(dob, appointed, elected, barcode, files); // Update the Record
                if (rows == null)
                {
                    return; // error already shown, keep the entered values
                }
                if (rows == 0)
                {
                    TopMostDialogs.ShowTopMost("No member with Passport / ID '" + key + "' was found. Nothing was updated.", "ZAFMC - Update Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ResetForm(); //Resets Web Controls
                CongregationConnection();  //Refresh the DBase
                TopMostDialogs.ShowTopMost("Member " + key + " updated.", "ZAFMC - Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (barcodeFailed)
                {
                    TopMostDialogs.ShowTopMost("Member " + key + " was updated, but the barcode could not be regenerated. Select the member and tick Generate Barcode to try again.", "ZAFMC - Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
               if (MPJ == DialogResult.No)
            {
                ResetForm(); //Resets Web Controls (also clears the search box)
                CongregationConnection(); //Refresh the DBase
            }

        }

        //Update Congregation(Method): the Membership Number is permanent and is never sent.
        //Returns the number of updated rows, or null on error.
        private int? UpdateEditedCongregation(DateTime? dob, DateTime? appointed, DateTime? elected, byte[] barcode, MemberUploads files)
        {

            string ALLAN = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection GRACE = new SqlConnection(ALLAN);
            SqlCommand ANITA = new SqlCommand("UpdateCongregation", GRACE);

            try
            {
                ANITA.CommandType = System.Data.CommandType.StoredProcedure;
                AddMemberParameters(ANITA, dob, appointed, elected, barcode, files);
                SqlParameter rows = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
                ANITA.Parameters.Add(rows);


                GRACE.Open();
                ANITA.ExecuteNonQuery();
                return rows.Value == DBNull.Value ? 0 : Convert.ToInt32(rows.Value);

            }
            catch (Exception ZIM)
            {
                ShowDbError(ZIM, "updated", "ZAFMC- Update Error");
                return null;
            }
            finally
            {
                ANITA.Dispose(); //Clean up memory
                GRACE.Close(); //Close DBase  connection

            }
        }

        // Grid "Select": loads that member into the form for editing
        protected void CongregantsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "LoadMember")
            {
                LoadMember(Convert.ToString(e.CommandArgument));
            }
        }

        // Reads one member from the database (parameterised) and fills every form field
        private void LoadMember(string passportId)
        {
            ResetForm();

            string cs = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            const string sql = "SELECT TOP 1 [CongMembershipNumber],[CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince],[CongDistrict],[CongZone],[CongSection],[CongSnrLeader],[CongViceLeader],[CongBarcode] FROM [dbo].[Congregation] WHERE [CongPassportID] = @id";

            bool found = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, 15) { Value = passportId ?? string.Empty });
                    cn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            found = true;

                            Firstname.Text = ReadString(r, "CongName");
                            Surname.Text = ReadString(r, "CongSurname");
                            Profession.Text = ReadString(r, "CongProfession");
                            NextOfKin.Text = ReadString(r, "CongKin");
                            KinContact.Text = ReadString(r, "CongKinContact");
                            CellNumber.Text = ReadString(r, "CongCell");
                            PhysAddress.Text = ReadString(r, "CongAddress");
                            EmailAdd.Text = ReadString(r, "CongEmail");
                            DOB.Text = ReadDate(r, "CongDOB");
                            DateAppointed.Text = ReadDate(r, "CongDateAppointed");
                            DateElected.Text = ReadDate(r, "CongDateElected");

                            string gender = ReadString(r, "CongGender");
                            Gender.ClearSelection();
                            if (gender == "Male" || gender == "Female")
                            {
                                Gender.SelectedValue = gender;
                            }

                            SetDropDown(Titles, ReadString(r, "CongTitle"));
                            SetDropDown(MaritalStatus, ReadString(r, "CongStatus"));
                            SetDropDown(RankPosition, ReadString(r, "CongPosition"));
                            SetDropDown(ManagerialPost, ReadString(r, "CongManagerial"));
                            SetDropDown(Province, ReadString(r, "CongProvince"));
                            SetDropDown(District, ReadString(r, "CongDistrict"));
                            SetDropDown(Zones, ReadString(r, "CongZone"));
                            SetDropDown(Sect, ReadString(r, "CongSection"));
                            SetDropDown(SeniorLeader, ReadString(r, "CongSnrLeader"));
                            SetDropDown(ViceLeader, ReadString(r, "CongViceLeader"));

                            EditPassportID = ReadString(r, "CongPassportID");
                            CurrentMembershipNumber = ReadString(r, "CongMembershipNumber");

                            // Stored PDF417 barcode, and the payload of the saved values (to detect edits)
                            byte[] stored = r["CongBarcode"] as byte[];
                            CurrentBarcodePng = stored != null && stored.Length > 0 ? Convert.ToBase64String(stored) : null;
                            LoadedBarcodePayload = BuildFormPayload(CurrentMembershipNumber);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ResetForm();
                ShowDbError(ex, "loaded", "ZAFMC - Select Error");
                return;
            }

            // Shown after the connection is closed (MessageBox waits for OK)
            if (found)
            {
                TopMostDialogs.ShowTopMost("Editing " + CurrentMembershipNumber + " (" + EditPassportID + "). Passport / ID is locked. Click Reset for a new member.", "ZAFMC - Select Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TopMostDialogs.ShowTopMost("Member not found. It may have been deleted.", "ZAFMC - Select Member", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string ReadString(SqlDataReader r, string column)
        {
            object v = r[column];
            return v == DBNull.Value ? string.Empty : Convert.ToString(v);
        }

        private static string ReadDate(SqlDataReader r, string column)
        {
            object v = r[column];
            return v is DateTime ? ((DateTime)v).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty;
        }

        //View Congregation(Event): Search by Membership Number or Passport / ID
        protected void ViewCongregation_Click(object sender, EventArgs e)
        {

            CongregationView();

        }
        //View Congregation (Method)
        private void CongregationView()
        {
            string term = (SearchCongregant.Text ?? string.Empty).Trim();
            if (term.Length > CongregationRules.SearchMaxLength)
            {
                ShowValidationError("Search cannot be longer than " + CongregationRules.SearchMaxLength + " characters.", "ZAFMC-Viewing Events");
                return;
            }

            CongregantsList.PageIndex = 0;
            CongregationConnection(); // Rebinds the grid; ZAFMCCong_Selecting applies the search
            ShowCongregantsModal();
        }

        // Applies the search box to the grid's data source as a parameterised "starts with" filter
        protected void ZAFMCCong_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string term = (SearchCongregant.Text ?? string.Empty).Trim();
            if (term.Length > CongregationRules.SearchMaxLength)
            {
                e.Cancel = true;
                return;
            }
            e.Command.Parameters["@FIND"].Value = CongregationRules.ToLikePrefix(term);
        }

        // Keeps the Congregants list open after paging / sorting
        protected void CongregantsList_Changed(object sender, EventArgs e)
        {
            ShowCongregantsModal();
        }

        // Re-opens the Congregants modal after a postback by clicking the existing View link
        private void ShowCongregantsModal()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowCongregants",
                "window.addEventListener('load', function () { var v = document.getElementById('" + CongregaView.ClientID + "'); if (v) { v.click(); } });", true);
        }

        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetForm(); //Resets Web Controls
        }

        // Back to new-member mode: clears every field and the current member state
        private void ResetForm()
        {
            ResetControls(Page); //Resets Web Controls
            EditPassportID = null;
            CurrentMembershipNumber = null;
            CurrentBarcodePng = null;
            LoadedBarcodePayload = null;
            ReservedMembershipNumber = null; // never carried over to another member (stays burned in the registry)
            GeneratedBarcodePayload = null;
            GenerateBarcode.Checked = false;
            ViewState["LegacyDropDowns"] = null;
            PassportID.ReadOnly = false;
        }

        // "Generate Barcode" ticked: builds the PDF417 barcode from the captured details and shows it.
        // Loaded member: the final barcode is stored on its row at once (dbo.SetCongregationBarcode).
        // New member: reserves the Membership Number (once per record), shows it and encodes it; Save stores
        // the member with that same number and this barcode.
        protected void GenerateBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (!GenerateBarcode.Checked)
            {
                return; // unticked: nothing to do
            }

            string missing = CongregationRules.ValidateBarcodeFields(GetDropDownValue(Titles), Firstname.Text, Surname.Text,
                DOB.Text, Gender.SelectedValue, EditPassportID ?? PassportID.Text);
            DateTime? dob, appointed, elected;
            if (missing != null)
            {
                GenerateBarcode.Checked = false;
                ShowValidationError(missing, "ZAFMC - Generate Barcode");
                return;
            }
            if (!TryReadMemberForm("ZAFMC - Generate Barcode", out dob, out appointed, out elected))
            {
                GenerateBarcode.Checked = false;
                return;
            }

            if (EditPassportID == null)
            {
                string reserved = EnsureReservedMembershipNumber();
                if (reserved == null)
                {
                    GenerateBarcode.Checked = false;
                    TopMostDialogs.ShowTopMost("The Membership Number could not be generated. Please contact the IT Department.", "ZAFMC - Generate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (NewMemberBarcode(reserved) == null)
                {
                    GenerateBarcode.Checked = false;
                    TopMostDialogs.ShowTopMost("The barcode could not be generated.", "ZAFMC - Generate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            // The stored barcode must match the stored row: encoded fields edited but not saved yet -> Edit first
            if (BuildFormPayload(CurrentMembershipNumber) != LoadedBarcodePayload)
            {
                GenerateBarcode.Checked = false;
                ShowValidationError("Title, Firstname, Surname, Date Of Birth or Male / Female was changed. Click Edit to update the member; the barcode is regenerated with the update.", "ZAFMC - Generate Barcode");
                return;
            }

            byte[] barcode = GenerateFormBarcode(CurrentMembershipNumber);
            int rows = barcode == null ? -1 : StoreBarcode(EditPassportID, barcode);
            if (rows != 1)
            {
                GenerateBarcode.Checked = false;
                TopMostDialogs.ShowTopMost(rows == 0
                    ? "No member with Passport / ID '" + EditPassportID + "' was found. The barcode was not saved."
                    : "The barcode could not be generated or saved.", "ZAFMC - Generate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CurrentBarcodePng = Convert.ToBase64String(barcode);
        }

        // New member: the page's reserved Membership Number, reserved on first use and reused for this record
        // (re-ticking or a retried Save never burns another one). Shown in the read-only box.
        // Returns null when the database could not reserve one (logged, never thrown).
        private string EnsureReservedMembershipNumber()
        {
            if (ReservedMembershipNumber == null)
            {
                string number = ReserveMembershipNumber();
                if (number == null)
                {
                    return null;
                }
                ReservedMembershipNumber = number;
            }
            CurrentMembershipNumber = ReservedMembershipNumber;
            return ReservedMembershipNumber;
        }

        // New member: the barcode with the reserved number, shown in the Barcode Image box. The one already
        // generated is kept while its encoded details are unchanged, otherwise a new one is generated.
        // Returns null when it cannot be generated.
        private byte[] NewMemberBarcode(string reserved)
        {
            string payload = BuildFormPayload(reserved);
            if (CurrentBarcodePng != null && GeneratedBarcodePayload == payload)
            {
                return Convert.FromBase64String(CurrentBarcodePng);
            }
            byte[] barcode = GenerateFormBarcode(reserved);
            CurrentBarcodePng = barcode == null ? null : Convert.ToBase64String(barcode);
            GeneratedBarcodePayload = barcode == null ? null : payload;
            return barcode;
        }

        // dbo.ReserveMembershipNumber: a new, never-used number recorded in the registry as reserved.
        // Returns null on error (logged, never thrown).
        private static string ReserveMembershipNumber()
        {
            string cs = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand("ReserveMembershipNumber", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter number = new SqlParameter("@MembershipNumber", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(number);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    string value = Convert.ToString(number.Value);
                    return CongregationRules.IsMembershipNumber(value) ? value : null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return null;
            }
        }

        // Loaded member: shows the stored photo in the photo box and says which ID document is stored.
        // Read on every request (not kept in ViewState: files are up to 5 MB). New-member mode: nothing shown.
        private void ShowStoredFiles(string passportId)
        {
            PersonPhoto.ImageUrl = string.Empty;
            PersonPhoto.AlternateText = string.Empty;
            PassIDStored.Text = string.Empty;
            if (passportId == null)
            {
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            const string sql = "SELECT [CongPhotoData],[CongPhotoContentType],[CongPassIDFileName],[CongPassIDContentType],DATALENGTH([CongPassIDData]) AS [PassIDBytes] FROM [dbo].[Congregation] WHERE [CongPassportID] = @id";
            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, 15) { Value = passportId });
                    cn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            return;
                        }
                        byte[] photo = r["CongPhotoData"] as byte[];
                        string photoType = ReadString(r, "CongPhotoContentType");
                        if (photo != null && photo.Length > 0 && (photoType == "image/jpeg" || photoType == "image/png"))
                        {
                            PersonPhoto.ImageUrl = "data:" + photoType + ";base64," + Convert.ToBase64String(photo);
                            PersonPhoto.AlternateText = "Photo of the member";
                        }
                        object idBytes = r["PassIDBytes"];
                        if (idBytes != DBNull.Value && Convert.ToInt64(idBytes) > 0)
                        {
                            PassIDStored.Text = "Stored: " + ReadString(r, "CongPassIDFileName") + " (" + ReadString(r, "CongPassIDContentType") + ", "
                                + Math.Max(1, Convert.ToInt64(idBytes) / 1024) + " KB)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString()); // display only: never blocks the page
            }
        }

        // Barcode payload of the details currently in the form
        private string BuildFormPayload(string membershipNumber)
        {
            DateTime? dob;
            CongregationRules.ValidateOptionalDate(DOB.Text, "Date Of Birth", out dob);
            return CongregationBarcode.BuildPayload(membershipNumber, GetDropDownValue(Titles), Firstname.Text, Surname.Text,
                dob, Gender.SelectedValue, EditPassportID ?? PassportID.Text);
        }

        // PDF417 PNG of the form details, or null when it cannot be generated (logged, never thrown)
        private byte[] GenerateFormBarcode(string membershipNumber)
        {
            try
            {
                return CongregationBarcode.RenderPng(BuildFormPayload(membershipNumber));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return null;
            }
        }

        // Stores the barcode in dbo.Congregation.CongBarcode of that member (null stores NULL).
        // Returns the number of updated rows (1 = stored), or -1 on error (logged, never thrown).
        private static int StoreBarcode(string passportId, byte[] png)
        {
            string cs = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand("SetCongregationBarcode", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CongPassportID", SqlDbType.NVarChar, 15) { Value = passportId });
                    cmd.Parameters.Add(new SqlParameter("@CongBarcode", SqlDbType.VarBinary, -1) { Value = (object)png ?? DBNull.Value });
                    SqlParameter rows = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(rows);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return rows.Value == DBNull.Value ? 0 : Convert.ToInt32(rows.Value);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return -1;
            }
        }

        // Resets all Controls on the Web Form
        private void ResetControls(System.Web.UI.Control JP)
        {
            foreach (System.Web.UI.Control ctr in JP.Controls)
            {
                //TexBoxes Control
                if (ctr is System.Web.UI.WebControls.TextBox)
                {
                    if (ctr is System.Web.UI.WebControls.TextBox TB) //Pattern Matching
                    {
                        TB.Text = string.Empty;
                    }
                }
                else
                {
                    if (ctr.Controls.Count > 0)
                    {
                        ResetControls(ctr);
                    }
                }

                //DropDownList Control
                if (ctr is DropDownList)
                {
                    if (ctr is DropDownList DDL)   //Pattern Matching
                    {
                        DDL.SelectedValue = Convert.ToString(-1);
                    }
                    else
                        if (ctr.Controls.Count > 0)
                    {
                        ResetControls(ctr);
                    }
                }

                //RadioButtonList Control (Gender)
                if (ctr is RadioButtonList RBL)   //Pattern Matching
                {
                    RBL.ClearSelection();
                }



            }


        }
        //Uploads Photos and ID: returns a short summary that is appended to the Save message
        private string UpLoadsFiles()
        {
            string result = string.Empty;
            try
            {
                if (PhotoUpload.HasFile)
                {
                    PhotoUpload.SaveAs(Server.MapPath( @"~/ZION/Congregation/Photos/") + PhotoUpload.FileName);
                    result += " Photo uploaded: " + PhotoUpload.FileName + " (" + (PhotoUpload.PostedFile.ContentLength / 1000) + " KB).";

                }
                if(IDPass.HasFile)
                {

                    IDPass.SaveAs(Server.MapPath(@"~/ZION/Congregation/PassportID/") + IDPass.FileName);
                    result += " Passport / ID uploaded: " + IDPass.FileName + " (" + (IDPass.PostedFile.ContentLength / 1000) + " KB).";
                }
            }
            catch (Exception Upload)
            {
                System.Diagnostics.Trace.TraceError(Upload.ToString());
                TopMostDialogs.ShowTopMost("The Photo / ID upload failed.", "ZAFMC - (Photo / Identity) Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return result;
        }

        

    }
}




