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
    public partial class UserMasterListing : System.Web.UI.Page
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
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserName = txtFilterUserName.Text;

                UserMasterManager objUserMasterManager = new UserMasterManager();

                DataTable userDataBind = objUserMasterManager.UserMasterGridBind(objUserMaster);

                GridView1.DataSource = userDataBind;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Master/UserMasterEntry.aspx?mode=I");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUmlBack_Click(object sender, EventArgs e)
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
                string userId = GridView1.DataKeys[e.RowIndex].Values["USER_ID"].ToString();


                UserMasterManager objUserMasterManager = new UserMasterManager();
                int isDeleted = objUserMasterManager.DeleteFromUserMaster(userId);

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

        protected void btnUserUpdate_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    Button btnUserUpdate = (Button)sender;

                    GridViewRow row = (GridViewRow)btnUserUpdate.NamingContainer;

                    string[] args = e.CommandArgument.ToString().Split(';');
                    string userId = args[0];


                    Response.Redirect($"~/Master/UserMasterEntry.aspx?userId={userId}&&mode=U");
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
            txtFilterUserName.Text = string.Empty;
        }
    }
}