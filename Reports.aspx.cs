using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZAFMC
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            //Report links: Web.config appSettings ReportLinkMode = PowerBIDesktop (default) or SSRS
            string mode = (ConfigurationManager.AppSettings["ReportLinkMode"] ?? string.Empty).Trim();
            useSsrs = string.Equals(mode, "SSRS", StringComparison.OrdinalIgnoreCase);

            //SSRS web portal base URL comes from Web.config (appSettings: ReportServerBaseUrl)
            string baseUrl = DefaultReportServerBaseUrl;
            string configured = (ConfigurationManager.AppSettings["ReportServerBaseUrl"] ?? string.Empty).Trim();
            Uri configuredUrl;
            if (Uri.TryCreate(configured, UriKind.Absolute, out configuredUrl)
                && (configuredUrl.Scheme == Uri.UriSchemeHttp || configuredUrl.Scheme == Uri.UriSchemeHttps))
            {
                baseUrl = configured;
            }
            reportFolderUrl = baseUrl.TrimEnd('/') + "/ZAFMC/ZAFMCReports/";

            //Each SSRS report name is also the name of its page in PowerBI\ZAFMCReports.pbip
            SetReportLink(CongReport, "Congregation");
            SetReportLink(CHRCReport, "Churches");
            SetReportLink(TestmReport, "MediaTestimony");
            SetReportLink(SMNReport, "MediaSermon");
            SetReportLink(EVTReport, "Events");
            SetReportLink(PassReports, "Passovers");
            SetReportLink(MemReport, "PassMemorial");
            SetReportLink(BigSundayReport, "PassBigSunday");
            SetReportLink(WeddReport, "PassWedding");
            SetReportLink(GradReport, "PassGraduation");
            SetReportLink(WOMReport, "PassWomen");
            SetReportLink(GMReports, "PassGeneralMeeting");
            SetReportLink(PROJREport, "Projects");
            SetReportLink(LEADReport, "Leadership");
            SetReportLink(ADMReport, "Administration");
            SetReportLink(DCDReport, "Deceased");

        }

        //Used when ReportServerBaseUrl is missing or is not an absolute http(s) URL
        private const string DefaultReportServerBaseUrl = "http://JACOB:80/ZionReports";

        //Custom URL protocol registered on this PC by PowerBI\Register-ZafmcReportLinks.ps1
        private const string PowerBIDesktopScheme = "zafmc-pbi:";

        private string reportFolderUrl;
        private bool useSsrs;

        private void SetReportLink(HyperLink link, string reportName)
        {
            if (useSsrs)
            {
                link.NavigateUrl = reportFolderUrl + reportName;
                return;
            }

            //Opens the Power BI Desktop page locally; a custom protocol must not open a blank tab
            link.NavigateUrl = PowerBIDesktopScheme + reportName;
            link.Target = string.Empty;
        }
        protected void CongregationView_Click()
        {
           
        }

    }


}