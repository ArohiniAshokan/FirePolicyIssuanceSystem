using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class TariffMasterListing : System.Web.UI.Page
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

                    objCodesMaster.CmType = "RISK_CLASS";

                    ddlFilterRiskClass.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlFilterRiskClass.DataValueField = "CODE";
                    ddlFilterRiskClass.DataTextField = "TEXT";
                    ddlFilterRiskClass.DataBind();

                    objCodesMaster.CmType = "OCCUPANCY";

                    ddlFilterRiskOccup.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlFilterRiskOccup.DataValueField = "CODE";
                    ddlFilterRiskOccup.DataTextField = "TEXT";
                    ddlFilterRiskOccup.DataBind();

                    BindData();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void BindData()
        {
            try
            {

                int riskClass = Convert.ToInt32(string.IsNullOrEmpty(ddlFilterRiskClass.SelectedValue) ? null : ddlFilterRiskClass.SelectedValue); ;
                int riskOccup = Convert.ToInt32(string.IsNullOrEmpty(ddlFilterRiskOccup.SelectedValue) ? null : ddlFilterRiskOccup.SelectedValue);
                double? si = Convert.ToDouble(string.IsNullOrEmpty(txtFilterSI.Text) ? null : txtFilterSI.Text);

                TariffMasterManager objTariffMasterManager = new TariffMasterManager();

                DataTable tariffData = objTariffMasterManager.TariffMasterGridBind(riskClass, riskOccup, si);

                GridView1.DataSource = tariffData;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void btnAddNewTariff_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Master/TariffMasterEntry.aspx?mode=I");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnTmlBack_Click(object sender, EventArgs e)
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string tmUid = GridView1.DataKeys[e.RowIndex].Values["TM_UID"].ToString();


                TariffMasterManager objTariffMasterManager = new TariffMasterManager();
                int isDeleted = objTariffMasterManager.DeleteFromTariffMaster(tmUid);

                if (isDeleted > 0)
                {
                    BindData();

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S3";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}')", true);
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnTariffUpdate_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    Button btnTariffUpdate = (Button)sender;

                    GridViewRow row = (GridViewRow)btnTariffUpdate.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string tmUid = args[0];


                    Response.Redirect($"~/Master/TariffMasterEntry.aspx?tmUid={tmUid}&&mode=U");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnApplyFilters_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnHistory_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowHistory")
                {
                    Button btnTariffHistory = (Button)sender;

                    GridViewRow row = (GridViewRow)btnTariffHistory.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string tmUid = args[0];
                    //string tmUid = txtTmUid.Text;


                    Response.Redirect($"~/Master/TariffMasterHistory.aspx?tmUid={tmUid}&&mode=U");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilterSI.Text = string.Empty;
            ddlFilterRiskClass.SelectedValue = string.Empty;
            ddlFilterRiskOccup.SelectedValue = string.Empty;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

    }
}