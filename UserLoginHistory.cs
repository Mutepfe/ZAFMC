using System;
using System.Configuration; //For DBase connection in the Web.Config
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;

namespace ZAFMC
{
    // Writes one row to dbo.UserLoginHistory per successful login (shown in "User LogIn History"
    // on the User Account Info screen).
    public static class UserLoginHistory
    {
        // A web server cannot read the browser's MAC address. This is the MAC of the machine running
        // the application (the user's PC when the app and the browser run on the same machine):
        // the first operational, non-loopback, non-tunnel, non-virtual network adapter, as AA-BB-CC-DD-EE-FF.
        public static string ServerMacAddress()
        {
            NetworkInterface nic = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up
                    && n.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && n.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                    && !IsVirtual(n)
                    && n.GetPhysicalAddress().GetAddressBytes().Length == 6)
                .OrderBy(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet || n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ? 0 : 1)
                .FirstOrDefault();
            return nic == null ? null : FormatMac(nic.GetPhysicalAddress().GetAddressBytes());
        }

        public static string FormatMac(byte[] address)
        {
            return string.Join("-", address.Select(b => b.ToString("X2")));
        }

        private static bool IsVirtual(NetworkInterface nic)
        {
            string text = (nic.Description + " " + nic.Name).ToLowerInvariant();
            string[] markers = { "virtual", "hyper-v", "vmware", "virtualbox", "vethernet", "loopback", "pseudo", "tap-", "wan miniport", "bluetooth", "docker", "wsl" };
            return markers.Any(m => text.Contains(m));
        }

        // Records a successful login. Firstname / Surname are looked up from the user record (dbo.LogIn),
        // the date and time is the server's. Never throws: a failure is traced and the login continues.
        public static void Record(string username, string clientIp)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ZionRegisterUser"].ConnectionString; // ZionRegisterUser From Web.Config under ConnectionString Settings
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand("AddUserLoginHistory", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 20) { Value = username ?? string.Empty });
                    cmd.Parameters.Add(new SqlParameter("@MacAddress", SqlDbType.NVarChar, 40) { Value = (object)ServerMacAddress() ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ClientIP", SqlDbType.NVarChar, 45) { Value = (object)clientIp ?? DBNull.Value });
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Login history not recorded: " + ex);
            }
        }
    }
}
