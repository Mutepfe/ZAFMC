using System;
using System.Configuration; //For DBase connection in the Web.Config
using System.Data;
using System.Data.SqlClient;

namespace ZAFMC
{
    // Lock screen (Site.Master, #UserLocked modal): unlocks only with the stored password of the user who
    // is logged in (Session["LoggedInUsername"]). No MessageBox / Session here, so the check can be tested.
    public static class LockScreen
    {
        // Consecutive wrong unlock passwords in one session before the user is logged out
        public const int MaxFailedAttempts = 5;

        // Session key of the consecutive wrong unlock passwords (reset by a correct unlock or a new login)
        public const string FailedAttemptsSessionKey = "UnlockFailedAttempts";

        public enum UnlockResult
        {
            Unlocked,       // stored password matched; the failed-attempt count is reset
            WrongPassword,  // stays locked; one more failed attempt
            NotLoggedIn,    // nobody is logged in: nothing to unlock (not counted)
            LockedOut,      // MaxFailedAttempts reached: log the user out
            Error           // the stored password could not be read (not counted)
        }

        // loggedInUsername: Session["LoggedInUsername"]. passesComplexity: result of the existing complexity
        // pre-check (a password that fails it is never compared, but counts as a wrong attempt).
        // storedPasswordLookup: returns the stored password of a username, or null when there is none.
        public static UnlockResult Check(string loggedInUsername, string typedPassword, bool passesComplexity,
            int failedAttempts, Func<string, string> storedPasswordLookup, out int failedAttemptsAfter)
        {
            failedAttemptsAfter = failedAttempts;
            if (string.IsNullOrWhiteSpace(loggedInUsername))
            {
                return UnlockResult.NotLoggedIn;
            }

            bool match = false;
            if (passesComplexity && !string.IsNullOrEmpty(typedPassword))
            {
                string stored;
                try
                {
                    stored = storedPasswordLookup(loggedInUsername);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceError("Lock screen: stored password not read: " + ex.GetType().Name);
                    return UnlockResult.Error;
                }
                match = stored != null && FixedTimeEquals(typedPassword, stored);
            }

            if (match)
            {
                failedAttemptsAfter = 0;
                return UnlockResult.Unlocked;
            }
            failedAttemptsAfter = failedAttempts + 1;
            return failedAttemptsAfter >= MaxFailedAttempts ? UnlockResult.LockedOut : UnlockResult.WrongPassword;
        }

        // Stored password of a registered user (dbo.LogIn.LogInPassword, stored as plain text), or null.
        // Exact match on the username (parameterised, no LIKE wildcards).
        public static string GetStoredPassword(string username)
        {
            string cs = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP (1) [LogInPassword] FROM [dbo].[LogIn] WHERE [LogInPassportID] = @Username", cn))
            {
                cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 20) { Value = username ?? string.Empty });
                cn.Open();
                object value = cmd.ExecuteScalar();
                return value == null || value == DBNull.Value ? null : (string)value;
            }
        }

        // Case-sensitive comparison whose time does not depend on where the strings differ
        public static bool FixedTimeEquals(string a, string b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            int diff = a.Length ^ b.Length;
            int length = Math.Max(a.Length, b.Length);
            for (int i = 0; i < length; i++)
            {
                char x = i < a.Length ? a[i] : '\0';
                char y = i < b.Length ? b[i] : '\0';
                diff |= x ^ y;
            }
            return diff == 0;
        }
    }
}
