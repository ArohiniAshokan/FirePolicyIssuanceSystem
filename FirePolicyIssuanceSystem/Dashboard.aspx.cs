using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FirePolicyIssuanceSystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (!IsPostBack)
                {
                    FirePolicyManager objFirePolicyManager = new FirePolicyManager();

                    lblTotalPolicies.Text = objFirePolicyManager.TotalPolicyCount();

                    DataTable dt = objFirePolicyManager.FetchDashboardData();

                    DataRow row = dt.Rows[0];

                    lblTotalPoliciesApproved.Text = row["POLICY_COUNT"].ToString();
                    lblTotalSumInsured.Text = Convert.ToDecimal(row["SI"]).ToString("#,##0.00");
                    lblTotalPremium.Text = Convert.ToDecimal(row["Premium"]).ToString("#,##0.00");

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}