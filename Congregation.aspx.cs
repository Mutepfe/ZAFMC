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
                ShowValidationError(error);
                return;
            }

            int? rows = CongregationDelete(key);
            if (rows == null)
            {
                return; // error already shown
            }
            if (rows == 0)
            {
                ShowMessage("No member with Passport / ID '" + key + "' was found. Nothing was deleted.", "warning");
                return;
            }
            ResetForm(); //Resets Web Controls
            CongregationConnection(); // Refresh the Database
            ShowMessage("Member " + key + " deleted.", "success");

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
                ShowDbError(ex, "deleted");
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
                ShowMessage("You are editing an existing member. Use Edit to update it, or Reset for a new member.", "warning");
                return;
            }

            CurrentMembershipNumber = null; // a new member is being created
            string number = CongregationAddNew(); // Add or Save Record
            if (number == null)
            {
                return; // Validation or insert failed: keep the entered values
            }
            string uploads = UpLoadsFiles();       //Save Photo
            ResetForm(); //Resets Web Controls
            CongregationConnection(); // Refresh Database
            CurrentMembershipNumber = number; // Show the generated Membership Number (re-applied in Page_PreRender)
            ShowMessage("Member saved. Membership Number: " + number + "." + uploads, "success");


        }
        //AddNew Congregation (Method): returns the generated Membership Number, or null when nothing was saved
        private string CongregationAddNew()
        {
            DateTime? dob, appointed, elected;
            if (!TryReadMemberForm(out dob, out appointed, out elected))
            {
                return null;
            }

            string CongInsert = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CID = new SqlConnection(CongInsert);
            SqlCommand Prosper = new SqlCommand("AddNewCongregation", CID);

            try
            {

                Prosper.CommandType = CommandType.StoredProcedure;

                AddMemberParameters(Prosper, dob, appointed, elected);
                // Generated by the database; any value typed or posted for it is ignored
                SqlParameter membership = new SqlParameter("@CongMembershipNumber", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output };
                Prosper.Parameters.Add(membership);

                CID.Open();
                Prosper.ExecuteNonQuery();

                string number = Convert.ToString(membership.Value);
                if (!CongregationRules.IsMembershipNumber(number))
                {
                    ShowMessage("The Membership Number could not be generated. Please contact the IT Department.", "danger");
                    return null;
                }
                return number;

            }
            catch (Exception P)
            {
                ShowDbError(P, "saved");
                return null;
            }
            finally
            {
                Prosper.Dispose(); //Clean up memory
                CID.Close(); //Close DBase  connection

            }
        }

        // Validates the member form; shows the first problem and returns false when invalid
        private bool TryReadMemberForm(out DateTime? dob, out DateTime? appointed, out DateTime? elected)
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
                ShowValidationError(error);
                return false;
            }
            return true;
        }

        private void ShowValidationError(string message)
        {
            ShowMessage(message, "warning");
        }

        // Shows an on-page Bootstrap alert; kind = success | warning | danger | info
        private void ShowMessage(string text, string kind)
        {
            CongAlert.CssClass = "alert alert-" + kind + " alert-dismissible";
            CongAlertText.Text = HttpUtility.HtmlEncode(text);
            CongAlert.Visible = true;
        }

        // Logs the full error on the server and shows a friendly message (no SQL details in the browser)
        private void ShowDbError(Exception ex, string action)
        {
            System.Diagnostics.Trace.TraceError(ex.ToString());
            SqlException sql = ex as SqlException;
            if (sql != null && (sql.Number == 2627 || sql.Number == 2601))
            {
                ShowMessage("A member with this Passport / ID already exists.", "danger");
                return;
            }
            ShowMessage("The record could not be " + action + ". Please contact the IT Department.", "danger");
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
        private void AddMemberParameters(SqlCommand cmd, DateTime? dob, DateTime? appointed, DateTime? elected)
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
            cmd.Parameters.AddWithValue("@CongPhoto", PersonPhoto.ImageUrl);
            cmd.Parameters.AddWithValue("@CongPassID", PassID.ImageUrl);
            cmd.Parameters.AddWithValue("@CongregationID", DBNull.Value);
            // image columns: must be typed as Image (an nvarchar value clashes with image)
            cmd.Parameters.Add(new SqlParameter("@CongFingerprint", SqlDbType.Image) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@CongBarcode", SqlDbType.Image) { Value = DBNull.Value });
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
                throw new Exception("Failed to connect to the database");
            }

        }

        //Update Congregation(Event): the "are you sure?" confirmation runs in the browser (OnClientClick)
        protected void EditCongregation_Click(object sender, EventArgs e)
        {
            DateTime? dob, appointed, elected;
            if (!TryReadMemberForm(out dob, out appointed, out elected))
            {
                return;
            }

            string key = EditPassportID ?? PassportID.Text;
            int? rows = UpdateEditedCongregation(dob, appointed, elected); // Update the Record
            if (rows == null)
            {
                return; // error already shown, keep the entered values
            }
            if (rows == 0)
            {
                ShowMessage("No member with Passport / ID '" + key + "' was found. Nothing was updated.", "warning");
                return;
            }
            ResetForm(); //Resets Web Controls
            CongregationConnection();  //Refresh the DBase
            ShowMessage("Member " + key + " updated.", "success");

        }

        //Update Congregation(Method): the Membership Number is permanent and is never sent.
        //Returns the number of updated rows, or null on error.
        private int? UpdateEditedCongregation(DateTime? dob, DateTime? appointed, DateTime? elected)
        {

            string ALLAN = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection GRACE = new SqlConnection(ALLAN);
            SqlCommand ANITA = new SqlCommand("UpdateCongregation", GRACE);

            try
            {
                ANITA.CommandType = System.Data.CommandType.StoredProcedure;
                AddMemberParameters(ANITA, dob, appointed, elected);
                SqlParameter rows = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
                ANITA.Parameters.Add(rows);


                GRACE.Open();
                ANITA.ExecuteNonQuery();
                return rows.Value == DBNull.Value ? 0 : Convert.ToInt32(rows.Value);

            }
            catch (Exception ZIM)
            {
                ShowDbError(ZIM, "updated");
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
            const string sql = "SELECT TOP 1 [CongMembershipNumber],[CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince],[CongDistrict],[CongZone],[CongSection],[CongSnrLeader],[CongViceLeader] FROM [dbo].[Congregation] WHERE [CongPassportID] = @id";

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, 15) { Value = passportId ?? string.Empty });
                    cn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            ShowMessage("Member not found. It may have been deleted.", "warning");
                            return;
                        }

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
                        ShowMessage("Editing " + CurrentMembershipNumber + " (" + EditPassportID + "). Passport / ID is locked. Click Reset for a new member.", "info");
                    }
                }
            }
            catch (Exception ex)
            {
                ResetForm();
                ShowDbError(ex, "loaded");
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
                ShowValidationError("Search cannot be longer than " + CongregationRules.SearchMaxLength + " characters.");
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
            ViewState["LegacyDropDowns"] = null;
            PassportID.ReadOnly = false;
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
                result += " The photo / ID upload failed.";

            }
            return result;
        }

        

    }
}




