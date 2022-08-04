using System;
using System.Collections.Generic;
using System.Configuration; //For DBase connection in the Web.Config
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ZAFMC.Models;


namespace ZAFMC
{
    public partial class Churches : System.Web.UI.Page
    {
        //Page Load (Event)
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            // Resets Controls
            ResetControls(Page);
        }

        //Delete  Church (Event)
        protected void DeleteChurch_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            ChurchDelete();
            ResetControls(Page); //Resets web Controls 

        }

        //Delete  Church(Method)
        private void ChurchDelete()
        {
            //Using SQL Stored Procedure

            string CHDel = ConfigurationManager.ConnectionStrings["ZionChurch"].ConnectionString; // ZionChurch From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(CHDel);
            SqlCommand ABC = new SqlCommand("DeleteChurch", DP);

            try
            {
                ABC.Parameters.AddWithValue("@DelChurch", ChurchName.Text); //@DelChurch StoredProcedure actual parameter passed in the DB
                ABC.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                ABC.ExecuteNonQuery();
                ChurchConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                ABC.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
            }

        }

        //Refresh Church DBase
        protected void RefreshChurch_Click(object sender, EventArgs e)
        {
            ChurchConnection();
            ResetControls(Page); //Resets Web Controls
        }

        //AddNew Church (Event)
        protected void SaveChurch_Click(object sender, EventArgs e)
        {
            ChurchAddNew(); // Add or Save Record
            ChurchConnection(); // Refresh Database

        }
        //AddNew Church (Method)
        private void ChurchAddNew()
        {

            string CHInsert = ConfigurationManager.ConnectionStrings["ZionChurch"].ConnectionString; // ZionMediaNews From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(CHInsert);
            SqlCommand GRA = new SqlCommand("AddNewChurch", MR);

            try
            {

                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@ChurchID", DBNull.Value);
                GRA.Parameters.AddWithValue("@ChurchName", ChurchName.Text);
                GRA.Parameters.AddWithValue("@ChurchLeader", ChurchLeader.Text);
                GRA.Parameters.AddWithValue("@ChurchLeaderCell", LeaderCell.Text);
                GRA.Parameters.AddWithValue("@ChurchLeaderEmail", LeaderEmail.Text);
                GRA.Parameters.AddWithValue("@ChurchDateOpened", DateOpened.Text);
                GRA.Parameters.AddWithValue("@ChurchNumMembers", NumberOfMembers.Text);
                GRA.Parameters.AddWithValue("@ChurchProvince", ChurchProvince.Text);
                GRA.Parameters.AddWithValue("@ChurchDistrict", ChurchDistrict.Text);
                GRA.Parameters.AddWithValue("@ChurchZone", ChurchZone.Text);
                GRA.Parameters.AddWithValue("@ChurchSection", ChurchSection.Text);
                GRA.Parameters.AddWithValue("@ChurchPastor", ChurchPastors.Text);
                GRA.Parameters.AddWithValue("@ChurchEvangelist", ChurchEvangelist.Text);
                GRA.Parameters.AddWithValue("@ChurchPreachers", ChurchPreacher.Text);
                GRA.Parameters.AddWithValue("@ChurchProphets", ChurchProphet.Text);
                GRA.Parameters.AddWithValue("@ChurchDecons", ChurchDecon.Text);
                GRA.Parameters.AddWithValue("@ChurchMembers", ChurchMember.Text);

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
                ResetControls(Page); // Resets all Web Form Controls
            }
        }

        //Refresh Church(Method)
        public void ChurchConnection()
        {

            string CHConn = ConfigurationManager.ConnectionStrings["ZionChurch"].ConnectionString; // ZionChurch From Web.Config under ConnectionString Settings
            SqlConnection BM = new SqlConnection(CHConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [ChrchName] as [Name],[ChrchLeader] as [Leader],[ChrchLeaderCell] as [Leader Cell],[ChrchLeaderEmail] as [Leader Email],(REPLACE(convert(nvarchar,[ChrchDateOpened],106),'','/')) as [Date Opened],[ChrchNumMembers] as [Total Members],[ChrchProvince] as [Province],[ChrchDistrict] as [District],[ChrchZone] as [Zone] ,[ChrchSection] as [Section],[ChrchPastor] as [Pastor List],[ChrchEvangelist] as [Evangelist List],[ChrchPreachers] as [Preachers List],[ChrchProphets] as [Prophets List],[ChrchDecons] as [Decons List],[ChrchMembers] as [Members List] FROM [dbo].[Churches] ORDER BY [ChrchName] ", BM);
            try
            {
                BM.Open();
                
                SqlDataReader MHO = TQ.ExecuteReader();
                
                //CHURCHESLIST.DataSource = MHO;
                //CHURCHESLIST.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                TQ.Dispose(); //Clean up memory
                BM.Close(); //Close DBase  connection
                ResetControls(Page); // Resets all Web Form Controls
            }

        }

        //Update Church(Event) 
        protected void EditChurch_Click(object sender, EventArgs e)
        {

            DialogResult CH = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (CH == DialogResult.Yes)
            {
                UpdateEditedChurch(); // Update the Record
                ChurchConnection();  //Refresh the DBase
            }
            else
               if (CH == DialogResult.No)
            {

                ChurchConnection();
            }

        }

        //Update Church(Method) 
        private void UpdateEditedChurch()
        {

            string MD = ConfigurationManager.ConnectionStrings["ZionChurch"].ConnectionString; // ZionChurch From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateChurch", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@ChurchID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@ChurchName", ChurchName.Text);
                Jethro.Parameters.AddWithValue("@ChurchLeader", ChurchLeader.Text);
                Jethro.Parameters.AddWithValue("@ChurchLeaderCell", LeaderCell.Text);
                Jethro.Parameters.AddWithValue("@ChurchLeaderEmail", LeaderEmail.Text);
                Jethro.Parameters.AddWithValue("@ChurchDateOpened", DateOpened.Text);
                Jethro.Parameters.AddWithValue("@ChurchNumMembers", NumberOfMembers.Text);
                Jethro.Parameters.AddWithValue("@ChurchProvince", ChurchProvince.Text);
                Jethro.Parameters.AddWithValue("@ChurchDistrict", ChurchDistrict.Text);
                Jethro.Parameters.AddWithValue("@ChurchZone", ChurchZone.Text);
                Jethro.Parameters.AddWithValue("@ChurchSection", ChurchSection.Text);
                Jethro.Parameters.AddWithValue("@ChurchPastor", ChurchPastors.Text);
                Jethro.Parameters.AddWithValue("@ChurchEvangelist", ChurchEvangelist.Text);
                Jethro.Parameters.AddWithValue("@ChurchPreachers", ChurchPreacher.Text);
                Jethro.Parameters.AddWithValue("@ChurchProphets", ChurchProphet.Text);
                Jethro.Parameters.AddWithValue("@ChurchDecons", ChurchDecon.Text);
                Jethro.Parameters.AddWithValue("@ChurchMembers", ChurchMember.Text);

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
                ResetControls(Page); //Resets all Controls on the Page

            }
        }

        //View Church(Event)
        protected void ViewChurch_Click(object sender, EventArgs e)
        {

            /*The View for the Church List is being populated 
              by the MODAL FORM in the last section of the 
              Churches.ASPX area.
              It comes out as a pop up on the screen              
             */




            //ChurchView();


        }
        //View Church (Method)
        public void ChurchView()
        {

            string MG = ConfigurationManager.ConnectionStrings["ZionChurch"].ConnectionString; // ZionChurch From Web.Config under ConnectionString Settings
            SqlConnection CH = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [ChrchName] as [Name],[ChrchLeader] as [Leader],[ChrchLeaderCell] as [Leader Cell],[ChrchLeaderEmail] as [Leader Email],(REPLACE(convert(nvarchar,[ChrchDateOpened],106),'','/')) as [Date Opened],[ChrchNumMembers] as [Total Members],[ChrchProvince] as [Province],[ChrchDistrict] as [District],[ChrchZone] as [Zone] ,[ChrchSection] as [Section],[ChrchPastor] as [Pastor List],[ChrchEvangelist] as [Evangelist List],[ChrchPreachers] as [Preachers List],[ChrchProphets] as [Prophets List],[ChrchDecons] as [Decons List],[ChrchMembers] as [Members List] FROM [dbo].[Churches] ORDER BY [ChrchName] ASC", CH);

            try
            {
                
                CH.Open();
                SqlDataReader DB = ZA.ExecuteReader();
                CHURCHESLIST.DataSource = DB; // Attach searched data to DataGridView
                CHURCHESLIST.DataBind();  // Bind data to DataGridView


            }
            catch (Exception FORD)
            {
                MessageBox.Show(FORD.ToString(), "ZAFMC-Viewing Churches", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                CH.Close(); //Close DBase  connection
            }
        }

        protected void CHURCHESLIST_Load(object sender, EventArgs e)
        {
           


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

        protected void ChurchDistrict_TextChanged()
        {
            if (ChurchProvince.SelectedValue =="Gauteng")
            {
                
            }
        }

    }
}