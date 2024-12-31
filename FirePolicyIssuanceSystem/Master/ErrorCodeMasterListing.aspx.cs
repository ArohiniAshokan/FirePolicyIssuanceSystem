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
    public partial class ErrorCodeMasterListing : System.Web.UI.Page
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

                    objCodesMaster.CmType = "ERR_TYPE";

                    ddlErrType.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlErrType.DataValueField = "CODE";
                    ddlErrType.DataTextField = "TEXT";
                    ddlErrType.DataBind();

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
                ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                objErrorCodeMaster.ErrType = ddlErrType.SelectedValue;

                ErrorCodeMasterManager objErrorCodesMasterManager = new ErrorCodeMasterManager();

                DataTable adminView = objErrorCodesMasterManager.CodesMasterGridBind(objErrorCodeMaster);

                GridView1.DataSource = adminView;
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
                Response.Redirect("~/Master/ErrorCodeMasterEntry.aspx?mode=I");
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
                    Button btnUpdate = (Button)sender;

                    GridViewRow row = (GridViewRow)btnUpdate.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string errCode = args[0];


                    Response.Redirect($"~/Master/ErrorCodeMasterEntry.aspx?code={errCode}&&mode=U");
                }
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string errCode = GridView1.DataKeys[e.RowIndex].Values["ERR_CODE"].ToString();


                ErrorCodeMasterManager objErrorCodesMasterManager = new ErrorCodeMasterManager();
                int isDeleted = objErrorCodesMasterManager.DeleteFromCodeMaster(errCode);

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

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            ddlErrType.SelectedValue = string.Empty;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}