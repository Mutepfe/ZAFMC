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
    public partial class MediaCentre : System.Web.UI.Page
    {
        //Page Load (Event)
        protected void Page_Load(object sender, EventArgs e)
        {
            MediaNews.Visible = false;
            MediaSermons.Visible = false;
            MediaVideos.Visible = false;
            MediaTestm.Visible = false;

            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            //Reset Controls
            ResetControls(Page);
        }

        //View Media News(Event)
        protected void ViewMediaNews_Click(object sender, EventArgs e)
        {
            MediaNews.Visible = true;
            MediaList.ToolTip = "ZAFMC - Media News...";
            
            MediaNewsView();

        }
        //View Media News (Method)
        private void MediaNewsView()
        {

            MediaNews.Visible = true;
            string MG = ConfigurationManager.ConnectionStrings["ZionMediaNews"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection NEWS = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [NewsName] as [News Headline],(REPLACE(convert(nvarchar,[NewsDate],106),'','/')) as [Published Date],[NewsFile] as [News File] FROM [dbo].[MediaNews] WHERE [NewsName] LIKE @FIND ", NEWS);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", NewsName.Text + "%");
                NEWS.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                MediaList.DataSource = DB; // Attach searched data to DataGridView
                MediaList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                NEWS.Close(); //Close DBase  connection
            }
        }

        //Refresh Media News
        protected void RefreshMediaNews_Click(object sender, EventArgs e)
        {
            MediaNewsConnection();
            
            ResetControls(Page);//Reset Controls
        }

        //Update MediaNews(Event) 
        protected void EditMediaNews_Click(object sender, EventArgs e)
        {
            MediaNews.Visible = true;
            DialogResult YG = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (YG == DialogResult.Yes)
            {
                MediaNews.Visible = true;
                UpdateEditedMediaNews(); // Update the Record
                MediaNewsConnection();  //Refresh the DBase
                ResetControls(Page);//Reset Controls
            }
            else
               if (YG == DialogResult.No)
            {

                MediaNewsConnection();

                ResetControls(Page);//Reset Controls
            }

        }

        //Update MediaNews(Method) 
        private void UpdateEditedMediaNews()
        {
            MediaNews.Visible = true;
            string MD = ConfigurationManager.ConnectionStrings["ZionMediaNews"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateMediaNews", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@NewsID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@NewsName", NewsName.Text);
                Jethro.Parameters.AddWithValue("@NewsDate", NewsDate.Text);
                Jethro.Parameters.AddWithValue("@NewsFile", NewsFile.Text);

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

        //AddNew MediaNews (Event)
        protected void SaveMediaNews_Click(object sender, EventArgs e)
        {
            MediaNewsAddNew(); // Add or Save Record
            MediaNewsConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew MediaNews (Method)
        private void MediaNewsAddNew()
        {

            string NewsInsert = ConfigurationManager.ConnectionStrings["ZionMediaNews"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(NewsInsert);
            SqlCommand GRA = new SqlCommand("AddNewMediaNews", MR);

            try
            {
                MediaNews.Visible = true;
                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@NewsID", DBNull.Value);
                GRA.Parameters.AddWithValue("@NewsName", NewsName.Text);
                GRA.Parameters.AddWithValue("@NewsDate", NewsDate.Text);
                GRA.Parameters.AddWithValue("@NewsFile", NewsFile.Text);

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

                ResetControls(Page);//Reset Controls

            }
        }

        //Refresh MediaNews(Method)
        public void MediaNewsConnection()
        {
            MediaNews.Visible = true;
            string NewsConn = ConfigurationManager.ConnectionStrings["ZionMediaNews"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection ZV = new SqlConnection(NewsConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [NewsName] as [News Headline],(REPLACE(convert(nvarchar,[NewsDate],106),'','/')) as [Published Date],[NewsFile] as [News File] FROM [dbo].[MediaNews] ORDER BY [NewsName]", ZV);
            try
            {
                ZV.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                MediaList.DataSource = MHO;
                MediaList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                TQ.Dispose(); //Clean up memory
                ZV.Close(); //Close DBase  connection

                ResetControls(Page);//Reset Controls
            }

        }

        //Delete  MediaNews (Event)
        protected void DeleteMediaNews_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            MediaNews.Visible = true;
            MediaNewsDelete();

            ResetControls(Page);//Reset Controls
        }

        //Delete  MediaNews(Method)
        private void MediaNewsDelete()
        {
            //Using SQL Stored Procedure

            string NewsDel = ConfigurationManager.ConnectionStrings["ZionMediaNews"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(NewsDel);
            SqlCommand Theo = new SqlCommand("DeleteMediaNews", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelNews", NewsName.Text); //@DelNews StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                MediaNewsConnection(); // Refresh the Database

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


        //View MediaSermon(Event)
        protected void ViewMediaSermon_Click(object sender, EventArgs e)
        {
            MediaSermons.Visible = true;
            MediaList.ToolTip = "ZAFMC - Media Sermons...";
            MediaSermonView();

        }
        //View MediaSermon (Method)
        private void MediaSermonView()
        {

            MediaSermons.Visible = true;
            string MG = ConfigurationManager.ConnectionStrings["ZionMediaSermon"].ConnectionString; // ZionMediaSermon From Web.Config under ConnectionString Settings
            SqlConnection SERM = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [SermonPreacher] as [Preacher],[SermonEventName] as [Occassion],(REPLACE(convert(nvarchar,[SermonDate],106),'','/')) as [Sermon Date],[SermonVerse] as [Bible Verse],[SermonFile] as [Sermon File] FROM [dbo].[MediaSermon] WHERE [SermonFile] LIKE @FIND ", SERM);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", SermonFile.Text + "%");
                SERM.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                MediaList.DataSource = DB; // Attach searched data to DataGridView
                MediaList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                SERM.Close(); //Close DBase  connection
            }
        }

        //Update MediaSermon(Event) 
        protected void EditMediaSermon_Click(object sender, EventArgs e)
        {
            MediaSermons.Visible = true;
            DialogResult YG = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (YG == DialogResult.Yes)
            {
                UpdateEditedMediaSermon(); // Update the Record
                MediaSermonConnection();  //Refresh the DBase

                ResetControls(Page);//Reset Controls
            }
            else
               if (YG == DialogResult.No)
            {

                MediaSermonConnection();

                ResetControls(Page);//Reset Controls
            }

        }

        //Update MediaSermon(Method) 
        private void UpdateEditedMediaSermon()
        {
            MediaSermons.Visible = true;
            string MD = ConfigurationManager.ConnectionStrings["ZionMediaSermon"].ConnectionString; // ZionMediaSermon From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateMediaSermon", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@SermonID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@SermonPreacher", Preacher.Text);
                Jethro.Parameters.AddWithValue("@SermonEventName", EventName.Text);
                Jethro.Parameters.AddWithValue("@SermonDate", DatePreached.Text);
                Jethro.Parameters.AddWithValue("@SermonVerse", MainBibleVerse.Text);
                Jethro.Parameters.AddWithValue("@SermonFile", SermonFile.Text);

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

        //Delete  MediaSermon (Event)
        protected void DeleteMediaSermon_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            MediaSermons.Visible = true;
            MediaSermonDelete();

            ResetControls(Page);//Reset Controls

        }

        //Delete  MediaSermon(Method)
        private void MediaSermonDelete()
        {
            //Using SQL Stored Procedure

            string NewsDel = ConfigurationManager.ConnectionStrings["ZionMediaSermon"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(NewsDel);
            SqlCommand Theo = new SqlCommand("DeleteMediaSermon", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelSermon", NewsName.Text); //@DelSermon StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                MediaSermonConnection(); // Refresh the Database

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

        //AddNew MediaSermon (Event)
        protected void SaveMediaSermon_Click(object sender, EventArgs e)
        {
            MediaSermonAddNew(); // Add or Save Record
            MediaSermonConnection(); // Refresh Database

            ResetControls(Page);//Reset Controls


        }

        //Refresh MediaSermon
        protected void RefreshMediaSermon_Click(object sender, EventArgs e)
        {
            MediaSermonConnection();
        }
        //Refresh MediaVideo
        protected void RefreshMediaVideo_Click(object sender, EventArgs e)
        {
            MediaVideoConnection();
        }

        //AddNew MediaSermon (Method)
        private void MediaSermonAddNew()
        {

            string SermInsert = ConfigurationManager.ConnectionStrings["ZionMediaSermon"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(SermInsert);
            SqlCommand GRA = new SqlCommand("AddNewMediaSermon", MR);

            try
            {
                MediaSermons.Visible = true;
                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@SermonID", DBNull.Value);
                GRA.Parameters.AddWithValue("@SermonPreacher", Preacher.Text);
                GRA.Parameters.AddWithValue("@SermonEventName", EventName.Text);
                GRA.Parameters.AddWithValue("@SermonDate", DatePreached.Text);
                GRA.Parameters.AddWithValue("@SermonVerse", MainBibleVerse.Text);
                GRA.Parameters.AddWithValue("@SermonFile", SermonFile.Text);

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

                ResetControls(Page);//Reset Controls
            }
        }

        //Refresh MediaSermon(Method)
        public void MediaSermonConnection()
        {
            MediaSermons.Visible = true;
            string SermConn = ConfigurationManager.ConnectionStrings["ZionMediaSermon"].ConnectionString; // ZionMediaSermon From Web.Config under ConnectionString Settings
            SqlConnection ZV = new SqlConnection(SermConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [SermonPreacher] as [Preacher],[SermonEventName] as [Occassion],(REPLACE(convert(nvarchar,[SermonDate],106),'','/')) as [Sermon Date],[SermonVerse] as [Bible Verse],[SermonFile] as [Sermon File] FROM [dbo].[MediaSermon] ORDER BY [SermonFile] ", ZV);
            try
            {
                ZV.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                MediaList.DataSource = MHO;
                MediaList.DataBind();
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

        //Delete  MediaVideo (Event)
        protected void DeleteMediaVideo_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            MediaVideos.Visible = true;
            MediaVideoDelete();

            ResetControls(Page);//Reset Controls

        }

        //Delete  MediaVideo(Method)
        private void MediaVideoDelete()
        {
            //Using SQL Stored Procedure

            string VMDel = ConfigurationManager.ConnectionStrings["ZionMediaVideo"].ConnectionString; // ZionMediaVideo From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(VMDel);
            SqlCommand Theo = new SqlCommand("DeleteMediaVideo", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelVM", VideoMusic.Text); //@DelVM StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                MediaVideoConnection(); // Refresh the Database

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

        //AddNew MediaVideo (Event)
        protected void SaveMediaVideo_Click(object sender, EventArgs e)
        {
            MediaVideos.Visible = true;
            MediaVideoAddNew(); // Add or Save Record
            MediaVideoConnection(); // Refresh Database

            ResetControls(Page);//Reset Controls


        }
        //AddNew MediaVideo (Method)
        private void MediaVideoAddNew()
        {

            string VMInsert = ConfigurationManager.ConnectionStrings["ZionMediaVideo"].ConnectionString; // ZionMediaVideo From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(VMInsert);
            SqlCommand GRA = new SqlCommand("AddNewMediaVideo", MR);

            try
            {
                MediaVideos.Visible = true;
                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@VideoID", DBNull.Value);
                GRA.Parameters.AddWithValue("@VideoName", VideoName.Text);
                GRA.Parameters.AddWithValue("@VideoOccassion", VideoOccassion.Text);
                GRA.Parameters.AddWithValue("@VideoDate", VideoDate.Text);
                GRA.Parameters.AddWithValue("@VideoFile", VideoMusic.Text);

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

        //Refresh MediaVideo(Method)
        public void MediaVideoConnection()
        {
            MediaVideos.Visible = true;
            string VMConn = ConfigurationManager.ConnectionStrings["ZionMediaVideo"].ConnectionString; // ZionMediaVideo From Web.Config under ConnectionString Settings
            SqlConnection ZV = new SqlConnection(VMConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [VideoName] as [Video Name],[VideoOccassion] as [Occassion],(REPLACE(convert(nvarchar,[VideoDate],106),'','/')) as [Recorded Date],[VideoFile] as [Video File] FROM [dbo].[MediaVideo] ORDER BY [VideoName]", ZV);
            try
            {
                ZV.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                MediaList.DataSource = MHO;
                MediaList.DataBind();
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

        //Refresh MediaVideo DBase
        protected void RefresMediaVideo_Click(object sender, EventArgs e)
        {
            MediaVideoConnection();
        }

        //Update MediaVideo(Event) 
        protected void EditMediaVideo_Click(object sender, EventArgs e)
        {
            MediaVideos.Visible = true;
            DialogResult YG = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (YG == DialogResult.Yes)
            {
                UpdateEditedMediaVideo(); // Update the Record
                MediaVideoConnection();  //Refresh the DBase
            }
            else
               if (YG == DialogResult.No)
            {

                MediaVideoConnection();
            }

        }

        //Update MediaVideo(Method) 
        private void UpdateEditedMediaVideo()
        {
            MediaVideos.Visible = true;
            string MD = ConfigurationManager.ConnectionStrings["ZionMediaVideo"].ConnectionString; // ZionMediaVideo From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateMediaVideo", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@VideoID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@VideoName", VideoName.Text);
                Jethro.Parameters.AddWithValue("@VideoOccassion", VideoOccassion.Text);
                Jethro.Parameters.AddWithValue("@VideoDate", VideoDate.Text);
                Jethro.Parameters.AddWithValue("@VideoFile", VideoMusic.Text);

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

                ResetControls(Page);//Reset Controls

            }
        }


        //View MediaVideo(Event)
        protected void ViewMediaVideo_Click(object sender, EventArgs e)
        {
            MediaVideos.Visible = true;
            MediaList.ToolTip = "ZAFMC - Media Videos...";
            MediaVideoView();

        }
        //View MediaVideo (Method)
        private void MediaVideoView()
        {

            MediaVideos.Visible = true;
            string MG = ConfigurationManager.ConnectionStrings["ZionMediaVideo"].ConnectionString; // ZionMediaVideo From Web.Config under ConnectionString Settings
            SqlConnection VIDEOS = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [VideoName] as [Video Name],[VideoOccassion] as [Occassion],(REPLACE(convert(nvarchar,[VideoDate],106),'','/')) as [Recorded Date],[VideoFile] as [Video File] FROM [dbo].[MediaVideo] WHERE [VideoName] LIKE @FIND ", VIDEOS);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", VideoMusic.Text + "%");
                VIDEOS.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                MediaList.DataSource = DB; // Attach searched data to DataGridView
                MediaList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                VIDEOS.Close(); //Close DBase  connection
            }
        }

        //Delete  MediaTestimony (Event)
        protected void DeleteMediaTestimony_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure
            MediaTestm.Visible = true;
            MediaTestimonyDelete();

            ResetControls(Page);//Reset Controls
        }

        //Delete  MediaTestimony(Method)
        private void MediaTestimonyDelete()
        {
            //Using SQL Stored Procedure

            string TMDel = ConfigurationManager.ConnectionStrings["ZionMediaTestimony"].ConnectionString; // ZionMediaTestimony From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(TMDel);
            SqlCommand Theo = new SqlCommand("DeleteMediaTestimony", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelTM", TestimonyFile.Text); //@DelTM StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                MediaNewsConnection(); // Refresh the Database

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

        //AddNew MediaTestimony (Event)
        protected void SaveMediaTestimony_Click(object sender, EventArgs e)
        {
            MediaTestimonyAddNew(); // Add or Save Record
            MediaTestimonyConnection(); // Refresh Database

            ResetControls(Page);//Reset Controls


        }
        //AddNew MediaTestimony (Method)
        private void MediaTestimonyAddNew()
        {

            string TMInsert = ConfigurationManager.ConnectionStrings["ZionMediaTestimony"].ConnectionString; // MediaTestimony From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(TMInsert);
            SqlCommand GRA = new SqlCommand("AddNewMediaTestimony", MR);

            try
            {
                MediaTestm.Visible = true;
                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@TestimonyID", DBNull.Value);
                GRA.Parameters.AddWithValue("@TestmTestifier", Testifier.Text);
                GRA.Parameters.AddWithValue("@TestmDate", TestimonyDate.Text);
                GRA.Parameters.AddWithValue("@TestmFile", TestimonyFile.Text);

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

        //Refresh MediaTestimony(Method)
        public void MediaTestimonyConnection()
        {
            MediaTestm.Visible = true;
            string TMConn = ConfigurationManager.ConnectionStrings["ZionMediaTestimony"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection ZV = new SqlConnection(TMConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [TestmTestifier] as [Testifier],(REPLACE(convert(nvarchar,[TestmDate],106),'','/')) as [Testimony Date],[TestmFile] as [Testimony File] FROM [dbo].[MediaTestimony]  ORDER BY [TestmTestifier] ", ZV);
            try
            {
                ZV.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                MediaList.DataSource = MHO;
                MediaList.DataBind();
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
        //Update MediaTestimony(Event) 
        protected void EditMediaTestimony_Click(object sender, EventArgs e)
        {
            MediaTestm.Visible = true;
            DialogResult YG = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (YG == DialogResult.Yes)
            {
                UpdateEditedMediaTestimony(); // Update the Record
                MediaTestimonyConnection();  //Refresh the DBase
            }
            else
               if (YG == DialogResult.No)
            {

                MediaTestimonyConnection();

                ResetControls(Page);//Reset Controls
            }

        }

        //Update MediaTestimony(Method) 
        private void UpdateEditedMediaTestimony()
        {
            MediaTestm.Visible = true;
            string MD = ConfigurationManager.ConnectionStrings["ZionMediaTestimony"].ConnectionString; // ZionMediaTestimony From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateMediaTestimony", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@TestimonyID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@TestmTestifier", Testifier.Text);
                Jethro.Parameters.AddWithValue("@TestmDate", TestimonyDate.Text);
                Jethro.Parameters.AddWithValue("@TestmFile", TestimonyFile.Text);

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


        //View MediaTestimony(Event)
        protected void ViewMediaTestimony_Click(object sender, EventArgs e)
        {
            MediaNews.Visible = true;
            MediaList.ToolTip = "ZAFMC - Media Testimons...";
            MediaTestimonyView();

        }
        //View MediaTestimony (Method)
        private void MediaTestimonyView()
        {

            MediaNews.Visible = true;
            string MG = ConfigurationManager.ConnectionStrings["ZionMediaTestimony"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection TESTM = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [TestmTestifier] as [Testifier],(REPLACE(convert(nvarchar,[TestmDate],106),'','/')) as [Testimony Date],[TestmFile] as [Testimony File] FROM [dbo].[MediaTestimony]  WHERE [TestmFile] LIKE @FIND ", TESTM);

            try
            {
                ZA.Parameters.AddWithValue("@FIND", TestimonyFile.Text + "%");
                TESTM.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                MediaList.DataSource = DB; // Attach searched data to DataGridView
                MediaList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                TESTM.Close(); //Close DBase  connection
            }
        }

        //Refresh MediaTestimony DBase
        protected void RefreshMediaTestimony_Click(object sender, EventArgs e)
        {
            MediaTestimonyConnection();
        }

        //Reset (Event)
        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
        }
        // Resets all Controls on the Web Form (Method)
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