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
    public partial class TariffMasterEntry : System.Web.UI.Page
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

                    ddlRiskClassFm.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlRiskClassFm.DataValueField = "CODE";
                    ddlRiskClassFm.DataTextField = "TEXT";
                    ddlRiskClassFm.DataBind();

                    objCodesMaster.CmType = "RISK_CLASS";

                    ddlRiskClassTo.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlRiskClassTo.DataValueField = "CODE";
                    ddlRiskClassTo.DataTextField = "TEXT";
                    ddlRiskClassTo.DataBind();

                    objCodesMaster.CmType = "OCCUPANCY";

                    ddlRiskOccFm.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlRiskOccFm.DataValueField = "CODE";
                    ddlRiskOccFm.DataTextField = "TEXT";
                    ddlRiskOccFm.DataBind();

                    objCodesMaster.CmType = "OCCUPANCY";

                    ddlRiskOccTo.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlRiskOccTo.DataValueField = "CODE";
                    ddlRiskOccTo.DataTextField = "TEXT";
                    ddlRiskOccTo.DataBind();

                    if (Request.QueryString["mode"].ToString() == "I")
                    {
                        btnTmSave.Visible = true;
                        btnCancel.Visible = true;
                        btnTariffHistory.Visible = false;

                        TariffMasterManager objTariffMasterManager = new TariffMasterManager();
                        txtTmUid.Text = Convert.ToString(objTariffMasterManager.GetNextTmUid());
                        
                    }
                    else if (Request.QueryString["mode"].ToString() == "U")
                    {
                        btnTmUpdate.Visible = true;
                        btnCancel.Visible = true;
                        

                        txtTmUid.Enabled = false;

                        TariffMaster objTariffMaster = new TariffMaster();
                        string id = Request.QueryString["tmUid"].ToString();
                        objTariffMaster.TmUid = Convert.ToInt32(Request.QueryString["tmUid"].ToString());

                        TariffMasterManager objTariffMasterManager = new TariffMasterManager();

                        TariffMaster objTariffMaster1 = new TariffMaster();
                        objTariffMaster1 = objTariffMasterManager.FetchDetails(objTariffMaster);

                        txtTmUid.Text = Convert.ToString(objTariffMaster1.TmUid);
                        ddlRiskClassFm.SelectedValue = objTariffMaster1.TmRiskClassFm;
                        ddlRiskClassTo.SelectedValue = objTariffMaster1.TmRiskClassTo;
                        ddlRiskOccFm.SelectedValue = objTariffMaster1.TmOccFm;
                        ddlRiskOccTo.SelectedValue = objTariffMaster1.TmOccTo;
                        txtRiskSiFrom.Text = objTariffMaster1.TmSiFm.ToString("F2");
                        txtRiskSiTo.Text = objTariffMaster1.TmSiTo.ToString("F2");
                        txtRiskRate.Text = objTariffMaster1.TmRiskRate.ToString("F2");

                        //BindHistoryData(objTariffMaster1.TmUid);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    
        protected void btnTmeBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Master/TariffMasterListing.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtTmUid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tmUid = txtTmUid.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(tmUid))
                {
                    TariffMasterManager objTariffMasterManager = new TariffMasterManager();
                    bool exists = objTariffMasterManager.CheckTmUidExists(tmUid);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E17";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    //lblInvalidUser.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    if (exists)
                    {

                        lblValidationMessage1.Text = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);
                        txtTmUid.Text = "";
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

        protected void btnTmSave_Click(object sender, EventArgs e)
        {
            try
            {
                TariffMaster objTariffMaster = new TariffMaster();
                objTariffMaster.TmUid = Convert.ToInt32(txtTmUid.Text);

                objTariffMaster.TmRiskClassFm = ddlRiskClassFm.SelectedValue;
                objTariffMaster.TmRiskClassTo = ddlRiskClassTo.SelectedValue;
                objTariffMaster.TmOccFm = ddlRiskOccFm.SelectedValue;
                objTariffMaster.TmOccTo = ddlRiskOccTo.SelectedValue;
                objTariffMaster.TmSiFm = Convert.ToDouble(txtRiskSiFrom.Text);
                objTariffMaster.TmSiTo = Convert.ToDouble(txtRiskSiTo.Text);
                objTariffMaster.TmRiskRate = Convert.ToDouble(txtRiskRate.Text);
                objTariffMaster.TmCrBy = Session["User"].ToString();
                objTariffMaster.TmCrDt = DateTime.Now;

                TariffMasterManager objTariffMasterManager = new TariffMasterManager();
                //objTariffMaster.TmUid = objTariffMasterManager.GetNextTmUid();

                if (objTariffMasterManager.TariffMasterSave(objTariffMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster1 = new ErrorCodeMaster();
                    objErrorCodeMaster1.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager1 = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager1.FetchMessage(objErrorCodeMaster1);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objTariffMaster.TmUid}', )", true);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnTmUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                TariffMaster objTariffMaster = new TariffMaster();

                objTariffMaster.TmUid = Convert.ToInt32(txtTmUid.Text);
                objTariffMaster.TmRiskClassFm = ddlRiskClassFm.SelectedValue;
                objTariffMaster.TmRiskClassTo = ddlRiskClassTo.SelectedValue;
                objTariffMaster.TmOccFm = ddlRiskOccFm.SelectedValue;
                objTariffMaster.TmOccTo = ddlRiskOccTo.SelectedValue;
                objTariffMaster.TmSiFm = Convert.ToDouble(txtRiskSiFrom.Text);
                objTariffMaster.TmSiTo = Convert.ToDouble(txtRiskSiTo.Text);
                objTariffMaster.TmRiskRate = Convert.ToDouble(txtRiskRate.Text);
                objTariffMaster.TmUpBy = Session["User"].ToString();
                objTariffMaster.TmUpDt = DateTime.Now;

                TariffMasterManager objTariffMasterManager = new TariffMasterManager();
                if (objTariffMasterManager.TariffMasterUpdate(objTariffMaster))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objTariffMaster.TmUid}')", true);

                    //BindHistoryData(objTariffMaster.TmUid);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlRiskClassFm.SelectedValue = string.Empty;
            ddlRiskClassTo.SelectedValue = string.Empty;
            ddlRiskOccFm.SelectedValue = string.Empty;
            ddlRiskOccTo.SelectedValue = string.Empty;
            txtRiskSiFrom.Text = string.Empty;
            txtRiskSiTo.Text = string.Empty;
            txtRiskRate.Text = string.Empty;

        }

        protected void btnTariffHistory_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowHistory")
                {
                   
                    string tmUid = txtTmUid.Text;
                    

                    Response.Redirect($"~/Master/TariffMasterHistory.aspx?tmUid={tmUid}&&mode=U");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}