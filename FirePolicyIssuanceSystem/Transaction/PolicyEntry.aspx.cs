using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Web.Services;
using System.Data;

namespace FirePolicyIssuanceSystem.Transaction
{
    public partial class PolicyEntry : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetErrorMessage()
        {
            ErrorCodeMaster objErrorCodeMaster1 = new ErrorCodeMaster { ErrCode = "E3" };
            ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
            return objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster1);
        }

        [WebMethod]
        public static string GetErrorMessage2()
        {
            ErrorCodeMaster objErrorCodeMaster2 = new ErrorCodeMaster { ErrCode = "E4" };
            ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
            return objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster2);
        }


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

                    txtPolicyIssueDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    string polUid = Request.QueryString["PolUid"];

                    CodesMaster objCodesMaster = new CodesMaster();
                    CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                    objCodesMaster.CmType = "ASSR_TYPE";

                    ddlAssrType.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlAssrType.DataValueField = "CODE";
                    ddlAssrType.DataTextField = "TEXT";
                    ddlAssrType.DataBind();

                    objCodesMaster.CmType = "PRODUCT";

                    ddlProduct.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlProduct.DataValueField = "CODE";
                    ddlProduct.DataTextField = "TEXT";
                    ddlProduct.DataBind();

                    objCodesMaster.CmType = "CURRENCY";

                    ddlSiCurrency.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlSiCurrency.DataValueField = "CODE";
                    ddlSiCurrency.DataTextField = "TEXT";
                    ddlSiCurrency.DataBind();

                    ddlPremCurrency.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                    ddlPremCurrency.DataValueField = "CODE";
                    ddlPremCurrency.DataTextField = "TEXT";
                    ddlPremCurrency.DataBind();

                    txtToDate.Attributes.Add("readonly", "readonly");

                    if (!string.IsNullOrEmpty(polUid))
                    {
                        // Load existing policy data based on PolUid
                        ddlSiCurrency.Enabled = false;
                        ddlPremCurrency.Enabled = false;

                        BindData();

                        objCodesMaster.CmType = "RISK_CLASS";

                        ddlRiskClass.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                        ddlRiskClass.DataValueField = "CODE";
                        ddlRiskClass.DataTextField = "TEXT";
                        ddlRiskClass.DataBind();

                        objCodesMaster.CmType = "CONSTR_TYPE";

                        ddlConstrType.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                        ddlConstrType.DataValueField = "CODE";
                        ddlConstrType.DataTextField = "TEXT";
                        ddlConstrType.DataBind();

                        objCodesMaster.CmType = "OCCUPANCY";

                        ddlOccType.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                        ddlOccType.DataValueField = "CODE";
                        ddlOccType.DataTextField = "TEXT";
                        ddlOccType.DataBind();

                        btnSave.Visible = false;
                        //btnApproval.Visible = false;


                        btnCancel.Visible = true;
                        btnAddRisk.Visible = true;
                        btnUpdatePolicy.Visible = true;
                        btnSaveRisk.Visible = true;
                        btnUpdateRisk.Visible = false;


                        FirePolicy objFirePolicy1 = new FirePolicy();
                        objFirePolicy1.PolUid = Convert.ToInt32(Request.QueryString["PolUid"].ToString());

                        FirePolicyManager objFirePolicyManager1 = new FirePolicyManager();

                        FirePolicy objFirePolicy2 = new FirePolicy();
                        objFirePolicy2 = objFirePolicyManager1.PolicyFetchDetails(objFirePolicy1);

                        FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();
                        int riskRowCount = objFirePolicyRiskManager.RiskRowCount(objFirePolicy2.PolUid);

                       

                        if (objFirePolicy2.PolApprStatus == "A")
                        {
                            btnCopy.Visible = true;
                            btnPrint.Visible = true;

                            btnAddRisk.Visible = false;
                            btnUpdatePolicy.Visible = false;
                            btnSave.Visible = false;
                            btnSave.Visible = false;
                            btnApproval.Visible = false;
                            btnCancel.Visible = false;

                            GridViewRisk.Columns[GridViewRisk.Columns.Count - 1].Visible = false;

                            ddlProduct.Enabled = false;
                            txtFromDate.Enabled = false;
                            txtAssrName.Enabled = false;
                            txtAssrAddress.Enabled = false;
                            txtAssrMobile.Enabled = false;
                            txtAssrEmail.Enabled = false;
                            txtAssrDob.Enabled = false;
                            ddlAssrOccupation.Enabled = false;
                            ddlAssrType.Enabled = false;
                            txtNationalID.Enabled = false;
                            chkMultiRisk.Enabled = false;
                            ddlSiCurrency.Enabled = false;
                            txtSiCurrRate.Enabled = false;
                            ddlPremCurrency.Enabled = false;
                            txtPremCurrRate.Enabled = false;
                            txtFCSI.Enabled = false;
                            txtLCSI.Enabled = false;
                            txtGrossFCPremium.Enabled = false;
                            txtGrossLCPremium.Enabled = false;
                            txtVATFC.Enabled = false;
                            txtVATLC.Enabled = false;
                            txtNetFC.Enabled = false;
                            txtNetLC.Enabled = false;

                        }
                        else
                        {

                            if (riskRowCount >= 1)
                            {
                                btnApproval.Visible = true;
                            }
                            else
                            {
                                btnApproval.Visible = false;
                            }
                        }

                        txtPolicyNo.Text = objFirePolicy2.PolNo;
                        txtPolicyIssueDate.Text = objFirePolicy2.PolIssDt?.ToString("yyyy-MM-dd");
                        ddlProduct.SelectedValue = objFirePolicy2.PolProdCode;
                        txtAssrName.Text = objFirePolicy2.PolAssrName;
                        txtAssrAddress.Text = objFirePolicy2.PolAssrAddress;
                        txtAssrDob.Text = objFirePolicy2.PolAssrDob?.ToString("yyyy-MM-dd");
                        txtAssrEmail.Text = objFirePolicy2.PolAssrEmail;
                        txtAssrMobile.Text = objFirePolicy2.PolAssrMobile;
                        txtNationalID.Text = objFirePolicy2.PolAssrCivilId;
                        ddlAssrType.SelectedValue = objFirePolicy2.PolAssrType;

                        if (objFirePolicy2.PolAssrType == "1")
                        {
                            objCodesMaster.CmType = "INDIV_TYPE";
                        }
                        else if (objFirePolicy2.PolAssrType == "2")
                        {
                            objCodesMaster.CmType = "CORP_TYPE";
                        }
                        ddlAssrOccupation.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                        ddlAssrOccupation.DataValueField = "CODE";
                        ddlAssrOccupation.DataTextField = "TEXT";
                        ddlAssrOccupation.DataBind();

                      
                        ddlAssrOccupation.SelectedValue = objFirePolicy2.PolAssrOccupation;

                        if (objFirePolicy2.PolAssrType == "2")
                        {
                            rfvAssrDob.Enabled = false;
                            rfvNationalId.Enabled = false;
                            rfvAssrMobile.Enabled = false;
                        }
                        chkMultiRisk.Checked = objFirePolicy2.PolMultRiskYn == "Y";

                        if (!chkMultiRisk.Checked)
                        {
                            if (riskRowCount > 0)
                            {
                                btnAddRisk.Visible = false;
                            }
                            else
                            {
                                btnAddRisk.Visible = true;
                            }
                        }
                        //MultiRiskYN(objFirePolicy1.PolUid);

                        
                            if (objFirePolicy2.PolFmDt?.ToString("yyyy-MM-dd") == "0001-01-01")
                            {
                                txtFromDate.Text = "";
                            }
                            else
                            {
                                txtFromDate.Text = objFirePolicy2.PolFmDt?.ToString("yyyy-MM-dd");
                            }
                            if (objFirePolicy2.PolToDt?.ToString("yyyy-MM-dd") == "0001-01-01")
                            {
                                txtToDate.Text = "";
                            }
                            else
                            {
                                txtToDate.Text = objFirePolicy2.PolToDt?.ToString("yyyy-MM-dd");
                            }
                        
                        ddlSiCurrency.SelectedValue = objFirePolicy2.PolSiCurrency;
                        ddlPremCurrency.SelectedValue = objFirePolicy2.PolPremCurrency;
                        txtSiCurrRate.Text = objFirePolicy2.PolSiCurrRate.ToString();
                        txtPremCurrRate.Text = objFirePolicy2.PolPremCurrRate.ToString();
                        txtFCSI.Text = objFirePolicy2.PolFcSi.ToString("F2");
                        txtLCSI.Text = objFirePolicy2.PolLcSi.ToString("F2");
                        txtGrossFCPremium.Text = objFirePolicy2.PolGrossFcPrem.ToString("F2");
                        txtGrossLCPremium.Text = objFirePolicy2.PolGrossLcPrem.ToString("F2");
                        txtVATFC.Text = objFirePolicy2.PolVatFcAmt.ToString("F2");
                        txtVATLC.Text = objFirePolicy2.PolVatLcAmt.ToString("F2");
                        txtNetLC.Text = objFirePolicy2.PolNetLcPrem.ToString("F2");
                        txtNetFC.Text = objFirePolicy2.PolNetFcPrem.ToString("F2");

                    }
                    else
                    {
                        // New policy entry
                        btnSave.Visible = true;
                        btnCancel.Visible = true;

                        //btnAddRisk.Visible = false;
                        //btnUpdatePolicy.Visible = false;
                        //btnApproval.Visible = false;

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
                FirePolicy objFirePolicy = new FirePolicy();

                objFirePolicy.PolAssrName = txtAssrName.Text.Trim();
                objFirePolicy.PolAssrAddress = txtAssrAddress.Text.Trim();
                objFirePolicy.PolAssrMobile = txtAssrMobile.Text.Trim();
                objFirePolicy.PolAssrEmail = txtAssrEmail.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtAssrDob.Text) && DateTime.TryParse(txtAssrDob.Text, out DateTime parsedDate))
                {
                    objFirePolicy.PolAssrDob = parsedDate;
                }
                else
                {
                    objFirePolicy.PolAssrDob = null;
                }

                objFirePolicy.PolAssrOccupation = ddlAssrOccupation.SelectedValue;
                objFirePolicy.PolAssrType = ddlAssrType.SelectedValue;
                objFirePolicy.PolProdCode = ddlProduct.SelectedValue;
                objFirePolicy.PolAssrCivilId = txtNationalID.Text.Trim();
                objFirePolicy.PolFmDt = DateTime.Parse(txtFromDate.Text);
                objFirePolicy.PolSiCurrency = ddlSiCurrency.SelectedValue;
                objFirePolicy.PolMultRiskYn = chkMultiRisk.Checked ? "Y" : "N";
                objFirePolicy.PolPremCurrency = ddlPremCurrency.SelectedValue;
                objFirePolicy.PolIssDt = DateTime.Now;
                objFirePolicy.PolToDt = DateTime.Parse(txtToDate.Text);
                objFirePolicy.PolSiCurrRate = Convert.ToDouble(txtSiCurrRate.Text);
                objFirePolicy.PolPremCurrRate = Convert.ToDouble(txtPremCurrRate.Text);
                objFirePolicy.PolCrBy = Session["User"].ToString();
                objFirePolicy.PolCrDt = DateTime.Now;
                objFirePolicy.PolApprStatus = "N";

                FirePolicyManager objFirePolicyManager = new FirePolicyManager();
                objFirePolicy.PolUid = objFirePolicyManager.GetNextPolUid();
                objFirePolicy.PolNo = objFirePolicyManager.GeneratePolicyNumber(DateTime.Now.Year, objFirePolicy.PolProdCode, objFirePolicy.PolUid);

                if (objFirePolicyManager.FirePolicySave(objFirePolicy))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objFirePolicy.PolUid}')", true);

                    //chkMultiRisk.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnPeBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Transaction/PolicyListing.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void txtAssrDob_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsValidControlEvent(sender))
                {
                    DateTime dob = DateTime.Parse(txtAssrDob.Text);
                    DateTime today = DateTime.Now;

                    int age = today.Year - dob.Year;
                    if (dob > today.AddYears(-age)) age--;

                    if (age < 18 || age > 65)
                    {
                        ErrorCodeMaster objErrorCodeMaster2 = new ErrorCodeMaster();
                        objErrorCodeMaster2.ErrCode = "E4";
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster2);

                        ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        txtAssrDob.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsValidControlEvent(sender))
                {
                    DateTime fromDate = DateTime.Parse(txtFromDate.Text);
                    DateTime today = DateTime.Now.Date;

                    if (fromDate < today)
                    {
                        ErrorCodeMaster objErrorCodeMaster1 = new ErrorCodeMaster();
                        objErrorCodeMaster1.ErrCode = "E3";
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster1);

                        ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        txtFromDate.Text = "";
                    }
                    else
                    {

                        DateTime toDate = DateTime.Parse(txtFromDate.Text).AddMonths(12).AddDays(-1);
                        txtToDate.Text = toDate.ToString("yyyy-MM-dd");

                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlSiCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedCurrency = ddlSiCurrency.SelectedValue;

                if (!string.IsNullOrEmpty(selectedCurrency))
                {

                    CodesMaster objCodesMaster = new CodesMaster
                    {
                        CmType = "CURRENCY",
                        CmCode = selectedCurrency
                    };

                    CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                    txtSiCurrRate.Text = objCodesMasterManager.FetchCurrencyValue(objCodesMaster);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlPremCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedCurrency = ddlPremCurrency.SelectedValue;

                if (!string.IsNullOrEmpty(selectedCurrency))
                {

                    CodesMaster objCodesMaster = new CodesMaster
                    {
                        CmType = "CURRENCY",
                        CmCode = selectedCurrency
                    };

                    CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                    txtPremCurrRate.Text = objCodesMasterManager.FetchCurrencyValue(objCodesMaster);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSaveRisk_Click(object sender, EventArgs e)
        {

            try
            {

                FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

                objFirePolicyRisk.RiskPolUid = Convert.ToInt32(Request.QueryString["PolUid"].ToString());
                objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
                objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
                objFirePolicyRisk.RiskConstrType = ddlConstrType.SelectedValue;
                objFirePolicyRisk.RiskLocation = txtRiskLocation.Text;
                objFirePolicyRisk.RiskDesc = txtRiskDescription.Text;
                objFirePolicyRisk.RiskFcSi = Convert.ToDouble(txtRiskFcSi.Text);
                objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);
                objFirePolicyRisk.RiskPremRate = Convert.ToDouble(txtRiskPremRate.Text);
                objFirePolicyRisk.RiskFcPrem = Convert.ToDouble(txtRiskFcPrem.Text);
                objFirePolicyRisk.RiskLcPrem = Convert.ToDouble(txtRiskLcPrem.Text);
                objFirePolicyRisk.RiskCrBy = Session["User"].ToString();
                objFirePolicyRisk.RiskCrDt = DateTime.Now;


                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

                if (objFirePolicyRiskManager.FirePolicyRiskSave(objFirePolicyRisk))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S1";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objFirePolicy.PolUid}')", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showSuccess('{message}')", true);

                    btnApproval.Visible = true;
                    BindData();
                    UpdateBackPolicyTable();
                    MultiRiskYN(objFirePolicyRisk.RiskPolUid);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void MultiRiskYN(int riskPolUid)
        {
            FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();
            int riskRowCount = objFirePolicyRiskManager.RiskRowCount(riskPolUid);

            //chkMultiRisk.Enabled = false;

            if (!chkMultiRisk.Checked)
            {
                if (riskRowCount < 1)
                {
                    btnAddRisk.Visible = true;

                }
                else
                {
                    btnAddRisk.Visible = false;
                }
            }
            else
            {
                btnAddRisk.Visible = true;

            }

            if (riskRowCount >= 1)
            {
                btnApproval.Visible = true;
            }
            else
            {
                btnApproval.Visible = false;
            }


        }
        private void UpdateBackPolicyTable()
        {
            try
            {
                string riskPolUid = Request.QueryString["PolUid"];

                FirePolicy objFirePolicy = new FirePolicy();

                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();
                objFirePolicy = objFirePolicyRiskManager.FetchRiskPremiumDetails(riskPolUid);

                objFirePolicy.PolUid = Convert.ToInt32(riskPolUid);
                objFirePolicy.PolVatFcAmt = 0.05 * objFirePolicy.PolGrossFcPrem;
                objFirePolicy.PolVatLcAmt = 0.05 * objFirePolicy.PolGrossLcPrem;
                objFirePolicy.PolNetFcPrem = objFirePolicy.PolGrossFcPrem + objFirePolicy.PolVatFcAmt;
                objFirePolicy.PolNetLcPrem = objFirePolicy.PolGrossLcPrem + objFirePolicy.PolVatLcAmt;

                FirePolicyManager objFirePolicyManager1 = new FirePolicyManager();

                if (objFirePolicyManager1.UpdatePremiumToFirePolicy(objFirePolicy))
                {
                    txtFCSI.Text = objFirePolicy.PolFcSi.ToString("F2");
                    txtLCSI.Text = objFirePolicy.PolLcSi.ToString("F2");
                    txtGrossFCPremium.Text = objFirePolicy.PolGrossFcPrem.ToString("F2");
                    txtGrossLCPremium.Text = objFirePolicy.PolGrossLcPrem.ToString("F2");
                    txtVATFC.Text = objFirePolicy.PolVatFcAmt.ToString("F2");
                    txtVATLC.Text = objFirePolicy.PolVatLcAmt.ToString("F2");
                    txtNetLC.Text = objFirePolicy.PolNetLcPrem.ToString("F2");
                    txtNetFC.Text = objFirePolicy.PolNetFcPrem.ToString("F2");
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

                string riskPolUid = Request.QueryString["PolUid"];

                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

                DataTable riskData = objFirePolicyRiskManager.RiskDataGridBind(riskPolUid);

                GridViewRisk.DataSource = riskData;
                GridViewRisk.DataBind();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdatePolicy_Click(object sender, EventArgs e)
        {
            try
            {

                FirePolicy objFirePolicy = new FirePolicy();
                objFirePolicy.PolUid = Convert.ToInt32(Request.QueryString["PolUid"]);

                objFirePolicy.PolProdCode = ddlProduct.SelectedValue;
                objFirePolicy.PolAssrName = txtAssrName.Text;
                objFirePolicy.PolAssrAddress = txtAssrAddress.Text;
                //objFirePolicy.PolAssrDob = Convert.ToDateTime(txtAssrDob.Text);
                if (!string.IsNullOrWhiteSpace(txtAssrDob.Text) && DateTime.TryParse(txtAssrDob.Text, out DateTime parsedDate))
                {
                    objFirePolicy.PolAssrDob = parsedDate;
                }
                else
                {
                    objFirePolicy.PolAssrDob = null; // or handle the error as needed
                }
                objFirePolicy.PolAssrEmail = txtAssrEmail.Text;
                objFirePolicy.PolAssrMobile = txtAssrMobile.Text;
                objFirePolicy.PolAssrCivilId = txtNationalID.Text;
                objFirePolicy.PolAssrType = ddlAssrType.SelectedValue;
                objFirePolicy.PolAssrOccupation = ddlAssrOccupation.SelectedValue;
                objFirePolicy.PolMultRiskYn = chkMultiRisk.Checked ? "Y" : "N";
                objFirePolicy.PolFmDt = Convert.ToDateTime(txtFromDate.Text);
                
                objFirePolicy.PolToDt = Convert.ToDateTime(txtToDate.Text);
                objFirePolicy.PolSiCurrency = ddlSiCurrency.SelectedValue;
                objFirePolicy.PolPremCurrency = ddlPremCurrency.SelectedValue;
                objFirePolicy.PolSiCurrRate = Convert.ToDouble(txtSiCurrRate.Text);
                objFirePolicy.PolPremCurrRate = Convert.ToDouble(txtPremCurrRate.Text);
                objFirePolicy.PolUpBy = Session["User"].ToString();
                objFirePolicy.PolUpDt = DateTime.Now;


                FirePolicyManager objFirePolicyManager = new FirePolicyManager();
                if (objFirePolicyManager.FirePolicyUpdate(objFirePolicy))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);


                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{objFirePolicy.PolUid}')", true);


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool IsValidControlEvent(object sender)
        {
            try
            {
                Control objControl = sender as Control;
                if (objControl != null)
                {
                    return (objControl.UniqueID == Request.Params["__EVENTTARGET"].ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void ddlAssrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CodesMaster objCodesMaster = new CodesMaster();
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                ddlAssrOccupation.Items.Clear();

                if (ddlAssrType.SelectedValue == "2")
                {
                    rfvAssrDob.Enabled = false;
                    rfvAssrMobile.Enabled = false;
                    rfvNationalId.Enabled = false;

                    objCodesMaster.CmType = "CORP_TYPE";

                }
                else if (ddlAssrType.SelectedValue == "1")
                {
                    rfvAssrDob.Enabled = true;
                    rfvAssrMobile.Enabled = true;
                    rfvNationalId.Enabled = true;

                    objCodesMaster.CmType = "INDIV_TYPE";
                }
                else
                {
                    rfvAssrDob.Enabled = true;
                    rfvAssrMobile.Enabled = true;
                    rfvNationalId.Enabled = true;
                }

                ddlAssrOccupation.DataSource = objCodesMasterManager.DropDownFilling(objCodesMaster);
                ddlAssrOccupation.DataValueField = "CODE";
                ddlAssrOccupation.DataTextField = "TEXT";
                ddlAssrOccupation.DataBind();

                ddlAssrOccupation.Items.Insert(0, new ListItem("-- Select --", ""));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void txtRiskFcSi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtRiskFcSi.Text) && ddlRiskClass.SelectedValue != null && ddlOccType.SelectedValue != null)
                {
                    double? amount = Convert.ToDouble(txtRiskFcSi.Text);
                    string currencyCode = ddlSiCurrency.SelectedValue;
                    string currencyType = "CURRENCY";

                    CalculateLcSi objCalculateLcSi = new CalculateLcSi();
                    txtRiskLcSi.Text = objCalculateLcSi.ConvertCurrency(amount, currencyCode, currencyType).Value.ToString("F2");

                    FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

                    objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
                    objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
                    objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);

                    FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();


                    double? premiumRate = objFirePolicyRiskManager.GetPremiumRate(objFirePolicyRisk);

                    if (premiumRate == null)
                    {

                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster { ErrCode = "E5" };
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        txtRiskPremRate.Text = "";
                        txtRiskLcSi.Text = "";
                        txtRiskFcPrem.Text = "";
                        txtRiskLcPrem.Text = "";
                    }
                    else
                    {
                        txtRiskPremRate.Text = premiumRate.Value.ToString("F2");
                        double? lcSi = Convert.ToDouble(txtRiskLcSi.Text);

                        double? amountLcPrem = (premiumRate / 100) * lcSi;
                        txtRiskLcPrem.Text = amountLcPrem.Value.ToString("F2");
                        string currencyCodePrem = ddlPremCurrency.SelectedValue;

                        txtRiskFcPrem.Text = objCalculateLcSi.ConvertToFC(amountLcPrem, currencyCodePrem, currencyType).Value.ToString("F2");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{ex.Message}')", true);               
            }
        }

        //protected void btnCalculatePR_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["riskFcSi"] = Convert.ToDouble(txtRiskFcSi.Text);
        //        Session["riskClass"] = ddlRiskClass.SelectedValue;
        //        Session["riskOccup"] = ddlOccType.SelectedValue;

        //        FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

        //        objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
        //        objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
        //        objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);

        //        FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

        //        try
        //        {
        //            double? premiumRate = objFirePolicyRiskManager.GetPremiumRate(objFirePolicyRisk);

        //            if (premiumRate == null)
        //            {

        //                ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster { ErrCode = "E5" };
        //                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        //                string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

        //                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
        //                txtRiskPremRate.Text = "";
        //            }
        //            else
        //            {
        //                txtRiskPremRate.Text = premiumRate.Value.ToString();
        //                double? lcSi = Convert.ToDouble(txtRiskLcSi.Text);

        //                double? amountLcPrem = (premiumRate / 100) * lcSi;
        //                txtRiskLcPrem.Text = amountLcPrem.ToString();
        //                string currencyCode = ddlPremCurrency.SelectedValue;
        //                string currencyType = "CURRENCY";

        //                CalculateLcSi objCalculateLcSi = new CalculateLcSi();
        //                txtRiskFcPrem.Text = Convert.ToString(objCalculateLcSi.ConvertToFC(amountLcPrem, currencyCode, currencyType));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string riskUid = GridViewRisk.DataKeys[e.RowIndex].Values["RISK_UID"].ToString();
                string riskPolUid = Request.QueryString["PolUid"];

                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();
                int isDeleted = objFirePolicyRiskManager.DeleteFromRisk(riskUid, riskPolUid);

                if (isDeleted > 0)
                {
                    BindData();

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S3";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"showSuccess('{message}')", true);

                    UpdateBackPolicyTable();
                    MultiRiskYN(Convert.ToInt32(riskPolUid));


                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdateRisk_Click(object sender, EventArgs e)
        {
            try
            {
               
                int riskUid = Convert.ToInt32(((Button)sender).CommandArgument);
                string riskPolUid = Request.QueryString["PolUid"];


                FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

                objFirePolicyRisk.RiskUid = riskUid;
                objFirePolicyRisk.RiskPolUid = Convert.ToInt32(riskPolUid);
                objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
                objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
                objFirePolicyRisk.RiskConstrType = ddlConstrType.SelectedValue;
                objFirePolicyRisk.RiskLocation = txtRiskLocation.Text;
                objFirePolicyRisk.RiskDesc = txtRiskDescription.Text;
                objFirePolicyRisk.RiskFcSi = Convert.ToDouble(txtRiskFcSi.Text);
                objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);
                objFirePolicyRisk.RiskPremRate = Convert.ToDouble(txtRiskPremRate.Text);
                objFirePolicyRisk.RiskFcPrem = Convert.ToDouble(txtRiskFcPrem.Text);
                objFirePolicyRisk.RiskLcPrem = Convert.ToDouble(txtRiskLcPrem.Text);
                objFirePolicyRisk.RiskUpBy = Session["User"].ToString();
                objFirePolicyRisk.RiskUpDt = DateTime.Now;

                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

                if (objFirePolicyRiskManager.FirePolicyRiskUpdate(objFirePolicyRisk))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "S2";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", $"showSuccess('{message}')", true);
                    BindData();
                    UpdateBackPolicyTable();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void btnRiskUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRiskUpdate = (Button)sender;
                GridViewRow row = (GridViewRow)btnRiskUpdate.NamingContainer;

                string riskUid = btnRiskUpdate.CommandArgument;
                string riskPolUid = Request.QueryString["PolUid"];

                btnSaveRisk.Visible = false;
                btnUpdateRisk.Visible = true;
                btnCancelRisk.Visible = true;

                btnUpdateRisk.CommandArgument = riskUid;

                FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();
                objFirePolicyRisk.RiskUid = Convert.ToInt32(riskUid);
                objFirePolicyRisk.RiskPolUid = Convert.ToInt32(riskPolUid);

                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

                FirePolicyRisk objFirePolicyRisk1 = new FirePolicyRisk();
                objFirePolicyRisk1 = objFirePolicyRiskManager.FetchDetails(objFirePolicyRisk);

                ddlRiskClass.SelectedValue = objFirePolicyRisk1.RiskClass;
                ddlOccType.SelectedValue = objFirePolicyRisk1.RiskOccupType;
                ddlConstrType.SelectedValue = objFirePolicyRisk1.RiskConstrType;
                txtRiskLocation.Text = objFirePolicyRisk1.RiskLocation;
                txtRiskDescription.Text = objFirePolicyRisk1.RiskDesc;
                txtRiskFcSi.Text = objFirePolicyRisk1.RiskFcSi.ToString("F2");
                txtRiskLcSi.Text = objFirePolicyRisk1.RiskLcSi.ToString("F2");
                txtRiskPremRate.Text = objFirePolicyRisk1.RiskPremRate.ToString("F2");
                txtRiskFcPrem.Text = objFirePolicyRisk1.RiskFcPrem.ToString("F2");
                txtRiskLcPrem.Text = objFirePolicyRisk1.RiskLcPrem.ToString("F2");

               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "openRiskModal();", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAddRisk_Click(object sender, EventArgs e)
        {
           
            try
            {

                btnSaveRisk.Visible = true;
                btnCancelRisk.Visible = true;
                btnUpdateRisk.Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();

                int riskPolUid = Convert.ToInt32(Request.QueryString["PolUid"]);
                int riskRowCount = objFirePolicyRiskManager.RiskRowCount(riskPolUid);
                string user = Session["User"].ToString();

                FirePolicyManager objFirePolicyManager = new FirePolicyManager();

                if (riskRowCount > 0)
                {
                    if (objFirePolicyManager.ApproveStatusToDB(riskPolUid, user))
                    {
                        string polUid = Request.QueryString["PolUid"].ToString();
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "S4";
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);


                        btnApproval.Visible = false;
                        btnAddRisk.Visible = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "success", $"successAlert('{message}', '{polUid}')", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "success", $"showSuccess('{message}')", true);
                        //Response.Redirect($"~/Transaction/PolicyEntry.aspx?PolUid={polUid}");
                    }
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "E7";
                    ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                    string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                    btnApproval.Visible = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ddlProduct.SelectedValue = string.Empty;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                txtAssrName.Text = string.Empty;
                txtAssrAddress.Text = string.Empty;
                txtAssrEmail.Text = string.Empty;
                txtAssrMobile.Text = string.Empty;
                ddlAssrType.SelectedValue = string.Empty;
                ddlAssrOccupation.SelectedValue = string.Empty;
                txtNationalID.Text = string.Empty;
                txtAssrDob.Text = string.Empty;
                chkMultiRisk.Checked = false;
                ddlSiCurrency.SelectedValue = string.Empty;
                txtSiCurrRate.Text = string.Empty;
                ddlPremCurrency.SelectedValue = string.Empty;
                txtPremCurrRate.Text = string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

      
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int oldPolUid = Convert.ToInt32(Request.QueryString["PolUid"]);

                FirePolicyManager objFirePolicyManager = new FirePolicyManager();
                var result = objFirePolicyManager.CopyPolicy(oldPolUid);

                if (result.status == 1)
                {
                    ddlProduct.Enabled = false;
                    txtAssrName.Enabled = false;
                    txtAssrAddress.Enabled = false;
                    txtAssrMobile.Enabled = false;
                    txtAssrEmail.Enabled = false;
                    txtAssrDob.Enabled = false;
                    ddlAssrOccupation.Enabled = false;
                    ddlAssrType.Enabled = false;
                    txtNationalID.Enabled = false;
                    chkMultiRisk.Enabled = false;
                    ddlSiCurrency.Enabled = false;
                    txtSiCurrRate.Enabled = false;
                    ddlPremCurrency.Enabled = false;
                    txtPremCurrRate.Enabled = false;
                    txtFCSI.Enabled = false;
                    txtLCSI.Enabled = false;
                    txtGrossFCPremium.Enabled = false;
                    txtGrossLCPremium.Enabled = false;
                    txtVATFC.Enabled = false;
                    txtVATLC.Enabled = false;
                    txtNetFC.Enabled = false;
                    txtNetLC.Enabled = false;

                    Response.Redirect($"~/Transaction/PolicyEntry.aspx?PolUid={result.newPolUid}");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{result.errMsg}')", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkMultiRisk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["PolUid"].ToString()))
                {
                    int polUid = Convert.ToInt32(Request.QueryString["PolUid"]);

                    FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();
                    int riskRowCount = objFirePolicyRiskManager.RiskRowCount(polUid);

                    if (riskRowCount > 1)
                    {
                        if (!chkMultiRisk.Checked)
                        {
                            chkMultiRisk.Checked = true;

                            ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster { ErrCode = "E8" };
                            ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                            string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                            ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{ex.Message}')", true);
            }
        }

        protected void ddlRiskClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtRiskFcSi.Text) && ddlRiskClass.SelectedValue != null && ddlOccType.SelectedValue != null)
                {
                    double? amount = Convert.ToDouble(txtRiskFcSi.Text);
                    string currencyCode = ddlSiCurrency.SelectedValue;
                    string currencyType = "CURRENCY";

                    CalculateLcSi objCalculateLcSi = new CalculateLcSi();
                    txtRiskLcSi.Text = objCalculateLcSi.ConvertCurrency(amount, currencyCode, currencyType).Value.ToString("F2");

                    FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

                    objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
                    objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
                    objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);

                    FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();


                    double? premiumRate = objFirePolicyRiskManager.GetPremiumRate(objFirePolicyRisk);

                    if (premiumRate == null)
                    {

                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster { ErrCode = "E5" };
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        txtRiskPremRate.Text = "";
                        txtRiskLcSi.Text = "";
                        txtRiskFcPrem.Text = "";
                        txtRiskLcPrem.Text = "";
                    }
                    else
                    {
                        txtRiskPremRate.Text = premiumRate.Value.ToString("F2");
                        double? lcSi = Convert.ToDouble(txtRiskLcSi.Text);

                        double? amountLcPrem = (premiumRate / 100) * lcSi;
                        txtRiskLcPrem.Text = amountLcPrem.Value.ToString("F2");
                        string currencyCodePrem = ddlPremCurrency.SelectedValue;

                        txtRiskFcPrem.Text = objCalculateLcSi.ConvertToFC(amountLcPrem, currencyCodePrem, currencyType).Value.ToString("F2");
                    }
            }
            }
            catch (Exception ex)
            {

               ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{ex.Message}')", true); 
            }
        }

        protected void ddlOccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtRiskFcSi.Text) && ddlRiskClass.SelectedValue != null && ddlOccType.SelectedValue != null)
                {
                    double? amount = Convert.ToDouble(txtRiskFcSi.Text);
                    string currencyCode = ddlSiCurrency.SelectedValue;
                    string currencyType = "CURRENCY";

                    CalculateLcSi objCalculateLcSi = new CalculateLcSi();
                    txtRiskLcSi.Text = objCalculateLcSi.ConvertCurrency(amount, currencyCode, currencyType).Value.ToString("F2");

                    FirePolicyRisk objFirePolicyRisk = new FirePolicyRisk();

                    objFirePolicyRisk.RiskClass = ddlRiskClass.SelectedValue;
                    objFirePolicyRisk.RiskOccupType = ddlOccType.SelectedValue;
                    objFirePolicyRisk.RiskLcSi = Convert.ToDouble(txtRiskLcSi.Text);

                    FirePolicyRiskManager objFirePolicyRiskManager = new FirePolicyRiskManager();


                    double? premiumRate = objFirePolicyRiskManager.GetPremiumRate(objFirePolicyRisk);

                    if (premiumRate == null)
                    {

                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster { ErrCode = "E5" };
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string message = objErrorCodeMasterManager.FetchMessage(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{message}')", true);
                        txtRiskPremRate.Text = "";
                        txtRiskLcSi.Text = "";
                        txtRiskFcPrem.Text = "";
                        txtRiskLcPrem.Text = "";
                    }
                    else
                    {
                        txtRiskPremRate.Text = premiumRate.Value.ToString("F2");
                        double? lcSi = Convert.ToDouble(txtRiskLcSi.Text);

                        double? amountLcPrem = (premiumRate / 100) * lcSi;
                        txtRiskLcPrem.Text = amountLcPrem.Value.ToString("F2");
                        string currencyCodePrem = ddlPremCurrency.SelectedValue;

                        txtRiskFcPrem.Text = objCalculateLcSi.ConvertToFC(amountLcPrem, currencyCodePrem, currencyType).Value.ToString("F2");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"showError('{ex.Message}')", true);
            }
        }
    }
}
