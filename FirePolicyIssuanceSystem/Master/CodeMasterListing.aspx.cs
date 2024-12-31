using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class CodeMasterListing : System.Web.UI.Page
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

                    CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                    ddlFilterCmType.DataSource = objCodesMasterManager.DropDownFillingCM();
                    ddlFilterCmType.DataValueField = "TYPE";
                    //ddlFilterCmType.DataTextField = "TYPE";
                    ddlFilterCmType.DataBind();

                    BindData();
                }
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
                Response.Redirect("~/Master/CodeMasterEntry.aspx?mode=I");
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

        private void BindData()
        {
            try
            {
                CodesMaster objCodesMaster = new CodesMaster();

                objCodesMaster.CmType = ddlFilterCmType.SelectedValue;

                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                DataTable adminView = objCodesMasterManager.CodesMasterGridBind(objCodesMaster);

                GridView1.DataSource = adminView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void UsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string cmCode = GridView1.DataKeys[e.RowIndex].Values["CM_CODE"].ToString();
                string cmType = GridView1.DataKeys[e.RowIndex].Values["CM_TYPE"].ToString();

                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                int isDeleted = objCodesMasterManager.DeleteFromCodeMaster(cmCode, cmType);

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


        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    Button btnUpdate = (Button)sender;

                    GridViewRow row = (GridViewRow)btnUpdate.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string cmCode = args[0];
                    string cmType = args[1];

                    Response.Redirect($"~/Master/CodeMasterEntry.aspx?code={cmCode}&&Type={cmType}&&mode=U");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            ddlFilterCmType.SelectedValue = string.Empty;
        }
    }
}