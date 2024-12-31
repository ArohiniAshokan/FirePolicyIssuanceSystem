using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace FirePolicyIssuanceSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["logout"] == "true")
                {
                    //lblLogout.Text = "Logged Out Successfully";
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E14";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    lblLogout.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserName = txtUserName.Text.Trim();
                objUserMaster.UserPassword = txtPassword.Text.Trim();

                UserMasterManager objUserMasterManager = new UserMasterManager();
                if (objUserMasterManager.Login(objUserMaster))
                {
                    Session["User"] = objUserMaster.UserName;
                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E1";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    lblInvalidUser.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}