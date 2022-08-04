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
using ZAFMC.Models;
using System.Data;

namespace ZAFMC
{
    public partial class Passovers : System.Web.UI.Page
    {
        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Pass.Visible = false;

            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        //Delete (Event)
        protected void DeletePassover_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            Pass.Visible = true;
            PassDelete();

        }
        //Delete (Method)
        private void PassDelete()
        {
            //Using SQL Stored Procedure
            Pass.Visible = true;
            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassover"].ConnectionString; // ZionPassover From Web.Config under ConnectionString Settings
            SqlConnection Con = new SqlConnection(PassDel);
            SqlCommand Jethro = new SqlCommand("DeletePassover", Con);

            try
            {
                Jethro.Parameters.AddWithValue("@DelPass", PassoverVenue.Text); //@DelPass StoredProcedure actual parameter passed in the DB
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Con.Open();
                Jethro.ExecuteNonQuery();
                PassoverConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                Con.Close(); //Close DBase  connection
            }





        }

        //AddNew Record (Method)
        protected void SavePassover_Click(object sender, EventArgs e)
        {
            PassoverAddNew(); // Add or Save Record
            PassoverConnection(); // Refresh Database
            ResetControls(Page);


        }


        //Refresh Passover DBase
        protected void RefreshPassover_Click(object sender, EventArgs e)
        {
            Pass.Visible = true;
            PassoverConnection();
        }

        //AddNew Record (Method)
        private void PassoverAddNew()
        {

            string PassInsert = ConfigurationManager.ConnectionStrings["ZionPassover"].ConnectionString; // ZionPassover From Web.Config under ConnectionString Settings
            SqlConnection DJ = new SqlConnection(PassInsert);
            SqlCommand BM = new SqlCommand("AddNewPassover", DJ);

            try
            {
                Pass.Visible = true;
                BM.CommandType = CommandType.StoredProcedure;
                BM.Parameters.AddWithValue("@PassoverID", DBNull.Value);
                BM.Parameters.AddWithValue("@PassoverDate", PassoverDate.Text);
                BM.Parameters.AddWithValue("@PassoverVenue", PassoverVenue.Text);
                BM.Parameters.AddWithValue("@PassoverVenueLeader", VenueLeader.Text);
                BM.Parameters.AddWithValue("@PassoverLeaderMobile", LeaderMobile.Text);

                DJ.Open();
                BM.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                BM.Dispose(); //Clean up memory
                DJ.Close(); //Close DBase  connection

            }
        }

        //Refresh the DBase(Method)
        public void PassoverConnection()
        {

            string PassCon = ConfigurationManager.ConnectionStrings["ZionPassover"].ConnectionString; // ZionPassover From Web.Config under ConnectionString Settings
            SqlConnection JPM = new SqlConnection(PassCon);
            SqlCommand Chitsva = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,PassoverDate,106),'','/')) as [Passover Date],[PassoverVenue] as [Passover Venue],[PassoverVenueLeader] as [Venue Leader],[PassoverLeaderMobile] as [Leader Mobile] FROM [dbo].[Passover] ORDER BY [PassoverVenue]", JPM);
            try
            {
                JPM.Open();
                SqlDataReader MB = Chitsva.ExecuteReader();
                PassoverList.DataSource = MB;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Chitsva.Dispose(); //Clean up memory
                JPM.Close(); //Close DBase  connection
            }

        }

        //Update (Event) 
        protected void EditPassover_Click(object sender, EventArgs e)
        {

            DialogResult EVT = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (EVT == DialogResult.Yes)
            {
                UpdateEditedPassover(); // Update the Record
                PassoverConnection();  //Refresh the DBase
            }
            else
               if (EVT == DialogResult.No)
            {
                PassoverConnection();
            }

        }

        //Update (Method) 
        private void UpdateEditedPassover()
        {
            string MJ = ConfigurationManager.ConnectionStrings["ZionPassover"].ConnectionString; // ZionPassover From Web.Config under ConnectionString Settings
            SqlConnection EvtUpdate = new SqlConnection(MJ);
            SqlCommand Jethro = new SqlCommand("UpdatePassover", EvtUpdate);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@PassoverID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@PassoverDate", PassoverDate.Text);
                Jethro.Parameters.AddWithValue("@PassoverVenue", PassoverVenue.Text);
                Jethro.Parameters.AddWithValue("@PassoverVenueLeader", VenueLeader.Text);
                Jethro.Parameters.AddWithValue("@PassoverLeaderMobile", LeaderMobile.Text);

                EvtUpdate.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception Mhofu)
            {
                MessageBox.Show(Mhofu.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                EvtUpdate.Close(); //Close DBase  connection

            }
        }
        //View Record(Event)
        protected void ViewPassover_Click(object sender, EventArgs e)
        {
            Pass.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Passovers";
            PassView();

        }
        //View Record (Method)
        private void PassView()
        {

            string EV = ConfigurationManager.ConnectionStrings["ZionPassover"].ConnectionString; // ZionPassover From Web.Config under ConnectionString Settings
            SqlConnection EvtView = new SqlConnection(EV);
            SqlCommand PM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,PassoverDate,106),'','/')) as [Passover Date],[PassoverVenue] as [Passover Venue],[PassoverVenueLeader] as [Venue Leader],[PassoverLeaderMobile] as [Leader Mobile] FROM [dbo].[Passover] WHERE [PassoverVenue] LIKE @FIND ", EvtView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", PassoverVenue.Text + "%");
                EvtView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                EvtView.Close(); //Close DBase  connection
            }
        }

        //AddNew Memorial (Event)
        protected void SavePassMemorial_Click(object sender, EventArgs e)
        {
            PassMemorialAddNew(); // Add or Save Record
            PassMemorialConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew Memorial (Method)
        private void PassMemorialAddNew()
        {

            string PMInsert = ConfigurationManager.ConnectionStrings["ZionPassMemorial"].ConnectionString; // ZionPassMemorial From Web.Config under ConnectionString Settings
            SqlConnection DJ = new SqlConnection(PMInsert);
            SqlCommand XM = new SqlCommand("AddNewPassMemorial", DJ);

            try
            {
                Memo.Visible = true;
                XM.CommandType = CommandType.StoredProcedure;
                XM.Parameters.AddWithValue("@MemorialID", DBNull.Value);
                XM.Parameters.AddWithValue("@MemorialDate", MemorialDate.Text);
                XM.Parameters.AddWithValue("@DeceasedName", DeceasedName.Text);
                XM.Parameters.AddWithValue("@MemorialVenue", MemorialVenue.Text);
                XM.Parameters.AddWithValue("@ChurchLeader", ChurchLeader.Text);
                XM.Parameters.AddWithValue("@RelativeMobile", RelativeNumber.Text);

                DJ.Open();
                XM.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                XM.Dispose(); //Clean up memory
                DJ.Close(); //Close DBase  connection

            }
        }

        //Refresh Memorial(Method)
        public void PassMemorialConnection()
        {
            Memo.Visible = true;
            string PMCon = ConfigurationManager.ConnectionStrings["ZionPassMemorial"].ConnectionString; // ZionPassMemorial From Web.Config under ConnectionString Settings
            SqlConnection PaMe = new SqlConnection(PMCon);
            SqlCommand Chitsva = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[MemorialDate],106),'','/')) as [Memorial Date],[DeceasedName] as [Deceased Name],[MemorialVenue] as [Memorial Venue],[ChurchLeader] as [Church Leader],[RelativeMobile] as [Relative Mobile] FROM [dbo].[PassMemorial] ORDER BY [DeceasedName]", PaMe);
            try
            {
                PaMe.Open();
                SqlDataReader MHOFU = Chitsva.ExecuteReader();
                PassoverList.DataSource = MHOFU;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Chitsva.Dispose(); //Clean up memory
                PaMe.Close(); //Close DBase  connection
            }

        }

        //Refresh Memorial(Event)
        protected void RefreshPassMemorial_Click(object sender, EventArgs e)
        {
            Memo.Visible = true;
            PassMemorialConnection();
        }

        //Update Memorial(Event) 
        protected void EditPassMemorial_Click(object sender, EventArgs e)
        {

            DialogResult EVT = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (EVT == DialogResult.Yes)
            {
                UpdateEditedPassMemorial(); // Update the Record
                PassMemorialConnection();  //Refresh the DBase
            }
            else
               if (EVT == DialogResult.No)
            {
                PassMemorialConnection();
            }

        }

        //Update Memorial(Method) 
        private void UpdateEditedPassMemorial()
        {
            string MP = ConfigurationManager.ConnectionStrings["ZionPassMemorial"].ConnectionString; // ZionPassMemorial From Web.Config under ConnectionString Settings
            SqlConnection PMU = new SqlConnection(MP);
            SqlCommand Jethro = new SqlCommand("UpdatePassMemorial", PMU);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@MemorialID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@MemorialDate", MemorialDate.Text);
                Jethro.Parameters.AddWithValue("@DeceasedName", DeceasedName.Text);
                Jethro.Parameters.AddWithValue("@MemorialVenue", MemorialVenue.Text);
                Jethro.Parameters.AddWithValue("@ChurchLeader", ChurchLeader.Text);
                Jethro.Parameters.AddWithValue("@RelativeMobile", RelativeNumber.Text);

                PMU.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                PMU.Close(); //Close DBase  connection

            }
        }

        //View Memorial(Event)
        protected void ViewPassMemorial_Click(object sender, EventArgs e)
        {
            Memo.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Memorial Services";
            PassMemorialView();

        }
        //View Memorial (Method)
        private void PassMemorialView()
        {

            string PMV = ConfigurationManager.ConnectionStrings["ZionPassMemorial"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection PMView = new SqlConnection(PMV);
            SqlCommand PM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[MemorialDate],106),'','/')) as [Memorial Date],[DeceasedName] as [Deceased Name],[MemorialVenue] as [Memorial Venue],[ChurchLeader] as [Church Leader],[RelativeMobile] as [Relative Mobile] FROM [dbo].[PassMemorial] WHERE [DeceasedName] LIKE @FIND ", PMView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", DeceasedName.Text + "%");
                PMView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                PMView.Close(); //Close DBase  connection
            }
        }

        //Refresh Big Sunday(Event)
        protected void RefreshPassBG_Click(object sender, EventArgs e)
        {
            PassBGConnection();
        }
        //AddNew BidSunday (Event)
        protected void SavePassBG_Click(object sender, EventArgs e)
        {
            PassBGAddNew(); // Add or Save Record
            PassBGConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew BidSunday (Method)
        private void PassBGAddNew()
        {

            string BGSInsert = ConfigurationManager.ConnectionStrings["ZionPassBigSunday"].ConnectionString; // ZionPassBigSunday From Web.Config under ConnectionString Settings
            SqlConnection BGS = new SqlConnection(BGSInsert);
            SqlCommand Big = new SqlCommand("AddNewPassBG", BGS);

            try
            {
                BigSund.Visible = true;
                Big.CommandType = CommandType.StoredProcedure;
                Big.Parameters.AddWithValue("@BigSundayID", DBNull.Value);
                Big.Parameters.AddWithValue("@BigSundayDate", BigSundayDate.Text);
                Big.Parameters.AddWithValue("@BigSundayVenue", BigSundayVenue.Text);
                Big.Parameters.AddWithValue("@BigSundayLeader", BigVenueLeader.Text);
                Big.Parameters.AddWithValue("@BigSundayLeaderCell", LeaderCell.Text);

                BGS.Open();
                Big.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                Big.Dispose(); //Clean up memory
                BGS.Close(); //Close DBase  connection

            }
        }

        //Refresh BigSunday(Method)
        public void PassBGConnection()
        {
            BigSund.Visible = true;
            string BGCon = ConfigurationManager.ConnectionStrings["ZionPassBigSunday"].ConnectionString; // ZionPassBigSunday From Web.Config under ConnectionString Settings
            SqlConnection BQ = new SqlConnection(BGCon);
            SqlCommand BC = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[BigSundayDate],106),'','/')) as [Date],[BigSundayVenue] as [Venue],[BigSundayLeader] as [Church Leader],[BigSundayLeaderCell] as [Leader Cell] FROM [dbo].[PassBigSunday] ORDER BY [BigSundayVenue]", BQ);
            try
            {
                BQ.Open();
                SqlDataReader MHOFU = BC.ExecuteReader();
                PassoverList.DataSource = MHOFU;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                BC.Dispose(); //Clean up memory
                BQ.Close(); //Close DBase  connection
            }

        }

        //Update BigSunday(Event) 
        protected void EditPassBG_Click(object sender, EventArgs e)
        {
            BigSund.Visible = true;
            DialogResult MT = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MT == DialogResult.Yes)
            {
                UpdateEditedPassBG(); // Update the Record
                PassBGConnection();  //Refresh the DBase
            }
            else
               if (MT == DialogResult.No)
            {
                PassBGConnection();
            }

        }


        //Update BigSunday(Method) 
        private void UpdateEditedPassBG()
        {
            BigSund.Visible = true;
            string VK = ConfigurationManager.ConnectionStrings["ZionPassBigSunday"].ConnectionString; // ZionPassBigSunday From Web.Config under ConnectionString Settings
            SqlConnection PMU = new SqlConnection(VK);
            SqlCommand Jethro = new SqlCommand("UpdatePassBigSunday", PMU);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@BigSundayID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@BigSundayDate", BigSundayDate.Text);
                Jethro.Parameters.AddWithValue("@BigSundayVenue", BigSundayVenue.Text);
                Jethro.Parameters.AddWithValue("@BigSundayLeader", BigVenueLeader.Text);
                Jethro.Parameters.AddWithValue("@BigSundayLeaderCell", LeaderCell.Text);

                PMU.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                PMU.Close(); //Close DBase  connection

            }
        }

        //View BigSunday(Event)
        protected void ViewPassBG_Click(object sender, EventArgs e)
        {
            BigSund.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Big Sundays";
            PassBGView();

        }
        //View BigSunday (Method)
        private void PassBGView()
        {

            string MV = ConfigurationManager.ConnectionStrings["ZionPassBigSunday"].ConnectionString; // ZionPassBigSunday From Web.Config under ConnectionString Settings
            SqlConnection MView = new SqlConnection(MV);
            SqlCommand ZM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[BigSundayDate],106),'','/')) as [Date],[BigSundayVenue] as [Venue],[BigSundayLeader] as [Church Leader],[BigSundayLeaderCell] as [Leader Cell] FROM [dbo].[PassBigSunday] WHERE [BigSundayVenue] LIKE @FIND ", MView);

            try
            {
                ZM.Parameters.AddWithValue("@FIND", BigSundayVenue.Text + "%");
                MView.Open();
                SqlDataReader Jac = ZM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZM.Dispose(); //Clean up memory
                MView.Close(); //Close DBase  connection
            }
        }

        //AddNew Graduation (Event)
        protected void SavePassGraduation_Click(object sender, EventArgs e)
        {
            PassGraduationAddNew(); // Add or Save Record
            PassGraduationConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew Graduation (Method)
        private void PassGraduationAddNew()
        {

            string GradInsert = ConfigurationManager.ConnectionStrings["ZionPassGraduation"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(GradInsert);
            SqlCommand GRA = new SqlCommand("AddNewPassGraduation", MR);

            try
            {
                Grad.Visible = true;
                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@GraduationID", DBNull.Value);
                GRA.Parameters.AddWithValue("@GraduationDate", CeremonyDate.Text);
                GRA.Parameters.AddWithValue("@GraduationVenue", CeremonyVenue.Text);
                GRA.Parameters.AddWithValue("@GraduateName", GraduateName.Text);
                GRA.Parameters.AddWithValue("@GraduateMobile", GraduateCell.Text);
                GRA.Parameters.AddWithValue("@GraduateDegree", DegreeProgramme.Text);
                GRA.Parameters.AddWithValue("@GraduateLeader", ChrchLeader.Text);
                GRA.Parameters.AddWithValue("@GraduateLeaderMobile", LdrCell.Text);

                MR.Open();
                GRA.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                GRA.Dispose(); //Clean up memory
                MR.Close(); //Close DBase  connection

            }
        }

        //Refresh Graduation(Method)
        public void PassGraduationConnection()
        {
            Grad.Visible = true;
            string GCon = ConfigurationManager.ConnectionStrings["ZionPassGraduation"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection ZV = new SqlConnection(GCon);
            SqlCommand TQ = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[GraduationDate], 106), '', '/')) as [Ceremony Date],[GraduationVenue] as [Venue],[GraduateName] as [Graduatee],[GraduateMobile] as [Graduate Cell],[GraduateDegree] as [Degree],[GraduateLeader] as [Church Leader],[GraduateLeaderMobile] as [Leader Cell] FROM[dbo].[PassGraduation] ORDER BY[GraduateName]", ZV);
            try
            {
                ZV.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                PassoverList.DataSource = MHO;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                TQ.Dispose(); //Clean up memory
                ZV.Close(); //Close DBase  connection
            }

        }

        //Update Graduation(Event) 
        protected void EditPassGraduation_Click(object sender, EventArgs e)
        {
            Grad.Visible = true;
            DialogResult YG = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (YG == DialogResult.Yes)
            {
                UpdateEditedPassGraduation(); // Update the Record
                PassGraduationConnection();  //Refresh the DBase
            }
            else
               if (YG == DialogResult.No)
            {

                PassGraduationConnection();
            }

        }

        //Update Graduation(Method) 
        private void UpdateEditedPassGraduation()
        {
            Grad.Visible = true;
            string MD = ConfigurationManager.ConnectionStrings["ZionPassGraduation"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdatePassGraduation", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@GraduationID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@GraduationDate", CeremonyDate.Text);
                Jethro.Parameters.AddWithValue("@GraduationVenue", CeremonyVenue.Text);
                Jethro.Parameters.AddWithValue("@GraduateName", GraduateName.Text);
                Jethro.Parameters.AddWithValue("@GraduateMobile", GraduateCell.Text);
                Jethro.Parameters.AddWithValue("@GraduateDegree", DegreeProgramme.Text);
                Jethro.Parameters.AddWithValue("@GraduateLeader", ChrchLeader.Text);
                Jethro.Parameters.AddWithValue("@GraduateLeaderMobile", LdrCell.Text);

                ZW.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                ZW.Close(); //Close DBase  connection

            }
        }


        //View Graduation(Event)
        protected void ViewPassGraduation_Click(object sender, EventArgs e)
        {
            Grad.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Graduations";
            PassGraduationView();


        }
        //View Graduation (Method)
        private void PassGraduationView()
        {

            string MG = ConfigurationManager.ConnectionStrings["ZionPassGraduation"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection GView = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[GraduationDate],106),'','/')) as [Ceremony Date],[GraduationVenue] as [Venue],[GraduateName] as [Graduatee],[GraduateMobile] as [Graduate Cell],[GraduateDegree] as [Degree],[GraduateLeader] as [Church Leader],[GraduateLeaderMobile] as [Leader Cell] FROM [dbo].[PassGraduation] WHERE [GraduateName] LIKE @FIND ", GView);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", GraduateName.Text + "%");
                GView.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                PassoverList.DataSource = DB; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                GView.Close(); //Close DBase  connection
            }
        }

        //Refresh Database
        protected void RefreshPassGraduation_Click(object sender, EventArgs e)
        {
            PassGraduationConnection();
        }


        //View Youth(Event)
        protected void ViewPassYouth_Click(object sender, EventArgs e)
        {
            YTH.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Youth Conferences";
            PassYouthView();
        }
        //View Youth (Method)
        private void PassYouthView()
        {
            YTH.Visible = true;
            string PV = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassYouth From Web.Config under ConnectionString Settings
            SqlConnection YoView = new SqlConnection(PV);
            SqlCommand PM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,YouthDate,106),'','/')) as [Conference Date],[YouthVenue] as [Youth Venue],[YouthLeader] as [Youth Leader],[YouthDuration] as [Duration(Days)]FROM [dbo].[PassYouth] WHERE [YouthVenue]  LIKE @FIND ", YoView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", YouthConVenue.Text + "%");
                YoView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                YoView.Close(); //Close DBase  connection
            }
        }

        //Update Youth(Event) 
        protected void EditPassYouth_Click(object sender, EventArgs e)
        {
            YTH.Visible = true;
            DialogResult BC = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (BC == DialogResult.Yes)
            {
                UpdateEditedPassYouth(); // Update the Record
                PassYouthConnection();  //Refresh the DBase
            }
            else
               if (BC == DialogResult.No)
            {
                PassMemorialConnection();
            }

        }

        //Update Youth(Method) 
        private void UpdateEditedPassYouth()
        {
            string PR = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassYouth From Web.Config under ConnectionString Settings
            SqlConnection RU = new SqlConnection(PR);
            SqlCommand Jethro = new SqlCommand("UpdatePassYouth", RU);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@YouthID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@YouthVenue", YouthConVenue.Text);
                Jethro.Parameters.AddWithValue("@YouthLeader", YouthConLeader.Text);
                Jethro.Parameters.AddWithValue("@YouthDuration", YouthConDuration.Text);
                Jethro.Parameters.AddWithValue("@YouthDate", YouthConDate.Text);

                RU.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                RU.Close(); //Close DBase  connection

            }
        }

        //AddNew Youth (Event)
        protected void SavePassYouth_Click(object sender, EventArgs e)
        {
            PassYouthAddNew(); // Add or Save Record
            PassYouthConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew Youth (Method)
        private void PassYouthAddNew()
        {

            string DIANA = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassYouth From Web.Config under ConnectionString Settings
            SqlConnection CM = new SqlConnection(DIANA);
            SqlCommand XG = new SqlCommand("AddNewPassYouth", CM);

            try
            {
                YTH.Visible = true;
                XG.CommandType = CommandType.StoredProcedure;
                XG.Parameters.AddWithValue("@YouthID", DBNull.Value);
                XG.Parameters.AddWithValue("@YouthVenue", YouthConVenue.Text);
                XG.Parameters.AddWithValue("@YouthLeader", YouthConLeader.Text);
                XG.Parameters.AddWithValue("@YouthDuration", YouthConDuration.Text);
                XG.Parameters.AddWithValue("@YouthDate", YouthConDate.Text);

                CM.Open();
                XG.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                XG.Dispose(); //Clean up memory
                CM.Close(); //Close DBase  connection

            }
        }

        //Refresh Youth(Method)
        public void PassYouthConnection()
        {
            YTH.Visible = true;
            string YCon = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassYouth From Web.Config under ConnectionString Settings
            SqlConnection HZ = new SqlConnection(YCon);
            SqlCommand Chitsva = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,YouthDate,106),'','/')) as [Conference Date],[YouthVenue] as [Youth Venue],[YouthLeader] as [Youth Leader],[YouthDuration] as [Duration(Days)]FROM [dbo].[PassYouth] ORDER BY [YouthVenue]", HZ);
            try
            {
                HZ.Open();
                SqlDataReader MHOFU = Chitsva.ExecuteReader();
                PassoverList.DataSource = MHOFU;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Chitsva.Dispose(); //Clean up memory
                HZ.Close(); //Close DBase  connection
            }

        }

        //RefreshYouth DBASE Connection
        protected void RefreshPassYouth_Click(object sender, EventArgs e)
        {
            PassYouthConnection();
        }


        //View Women(Event)
        protected void ViewPassWomen_Click(object sender, EventArgs e)
        {
            WomEv.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Women Conferences";
            PassWomenView();


        }
        //View Women (Method)
        private void PassWomenView()
        {
            WomEv.Visible = true;
            string WH = ConfigurationManager.ConnectionStrings["ZionPassWomen"].ConnectionString; // ZionPassWomen From Web.Config under ConnectionString Settings
            SqlConnection WOMView = new SqlConnection(WH);
            SqlCommand RX = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,WomenDate,106),'','/')) as [Conference Date],[WomenVenue] as [Venue],[WomenLeader] as [Women Leader],[WomenDuration] as [Duration(Days)] FROM [dbo].[PassWomen] WHERE [WomenVenue] LIKE @FIND ", WOMView);

            try
            {
                RX.Parameters.AddWithValue("@FIND", WomenVenue.Text + "%");
                WOMView.Open();
                SqlDataReader Jac = RX.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                RX.Dispose(); //Clean up memory
                WOMView.Close(); //Close DBase  connection
            }
        }


        //Update Women(Event) 
        protected void EditPassWomen_Click(object sender, EventArgs e)
        {
            WomEv.Visible = true;
            DialogResult WR = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (WR == DialogResult.Yes)
            {
                UpdateEditedPassWomen(); // Update the Record
                PassWomenConnection();  //Refresh the DBase
            }
            else
               if (WR == DialogResult.No)
            {
                PassWomenConnection();
            }

        }

        //Update Women(Method) 
        private void UpdateEditedPassWomen()
        {
            string FK = ConfigurationManager.ConnectionStrings["ZionPassWomen"].ConnectionString; // ZionPassWomen From Web.Config under ConnectionString Settings
            SqlConnection QM = new SqlConnection(FK);
            SqlCommand Maria = new SqlCommand("UpdatePassWomen", QM);

            try
            {
                Maria.CommandType = System.Data.CommandType.StoredProcedure;
                Maria.Parameters.AddWithValue("@WomenID", DBNull.Value);
                Maria.Parameters.AddWithValue("@WomenDate", WomenDate.Text);
                Maria.Parameters.AddWithValue("@WomenVenue", WomenVenue.Text);
                Maria.Parameters.AddWithValue("@WomenLeader", WomenLeader.Text);
                Maria.Parameters.AddWithValue("@WomenDuration", WomenDuration.Text);

                QM.Open();
                Maria.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Maria.Dispose(); //Clean up memory
                QM.Close(); //Close DBase  connection

            }
        }

        //Refresh Women DBASE
        protected void RefreshPassWomen_Click(object sender, EventArgs e)
        {
            PassWomenConnection();
        }

        //AddNew Women (Event)
        protected void SavePassWomen_Click(object sender, EventArgs e)
        {
            PassWomenAddNew(); // Add or Save Record
            PassWomenConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls

        }
        //AddNew Women (Method)
        private void PassWomenAddNew()
        {

            string BKT = ConfigurationManager.ConnectionStrings["ZionPassWomen"].ConnectionString; // ZionPassWomen From Web.Config under ConnectionString Settings
            SqlConnection CM = new SqlConnection(BKT);
            SqlCommand XG = new SqlCommand("AddNewPassWomen", CM);

            try
            {
                WomEv.Visible = true;
                XG.CommandType = CommandType.StoredProcedure;
                XG.Parameters.AddWithValue("@WomenID", DBNull.Value);
                XG.Parameters.AddWithValue("@WomenDate", WomenDate.Text);
                XG.Parameters.AddWithValue("@WomenVenue", WomenVenue.Text);
                XG.Parameters.AddWithValue("@WomenLeader", WomenLeader.Text);
                XG.Parameters.AddWithValue("@WomenDuration", WomenDuration.Text);

                CM.Open();
                XG.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                XG.Dispose(); //Clean up memory
                CM.Close(); //Close DBase  connection

            }
        }

        //Refresh Women(Method)
        public void PassWomenConnection()
        {
            WomEv.Visible = true;
            string WomCon = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassYouth From Web.Config under ConnectionString Settings
            SqlConnection HZ = new SqlConnection(WomCon);
            SqlCommand Chitsva = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,WomenDate,106),'','/')) as [Conference Date],[WomenVenue] as [Venue],[WomenLeader] as [Women Leader],[WomenDuration] as [Duration(Days)] FROM [dbo].[PassWomen] ORDER BY [WomenVenue] ", HZ);
            try
            {
                HZ.Open();
                SqlDataReader MHOFU = Chitsva.ExecuteReader();
                PassoverList.DataSource = MHOFU;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Chitsva.Dispose(); //Clean up memory
                HZ.Close(); //Close DBase  connection
            }

        }

        //AddNew General (Event)
        protected void SavePassGeneralMeeting_Click(object sender, EventArgs e)
        {
            GenM.Visible = true;
            PassGeneralMeetingAddNew(); // Add or Save Record
            PassGeneralMeetingConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls

        }
        //AddNew General (Method)
        private void PassGeneralMeetingAddNew()
        {

            string GM = ConfigurationManager.ConnectionStrings["ZionPassGeneralMeeting"].ConnectionString; // ZionPassGeneralMeeting From Web.Config under ConnectionString Settings
            SqlConnection CM = new SqlConnection(GM);
            SqlCommand Gen = new SqlCommand("AddNewPassGeneralMeeting", CM);

            try
            {
                GenM.Visible = true;
                Gen.CommandType = CommandType.StoredProcedure;
                Gen.Parameters.AddWithValue("@GeneralID", DBNull.Value);
                Gen.Parameters.AddWithValue("@GeneralDate", GeneralDate.Text);
                Gen.Parameters.AddWithValue("@GeneralDescription", GMDescription.Text);
                Gen.Parameters.AddWithValue("@GeneralVenue", GMVenue.Text);
                Gen.Parameters.AddWithValue("@GeneralLeader", GMLeader.Text);
                Gen.Parameters.AddWithValue("@GeneralDuration", GMDuration.Text);

                CM.Open();
                Gen.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                Gen.Dispose(); //Clean up memory
                CM.Close(); //Close DBase  connection

            }
        }

        //Refresh General(Method)
        public void PassGeneralMeetingConnection()
        {
            GenM.Visible = true;
            string GMeet = ConfigurationManager.ConnectionStrings["ZionPassGeneralMeeting"].ConnectionString; // ZionPassGeneralMeeting From Web.Config under ConnectionString Settings
            SqlConnection KR = new SqlConnection(GMeet);
            SqlCommand SHALOM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[GeneralDate],106),'','/')) as [Date],[GeneralDescription] as [Description],[GeneralVenue] as [Venue],[GeneralLeader] as [Leader],[GeneralDuration] as [Duration(Days)] FROM [dbo].[PassGeneralMeeting] ORDER BY [GeneralVenue]", KR);
            try
            {
                KR.Open();
                SqlDataReader MHOFU = SHALOM.ExecuteReader();
                PassoverList.DataSource = MHOFU;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                SHALOM.Dispose(); //Clean up memory
                KR.Close(); //Close DBase  connection
            }

        }

        //Update General(Event) 
        protected void EditPassGeneralMeeting_Click(object sender, EventArgs e)
        {
            GenM.Visible = true;
            DialogResult WB = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (WB == DialogResult.Yes)
            {
                UpdateEditedPassGeneralMeeting(); // Update the Record
                PassGeneralMeetingConnection();  //Refresh the DBase
            }
            else
               if (WB == DialogResult.No)
            {
                PassGeneralMeetingConnection();
            }

        }

        //Update General(Method) 
        private void UpdateEditedPassGeneralMeeting()
        {
            string PR = ConfigurationManager.ConnectionStrings["ZionPassGeneralMeeting"].ConnectionString; // ZionPassGeneralMeeting From Web.Config under ConnectionString Settings
            SqlConnection RU = new SqlConnection(PR);
            SqlCommand Jethro = new SqlCommand("UpdatePassGeneralMeeting", RU);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@GeneralID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@GeneralDate", GeneralDate.Text);
                Jethro.Parameters.AddWithValue("@GeneralDescription", GMDescription.Text);
                Jethro.Parameters.AddWithValue("@GeneralVenue", GMVenue.Text);
                Jethro.Parameters.AddWithValue("@GeneralLeader", GMLeader.Text);
                Jethro.Parameters.AddWithValue("@GeneralDuration", GMDuration.Text);

                RU.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                RU.Close(); //Close DBase  connection

            }
        }
        //Refresh General DBase
        protected void RefreshPassGeneralMeeting_Click(object sender, EventArgs e)
        {
            PassGeneralMeetingConnection();
        }


        //View General(Event)
        protected void ViewPassGeneralMeeting_Click(object sender, EventArgs e)
        {
            GenM.Visible = true;
            PassoverList.ToolTip = "ZAFMC - General Meetings";
            PassGeneralMeetingView();

        }
        //View General (Method)
        private void PassGeneralMeetingView()
        {
            GenM.Visible = true;
            string GV = ConfigurationManager.ConnectionStrings["ZionPassGeneralMeeting"].ConnectionString; // ZionPassGeneralMeeting From Web.Config under ConnectionString Settings
            SqlConnection GenView = new SqlConnection(GV);
            SqlCommand PM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[GeneralDate],106),'','/')) as [Date],[GeneralDescription] as [Description],[GeneralVenue] as [Venue],[GeneralLeader] as [Leader],[GeneralDuration] as [Duration(Days)] FROM [dbo].[PassGeneralMeeting] WHERE [GeneralVenue]  LIKE @FIND ", GenView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", GMVenue.Text + "%");
                GenView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                GenView.Close(); //Close DBase  connection
            }
        }


        //View Weddings(Event)
        protected void ViewPassWedding_Click(object sender, EventArgs e)
        {
            Wedds.Visible = true;
            PassoverList.ToolTip = "ZAFMC - Weddings";
            PassWeddingView();


        }
        //View Weddings (Method)
        private void PassWeddingView()
        {
            Wedds.Visible = true;
            string WV = ConfigurationManager.ConnectionStrings["ZionPassWedding"].ConnectionString; // ZionPassWedding From Web.Config under ConnectionString Settings
            SqlConnection WeddView = new SqlConnection(WV);
            SqlCommand PM = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[WeddingDate],106),'','/')) as [Date],[Venue] as [Venue],[Bride] as [Husband],[BrideMobile] as[Bride Mobile] ,[Groom] as [Wife] ,[GroomMobile] as [Wife Cell] ,[MarriageOfficer] as [Marriage Officer],[ChurchLeader] as [Church Leader],[LeaderMobile] as [Leader Cell] FROM [dbo].[PassWedding]WHERE [Bride] LIKE @FIND ", WeddView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", BrideName.Text + "%");
                WeddView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                PassoverList.DataSource = Jac; // Attach searched data to DataGridView
                PassoverList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                WeddView.Close(); //Close DBase  connection
            }
        }

        //Update Wedding(Event) 
        protected void EditPassWedding_Click(object sender, EventArgs e)
        {
            Wedds.Visible = true;
            DialogResult DR = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                UpdateEditedPassWedding(); // Update the Record
                PassWeddingConnection();  //Refresh the DBase
            }
            else
               if (DR == DialogResult.No)
            {
                PassWeddingConnection();
            }

        }

        //Update Wedding(Method) 
        private void UpdateEditedPassWedding()
        {
            string Weddz = ConfigurationManager.ConnectionStrings["ZionPassWedding"].ConnectionString; // ZionPassWedding From Web.Config under ConnectionString Settings
            SqlConnection BG = new SqlConnection(Weddz);
            SqlCommand ANITA = new SqlCommand("UpdatePassWedding", BG);

            try
            {
                ANITA.CommandType = System.Data.CommandType.StoredProcedure;
                ANITA.Parameters.AddWithValue("@WeddingID", DBNull.Value);
                ANITA.Parameters.AddWithValue("@WeddingDate", WeddingDate.Text);
                ANITA.Parameters.AddWithValue("@Venue", WeddingVenue.Text);
                ANITA.Parameters.AddWithValue("@Bride", BrideName.Text);
                ANITA.Parameters.AddWithValue("@BrideMobile", BrideMobile.Text);
                ANITA.Parameters.AddWithValue("@Groom", GroomName.Text);
                ANITA.Parameters.AddWithValue("@GroomMobile", GroomCell.Text);
                ANITA.Parameters.AddWithValue("@MarriageOfficer", MarriageOfficer.Text);
                ANITA.Parameters.AddWithValue("@ChurchLeader", WedChrchLeader.Text);
                ANITA.Parameters.AddWithValue("@LeaderMobile", WeddLeadCell.Text);

                BG.Open();
                ANITA.ExecuteNonQuery();

            }
            catch (Exception ZIM)
            {
                MessageBox.Show(ZIM.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                ANITA.Dispose(); //Clean up memory
                BG.Close(); //Close DBase  connection

            }
        }

        //Refresh Wedding DBASE
        protected void RefreshPassWedding_Click(object sender, EventArgs e)
        {
            PassWeddingConnection();
        }

        //AddNew Wedding (Event)
        protected void SavePassWedding_Click(object sender, EventArgs e)
        {
            PassWeddingAddNew(); // Add or Save Record
            PassWeddingConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls

        }
        //AddNew Wedding (Method)
        private void PassWeddingAddNew()
        {

            string WeddAdd = ConfigurationManager.ConnectionStrings["ZionPassWedding"].ConnectionString; // ZionPassWedding From Web.Config under ConnectionString Settings
            SqlConnection HD = new SqlConnection(WeddAdd);
            SqlCommand ALLAN = new SqlCommand("AddNewPassWedding", HD);

            try
            {
                Wedds.Visible = true;
                ALLAN.CommandType = CommandType.StoredProcedure;
                ALLAN.Parameters.AddWithValue("@WeddingID", DBNull.Value);
                ALLAN.Parameters.AddWithValue("@WeddingDate", WeddingDate.Text);
                ALLAN.Parameters.AddWithValue("@Venue", WeddingVenue.Text);
                ALLAN.Parameters.AddWithValue("@Bride", BrideName.Text);
                ALLAN.Parameters.AddWithValue("@BrideMobile", BrideMobile.Text);
                ALLAN.Parameters.AddWithValue("@Groom", GroomName.Text);
                ALLAN.Parameters.AddWithValue("@GroomMobile", GroomCell.Text);
                ALLAN.Parameters.AddWithValue("@MarriageOfficer", MarriageOfficer.Text);
                ALLAN.Parameters.AddWithValue("@ChurchLeader", WedChrchLeader.Text);
                ALLAN.Parameters.AddWithValue("@LeaderMobile", WeddLeadCell.Text);

                HD.Open();
                ALLAN.ExecuteNonQuery();

            }
            catch (Exception P)
            {
                MessageBox.Show(P.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                ALLAN.Dispose(); //Clean up memory
                HD.Close(); //Close DBase  connection

            }
        }

        //Refresh Wedding(Method)
        public void PassWeddingConnection()
        {
            Wedds.Visible = true;
            string WedzCon = ConfigurationManager.ConnectionStrings["ZionPassWedding"].ConnectionString; // ZionPassWedding From Web.Config under ConnectionString Settings
            SqlConnection THEO = new SqlConnection(WedzCon);
            SqlCommand WESLEY = new SqlCommand(@"SELECT (REPLACE(convert(nvarchar,[WeddingDate],106),'','/')) as [Date],[Venue] as [Venue],[Bride] as [Husband],[BrideMobile] as[Bride Mobile] ,[Groom] as [Wife] ,[GroomMobile] as [Wife Cell] ,[MarriageOfficer] as [Marriage Officer],[ChurchLeader] as [Church Leader],[LeaderMobile] as [Leader Cell] FROM [dbo].[PassWedding]ORDER BY [Bride] ", THEO);
            try
            {
                THEO.Open();
                SqlDataReader MARIA = WESLEY.ExecuteReader();
                PassoverList.DataSource = MARIA;
                PassoverList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                WESLEY.Dispose(); //Clean up memory
                THEO.Close(); //Close DBase  connection
            }

        }


        //Delete  BigSunday (Event)
        protected void DeletePassBG_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassBGDelete();

        }

        //Delete  BigSunday(Method)
        private void PassBGDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassBigSunday"].ConnectionString; // ZionPassBigSunday From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassBigSunday", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassBG", DeceasedName.Text); //@DelPassBG StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassoverConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }


        //Delete  Graduation (Event)
        protected void DeletePassGraduation_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassGraduationDelete();

        }

        //Delete  Graduation(Method)
        private void PassGraduationDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassGraduation"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassGraduation", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassGrad", GraduateName.Text); //@DelPassGrad StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassGraduationConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }


        //Delete  Youth (Event)
        protected void DeletePassYouth_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassYouthDelete();

        }

        //Delete  Youth(Method)
        private void PassYouthDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassYouth"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassYouth", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassYT", GraduateName.Text); //@DelPassYT StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassYouthConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }

        //Delete  Women (Event)
        protected void DeletePassWomen_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassWomenDelete();

        }

        //Delete  Women(Method)
        private void PassWomenDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassWomen"].ConnectionString; // ZionPassGraduation From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassWomen", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassWN", WomenVenue.Text); //@DelPassWN StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassWomenConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }

        //Delete  GeneralMeeting (Event)
        protected void DeletePassGeneralMeeting_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassGeneralMeetingDelete();

        }

        //Delete  GeneralMeeting(Method)
        private void PassGeneralMeetingDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassGeneral"].ConnectionString; // ZionPassGeneral From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassGeneralMeeting", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassGM", GMVenue.Text); //@DelPassGM StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassGeneralMeetingConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }

        //Delete  Wedding (Event)
        protected void DeletePassWedding_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassWeddingDelete();

        }

        //Delete  Wedding(Method)
        private void PassWeddingDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassWedding"].ConnectionString; // ZionPassGeneral From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassWedding", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassBride", BrideName.Text); //@DelPassBride StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassGeneralMeetingConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }


        //Delete  Memorial (Event)
        protected void DeletePassMemorial_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            PassMemorialDelete();

        }

        //Delete  Memorial(Method)
        private void PassMemorialDelete()
        {
            //Using SQL Stored Procedure

            string PassDel = ConfigurationManager.ConnectionStrings["ZionPassMemorial"].ConnectionString; // ZionPassGeneral From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PassDel);
            SqlCommand Theo = new SqlCommand("DeletePassMemorial", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPassMem", BrideName.Text); //@DelPassMem StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                PassGeneralMeetingConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }


        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
        }
        // Resets all Controls on the Web Form(Method)
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


    }
}