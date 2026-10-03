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

            //Report links: SSRS web portal base URL comes from Web.config (appSettings: ReportServerBaseUrl)
            string baseUrl = DefaultReportServerBaseUrl;
            string configured = (ConfigurationManager.AppSettings["ReportServerBaseUrl"] ?? string.Empty).Trim();
            Uri configuredUrl;
            if (Uri.TryCreate(configured, UriKind.Absolute, out configuredUrl)
                && (configuredUrl.Scheme == Uri.UriSchemeHttp || configuredUrl.Scheme == Uri.UriSchemeHttps))
            {
                baseUrl = configured;
            }
            reportFolderUrl = baseUrl.TrimEnd('/') + "/report/ZAFMC/ZAFMCReports/";

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
        private const string DefaultReportServerBaseUrl = "http://JACOB/Reports";

        private string reportFolderUrl;

        private void SetReportLink(HyperLink link, string reportName)
        {
            link.NavigateUrl = reportFolderUrl + reportName;
        }
        protected void CongregationView_Click()
        {
           
        }

    }


}