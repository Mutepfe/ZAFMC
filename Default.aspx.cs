using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ZAFMC
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ClearBrowserHistory();

            /* **-----------------------------**-------------------------------
             Do Not RESET Controls in Page Load!!! It affects AutoPostback
           --------------------------**-------------------------------------** */
           
          
        }
        //Display todays date & time
        protected void CurDateTime_Load(object sender, EventArgs e)
        {
            CurDateTime.Text = DateTime.Now.ToString("dd-MMMM-yy ~ HH:mm tt");
        }
        //Register a New System User
        protected void RegUser_CheckedChanged(object sender, EventArgs e)
        {
            if (RegUser.Checked == true)
            {
                RegUser.Checked = false;
                Exit.Checked = false;
                Response.Redirect("~/RegisterUser.aspx");

            }

        }

        //Enable or Disable Site.Master NAVBAR after SUCCESSFUL LogIn
       public void EnableDefaultMenus()
        {
            //Congregation
            System.Web.UI.HtmlControls.HtmlAnchor CONG = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Congregation");
            CONG.Visible = true;
            //Media
            System.Web.UI.HtmlControls.HtmlAnchor MED = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Media");
            MED.Visible = true;
            //Events
            System.Web.UI.HtmlControls.HtmlAnchor EVE = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Events");
            EVE.Visible = true;
            //Passovers
            System.Web.UI.HtmlControls.HtmlAnchor PAS = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Passovers");
            PAS.Visible = true;
            //Projects
            System.Web.UI.HtmlControls.HtmlAnchor PRO = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Projects");
            PRO.Visible = true;
            //Finance
            System.Web.UI.HtmlControls.HtmlAnchor FIN = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Finance");
            FIN.Visible = true;
            //Reports
            System.Web.UI.HtmlControls.HtmlAnchor REP = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Reports");
            REP.Visible = true;
            //Contact
            System.Web.UI.HtmlControls.HtmlAnchor CONT = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("Contact");
            CONT.Visible = true;
            //LoggedInUSer Details
            System.Web.UI.WebControls.Label LU = (System.Web.UI.WebControls.Label)Master.FindControl("LoggedUSer");
            LU.Visible = true;

           


        }


        //Log into the system 
        protected void LogIn_CheckedChanged(object sender, EventArgs e)
        {
            if (LogIn.Checked == true)
            {
                RegUser.Checked = false;
                Exit.Checked = false;
                              
                // Login into the APP
                LogOn();
                             
               
            }
        }
        //Login method
        private void LogOn()
        {
            var LIV = LogInValidation();
            var OTV = OTPValidation();
            var ATP = AuthenticatePassword();
            var APD = AuthenticatePassportID();
            

            var PD = Passwd.Text;

            if (string.IsNullOrEmpty(PD) || string.IsNullOrWhiteSpace(PD))
            {
                CheckPasswordComplexity();
                return;
            }

            else
            //Either ZimID or Passport Number is wrong
            if (!APD)
            {
                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                PassID.Focus();
                return;
            }
            else
            //Password is wrong
               if (!ATP)
            {
                Passwd.Text = "";
                OTPList.SelectedIndex = -1;
                OTPNumber.Text = "";
                OTPToken.Text = "";

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                Passwd.Focus();
                return;
            }

            else
            //OTP is missing
                if ((!LIV && !OTV) || (!LIV && OTV) || (LIV && !OTV))
            {
                OTPList.SelectedIndex = -1;
                OTPNumber.Text = "";

                //Clear LOGIN Checkbox
                LogIn.Checked = false;
                return;
            }
            else
            //Nothing is missing, all fields are correct
            if (LIV && OTV && APD)
            {
                //Enable all the Menus and login
                EnableDefaultMenus();
                //Reset Controls First
                ResetControls(Page);
                //Clear LOGIN Checkbox
                LogIn.Checked = false;
            }

        }
        //Clear FORM HISTORY or Typed text on Exit
        protected void ClearBrowserHistory()
        {
                                                                    ////Clear PageCache
                                                                    //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                                                                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                                                    //Response.Cache.SetNoStore();
                                                                    //Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                                                                    //Response.CacheControl = "no-cache";
                                                                    //Response.ExpiresAbsolute = DateTime.UtcNow.AddDays(-1d);
                                                                    //Response.Expires = -1500; //Minutes
                                                                    //Session.Contents.Clear();
                                                                    //Session.Contents.RemoveAll();
                                                                    //Session.Abandon();

            ////Clear TextBox AUTOCOMPLETE --Because of CHROME Browser
            PassID.Attributes.Add("AutoComplete", "Disabled");
            OTPNumber.Attributes.Add("AutoComplete", "Disabled");
            
        }
        //Close the Whole Application
        protected void Exit_CheckedChanged(object sender, EventArgs e)
        {
            if (Exit.Checked == true)
            {
                DialogResult Quit = MessageBox.Show("Do you want to close the Web APP", "ZAFMC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Quit == DialogResult.Yes)
                {
                    ClearBrowserHistory();

                    //Reset Controls First
                    ResetControls(Page);

                    //Close the APP
                    Environment.Exit(0);
                }
                else
                    if (Quit == DialogResult.No)
                {
                    //Reset Controls First
                    ResetControls(Page);

                    Exit.Checked = false;
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

        //Validate PASSWORD before AUTHENTICATING with the database
        private bool AuthenticatePassword()
        {

            try
            {
                string PD = Passwd.Text;
                string PasswordRegex = @"^.*((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1})(?=.{6,10}).*$";

                bool IsPasswordValid = Regex.IsMatch(PD, PasswordRegex);

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

            string Theo = Passwd.Text;


            //Password Field is Empty
            if (string.IsNullOrEmpty(Theo) || string.IsNullOrWhiteSpace(Theo))
            {
                MessageBox.Show("Password field is empty.", "Enter Password", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;

            }
            else
            //Password too Short
            if (Theo.Length < 6)
            {
                MessageBox.Show("Password is too short.", "Length", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Password too Long
            if (Theo.Length > 10)
            {
                MessageBox.Show("Password is too long.", "Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Password must have a Digit
            if (!Digit.IsMatch(Theo))
            {
                MessageBox.Show("Password must have a digit.", "Numbers", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Password must be between 6 and 10 Characters

            if (!MinMax.IsMatch(Theo))
            {
                MessageBox.Show("Minimum of (6) & Maximum of (10) Characters.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Lowercase character required

            if (!Lowercase.IsMatch(Theo))
            {
                MessageBox.Show("Small letters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Uppercase character required

            if (!Uppercase.IsMatch(Theo))
            {
                MessageBox.Show("Capital letters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Question);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }
            else
            //Special characters required

            if (!SpecialChar.IsMatch(Theo))
            {
                MessageBox.Show("Special characters are required.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Clear LOGIN Checkbox
                LogIn.Checked = false;

                //Set cursor on Password Textbox
                Passwd.Focus();
                return false;
            }

            else
            {
                return true;
            }
        }

        //Validation-username
        private void UsernameValidation()
        {
            string IDP = PassID.Text;
            try
            {
                if (!(IDP == "ID Number..") || (!(IDP == string.Empty)))
                {
                    //  Response.Redirect("~/RegisterUser.aspx");
                    ResetControls(Page);
                }
            }
            catch (ArgumentNullException ANE)
            {
                ANE.Message.ToString();
            }

        }

        //Validate all fields before login
        private bool LogInValidation()
        {

            var Digits = new Regex(pattern: @"([0-9]){5}");
            //Blank OTP
            if (string.IsNullOrEmpty(OTPNumber.Text) || string.IsNullOrWhiteSpace(OTPNumber.Text))
            {
                MessageBox.Show("OTP is required.", "OTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OTPList.SelectedIndex = -1;
                LogIn.Checked.Equals(false);
                OTPNumber.Focus();
                return false;
            }
            else

            //OTP Dropdown is not selected      
            if ((OTPList.SelectedIndex == -1) && Digits.IsMatch(OTPNumber.Text))
            {
                MessageBox.Show("How did you get your OTP?", "OTP Query", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                OTPNumber.Text = "";
                LogIn.Checked = false;
                return false;

            }
            else

            //OTP is less than 5 digits
            if (OTPNumber.Text.Length < 5)
            {
                MessageBox.Show("OTP must be 5 digits long.", "OTP Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OTPNumber.Text = "";
                LogIn.Checked = false;
                OTPNumber.Focus();
                return false;
            }
            else
            //Wrong OTP typed
            if (OTPNumber.Text != OTPToken.Text.ToString())
            {
                MessageBox.Show("Invalid OTP.", "OTP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                OTPNumber.Focus();
                LogIn.Checked = false;
                return false;
            }
            else
            //Typed SMS OTP is correct
            if (OTPNumber.Text == OTPToken.Text)
            {
                
                CalculateExpiryTime();
                return true;
            }
            return LogInValidation();
        }

        private static System.Timers.Timer CountTimer;
        //Countdown for OTP Expiry
        public void CountDownTimer()
        {
            //SMS option is Selected
            if (Convert.ToString(OTPList.SelectedItem) == "SMS")
            {
                string CellNumber = "+263772764465";//For now its Hard coded, this value must come from the Database
                var Pattern = new Regex(@"(?<=[\d]{4})[\d-\._\+%]*(?=[\d]{2})");
                string MJ = Regex.Replace(CellNumber, Convert.ToString(Pattern), m => new string('*', m.Length));

                CountTimer = new System.Timers.Timer
                {
                    Interval = 60000 //That is after 3 Minutes (60000 Milliseconds)
                };
                CountTimer.Start();
                CountTimer.Elapsed += CountDownTimerElapsed; //Elapsed Timer Event
                TimerCount.Text = RetrieveCountDownTimer(); // Simulate Time count towards expiry

                MessageBox.Show("Your OTP was SMSed to\n\n" + "(" + MJ + "). " + "And expires today\n\n" + TimerCount.Text + "\n\nValid for 3 minutes only!!", "OTP Validity ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
                //EMAIL option is Selected
                if (Convert.ToString(OTPList.SelectedItem) == "Email")
            {

                string EmailAddress = "bishop@zafmc.co.zw";//For now its Hard coded, this value must come from the Database
                var Pattern = new Regex(@"(?<=[\w]{2})[\w-\._\+%]*(?=[\w]{0}@*(?<=[\w]{4}))");
                string JJ = Regex.Replace(EmailAddress, (Convert.ToString(Pattern)), m => new string('*', m.Length));

                CountTimer = new System.Timers.Timer
                {
                    Interval = 60000 //That is after 3 Minutes (60000 Milliseconds)
                };
                CountTimer.Start();
                CountTimer.Elapsed += CountDownTimerElapsed; //Elapsed Timer Event
                TimerCount.Text = RetrieveCountDownTimer(); // Simulate Time count towards expiry

                MessageBox.Show("Your OTP was emailed to\n\n" + "(" + JJ + ")" + " .And expires today\n\n" + TimerCount.Text + "\n\nValid for 3 minutes only!!", "OTP Validity ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
        }

        #region
        //After 3 minutes OTP Expires
        public static readonly System.Timers.Timer Jakobe = new System.Timers.Timer();
        public void CalculateExpiryTime()
        {

            const long ThreeMinutes = 180000;

            Jakobe.Interval = ThreeMinutes; //Milliseconds
            Jakobe.Elapsed += Jakobe_Elapsed;
            Jakobe.Enabled = true;
            
        }
        //Three Minutes event handler for OTP Expiry
        public void Jakobe_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Jakobe.Enabled=false;
            Jakobe.Stop();
          
            //Message Box about OTP Expiry
            MessageBox.Show("Your OTP has Timed-Out,\n\nGenerate a new OTP.", "ReNew OTP", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Reset Controls
            ResetControls(Page);

        }
        #endregion

        #region
        //Timer event ---- DONT DELETE THIS CODE
        public void CountDownTimerElapsed(object sender, EventArgs e)
        {
            if (sender == CountTimer)
            {
                TimerCount.Text = RetrieveCountDownTimer();

            }
        }
        //Timer Computation ---- DONT DELETE THIS CODE
        private string RetrieveCountDownTimer()
        {
            string TimeFormatted = "";
            int Hr = DateTime.Now.Hour;
            int Min = DateTime.Now.Minute;
            int Sec = DateTime.Now.Second;

            TimeFormatted = (Hr < 0) ? "0" + Hr.ToString() : Hr.ToString();
            TimeFormatted += ":" + ((Min < 1) ? "0" + Min.ToString() : Min.ToString());
            TimeFormatted += ":" + ((Sec < 59) ? "0" + Sec.ToString() : Sec.ToString());

            var WeekDay = DateTime.Now.DayOfWeek.ToString();//Return Day of the week
            DateTime ExpiredTime = DateTime.Now;

            TimeFormatted = string.Concat(WeekDay, ", ", ExpiredTime.AddMinutes(3).ToString(format: "dd-MMM-yyyy @ HH:mm:ss tt"));

            return TimeFormatted;

        }
        #endregion
        //Wrong OTP(SMS)
        private void WrongOTPSMS()
        {
            Regex Symbols = new Regex(@"[a-zA-Z-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]");
            var Digits = new Regex(pattern: @"([0-9]){5}");

            try
            {

                //OTP less than 5 Digits
                if (OTPNumber.Text.Length < 5)
                {
                    MessageBox.Show("OTP must be 5 digits long.", "OTP Length", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    OTPList.SelectedIndex = -1;
                }
                else
                //Special characters NOT allowed 
                if (Symbols.IsMatch(OTPNumber.Text))
                {
                    MessageBox.Show("Alphabetical letters are not allowed.", "OTP Letters", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    OTPList.SelectedIndex = -1;
                    OTPNumber.Text = "";
                }
                else
                //Wrong OTP typed
                if (OTPNumber.Text != OTPToken.Text.ToString())
                {
                    MessageBox.Show("Invalid OTP.", "OTP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    OTPList.SelectedIndex = -1;
                    OTPNumber.Text = "";

                }
                else
                //OTP Dropdown is not selected      
                if ((OTPList.SelectedIndex == -1) || Digits.IsMatch(OTPNumber.Text))
                {
                    MessageBox.Show("How did you get your OTP?", "OTP Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OTP.Text = "";
                }
            }
            catch (Exception XP)
            {
                XP.Message.ToString();
            }

        }

        //Wrong OTP (Email)
        private void WrongOTPEmail()
        {
            Regex Symbols = new Regex(@"[a-zA-Z][-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]");
            try
            {
                //OTP less than 5 Digits
                if (OTPNumber.Text.Length < 5)
                {
                    MessageBox.Show("OTP must be 5 digits long.", "OTP Length", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    OTPList.SelectedIndex = -1;
                }
                else
                    //Special characters NOT allowed 
                    if (Symbols.IsMatch(OTPNumber.Text))
                {
                    MessageBox.Show("Alphabetical letters are not allowed.", "OTP Letters", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    OTPList.SelectedIndex = -1;
                    OTP.Text = "";
                }
                else
                   //Wrong OTP typed
                   if (OTPNumber.Text != OTPToken.Text.ToString())
                {
                    MessageBox.Show("Invalid OTP.", "OTP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    OTPList.SelectedIndex = -1;
                    OTP.Text = "";
                }
            }
            catch (Exception EC)
            {
                EC.Message.ToString();
            }
        }

        //Verify OTP Input
        private bool OTPValidation()
        {
            try
            {
                ////switch (OTPList.SelectedIndex) OR switch (OTPList.SelectedItem.Value)

                switch (OTPList.SelectedItem.Text)
                {
                    //No option is selected
                    case "Select":
                        try
                        {
                            MessageBox.Show("How did you receive your OTP?\n\nSelect OTP Dropdown.", "Email / SMS", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            LogIn.Checked = false;
                            break;

                        }
                        catch (StackOverflowException SFE)
                        {
                            SFE.Message.ToString();
                        }
                        break;
                    //SMS Option
                    case "SMS":
                        try
                        {
                            //Typed SMS OTP is correct
                            if (OTPNumber.Text == OTPToken.Text)
                            {
                                CalculateExpiryTime();
                                return true;
                            }
                            else
                                //Typed SMS OTP is wrong
                                if (OTPNumber.Text != OTPToken.Text)
                            {
                                WrongOTPSMS();
                                OTPNumber.Text = "";
                                return false;
                            }

                        }
                        catch (NullReferenceException NRE)
                        {
                            NRE.Message.ToString();
                        }
                        catch (ArgumentNullException ANE)
                        {
                            ANE.Message.ToString();
                        }
                        catch (StackOverflowException SFE)
                        {
                            SFE.Message.ToString();
                        }
                        break;

                    //Email Option          
                    case "Email":
                        try
                        {


                            //Typed Email OTP is correct
                            if (OTPNumber.Text == OTPToken.Text.ToString())
                            {
                                CalculateExpiryTime();
                                return true;
                            }
                            else
                                //Typed Email OTP is wrong
                                if (OTPNumber.Text != OTPToken.Text.ToString())
                            {
                                WrongOTPEmail();
                                return false;
                            }
                        }
                        catch (NullReferenceException NRE)
                        {
                            NRE.Message.ToString();
                        }
                        catch (ArgumentNullException ANE)
                        {
                            ANE.Message.ToString();
                        }
                        break;


                }
            }
            catch (StackOverflowException SFE)
            {
                SFE.Message.ToString();
            }

            LogIn.Checked = false;
            return false;

            //-//-//return OTPValidation();


        }

        //Generate 5 Digit OTP
        public int CreateOTP()
        {
            int MinOTP = 10000;
            int MaxOTP = 99999;

            Random GenerateOTP = new Random();

            return GenerateOTP.Next(MinOTP, MaxOTP);
        }
               
        //Email and SMS Selection
        protected void EmailedSMSed(object sender, EventArgs e)
        {
            // Let the TIMER for OTP Expiry start running, after 3 Miuntes the OTP will be EXPIRED. The code for "...CalculateExpiryTimer()... Function" will be triggered.
            Jakobe.Start();

            // Either Send an SMS OR Email..."Trigger/Call SMSandEmail() Function"
            SMSandEMAIL();

            //Check if OTP has expired OR not?
            CalculateExpiryTime();
        }
        //SMS & Email method
        protected void SMSandEMAIL()
        {
            string Zion = "ZAFMC-";
            if (Page.IsPostBack)//In GUI AUTOPOSTBACK must be 'True'
            {

                //SMS is Selected
                if (OTPList.SelectedValue == "SMS")
                {

                    var SMSOTP = string.Concat(Zion, CreateOTP().ToString());
                    MessageBox.Show("Your OTP Number is \n\n" + SMSOTP + " ", "SMSed OTP", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    OTPNumber.Focus();

                    //Below ignores the "ZAFMC" text on the OTP
                    OTPToken.Text = SMSOTP.Substring(Convert.ToChar(6));
                    CountDownTimer();
                    return;

                }
                else
                    //Email is selected
                    if (OTPList.SelectedValue == "Email")
                {

                    //var EmailOTP = string.Concat(Zion, CreateOTP().ToString());

                    var EmailOTP = string.Concat(Zion, EmailAlphaNumeric().ToString());
                    MessageBox.Show("Your OTP Passcode is \n\n" + EmailOTP + " ", "OTP Emailed", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    OTPNumber.Focus();
                    //Below ignores the "ZAFMC-" text on the OTP
                    OTPToken.Text = EmailOTP.Substring(Convert.ToChar(6));
                    CountDownTimer();
                    return;
                }
            }
        }
        #region
        //Generate ALPHANUMERIC OTP Passcode - Only for the Email        
        public static string EmailAlphaNumeric()
        {
            var AlphNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%";
            var OTPChar = new char[5];
            var RandChars = new Random();

            for ( int JP =0; JP < OTPChar.Length; JP++ )
            {
                OTPChar[JP] = AlphNum[RandChars.Next(AlphNum.Length)];

            }
            var GenEmailOTP = new string(OTPChar);
            return GenEmailOTP;
            
        }

        #endregion

        // Passsword Reset
        private void PasswordReset()
        {

            var JP = PassID.Text;
            //Security Check

            string Security = MessageBox.Show(Interaction.InputBox("Security Check (Question)", "What's your LogIn Username?", "Identity...")).ToString();
            try
            {


                //Either IDPassport Textbox is empty ... OR ... RESULT is empty
                if (string.IsNullOrEmpty(JP) || string.IsNullOrEmpty(Security) || string.IsNullOrWhiteSpace(JP) || string.IsNullOrWhiteSpace(Security))
                {

                    MessageBox.Show("Enter LogIn Username First.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PassID.Focus();
                    return;
                }
                else
                //Security & IDPassport Textbox texts match
                if (Security.Equals(JP, StringComparison.CurrentCultureIgnoreCase))
                {
                    UsernameValidation();
                }
                else
                //Security & IDPassport Textbox texts don't match
                if (!Security.Equals(JP, StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show("ID/Passport entered dosen't\nmatch with the system...", "Identity Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResetControls(Page);
                    PassID.Focus();
                }

            }
            catch (NullReferenceException NR)
            {
                NR.Message.ToString();
            }
            catch (ArgumentNullException AE)
            {
                AE.Message.ToString();
            }
            catch (FormatException FE)
            {
                FE.Message.ToString();
            }


            finally
            {
                ResetControls(Page);
            }

        }

        public void ForgotPassword(object sender, EventArgs e)
        {
            PasswordReset();
        }

        //Validate LogIn-Username before AUTHENTICATING with the database
        private bool AuthenticatePassportID()
        {
            try
            {
                string PID = PassID.Text;

                //Regex for Zimbabwean Passport Number (E.g JP450035)
                string PassportRegex = @"^(((?=.*[^a-z!@#$% ^&*_+():\ <>,.?|]{0})(?=.*[A-Z]{2,2})(?=.*[0-9]{6,6}))[A-Z0-9]{8,8}$)";
                //Regex for Zimbabwean ID-Number (e.g 99447589R75)
                string ZimIDRegex = @"^((((?=.*[^a-z!@#$% ^&*_+(): <>,.?\|]{0})(?=.*[0-9]{8,8})(?=.*[A-Z]){1,1}(?=.*[0-9]{2,2})))[0-9A-Z]{11,11}$)";

                //Validate Passport Characters
                bool PassportNumber = Regex.IsMatch(PID, PassportRegex);
                //Validate ID Characters
                bool ZimID = Regex.IsMatch(PID, ZimIDRegex);

                //Substring for Passport Number
                string PassNum = Convert.ToString(PID).Substring(0, 2);
                //Substring for Zimbabwean Identity
                string Chitupa = Convert.ToString(PID).Substring(0, 8);

                //If FIRST 2 Characters are LETTERS, then its a Zimbawean Passport Number
                string PN = @"^((?=.[A-Z]){2,2}((?=.*[^a-z0-9!@#$% &*_'():<>,.?\|]){0,0}))";
                //If FIRST 8 Characters are NUMBERS, then its a Zimbabwean ID
                string ZID = @"^((?=.[0-9]{8,8})((?=.*[^a-zA-Z!@#$% &*_'():<>,.?\|]){0,0}))";


                bool ZimPN = Regex.IsMatch(PassNum, PN);
                bool ZimIDN = Regex.IsMatch(Chitupa, ZID);


                //Correct Passport Number
                if (PassportNumber)
                {
                    if (PassportNumber && PID.Contains(PassNum))
                    {
                        MessageBox.Show("Valid Zimbabwean Passport!", "Passport Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                //Wrong Passport Number
                if (!PassportNumber)
                {


                    if ((!PassportNumber) && PID.Contains(PassNum) && (PID.Length < 8) && (PID.Length > 8))
                    {
                        MessageBox.Show("Invalid Zimbabwean Passport!", "Passport Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        PassID.Text = "";
                        PassID.Focus();
                        return false;
                    }
                }

                //Correct Zimbabwean ID
                if (ZimID)
                {
                    if (ZimID && PID.Contains(Chitupa))
                    {
                        MessageBox.Show("Valid Zimbabwean Identity!", "Zimbawean ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                //Wrong Zimbabwean ID
                if (!ZimID)
                {
                    if ((!ZimID) && PID.Contains(Chitupa) && (PID.Length < 11) && (PID.Length > 11))
                    {
                        MessageBox.Show("Invalid Zimbabwean Identity!", "Zimbawean ID", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        PassID.Text = "";
                        PassID.Focus();
                        return false;
                    }
                }

            }
            catch (ArgumentNullException IDP)
            {
                IDP.Message.ToString();

            }
            catch (ArgumentOutOfRangeException AORE)
            {
                AORE.Message.ToString();

            }
            return true;
        }

    }
}