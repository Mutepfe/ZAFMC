using System;
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
using System.Web.ModelBinding;
using ZAFMC.Models;

namespace ZAFMC
{
    public partial class Congregation : System.Web.UI.Page
    {
        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            ResetControls(Page); //Resets Web Controls


        }
        //Delete  Congregation (Event)
        protected void DeleteCongregation_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            CongregationDelete();
            ResetControls(Page); //Resets Web Controls

        }

        //Delete  Congregation(Method)
        private void CongregationDelete()
        {
            //Using SQL Stored Procedure

            string CongDel = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CD = new SqlConnection(CongDel);
            SqlCommand Cong = new SqlCommand("DeleteCongregation", CD);

            try
            {
                Cong.Parameters.AddWithValue("@DelCongregation", PassportID.Text); //@DelCongregation StoredProcedure actual parameter passed in the DB
                Cong.CommandType = System.Data.CommandType.StoredProcedure;
                CD.Open();
                Cong.ExecuteNonQuery();
                CongregationConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
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
            CongregationAddNew(); // Add or Save Record
            UpLoadsFiles();       //Save Photo
            CongregationConnection(); // Refresh Database
            ResetControls(Page); //Resets Web Controls


        }
        //AddNew Congregation (Method)
        private void CongregationAddNew()
        {

            string CongInsert = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CID = new SqlConnection(CongInsert);
            SqlCommand Prosper = new SqlCommand("AddNewCongregation", CID);

            try
            {

                Prosper.CommandType = CommandType.StoredProcedure;
                
                Prosper.Parameters.AddWithValue("@CongTitle", Titles.Text);
                Prosper.Parameters.AddWithValue("@CongName", Firstname.Text);
                Prosper.Parameters.AddWithValue("@CongSurname", Surname.Text);
                Prosper.Parameters.AddWithValue("@CongDOB", DOB.Text);
                Prosper.Parameters.AddWithValue("@CongGender", Gender.Text);
                Prosper.Parameters.AddWithValue("@CongPassportID", PassportID.Text);
                Prosper.Parameters.AddWithValue("@CongStatus", MaritalStatus.Text);
                Prosper.Parameters.AddWithValue("@CongProfession", Profession.Text);
                Prosper.Parameters.AddWithValue("@CongKin", NextOfKin.Text);
                Prosper.Parameters.AddWithValue("@CongKinContact", KinContact.Text);
                Prosper.Parameters.AddWithValue("@CongCell", CellNumber.Text);
                Prosper.Parameters.AddWithValue("@CongAddress", PhysAddress.Text);
                Prosper.Parameters.AddWithValue("@CongEmail", EmailAdd.Text);
                Prosper.Parameters.AddWithValue("@CongPosition", RankPosition.Text);
                Prosper.Parameters.AddWithValue("@CongDateAppointed", DateAppointed.Text);
                Prosper.Parameters.AddWithValue("@CongManagerial", ManagerialPost.Text);
                Prosper.Parameters.AddWithValue("@CongDateElected", DateElected.Text);
                Prosper.Parameters.AddWithValue("@CongProvince", Province.Text);
                Prosper.Parameters.AddWithValue("@CongDistrict", District.Text);
                Prosper.Parameters.AddWithValue("@CongZone", Zones.Text);
                Prosper.Parameters.AddWithValue("@CongSection", Sect.Text);
                Prosper.Parameters.AddWithValue("@CongSnrLeader", SeniorLeader.Text);
                Prosper.Parameters.AddWithValue("@CongViceLeader", ViceLeader.Text);
                Prosper.Parameters.AddWithValue("@CongPhoto", PersonPhoto.ImageUrl);
                Prosper.Parameters.AddWithValue("@CongPassID", PassID.ImageUrl);
                Prosper.Parameters.AddWithValue("@CongregationID", DBNull.Value);
                Prosper.Parameters.AddWithValue("@CongFingerprint", FingerPrint.ImageUrl);
                Prosper.Parameters.AddWithValue("@CongBarcode", Barcode.ImageUrl);
                
                CID.Open();
                Prosper.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                Prosper.Dispose(); //Clean up memory
                CID.Close(); //Close DBase  connection

            }
        }
        //Refresh Congregation(Event)
        protected void RefreshCongregation_Click(object sender, EventArgs e)
        {
            CongregationConnection();
            ResetControls(Page); //Resets Web Controls
        }
        //Refresh Congregation(Method)
        public void CongregationConnection()
        {

            string CongConn = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection FORD = new SqlConnection(CongConn);
            SqlCommand RANGER = new SqlCommand(@"SELECT [CongTitle] as [Title],[CongName] as [Name],[CongSurname] as [Surname],(REPLACE(convert(nvarchar,[CongDOB],106),'','/')) as [D.O.B],[CongGender] as [Gender],[CongPassportID] as [Identity],[CongStatus] as [Status],[CongProfession] as [Profession],[CongKin] as [Kin],[CongKinContact] as [Kin Contact],[CongCell] as [Mobile],[CongAddress] as [Address],[CongEmail] as [Email],[CongPosition] as [Position],(REPLACE(convert(nvarchar,[CongDateAppointed],106),'','/')) as [Date Appointed],[CongManagerial] as [Managerial Post],(REPLACE(convert(nvarchar,[CongDateElected],106),'','/')) as [Date Elected],[CongProvince] as [Province],[CongDistrict] as [District],[CongZone] as [Zone],[CongSection] as [Section],[CongSnrLeader] as [Snr. Leader],[CongViceLeader] as [Vice-Leader],[CongPhoto] as [Photo],[CongPassID] as [Identity-Photo],[CongFingerprint] as [Fingerprint],[CongBarcode] as [Encrypted-Data] FROM [dbo].[Congregation] ORDER BY [CongPassportID] ASC ", FORD);
            try
            {
                FORD.Open();
                SqlDataReader DOUBLECAB = RANGER.ExecuteReader();
                //ChurchList.DataSource = DOUBLECAB;
                //MediaList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                RANGER.Dispose(); //Clean up memory
                FORD.Close(); //Close DBase  connection
                ResetControls(Page); //Resets Web Controls
            }

        }

        //Update Congregation(Event) 
        protected void EditCongregation_Click(object sender, EventArgs e)
        {

            DialogResult MPJ = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MPJ == DialogResult.Yes)
            {
                UpdateEditedCongregation(); // Update the Record
                CongregationConnection();  //Refresh the DBase
            }
            else
               if (MPJ == DialogResult.No)
            {

                CongregationConnection();
                ResetControls(Page); //Resets Web Controls
            }

        }

        //Update Congregation(Method) 
        private void UpdateEditedCongregation()
        {

            string ALLAN = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection GRACE = new SqlConnection(ALLAN);
            SqlCommand ANITA = new SqlCommand("UpdateCongregation", GRACE);

            try
            {
                ANITA.CommandType = System.Data.CommandType.StoredProcedure;
                ANITA.Parameters.AddWithValue("@CongTitle", Titles.Text);
                ANITA.Parameters.AddWithValue("@CongName", Firstname.Text);
                ANITA.Parameters.AddWithValue("@CongSurname", Surname.Text);
                ANITA.Parameters.AddWithValue("@CongDOB", DOB.Text);
                ANITA.Parameters.AddWithValue("@CongGender", Gender.Text);
                ANITA.Parameters.AddWithValue("@CongPassportID", PassportID.Text);
                ANITA.Parameters.AddWithValue("@CongStatus", MaritalStatus.Text);
                ANITA.Parameters.AddWithValue("@CongProfession", Profession.Text);
                ANITA.Parameters.AddWithValue("@CongKin", NextOfKin.Text);
                ANITA.Parameters.AddWithValue("@CongKinContact", KinContact.Text);
                ANITA.Parameters.AddWithValue("@CongCell", CellNumber.Text);
                ANITA.Parameters.AddWithValue("@CongAddress", PhysAddress.Text);
                ANITA.Parameters.AddWithValue("@CongEmail", EmailAdd.Text);
                ANITA.Parameters.AddWithValue("@CongPosition", RankPosition.Text);
                ANITA.Parameters.AddWithValue("@CongAppointed", DateAppointed.Text);
                ANITA.Parameters.AddWithValue("@CongManagerial", ManagerialPost.Text);
                ANITA.Parameters.AddWithValue("@CongElected", DateElected.Text);
                ANITA.Parameters.AddWithValue("@CongProvince", Province.Text);
                ANITA.Parameters.AddWithValue("@CongDistrict", District.Text);
                ANITA.Parameters.AddWithValue("@CongZone", Zones.Text);
                ANITA.Parameters.AddWithValue("@CongSection", Sect.Text);
                ANITA.Parameters.AddWithValue("@CongSnrLeader", SeniorLeader.Text);
                ANITA.Parameters.AddWithValue("@CongViceLeader", ViceLeader.Text);
                ANITA.Parameters.AddWithValue("@CongPhoto", PersonPhoto.ImageUrl);
                ANITA.Parameters.AddWithValue("@CongPassID", PassID.ImageUrl);
                ANITA.Parameters.AddWithValue("@CongregationID", DBNull.Value);
                ANITA.Parameters.AddWithValue("@CongFingerprint", FingerPrint.ImageUrl);
                ANITA.Parameters.AddWithValue("@CongBarcode", Barcode.ImageUrl);


                GRACE.Open();
                ANITA.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                ANITA.Dispose(); //Clean up memory
                GRACE.Close(); //Close DBase  connection

            }
        }

        //View Congregation(Event)
        protected void ViewCongregation_Click(object sender, EventArgs e)
        {

            CongregationView();

        }
        //View Congregation (Method)
        private void CongregationView()
        {

            string MG = ConfigurationManager.ConnectionStrings["ZionCongregation"].ConnectionString; // ZionCongregation From Web.Config under ConnectionString Settings
            SqlConnection CH = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [CongTitle] as [Title],[CongName] as [Name],[CongSurname] as [Surname],(REPLACE(convert(nvarchar,[CongDOB],106),'','/')) as [D.O.B],[CongGender] as [Gender],[CongPassportID] as [Identity],[CongStatus] as [Status],[CongProfession] as [Profession],[CongKin] as [Kin],[CongKinContact] as [Kin Contact],[CongCell] as [Mobile],[CongAddress] as [Address],[CongEmail] as [Email],[CongPosition] as [Position],(REPLACE(convert(nvarchar,[CongDateAppointed],106),'','/')) as [Date Appointed],[CongManagerial] as [Managerial Post],(REPLACE(convert(nvarchar,[CongDateElected],106),'','/')) as [Date Elected],[CongProvince] as [Province],[CongDistrict] as [District],[CongZone] as [Zone],[CongSection] as [Section],[CongSnrLeader] as [Snr. Leader],[CongViceLeader] as [Vice-Leader],[CongPhoto] as [Photo],[CongPassID] as [Identity-Photo],[CongFingerprint] as [Fingerprint],[CongBarcode] as [Encrypted-Data] FROM [dbo].[Congregation] ORDER BY [CongPassportID] LIKE @FIND ", CH);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", PassportID.Text + "%");
                CH.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                //MediaList.DataSource = DB; // Attach searched data to DataGridView
                //MediaList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                CH.Close(); //Close DBase  connection
            }
        }

        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page); //Resets Web Controls
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



            }


        }
        //Uploads Photos and ID
        private void UpLoadsFiles()
        {
            try
            {
                if (PhotoUpload.HasFile)
                {
                    PhotoUpload.SaveAs(Server.MapPath( @"~/ZION/Congregation/Photos/") + PhotoUpload.FileName);
                    MessageBox.Show(PhotoUpload.FileName, "ZAFMC - Upload Photo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Photo Name : " + PhotoUpload.PostedFile.FileName +  "\n\n"  + "Photo Size : " + ((PhotoUpload.PostedFile.ContentLength) / 1000) + "MB " + " \n\n" + "Content Type : " + PhotoUpload.PostedFile.ContentType,"ZAFMC - Photo Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                }
                if(IDPass.HasFile)
                {
                    
                    IDPass.SaveAs(Server.MapPath(@"~/ZION/Congregation/PassportID/") + IDPass.FileName);
                    MessageBox.Show(IDPass.FileName, "ZAFMC - Upload Passport / Identity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Passport / Identity Name : " + IDPass.PostedFile.FileName + " \n\n " + "PassportID Size : " + ((IDPass.PostedFile.ContentLength) / 1000) + "MB "+ "\n\n" + "Content Type : " + IDPass.PostedFile.ContentType, "ZAFMC - PassportID Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Upload)
            {
                MessageBox.Show(Upload.Message.ToString(), "ZAFMC - (Photo / Identity) Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        

    }
}


