using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirePolicyIssuanceSystem.Master
{
    public partial class UserMasterEntry : System.Web.UI.Page
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
                    if (Request.QueryString["mode"].ToString() == "I")
                    {
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else if (Request.QueryString["mode"].ToString() == "U")
                    {
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;

                        txtUserId.Enabled = false;

                        UserMaster objUserMaster = new UserMaster();
                        objUserMaster.UserId = Request.QueryString["userId".ToString()];

                        UserMasterManager objUserMasterMansger = new UserMasterManager();

                        UserMaster objUserMaster1 = new UserMaster();
                        objUserMaster1 = objUserMasterMansger.FetchDetails(objUserMaster);

                        txtUserId.Text = objUserMaster1.UserId;
                        txtUserName.Text = objUserMaster1.UserName;
                        txtPassword.Text = objUserMaster1.UserPassword;
                        chkActiveYn.Checked = objUserMaster1.UserActiveYn == "Y";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUmBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Master/UserMasterListing.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string userId = txtUserId.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(userId))
                {
                    UserMasterManager objUserMasterManager = new UserMasterManager();
                    bool exists = objUserMasterManager.CheckUserIDExists(userId);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E13";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    if (exists)
                    {

                        lblValidationMessage1.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                        txtUserId.Text = "";
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
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = txtUserId.Text.Trim().ToUpper();
                objUserMaster.UserName = txtUserName.Text.Trim();
                objUserMaster.UserPassword = txtPassword.Text;
                objUserMaster.UserActiveYn = chkActiveYn.Checked ? "Y" : "N";
                objUserMaster.UserCrBy = Session["User"].ToString();
                objUserMaster.UserCrDt = DateTime.Now;

                UserMasterManager objUserMasterManager = new UserMasterManager();
                if (objUserMasterManager.UserMasterSave(objUserMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster1 = new ErrorCodeMaster();
                    objErrorCodeMaster1.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager1 = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager1.FetchMessage(objErrorCodeMaster1);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objUserMaster.UserId}', )", true);

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
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = Request.QueryString["userId"].ToString();
                objUserMaster.UserName = txtUserName.Text.Trim();
                objUserMaster.UserPassword = txtPassword.Text.Trim();
                objUserMaster.UserActiveYn = chkActiveYn.Checked ? "Y" : "N";
                objUserMaster.UserUpBy = Session["User"].ToString();
                objUserMaster.UserUpDt = DateTime.Now;

                UserMasterManager objUserMasterMansger = new UserMasterManager();
                if (objUserMasterMansger.UserMasterUpdate(objUserMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objUserMaster.UserId}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            chkActiveYn.Checked = false;
        }
    }
}