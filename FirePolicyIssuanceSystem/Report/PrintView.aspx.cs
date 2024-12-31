using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace FirePolicyIssuanceSystem.Report
{
    public partial class PrintView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FirePolicyManager objFirePolicyManager = new FirePolicyManager();

                    DataTable data = objFirePolicyManager.Listing_Details(Request.QueryString["PolUid"]);

                    ReportDocument reportDocument = new ReportDocument();
                    reportDocument.Load(Server.MapPath("~/Report/PolicyReport.rpt"));
                    reportDocument.SetDataSource(data);
                    ExportoPdf(reportDocument);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ExportoPdf(ReportDocument reportDocument)
        {
            try
            {
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                try
                {
                    reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "UserInformation" + "User");
                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.Message);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}