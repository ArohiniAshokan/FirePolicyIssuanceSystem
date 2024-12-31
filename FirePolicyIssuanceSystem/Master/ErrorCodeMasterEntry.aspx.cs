using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class ErrorCodeMasterEntry : System.Web.UI.Page
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

                    if (Request.QueryString["mode"].ToString() == "I")
                    {
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else if (Request.QueryString["mode"].ToString() == "U")
                    {
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;

                        txtErrCode.Enabled = false;
                        ddlErrType.Enabled = false;

                        ErrorCodeMaster objErrorCodesMaster1 = new ErrorCodeMaster();
                        objErrorCodesMaster1.ErrCode = Request.QueryString["code".ToString()];

                        ErrorCodeMasterManager objErrorCodesMasterManager1 = new ErrorCodeMasterManager();

                        ErrorCodeMaster objErrorCodesMaster2 = new ErrorCodeMaster();
                        objErrorCodesMaster2 = objErrorCodesMasterManager1.FetchDetails(objErrorCodesMaster1);

                        txtErrCode.Text = objErrorCodesMaster2.ErrCode;
                        ddlErrType.SelectedValue = objErrorCodesMaster2.ErrType;
                        txtErrDescription.Text = objErrorCodesMaster2.ErrDesc;
                    }
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
                Response.Redirect("~/Master/ErrorCodeMasterListing.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtErrCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string errCode = txtErrCode.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(errCode))
                {
                    ErrorCodeMasterManager objErrorCodesMasterManager = new ErrorCodeMasterManager();
                    bool exists = objErrorCodesMasterManager.CheckCodeTypeExists(errCode);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E16";
                    //ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    //lblInvalidUser.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                    if (exists)
                    {

                        lblValidationMessage1.Text = objErrorCodesMasterManager.FetchMessage(objErrorCodeMaster);
                        txtErrCode.Text = "";
                        lblValidationMessage1.Visible = true;

                    }
                    else
                    {
                        lblValidationMessage1.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorCodeMaster objErrorCodesMaster = new ErrorCodeMaster();
                objErrorCodesMaster.ErrCode = txtErrCode.Text.Trim().ToUpper();
                objErrorCodesMaster.ErrType = ddlErrType.SelectedValue;
                objErrorCodesMaster.ErrDesc = txtErrDescription.Text;
                objErrorCodesMaster.ErrCrBy = Session["User"].ToString();
                objErrorCodesMaster.ErrCrDt = DateTime.Now;

                ErrorCodeMasterManager objErrorCodesMasterManager = new ErrorCodeMasterManager();
                if (objErrorCodesMasterManager.ErrorCodeMasterSave(objErrorCodesMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster1 = new ErrorCodeMaster();
                    objErrorCodeMaster1.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager1 = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager1.FetchMessage(objErrorCodeMaster1);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objErrorCodesMaster.ErrCode}', )", true);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorCodeMaster objErrorCodesMaster = new ErrorCodeMaster();
                objErrorCodesMaster.ErrCode = Request.QueryString["code"].ToString();
                objErrorCodesMaster.ErrType = ddlErrType.SelectedValue;
                objErrorCodesMaster.ErrDesc = txtErrDescription.Text.Trim();
                objErrorCodesMaster.ErrUpBy = Session["User"].ToString();
                objErrorCodesMaster.ErrUpDt = DateTime.Now;

                ErrorCodeMasterManager objErrorCodesMasterManager = new ErrorCodeMasterManager();
                if (objErrorCodesMasterManager.ErrorCodeMasterUpdate(objErrorCodesMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objErrorCodesMaster.ErrCode}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlErrType.SelectedValue = string.Empty;
            txtErrDescription.Text = string.Empty;
        }
    }
}