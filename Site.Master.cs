
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Menu = System.Web.UI.WebControls.Menu;
using System.Text.RegularExpressions;

namespace ZAFMC
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggedID();
            ViewAccountInfo();

        }

        //Current logged User
        protected void LoggedInUser_Load(object sender, EventArgs e)
        {
            LoggedID();
        }
        //Take current LoggedIn User from LogIn screen
        private void LoggedID()
        {
            string PV = (string)Session["IDPassportValue"];

            //Pass string value to Menu (LoggedIn) User
            LoggedUSer.Text = PV;
            UserLogged.Font.Bold = true;
            UserLogged.Text = string.Concat("(", PV, ")");

            //Pass string value to Locked-Modal Form Textbox
            LogInUser.Text = PV;
            LogInUser.Font.Bold = true;
            LogInUser.ForeColor = System.Drawing.Color.Blue;

        }

        //View AccountInfo(Method)
        private void ViewAccountInfo()
        {

            string ACC = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            using (SqlConnection AccView = new SqlConnection(ACC))
            {
                //Below Session takes ID-Passport Value from LogIn screen TextBox; the value is used to search in the Database
                string IDP = (string)Session["IDPassportValue"];
                PassportIdentity.Text = IDP;

                //SqlCommand below must Select using the Logged in ID
                SqlCommand Jacob = new SqlCommand(@"SELECT [LogInPassportID],[LogInFirstname],[LogInSurname],[LogInMobileNumber],[LogInEmailAddress],[LogInChurchPosition],[LogInDateTime] FROM [LogIn] WHERE [LogInPassportID] LIKE @SEARCH ", AccView);
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
                    TopMostDialogs.ShowTopMost(MG.Message, "ZAFMC- Failed to Retrieve Data (Account Info)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    Jacob.Dispose();
                    AccView.Close();

                }

            }
        }


        // Resets all Controls on the Web Form
        private void ResetControls(System.Web.UI.Control JP)
        {

            try
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
            catch (ArgumentNullException ANE)
            {
                ANE.Message.ToString();
            }


        }

        //LogIn back into the Application
        protected void ReturnBack(Object sender, EventArgs e)
        {
            AuthenticatePassword();
            
        }

        //Validate Password
        private bool AuthenticatePassword()
        {

            try
            {
                string PL = PasswordLock.Text;
                string PasswordRegex = @"^.*((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1})(?=.{6,10}).*$";

                bool IsPasswordValid = Regex.IsMatch(PL, PasswordRegex);

                if (IsPasswordValid == true)
                {
                    // DisplayAlert("Password ", "Complexity requirements are correct.", "Valid");
                }
                else
                if (!IsPasswordValid)
                {
                    CheckPasswordComplexity();

                    return false;
                }
            }
            catch (ArgumentNullException Arg)
            {
                Arg.Message.ToString();
            }
            return true;

        }
        //Check if password meet requirements
        private bool CheckPasswordComplexity()
        {
            Regex Digit = new Regex(@"[0-9]+");
            Regex MinMax = new Regex(@"[6,10]");
            Regex Lowercase = new Regex(@"[a-z]+");
            Regex Uppercase = new Regex(@"[A-Z]+");
            var SpecialChar = new Regex(@"[!@#$%^&*()\-_=+{};:,<.>]+");

            string Theo = PasswordLock.Text;


            //Password Field is Empty
            if (string.IsNullOrEmpty(Theo) || string.IsNullOrWhiteSpace(Theo))
            {
                TopMostDialogs.ShowTopMost("Password field is empty.", "Enter Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;  
                return false;

            }
            else
            //Password too Short
            if (Theo.Length < 6)
            {
                TopMostDialogs.ShowTopMost("Password is too short.", "Length", MessageBoxButtons.OK, MessageBoxIcon.Question);
                PasswordLock.Text = "";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Password too Long
            if (Theo.Length > 10)
            {
                TopMostDialogs.ShowTopMost("Password is too long.", "Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PasswordLock.Text = " ";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Password must have a Digit
            if (!Digit.IsMatch(Theo))
            {
                TopMostDialogs.ShowTopMost("Password must have a digit.", "Numbers", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                PasswordLock.Text = " ";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Password must be between 6 and 10 Characters

            if (!MinMax.IsMatch(Theo))
            {
                TopMostDialogs.ShowTopMost("Minimum of (6) & Maximum of (10) Characters.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PasswordLock.Text = " ";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Lowercase character required

            if (!Lowercase.IsMatch(Theo))
            {
                TopMostDialogs.ShowTopMost("Small letters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                PasswordLock.Text = " ";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Uppercase character required

            if (!Uppercase.IsMatch(Theo))
            {
                TopMostDialogs.ShowTopMost("Capital letters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Question);
                PasswordLock.Text = " ";    
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }
            else
            //Special characters required

            if (!SpecialChar.IsMatch(Theo))
            {
                TopMostDialogs.ShowTopMost("Special characters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PasswordLock.Text = " ";
                //Set cursor on Password Textbox
                PasswordLock.Focus();
                LogInBack.Checked = false;
                return false;
            }

            else
            {
                return true;
            }
        }

        //Close the Whole Application
        protected void ExitApplication(object sender, EventArgs e)
        {
            if (ExitAPP.Checked == true)
            {
                DialogResult Quit = TopMostDialogs.ShowTopMost("Do you want to close the Web APP", "ZAFMC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Quit == DialogResult.Yes)
                {
                    ExitAPP.Checked = false;
                    ClearBrowserHistory();
                    //Close the APP
                    Environment.Exit(0);
                }
                else
                if (Quit == DialogResult.No)
                {
                    PasswordLock.Text = " ";
                    ExitAPP.Checked = false;
                }

            }
        }
        //Clear Browser history
        protected void ClearBrowserHistory()
        {
            ////Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.CacheControl = "no-cache";
            Response.ExpiresAbsolute = DateTime.UtcNow.AddDays(-1d);
            Response.Expires = -1500; //Minutes
            Session.Contents.Clear();
            Session.Contents.RemoveAll();
            Session.Abandon();

            //Clear TextBox AUTOCOMPLETE --Because of CHROME Browser
            PasswordLock.Attributes.Add("AutoComplete", "Disabled");

        }

        //Close the whole Application / System
        protected void ExitZAFMC(object sender, EventArgs e)
        {
            DialogResult Shutdown = TopMostDialogs.ShowTopMost("Do you want to close the Web APP", "ZAFMC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Shutdown == DialogResult.Yes)
            {
                ClearBrowserHistory();
                //Close the APP
                Environment.Exit(0);
            }
            else
            if (Shutdown == DialogResult.No)
            {

                
            }
        }

    }
}