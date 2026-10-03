using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration; //For DBase connection in the Web.Config
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ZAFMC.Models;


namespace ZAFMC
{
    public partial class Contact : Page
    {
        //Initial Page(Index) Load
        protected void Page_Load(object sender, EventArgs e)
        {
            WinUsername.Text = Environment.UserName.ToString();
            UserDomainName.Text = Environment.UserDomainName.ToString();
            MachineName.Text = Environment.MachineName.ToString();
            WindowsVersion.Text = Environment.OSVersion.ToString();
            BitOS.Text = Environment.Is64BitOperatingSystem.ToString();
            //FolderPath.Text = Environment.GetFolderPath();
            CurrentDirectory.Text = Environment.CurrentDirectory.ToString();
            LogicalDirectory.Text = Environment.GetLogicalDrives().ToString();
            SystemDirectory.Text = Environment.SystemDirectory.ToString();
            CPUCount.Text = Environment.ProcessorCount.ToString();
            Vers.Text = Environment.Version.ToString();
            

            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            

        }
       //Delete (Event)
        protected void DeleteContact_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            HighSixDeleteContact();
            

        }

        //Delete (Method)
        private void HighSixDeleteContact()
        {
            //Using SQL Stored Procedure

            string HighSixView = ConfigurationManager.ConnectionStrings["ZionHighSix"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection SixView = new SqlConnection(HighSixView);
            SqlCommand HSView = new SqlCommand("DeleteContact", SixView);

            try
            {
                HSView.Parameters.AddWithValue("@DelContact", NameSurname.SelectedValue); //DelContact StoredProcedure actual parameter
                HSView.CommandType = System.Data.CommandType.StoredProcedure;
                SixView.Open();
                HSView.ExecuteNonQuery();
                
                HighSixConnection(); // Refresh the Database
                

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                HSView.Dispose(); //Clean up memory
                SixView.Close(); //Close DBase  connection
            }
        }

        //Reset all Control(Event)
        protected void Resets_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
        }

        // Resets all Controls on the Web Form(Method)
        private void ResetControls(System.Web.UI.Control JP)
        {
            foreach (System.Web.UI.Control ctr in JP.Controls)
            {
                //TextBoxes Control
                if (ctr is System.Web.UI.WebControls.TextBox)
                {
                    System.Web.UI.WebControls.TextBox TB = ctr as System.Web.UI.WebControls.TextBox;
                    if(TB != null)
                    {
                        TB.Text = string.Empty;
                    }
                }
                else
                {
                    if(ctr.Controls.Count > 0)
                    {
                        ResetControls(ctr);
                    }
                }

                //DropDownList Control
                if (ctr is System.Web.UI.WebControls.DropDownList)
                {
                    System.Web.UI.WebControls.DropDownList DDL = ctr as System.Web.UI.WebControls.DropDownList;

                    if (DDL !=null)
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

        //Update (Event) 
        protected void EditContact_Click(object sender, EventArgs e)
        {
            //HighSix.AutoGenerateEditButton = true;
            DialogResult Reply = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Reply == DialogResult.Yes)
            {
                UpdateEditedContact(); // Update the Record
                HighSixConnection();  //Refresh the DBase
            }
            else
               if (Reply == DialogResult.No)
            {
                HighSixConnection();
            }

        }

        //Update (Method) 
        private void UpdateEditedContact()
        {
            string HighSixUpdate = ConfigurationManager.ConnectionStrings["ZionHighSix"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection SixUpdate = new SqlConnection(HighSixUpdate);
            SqlCommand HSUpdate = new SqlCommand("UpdateContact", SixUpdate);
            
            try
            {
                HSUpdate.CommandType = System.Data.CommandType.StoredProcedure;
                HSUpdate.Parameters.AddWithValue("@ContactName", NameSurname.Text);
                HSUpdate.Parameters.AddWithValue("@ContactPosition", RankPost.Text);
                HSUpdate.Parameters.AddWithValue("@ContactMail", EmailAddress.Text);
                HSUpdate.Parameters.AddWithValue("@ContactAddress", PhysicalAddress.Text);
                HSUpdate.Parameters.AddWithValue("@ContactID", DBNull.Value);
                HSUpdate.Parameters.AddWithValue("@ContactMobile", MobileNumber.Text);
                SixUpdate.Open();
                HSUpdate.ExecuteNonQuery();
                

            }
            catch (Exception HS)
            {
                TopMostDialogs.ShowTopMost(HS.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                HSUpdate.Dispose(); //Clean up memory
                SixUpdate.Close(); //Close DBase  connection

            }
        }
        //Initialise(Event)
        protected void HighSix_Load(object sender, EventArgs e)
        {
            HighSixConnection();
        }
       
        //DBase Connection(Method)
        public void HighSixConnection()
        {

            string HighSixCon = ConfigurationManager.ConnectionStrings["ZionHighSix"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection HSConnect = new SqlConnection(HighSixCon);
            SqlCommand HSCon = new SqlCommand("SELECT [ContactName] as Name, [ContactPosition] as Position,[ContactMobile] as Mobile, [ContactMail] as Email, [ContactAddress] as Address FROM [Contact] ORDER BY [ContactID]", HSConnect);
            try
            {
                HSConnect.Open();
                SqlDataReader HSC = HSCon.ExecuteReader();
                HighSix.DataSource = HSC;
                HighSix.DataBind();
            }
            catch(Exception JR)
            {
                throw new Exception(JR.Message);
            }
            finally
            {
                HSCon.Dispose(); //Clean up memory
                HSConnect.Close(); //Close DBase  connection
            }

        }

        protected void HighSix_Unload(object sender, EventArgs e)
        {

        }
        
        //View Record(Event)
        protected void ViewCont_Click(object sender, EventArgs e)
        {
            HighSixView();

            if (NameSurname.SelectedValue == ("-1"))
            {
                HighSixConnection();
            }

        }
        //View Record(Method)
        private void HighSixView()
        {

            string HighSixView = ConfigurationManager.ConnectionStrings["ZionHighSix"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection SixView = new SqlConnection(HighSixView);
            SqlCommand HSView = new SqlCommand("SELECT [ContactName] as Name, [ContactPosition] as Position,[ContactMobile] as Mobile, [ContactMail] as Email, [ContactAddress] as Address FROM [Contact] WHERE [ContactName] LIKE @SEARCH", SixView);

            try
            {
                HSView.Parameters.AddWithValue("@SEARCH", NameSurname.SelectedValue + "%");
                SixView.Open();
                SqlDataReader HSC = HSView.ExecuteReader();
                HighSix.DataSource = HSC; // Attach searched data to DataGridView
                HighSix.DataBind();  // Bind data to DataGridView

              
            }
            catch (Exception HSV)
            {
                TopMostDialogs.ShowTopMost(HSV.ToString(), "Viewing High Six", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                HSView.Dispose(); //Clean up memory
                SixView.Close(); //Close DBase  connection
            }
        }
        //Refresh  Record (Event)
        protected void EditRefresh_Click(object sender, EventArgs e)
        {
            HighSixConnection(); //Refresh DataGrid
            ResetControls(Page); //ResetControls only

            
        }
        //AddNew Record(Event)
        protected void SaveCont_Click(object sender, EventArgs e)
        {
            HighSixAddNew(); // Add or Save Record
            HighSixConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls
            

        }
        //AddNew Record(Method)
        private void HighSixAddNew()
        {

            string HighSixInsert = ConfigurationManager.ConnectionStrings["ZionHighSix"].ConnectionString; // ZionHighSix From Web.Config under ConnectionString Settings
            SqlConnection SixInsert = new SqlConnection(HighSixInsert);
            SqlCommand HSInsert = new SqlCommand("AddNewContact", SixInsert);

            try
            {
                HSInsert.CommandType = CommandType.StoredProcedure;
                HSInsert.Parameters.AddWithValue("@ContactName", NameSurname.Text);
                HSInsert.Parameters.AddWithValue("@ContactPosition", RankPost.Text);
                HSInsert.Parameters.AddWithValue("@ContactMail", EmailAddress.Text);
                HSInsert.Parameters.AddWithValue("@ContactMobile", MobileNumber.Text);
                HSInsert.Parameters.AddWithValue("@ContactAddress", PhysicalAddress.Text);
                HSInsert.Parameters.AddWithValue("@ContactID", DBNull.Value);
                SixInsert.Open();
                HSInsert.ExecuteNonQuery();

            }
            catch (Exception HS)
            {
                TopMostDialogs.ShowTopMost(HS.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                HSInsert.Dispose(); //Clean up memory
                SixInsert.Close(); //Close DBase  connection

            }
        }

        
    }
}