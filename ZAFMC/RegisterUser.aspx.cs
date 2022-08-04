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
using System.Security.Cryptography;

namespace ZAFMC
{
    public partial class RegisterUser : System.Web.UI.Page
    {
        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            ResetControls(Page); //Resets Controls
        }

        //Delete(LogIn)  RegisteredUser (Event)
        protected void DeleteRegisterUser_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            RegisterUserDelete();
            ResetControls(Page); //Resets Controls

        }

        //Delete(LogIn)  RegisteredUser(Method)
        private void RegisterUserDelete()
        {
            //Using SQL Stored Procedure

            string RUDel = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisteredUser From Web.Config under ConnectionString Settings
            SqlConnection RU = new SqlConnection(RUDel);
            SqlCommand RegUser = new SqlCommand("DeleteRegisterUser", RU);

            try
            {
                RegUser.Parameters.AddWithValue("@DelRegUser", PassportID.Text); //@DelRegUser StoredProcedure actual parameter passed in the DB
                RegUser.CommandType = System.Data.CommandType.StoredProcedure;
                RU.Open();
                RegUser.ExecuteNonQuery();
                RegisterUserConnection(); // Refresh the Database

            }
            catch (Exception ZX)
            {
                throw new Exception(ZX.Message);
            }
            finally
            {
                RegUser.Dispose(); //Clean up memory
                RU.Close(); //Close DBase  connection
            }





        }


        //View(LogIn) RegisteredUser(Event)
        protected void ViewRegisteredUser_Click(object sender, EventArgs e)
        {

            RegisteredUserView();

        }
        //View (LogIn) RegisteredUser (Method)
        private void RegisteredUserView()
        {

            string MG = ConfigurationManager.ConnectionStrings["ZionRegisteredUser"].ConnectionString; // ZionRegisteredUser From Web.Config under ConnectionString Settings
            SqlConnection CH = new SqlConnection(MG);
            SqlCommand ZA = new SqlCommand(@"SELECT [LogInFirstname] as [Firstname],[LogInSurname] as [Surname],[LogInPassportID] as [LogIn Username],[LogInOTP] as [Generated OTP],[LogInMobileNumber] as [Mobile Number],[LogInEmailAddress] as [Email Address],[LogInChurchPosition] as [Church Position] FROM [dbo].[LogIn] WHERE [LogInPassportID] LIKE @FIND ", CH);

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
                MessageBox.Show(FORD.ToString(), "ZAFMC - View Users", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                ZA.Dispose(); //Clean up memory
                CH.Close(); //Close DBase  connection
            }
        }


        //Update(LogIn) RegisterUser(Event) 
        protected void EditRegisterUser_Click(object sender, EventArgs e)
        {

            DialogResult CH = MessageBox.Show("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (CH == DialogResult.Yes)
            {
                UpdateEditedRegisterUser(); // Update the Record
                RegisterUserConnection();  //Refresh the DBase
                ResetControls(Page); //Resets Controls
            }
            else
               if (CH == DialogResult.No)
            {

                RegisterUserConnection();
                ResetControls(Page); //Resets Controls
            }

        }

        //Update (LogIn) RegisterUser(Method) 
        private void UpdateEditedRegisterUser()
        {

            string MD = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            SqlConnection ZW = new SqlConnection(MD);
            SqlCommand Jethro = new SqlCommand("UpdateRegisterUser", ZW);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;

                Jethro.Parameters.AddWithValue("@LogInPassportID", PassportID.Text);
                Jethro.Parameters.AddWithValue("@LogInPassword", PassWD.Text);
                Jethro.Parameters.AddWithValue("@LogInID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@LogInFirstname", LogInFirstname.Text);
                Jethro.Parameters.AddWithValue("@LogInSurname", LogInSurname.Text);
                Jethro.Parameters.AddWithValue("@LogInMobileNumber", LogInMobile.Text);
                Jethro.Parameters.AddWithValue("@LogInEmailAddress", LogInEmail.Text);
                Jethro.Parameters.AddWithValue("@LogInChurchPosition", RankPosition.Text);

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

        //AddNew (LogIn) RegisterUser (Event)
        protected void SaveRegisterUser_Click(object sender, EventArgs e)
        {
            //ValidatePassword(); // Check Password Requirements
            RegisterUserAddNew(); // Add or Save Record
            RegisterUserConnection(); // Refresh Database
            ResetControls(Page); //Resets Controls


        }
        //AddNew (LogIn) RegisterUser (Method)
        private void RegisterUserAddNew()
        {

            string RegInsert = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            SqlConnection MR = new SqlConnection(RegInsert);
            SqlCommand GRA = new SqlCommand("AddNewRegisterUser", MR);

            try
            {

                GRA.CommandType = CommandType.StoredProcedure;
                GRA.Parameters.AddWithValue("@LogInPassportID", PassportID.Text);
                GRA.Parameters.AddWithValue("@LogInPassword", PassWD.Text);
                GRA.Parameters.AddWithValue("@LogInID", DBNull.Value);
                GRA.Parameters.AddWithValue("@LogInFirstname", LogInFirstname.Text);
                GRA.Parameters.AddWithValue("@LogInSurname", LogInSurname.Text);
                GRA.Parameters.AddWithValue("@LogInMobileNumber", LogInMobile.Text);
                GRA.Parameters.AddWithValue("@LogInEmailAddress", LogInEmail.Text);
                GRA.Parameters.AddWithValue("@LogInChurchPosition", RankPosition.Text);

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

        //Refresh (LogIn) RegisterUser(Method)
        public void RegisterUserConnection()
        {

            string CHConn = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            SqlConnection BM = new SqlConnection(CHConn);
            SqlCommand TQ = new SqlCommand(@"SELECT [LogInFirstname] as [Firstname],[LogInSurname] as [Surname],[LogInPassportID] as [LogIn Username],[LogInOTP] as [Generated OTP],[LogInMobileNumber] as [Mobile Number],[LogInEmailAddress] as [Email Address],[LogInChurchPosition] as [Church Position] FROM [dbo].[LogIn] ORDER BY [LogInFirstname] ASC ", BM);
            try
            {
                BM.Open();
                SqlDataReader MHO = TQ.ExecuteReader();
                //MediaList.DataSource = MHO;
                //MediaList.DataBind();
            }
            catch(Exception JM)
            {
                MessageBox.Show(JM.Message.ToString(), "ZAFMC Connection Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            finally
            {
                TQ.Dispose(); //Clean up memory
                BM.Close(); //Close DBase  connection
                ResetControls(Page); //Resets Controls
            }

        }

        protected void RefreshRegisterUser_Click(object sender, EventArgs e)
        {
            RegisterUserConnection();
            ResetControls(Page); //Resets Controls
        }

        //Reset (Event)
        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page); //Resets Controls
        }
        // Resets all Controls on the Web Form
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

        //public static bool ValidatePassword()
        //{
        //    string UserPassword= PassWD.Text;

        //    //Minimun of 5 Characters & Maximum of 12 Characters
        //    if (UserPassword.Length < 5 || UserPassword.Length > 10)
        //      {
        //        MessageBox.Show("Password must be a Minimum of (5) Characters \n\t\t & \n Maximum of (10) Characters..", "Password Characters", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        return false;
        //      }
        //    else
        //    // No White Space OR Empty Spaces
        //    if (UserPassword.Contains(" "))
        //      {
        //        MessageBox.Show("Space bar key is NOT allowed !!", "Password White Space", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        return false;
        //      }
        //    else
        //    // At Least One Capital Letter
        //    if (!UserPassword.Any(char.IsUpper))
        //    {
        //        MessageBox.Show("Your password must have a Capital Letter", "Password Capital Letter", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    else
        //    // At Least One LowerCase Letter
        //    if (!UserPassword.Any(char.IsLower))
        //    {
        //        MessageBox.Show("Password must have a LowerCase Letter", "Password LowerCase Letter", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        return false;
        //    }
           
        //    // No two Similar Characters
        //    for (int Jac = 0; Jac < UserPassword.Length - 1; Jac++)
        //    {
        //        if (UserPassword[Jac] = UserPassword[Jac + 1])
        //            MessageBox.Show("Password has similar Characters", "Password Character Similarity", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    // Must have One Special Character
        //    string UniqueCharacters= @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
        //    char[] UC = UniqueCharacters.ToCharArray();
        //    foreach (char B in UC)
        //    {
        //        if (UserPassword.Contains(B))
        //            return true;
        //    }
        //    MessageBox.Show("Password must have a Special Character", "Password Special Character", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //    return false;

        //}


    }
}