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
//For Data Annotations(Two-Below)
using System.Web.ModelBinding;
using ZAFMC.Models;

namespace ZAFMC
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = false; //Hide the heading for Leadership, show ONLY on Click
            ZAFMCAdministration.Visible = false;
            ZAFMCDeceased.Visible = false;
            

            //Clear PageCahe
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void LeaderShipHeading_Click(object sender, EventArgs e)
        {
            
        }
        //AddNew Record(Event)
        protected void SaveLeader_Click(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = true;
            AboutLeadershipAddNew(); // Add or Save Record
            AboutLeadershipConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls
        }

        //Reset Event
        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
        }
        //Reset all Controls
        private void ResetControls(System.Web.UI.Control JP)
        {
            foreach (System.Web.UI.Control ctr in JP.Controls)
            {
                //TexBoxes Control
                if (ctr is System.Web.UI.WebControls.TextBox)
                {
                    System.Web.UI.WebControls.TextBox TB = ctr as System.Web.UI.WebControls.TextBox;
                    if (TB != null)
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
                if (ctr is System.Web.UI.WebControls.DropDownList)
                {
                    System.Web.UI.WebControls.DropDownList DDL = ctr as System.Web.UI.WebControls.DropDownList;

                    if (DDL != null)
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
        //AddNew Record(Method)
        private void AboutLeadershipAddNew()
        {
            ZAFMCLeadership.Visible = true;
            string AboutLeaderInsert = ConfigurationManager.ConnectionStrings["ZionAboutLeadership"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection LeaderInsert = new SqlConnection(AboutLeaderInsert);
            SqlCommand ALeadInsert = new SqlCommand("AddNewLeadership", LeaderInsert);

            try
            {
                ALeadInsert.CommandType = CommandType.StoredProcedure;
                ALeadInsert.Parameters.AddWithValue("@LeaderID", DBNull.Value);
                ALeadInsert.Parameters.AddWithValue("@LeaderName", ALeadName.Text);
                ALeadInsert.Parameters.AddWithValue("@LeaderSurname", ALeadSurnam.Text);
                ALeadInsert.Parameters.AddWithValue("@LeaderDOB", LeaderDOB.Text);
                ALeadInsert.Parameters.AddWithValue("@LeaderIdentity", LeaderID.Text);
                ALeadInsert.Parameters.AddWithValue("@LeaderPosition", LeaderRankPost.Text);
                LeaderInsert.Open();
                ALeadInsert.ExecuteNonQuery();

            }
            catch (Exception AL)
            {
                TopMostDialogs.ShowTopMost(AL.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                ALeadInsert.Dispose(); //Clean up memory
                LeaderInsert.Close(); //Close DBase  connection

            }
        }
        //DBase Connection(Method)
        public void AboutLeadershipConnection()
        {
            ZAFMCLeadership.Visible = true;
            string AboutLeadCon = ConfigurationManager.ConnectionStrings["ZionAboutLeadership"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection ALConnect = new SqlConnection(AboutLeadCon);
            SqlCommand ALCon = new SqlCommand(@"SELECT [LeaderName] as [Name],[LeaderSurname] as [Surname],(REPLACE(convert(nvarchar,LeaderDOB,106),'','/')) as [Date Of Birth],[LeaderIdentity] as [Identity],[LeaderPosition] as [Position] FROM [dbo].[AboutLeadership] ORDER BY [LeaderIdentity]", ALConnect);
            try
            {
                ALConnect.Open();
                SqlDataReader Leader = ALCon.ExecuteReader();
                AboutZION.DataSource = Leader;
                AboutZION.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                ALCon.Dispose(); //Clean up memory
                ALConnect.Close(); //Close DBase  connection
            }

        }

        //View Record(Event)
        protected void ViewLeader_Click(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = true;
            AboutLeadershipView();
        }
        //View Record(Method)
        private void AboutLeadershipView()
        {
            ZAFMCLeadership.Visible = true;
            string LV = ConfigurationManager.ConnectionStrings["ZionAboutLeadership"].ConnectionString; // ZionAboutLeadership From Web.Config under ConnectionString Settings
            SqlConnection LeadView = new SqlConnection(LV);
            SqlCommand ALView = new SqlCommand(@"SELECT [LeaderName] as [Name],[LeaderSurname] as [Surname],(REPLACE(convert(nvarchar,LeaderDOB,106),'','/')) as [Date Of Birth],[LeaderIdentity] as [Identity],[LeaderPosition] as [Position] FROM [dbo].[AboutLeadership] WHERE [LeaderIdentity] LIKE @FIND", LeadView);

            try
            {
                ALView.Parameters.AddWithValue("@FIND", LeaderID.Text + "%");
                LeadView.Open();
                SqlDataReader AL = ALView.ExecuteReader();
                AboutZION.DataSource = AL; // Attach searched data to DataGridView
                AboutZION.DataBind();  // Bind data to DataGridView
            }
            catch (Exception VW)
            {
                TopMostDialogs.ShowTopMost(VW.ToString(), "ZAFMC-Viewing Leadership", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ALView.Dispose(); //Clean up memory
                LeadView.Close(); //Close DBase  connection
            }
        }
        //Update (Event) 
        protected void EditLeader_Click(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = true;
            DialogResult Reply = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Reply == DialogResult.Yes)
            {
                UpdateEditedLeadership(); // Update the Record
                AboutLeadershipConnection();  //Refresh the DBase
            }
            else
               if (Reply == DialogResult.No)
            {
                AboutLeadershipConnection();
            }

        }

        //Update (Method) 
        private void UpdateEditedLeadership()
        {
            ZAFMCLeadership.Visible = true;
            string LU = ConfigurationManager.ConnectionStrings["ZionAboutLeadership"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection LeadUpdate = new SqlConnection(LU);
            SqlCommand ALUpdate = new SqlCommand("UpdateLeadership", LeadUpdate);

            try
            {
                ALUpdate.CommandType = System.Data.CommandType.StoredProcedure;
                ALUpdate.Parameters.AddWithValue("@LeaderID", DBNull.Value);
                ALUpdate.Parameters.AddWithValue("@LeaderName", ALeadName.Text);
                ALUpdate.Parameters.AddWithValue("@LeaderSurname", ALeadSurnam.Text);
                ALUpdate.Parameters.AddWithValue("@LeaderDOB", LeaderDOB.Text);
                ALUpdate.Parameters.AddWithValue("@LeaderIdentity", LeaderID.Text);
                ALUpdate.Parameters.AddWithValue("@LeaderPosition", LeaderRankPost.Text);

                LeadUpdate.Open();
                ALUpdate.ExecuteNonQuery();

            }
            catch (Exception LeaUpd)
            {
                TopMostDialogs.ShowTopMost(LeaUpd.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                ALUpdate.Dispose(); //Clean up memory
                LeadUpdate.Close(); //Close DBase  connection

            }
        }

        //Refresh Connection
        protected void RefreshLeader_Click(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = true;
            AboutLeadershipConnection(); //Refresh DataGrid
            ResetControls(Page); //ResetControls only
        }

        //Delete (Event)
        protected void DeleteLeader_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            ZAFMCLeadership.Visible = true;
            AboutLeadershipDelete();

        }

        protected void LeadershipHeading_Click(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = true;
        }
        //Delete (Method)
        private void AboutLeadershipDelete()
        {
            //Using SQL Stored Procedure

            string LeadDel = ConfigurationManager.ConnectionStrings["ZionAboutLeadership"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection LD = new SqlConnection(LeadDel);
            SqlCommand Remove = new SqlCommand("DeleteLeader", LD);

            try
            {
                Remove.Parameters.AddWithValue("@DelLDR", LeaderID.Text); //DelLDR StoredProcedure actual parameter
                Remove.CommandType = System.Data.CommandType.StoredProcedure;
                LD.Open();
                Remove.ExecuteNonQuery();
                AboutLeadershipConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Remove.Dispose(); //Clean up memory
                LD.Close(); //Close DBase  connection
            }


        }

        protected void AboutZION_Load(object sender, EventArgs e)
        {
            ZAFMCLeadership.Visible = false;
            ZAFMCAdministration.Visible = false;
            ZAFMCDeceased.Visible = false;
        }

        //AddNew (Event)
        protected void SaveAdmin_Click(object sender, EventArgs e)
        {
            ZAFMCAdministration.Visible = true;
            AboutAdminAddNew(); // Add or Save Record
            AboutAdminConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls

        }
        //AddNew (Method)
        private void AboutAdminAddNew()
        {
            string AboutAdminInsert = ConfigurationManager.ConnectionStrings["ZionAboutAdmin"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection AdminInsert = new SqlConnection(AboutAdminInsert);
            SqlCommand AboutAdmin = new SqlCommand("AddNewAdmin", AdminInsert);

            try
            {
                AboutAdmin.CommandType = CommandType.StoredProcedure;
                AboutAdmin.Parameters.AddWithValue("@AdminID", DBNull.Value);
                AboutAdmin.Parameters.AddWithValue("@AdminName", AdminName.Text);
                AboutAdmin.Parameters.AddWithValue("@AdminSurname", AdminSurname.Text);
                AboutAdmin.Parameters.AddWithValue("@AdminAppntDate", AdminAppDate.Text);
                AboutAdmin.Parameters.AddWithValue("@AdminInternComm", AdminIntCom.Text);
                AboutAdmin.Parameters.AddWithValue("@AdminInternDir", AdminIntDir.Text);

                AdminInsert.Open();
                AboutAdmin.ExecuteNonQuery();

            }
            catch (Exception AL)
            {
                TopMostDialogs.ShowTopMost(AL.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                AboutAdmin.Dispose(); //Clean up memory
                AdminInsert.Close(); //Close DBase  connection

            }
        }
        //Connect Administration
        public void AboutAdminConnection()
        {

            string AboutAdminCon = ConfigurationManager.ConnectionStrings["ZionAboutAdmin"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection AdmConnect = new SqlConnection(AboutAdminCon);
            SqlCommand AdCon = new SqlCommand(@"SELECT [AdminName] as FirstName,[AdminSurname] as Surname,(REPLACE(convert(nvarchar,AdminAppntDate,106),'','/')) as [Appointed Date],[AdminInternComm] as Committee,[AdminInternDir] as Director FROM [dbo].[AboutAdministration] ORDER BY [AdminSurname] ", AdmConnect);
            try
            {
                AdmConnect.Open();
                SqlDataReader ADM = AdCon.ExecuteReader();
                AboutZION.DataSource = ADM;
                AboutZION.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                AdCon.Dispose(); //Clean up memory
                AdmConnect.Close(); //Close DBase  connection
            }

        }
        //Update (Event)
        protected void EditAdmin_Click(object sender, EventArgs e)
        {
            ZAFMCAdministration.Visible = true;
            DialogResult ADM = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ADM == DialogResult.Yes)
            {
                UpdateEditedAdmin(); // Update the Record
                AboutAdminConnection();  //Refresh the DBase
            }
            else
               if (ADM == DialogResult.No)
            {
                AboutAdminConnection();
            }
        }
        //Update (Method)
        private void UpdateEditedAdmin()
        {
            string TM = ConfigurationManager.ConnectionStrings["ZionAboutAdmin"].ConnectionString; // ZionAboutAdmin From Web.Config under ConnectionString Settings
            SqlConnection AdminUpdate = new SqlConnection(TM);
            SqlCommand RUDO = new SqlCommand("UpdateAdmin", AdminUpdate);

            try
            {
                RUDO.CommandType = System.Data.CommandType.StoredProcedure;
                RUDO.Parameters.AddWithValue("@AdminID", DBNull.Value);
                RUDO.Parameters.AddWithValue("@AdminName", AdminName.Text);
                RUDO.Parameters.AddWithValue("@AdminSurname", AdminSurname.Text);
                RUDO.Parameters.AddWithValue("@AdminAppntDate", AdminAppDate.Text);
                RUDO.Parameters.AddWithValue("@AdminInternComm", AdminIntCom.Text);
                RUDO.Parameters.AddWithValue("@AdminInternDir", AdminIntDir.Text);

                AdminUpdate.Open();
                RUDO.ExecuteNonQuery();

            }
            catch (Exception Mhofu)
            {
                TopMostDialogs.ShowTopMost(Mhofu.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                RUDO.Dispose(); //Clean up memory
                AdminUpdate.Close(); //Close DBase  connection

            }
        }

        //View Record (Event)
        protected void ViewAdmin_Click(object sender, EventArgs e)
        {
            ZAFMCAdministration.Visible = true;
            AboutAdminView();
        }
        //View Record (Method)
        private void AboutAdminView()
        {

            string AV = ConfigurationManager.ConnectionStrings["ZionAboutAdmin"].ConnectionString; // ZionAboutAdmin From Web.Config under ConnectionString Settings
            SqlConnection AdmView = new SqlConnection(AV);
            SqlCommand ViewAd = new SqlCommand(@"SELECT [AdminName] as FirstName,[AdminSurname] as Surname,(REPLACE(convert(nvarchar,AdminAppntDate,106),'','/')) as [Appointed Date],[AdminInternComm] as Committee,[AdminInternDir] as Director FROM [dbo].[AboutAdministration] WHERE [AdminSurname] LIKE @FIND", AdmView);

            try
            {
                ViewAd.Parameters.AddWithValue("@FIND", AdminSurname.Text + "%");
                AdmView.Open();
                SqlDataReader JP = ViewAd.ExecuteReader();
                AboutZION.DataSource = JP; // Attach searched data to DataGridView
                AboutZION.DataBind();  // Bind data to DataGridView
            }
            catch (Exception VA)
            {
                TopMostDialogs.ShowTopMost(VA.ToString(), "ZAFMC-Viewing Administration", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ViewAd.Dispose(); //Clean up memory
                AdmView.Close(); //Close DBase  connection
            }
        }
        //Refresh Database
        protected void RefreshAdmin_Click(object sender, EventArgs e)
        {
            ZAFMCAdministration.Visible = true;
            AboutAdminConnection();
        }
        protected void DeleteAdmin_Click(object sender, EventArgs e)
        {
            ZAFMCAdministration.Visible = true;
            AboutAdminDelete();
        }
        //Delete (Method)
        private void AboutAdminDelete()
        {
            //Using SQL Stored Procedure

            string AdminDel = ConfigurationManager.ConnectionStrings["ZionAboutAdmin"].ConnectionString; // ZionAboutAdmin From Web.Config under ConnectionString Settings
            SqlConnection AD = new SqlConnection(AdminDel);
            SqlCommand Theo = new SqlCommand("DeleteAdmin", AD);

            try
            {
                Theo.Parameters.AddWithValue("@DelAdm", AdminSurname.Text); //DelAdm StoredProcedure actual parameter
                Theo.CommandType = CommandType.StoredProcedure;
                AD.Open();
                Theo.ExecuteNonQuery();
                AboutAdminConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                AD.Close(); //Close DBase  connection
            }
        }

        //View Record(Event)
        protected void ViewDeceased_Click(object sender, EventArgs e)
        {
            ZAFMCDeceased.Visible = true;
            AboutDeceasedView();


        }
        //View Record (Method)
        private void AboutDeceasedView()
        {
            ZAFMCDeceased.Visible = true;
            string DV = ConfigurationManager.ConnectionStrings["ZionAboutDeceased"].ConnectionString; // ZionAboutDeceased From Web.Config under ConnectionString Settings
            SqlConnection DecView = new SqlConnection(DV);
            SqlCommand Jacob = new SqlCommand(@"SELECT [DecName] as [Deceased Name],[DecSurname] as [Deceased Surname],[DecPassportID] as [Deceased Identity],(REPLACE(convert(nvarchar,DecDOD,106),'','/')) as [Date Of Death],[DecPosition] as Position,[DecBurialPlace] as [Burial Place],(REPLACE(convert(nvarchar,DecMemorial,106),'','/')) as [Memorial Date]FROM [dbo].[AboutDeceased] WHERE [DecPassportID] LIKE @FIND", DecView);

            try
            {
                Jacob.Parameters.AddWithValue("@FIND", DeceasedID.Text + "%");
                DecView.Open();
                SqlDataReader JM = Jacob.ExecuteReader();
                AboutZION.DataSource = JM; // Attach searched data to DataGridView
                AboutZION.DataBind();  // Bind data to DataGridView
            }
            catch (Exception VA)
            {
                TopMostDialogs.ShowTopMost(VA.ToString(), "ZAFMC-Viewing Deceased", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                Jacob.Dispose(); //Clean up memory
                DecView.Close(); //Close DBase  connection
            }
        }

        //Update (Event) 
        protected void EditDeceased_Click(object sender, EventArgs e)
        {

            DialogResult Dec = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Dec == DialogResult.Yes)
            {
                UpdateEditedDeceased(); // Update the Record
                AboutDeceasedConnection();  //Refresh the DBase
            }
            else
               if (Dec == DialogResult.No)
            {
                AboutDeceasedConnection();
            }

        }

        //Update (Method) 
        private void UpdateEditedDeceased()
        {
            string DecConn = ConfigurationManager.ConnectionStrings["ZionAboutDeceased"].ConnectionString; // ZionAboutDeceased From Web.Config under ConnectionString Settings
        SqlConnection DecUpdate = new SqlConnection(DecConn);
        SqlCommand FORD = new SqlCommand("UpdateDeceased", DecUpdate);

            try
            {
                FORD.CommandType = System.Data.CommandType.StoredProcedure;
                FORD.Parameters.AddWithValue("@DeceasedID", DBNull.Value);
                FORD.Parameters.AddWithValue("@DecName", DeceasedName.Text);
				FORD.Parameters.AddWithValue("@DecSurname", DeceasedSurname.Text);
                FORD.Parameters.AddWithValue("@DecPassportID", DeceasedID.Text);
                FORD.Parameters.AddWithValue("@DecDOD", DeceasedDate.Text);
                FORD.Parameters.AddWithValue("@DecPosition",DeceasedPosition.Text );
                FORD.Parameters.AddWithValue("@DecBurialPlace",DeceasedBurialPlace.Text );
				FORD.Parameters.AddWithValue("@DecMemorial",MemorialService.Text );
                DecUpdate.Open();
                FORD.ExecuteNonQuery();

            }
            catch (Exception Mhofu)
            {
                TopMostDialogs.ShowTopMost(Mhofu.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                FORD.Dispose(); //Clean up memory
                DecUpdate.Close(); //Close DBase  connection

            }
		}

        //AddNew Record (Event)
        protected void SaveDeceased_Click(object sender, EventArgs e)
        {
            AboutDeceasedAddNew(); // Add or Save Record
            AboutDeceasedConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew Record (Method)
        private void AboutDeceasedAddNew()
        {
            ZAFMCDeceased.Visible = true;
            string DeceasedInsert = ConfigurationManager.ConnectionStrings["ZionAboutDeceased"].ConnectionString; // ZionAboutDeceased From Web.Config under ConnectionString Settings
            SqlConnection DI = new SqlConnection(DeceasedInsert);
            SqlCommand Deceased = new SqlCommand("AddNewDeceased", DI);

            try
            {
                Deceased.CommandType = CommandType.StoredProcedure;
                Deceased.Parameters.AddWithValue("@DeceasedID", DBNull.Value);
                Deceased.Parameters.AddWithValue("@DecName", DeceasedName.Text);
                Deceased.Parameters.AddWithValue("@DecSurname", DeceasedSurname.Text);
                Deceased.Parameters.AddWithValue("@DecPassportID", DeceasedID.Text);
                Deceased.Parameters.AddWithValue("@DecDOD", DeceasedDate.Text);
                Deceased.Parameters.AddWithValue("@DecPosition", DeceasedPosition.Text);
                Deceased.Parameters.AddWithValue("@DecBurialPlace", DeceasedBurialPlace.Text);
                Deceased.Parameters.AddWithValue("@DecMemorial", MemorialService.Text);

                DI.Open();
                Deceased.ExecuteNonQuery();

            }
            catch (Exception AL)
            {
                TopMostDialogs.ShowTopMost(AL.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                Deceased.Dispose(); //Clean up memory
                DI.Close(); //Close DBase  connection

            }
        }

        //Load Up DBase
        public void AboutDeceasedConnection()
        {
            ZAFMCDeceased.Visible = true;
            string AboutDecCon = ConfigurationManager.ConnectionStrings["ZionAboutDeceased"].ConnectionString; // ZionAboutDeceased From Web.Config under ConnectionString Settings
            SqlConnection JPM = new SqlConnection(AboutDecCon);
            SqlCommand Mutepfe = new SqlCommand(@"SELECT [DecName] as [Deceased Name],[DecSurname] as [Deceased Surname],[DecPassportID] as [Deceased Identity],(REPLACE(convert(nvarchar,DecDOD,106),'','/')) as [Date Of Death],[DecPosition] as Position,[DecBurialPlace] as [Burial Place],(REPLACE(convert(nvarchar,DecMemorial,106),'','/')) as [Memorial Date]FROM [dbo].[AboutDeceased] ORDER BY [DecSurname] ", JPM);
            try
            {
                JPM.Open();
                SqlDataReader THEO = Mutepfe.ExecuteReader();
                AboutZION.DataSource = THEO;
                AboutZION.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database...!!");
            }
            finally
            {
                Mutepfe.Dispose(); //Clean up memory
                JPM.Close(); //Close DBase  connection
            }

        }

        //Delete (Event)
        protected void DeleteDeceased_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            ZAFMCDeceased.Visible = true;
            AboutDeceasedDelete();

        }

        //Delete (Method)
        private void AboutDeceasedDelete()
        {
            //Using SQL Stored Procedure
            ZAFMCDeceased.Visible = true;
            string DecDel = ConfigurationManager.ConnectionStrings["ZionAboutDeceased"].ConnectionString; // ZionAboutDeceased From Web.Config under ConnectionString Settings
            SqlConnection JETHRO = new SqlConnection(DecDel);
            SqlCommand MEMORY = new SqlCommand("DeleteDeceased", JETHRO);

            try
            {
                MEMORY.Parameters.AddWithValue("@DelPD", DeceasedID.Text); //DelPD StoredProcedure actual parameter
                MEMORY.CommandType = System.Data.CommandType.StoredProcedure;
                JETHRO.Open();
                MEMORY.ExecuteNonQuery();
                AboutDeceasedConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                MEMORY.Dispose(); //Clean up memory
                JETHRO.Close(); //Close DBase  connection
            }
        }

        //Refresh Deceased Data
        protected void RefreshDeceased_Click(object sender, EventArgs e)
        {
            ZAFMCDeceased.Visible = true;
            AboutDeceasedConnection();
        }
    }

}