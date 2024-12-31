using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI.HtmlControls;


namespace FirePolicyIssuanceSystem.Transaction
{
    public partial class PolicyListing : System.Web.UI.Page
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
                    CodesMaster objCodesMaster = new CodesMaster();
                    CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                    objCodesMaster.CmType = "PRODUCT";

                    ddlFilterProduct.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlFilterProduct.DataValueField = "CODE";
                    ddlFilterProduct.DataTextField = "TEXT";
                    ddlFilterProduct.DataBind();

                    objCodesMaster.CmType = "APPR_STATUS";

                    ddlFilterStatus.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlFilterStatus.DataValueField = "CODE";
                    ddlFilterStatus.DataTextField = "TEXT";
                    ddlFilterStatus.DataBind();


                    BindData();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void BindData(string sortExpression = null, string sortDirection = "ASC")
        {
            try
            {

                FirePolicy objFirePolicy = new FirePolicy();

                objFirePolicy.PolNo = txtFilterPolNo.Text;
                objFirePolicy.PolProdCode = ddlFilterProduct.SelectedValue;
                objFirePolicy.PolIssDt = Convert.ToDateTime(string.IsNullOrEmpty(txtFilterIssueDate.Text) ? null : txtFilterIssueDate.Text);
                objFirePolicy.PolFmDt = Convert.ToDateTime(string.IsNullOrEmpty(txtFilterFromDate.Text) ? null : txtFilterFromDate.Text);
                objFirePolicy.PolToDt = Convert.ToDateTime(string.IsNullOrEmpty(txtFilterToDate.Text) ? null : txtFilterToDate.Text);
                objFirePolicy.PolApprStatus = ddlFilterStatus.SelectedValue;

                FirePolicyManager objFirePolicyManager = new FirePolicyManager();

                DataSet ds = objFirePolicyManager.FirePolicyFilterGridBind(objFirePolicy);

                DataTable PolicyFilterList = ds.Tables[0];

                if(!string.IsNullOrEmpty(sortExpression))
                {
                    DataView dv = PolicyFilterList.DefaultView;
                    dv.Sort = $"{sortExpression} {sortDirection}";
                    PolicyFilterList = dv.ToTable();
                }

                GridView1.DataSource = PolicyFilterList;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Transaction/PolicyEntry.aspx?PolUid=");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Dashboard.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    Button buttonPolicyUpdate = (Button)sender;

                    GridViewRow row = (GridViewRow)buttonPolicyUpdate.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string polUid = args[0];


                    Response.Redirect($"~/Transaction/PolicyEntry.aspx?PolUid={polUid}");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "POL_APPR_STATUS")?.ToString();

                // Find the icons by their runat="server" ID
                var checkIcon = e.Row.FindControl("checkIcon") as HtmlGenericControl;
                var exclamationIcon = e.Row.FindControl("exclamationIcon") as HtmlGenericControl;

                if (status == "Approved")
                {
                    checkIcon.Attributes["style"] = "color: green; display: inline;";
                    exclamationIcon.Attributes["style"] = "display: none;";
                }
                else if (status == "Pending")
                {
                    checkIcon.Attributes["style"] = "display: none;";
                    exclamationIcon.Attributes["style"] = "color: orange; display: inline;";
                }
            }
        }

        protected void btnApplyFilters_Click(object sender, EventArgs e)
        {
            BindData();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilterPolNo.Text = string.Empty;
            txtFilterIssueDate.Text = string.Empty;
            txtFilterFromDate.Text = string.Empty;
            txtFilterToDate.Text = string.Empty;

            ddlFilterProduct.SelectedValue = string.Empty;
            ddlFilterStatus.SelectedValue = string.Empty;

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = "ASC";

            if(ViewState["SortExpression"] != null && ViewState["SortExpression"].ToString() == sortExpression)
            {
                sortDirection = (ViewState["SortDirection"].ToString() == "ASC") ? "DESC" : "ASC";

            }

            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            BindData(sortExpression, sortDirection);
        }

       
    }
}