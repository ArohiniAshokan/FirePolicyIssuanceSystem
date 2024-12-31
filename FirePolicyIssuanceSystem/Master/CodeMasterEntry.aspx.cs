using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class CodeMasterEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session["User"]==null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["mode"].ToString() == "I")
                    {
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else if (Request.QueryString["mode"].ToString() == "U")
                    {
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;

                        txtCode.Enabled = false;
                        txtType.Enabled = false;

                        CodesMaster objCodesMaster1 = new CodesMaster();
                        objCodesMaster1.CmCode = Request.QueryString["code".ToString()];
                        objCodesMaster1.CmType = Request.QueryString["type".ToString()];

                        CodesMasterManager objCodesMasterManager1 = new CodesMasterManager();

                        CodesMaster objCodesMaster2 = new CodesMaster();
                        objCodesMaster2 = objCodesMasterManager1.FetchDetails(objCodesMaster1);

                        txtCode.Text = objCodesMaster2.CmCode;
                        txtType.Text = objCodesMaster2.CmType;
                        txtDescription.Text = objCodesMaster2.CmDesc;
                        txtValue.Text = objCodesMaster2.CmValue;
                        chkActive.Checked = objCodesMaster2.CmActiveYn == "Y";

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
                Response.Redirect("~/Master/CodeMasterListing.aspx");
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
                CodesMaster objCodesMaster = new CodesMaster();
                objCodesMaster.CmCode = txtCode.Text.Trim().ToUpper();
                objCodesMaster.CmType = txtType.Text.Trim().ToUpper();
                objCodesMaster.CmValue = txtValue.Text.Trim();
                objCodesMaster.CmActiveYn = chkActive.Checked ? "Y" : "N";
                objCodesMaster.CmDesc = txtDescription.Text;
                objCodesMaster.CmCrBy = Session["User"].ToString();
                objCodesMaster.CmCrDt = DateTime.Now;

                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                if (objCodesMasterManager.CodeMasterSave(objCodesMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objCodesMaster.CmCode}', '{ objCodesMaster.CmType}')", true);

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
                CodesMaster objCodesMaster = new CodesMaster();
                objCodesMaster.CmCode = Request.QueryString["code"].ToString();
                objCodesMaster.CmType = Request.QueryString["type"].ToString();
                objCodesMaster.CmValue = txtValue.Text.Trim().ToUpper();
                objCodesMaster.CmActiveYn = chkActive.Checked ? "Y" : "N";
                objCodesMaster.CmDesc = txtDescription.Text.Trim();
                objCodesMaster.CmUpBy = Session["User"].ToString();
                objCodesMaster.CmUpDt = DateTime.Now;

                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                if (objCodesMasterManager.CodeMasterUpdate(objCodesMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objCodesMaster.CmCode}', '{ objCodesMaster.CmType}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cmCode = txtCode.Text.Trim().ToUpper();
                string cmType = txtType.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(cmType))
                {
                    CodesMasterManager objcodesmastermanager = new CodesMasterManager();
                    bool exists = objcodesmastermanager.CheckCodeTypeExists(cmCode, cmType);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E15";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    //lblInvalidUser.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    if (exists)
                    {

                        lblValidationMessage1.Text = "*";
                        lblValidationMessage2.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                        txtCode.Text = "";
                        txtType.Text = "";

                        lblValidationMessage1.Visible = true;
                        lblValidationMessage2.Visible = true;


                    }
                    else
                    {
                        lblValidationMessage1.Text = "";
                        lblValidationMessage2.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cmCode = txtCode.Text.Trim().ToUpper();
                string cmType = txtType.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(cmCode))
                {
                    CodesMasterManager objcodesmastermanager = new CodesMasterManager();
                    bool exists = objcodesmastermanager.CheckCodeTypeExists(cmCode, cmType);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E15";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    //lblInvalidUser.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    if (exists)
                    {

                        lblValidationMessage1.Text = "*";
                        lblValidationMessage2.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                        txtCode.Text = "";
                        txtType.Text = "";

                        lblValidationMessage1.Visible = true;
                        lblValidationMessage2.Visible = true;
                    }
                    else
                    {
                        lblValidationMessage1.Text = "";
                        lblValidationMessage2.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;
            txtDescription.Text = string.Empty;

            chkActive.Checked = false;
        }
    }
}