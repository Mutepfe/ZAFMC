
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
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewAccountInfo();
        }

        //Current logged User
        protected void LoggedInUser_Load(object sender, EventArgs e)
        {
            
            LoggedUSer.Text = Environment.UserName;
            //LoggedUser.Text=  PassID.Text;
            UserLogged.Text = string.Concat("(", Environment.UserName, ")");

        }

        protected void ExitZAFMC_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

            

        }
        protected void LoggedUserAccount_Load(object sender, EventArgs e)
        {
           LogInUser.Text = Environment.UserName;
           // LoggedUser.Text = PassID.Text;
           
          

        }

        //View AccountInfo(Method)
        private void ViewAccountInfo()
        {

            string ACC = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            using (SqlConnection AccView = new SqlConnection(ACC))
            {
                //SqlCommand below must Select using the Logged in ID
                PassportIdentity.Text = "ZX757575"; //This value is hardcoded for Testing ONLY; the actual ID must come from Log In window (ID TextBox) passed to ID box of this Account Info
                SqlCommand Jacob = new SqlCommand(@"SELECT   [LogInPassportID],[LogInFirstname],[LogInSurname],[LogInMobileNumber],[LogInEmailAddress],[LogInChurchPosition],[LogInDateTime] FROM [LogIn] WHERE [LogInPassportID] LIKE @SEARCH ", AccView);
                Jacob.Parameters.AddWithValue("@SEARCH", PassportIdentity.Text);
                AccView.Open();
                SqlDataReader JP = Jacob.ExecuteReader();
               
                try
                {
                    while (JP.Read())
                    {
                        // Bind SQL Values to Form Fields
                        PassportIdentity.Text = JP["LogInPassportID"].ToString();
                        LogFirstname.Text = JP["LogInFirstname"].ToString();
                        LogSurname.Text = JP["LogInSurname"].ToString();
                        LogMobile.Text = JP["LogInMobileNumber"].ToString();
                        LogEmail.Text = JP["LogInEmailAddress"].ToString();
                        LogRankPost.Text = JP["LogInChurchPosition"].ToString();

                    }
                }
                catch (Exception MG)
                {
                    MessageBox.Show(MG.Message, "ZAFMC- Failed to Retrieve Data (Account Info)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    Jacob.Dispose();
                    AccView.Close();

                }
            }
        }




    }
}