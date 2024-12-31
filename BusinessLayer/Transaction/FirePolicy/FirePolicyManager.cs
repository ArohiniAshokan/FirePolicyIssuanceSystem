using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Globalization;
using System.Web;



namespace BusinessLayer
{
    public class FirePolicyManager
    {
        public object Request { get; private set; }

        public DataSet FirePolicyFilterGridBind(FirePolicy objFirePolicy)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_NO"] = objFirePolicy.PolNo,
                    ["P_POL_PROD_CODE"] = objFirePolicy.PolProdCode,
                    ["P_POL_ISS_DT"] = objFirePolicy.PolIssDt,
                    ["P_POL_FM_DT"] = objFirePolicy.PolFmDt,
                    ["P_POL_TO_DT"] = objFirePolicy.PolToDt,
                    ["P_POL_APPR_STATUS"] = objFirePolicy.PolApprStatus

                };


                DataSet result = null;
                string query = "SELECT POL_UID, POL_NO, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = POL_PROD_CODE AND CM_TYPE = 'PRODUCT')PROD_CODE, POL_ISS_DT, POL_FM_DT, POL_TO_DT, DECODE(POL_APPR_STATUS, 'N', 'Pending', 'A', 'Approved') POL_APPR_STATUS, POL_ASSR_NAME, POL_LC_SI, POL_NET_LC_PREM FROM FIRE_POLICY WHERE 1=1 ORDER BY POL_UID DESC";

                if (!string.IsNullOrEmpty(objFirePolicy.PolNo))
                {
                    query = query.Replace("1=1","POL_NO = :P_POL_NO AND 1=1"); 
                   
                }

                if (!string.IsNullOrEmpty(objFirePolicy.PolProdCode))
                {
                    query = query.Replace("1=1", "POL_PROD_CODE = :P_POL_PROD_CODE AND 1=1");

                }

                if (objFirePolicy.PolIssDt != Convert.ToDateTime(null))
                {
                    query = query.Replace("1=1", "POL_ISS_DT = :P_POL_ISS_DT AND 1=1");

                }

                if (objFirePolicy.PolFmDt != Convert.ToDateTime(null))
                {
                    query = query.Replace("1=1", "POL_FM_DT >= :P_POL_FM_DT AND 1=1");

                }

                if (objFirePolicy.PolToDt != Convert.ToDateTime(null))
                {
                    query = query.Replace("1=1", "POL_TO_DT <= :P_POL_TO_DT AND 1=1");

                }

                if (!string.IsNullOrEmpty(objFirePolicy.PolApprStatus))
                {
                    query = query.Replace("1=1", "POL_APPR_STATUS = :P_POL_APPR_STATUS");
                   
                }

                result = DBConnection.ExecuteQuerySelect(paramValues,query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable FirePolicyGridBind()
        {
            try
            {
                DataTable result = null;
                string query = "SELECT POL_UID, POL_NO, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = POL_PROD_CODE AND CM_TYPE = 'PRODUCT')PROD_CODE, POL_ISS_DT, POL_FM_DT, POL_TO_DT, DECODE(POL_APPR_STATUS, 'N', 'Pending', 'A', 'Approved') POL_APPR_STATUS FROM FIRE_POLICY WHERE 1=1 ORDER BY POL_UID DESC";




                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetNextPolUid()
        {
            try
            {
                string query = "SELECT PolUidSeq.NEXTVAL FROM DUAL";
                return Convert.ToInt32(DBConnection.ExecuteScalar(query));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public (int newPolUid, int status, string errMsg) CopyPolicy(int originalPolUid)
        {
            return DBConnection.ExecuteCopyPolicyProcedure(originalPolUid);
        }

        public string GeneratePolicyNumber(int pYear, string pProdCode, int pPolUid)
        {
            try
            {
                string polNo = DBConnection.ExecuteFunction(pYear.ToString(), pProdCode, pPolUid.ToString());
                return polNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool FirePolicyUpdate(FirePolicy objFirePolicy)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_UID"] = objFirePolicy.PolUid,
                    ["P_POL_NO"] = objFirePolicy.PolNo,
                    ["P_POL_ASSR_NAME"] = objFirePolicy.PolAssrName,
                    ["P_POL_ASSR_ADDRESS"] = objFirePolicy.PolAssrAddress,
                    ["P_POL_ASSR_DOB"] = objFirePolicy.PolAssrDob,
                    ["P_POL_ASSR_EMAIL"] = objFirePolicy.PolAssrEmail,
                    ["P_POL_ASSR_MOBILE"] = objFirePolicy.PolAssrMobile,
                    ["P_POL_ASSR_CIVIL_ID"] = objFirePolicy.PolAssrCivilId,
                    ["P_POL_ASSR_TYPE"] = objFirePolicy.PolAssrType,
                    ["P_POL_ASSR_OCCUPATION"] = objFirePolicy.PolAssrOccupation,
                    ["P_POL_PROD_CODE"] = objFirePolicy.PolProdCode,
                    ["P_POL_MULTI_RISK"] = objFirePolicy.PolMultRiskYn,
                    ["P_POL_FM_DT"] = objFirePolicy.PolFmDt,
                    ["P_POL_SI_CURRENCY"] = objFirePolicy.PolSiCurrency,
                    ["P_POL_PREM_CURRENCY"] = objFirePolicy.PolPremCurrency,
                    ["P_POL_ISS_DT"] = objFirePolicy.PolIssDt,
                    ["P_POL_SI_CURR_RATE"] = Math.Round(objFirePolicy.PolSiCurrRate, 2),
                    ["P_POL_PREM_CURR_RATE"] = Math.Round(objFirePolicy.PolPremCurrRate, 2),
                    ["P_POL_TO_DT"] = objFirePolicy.PolToDt,
                    ["P_POL_UP_BY"] = objFirePolicy.PolUpBy,
                    ["P_POL_UP_DT"] = objFirePolicy.PolUpDt

                };

                string query = "UPDATE FIRE_POLICY SET POL_UP_BY = :P_POL_UP_BY, POL_UP_DT = :P_POL_UP_DT, POL_ASSR_NAME = :P_POL_ASSR_NAME, POL_ASSR_ADDRESS = :P_POL_ASSR_ADDRESS, POL_ASSR_MOBILE = :P_POL_ASSR_MOBILE, POL_ASSR_EMAIL = :P_POL_ASSR_EMAIL, POL_ASSR_CIVIL_ID = :P_POL_ASSR_CIVIL_ID, POL_ASSR_TYPE = :P_POL_ASSR_TYPE, POL_ASSR_OCCUPATION = :P_POL_ASSR_OCCUPATION, POL_PROD_CODE = :P_POL_PROD_CODE, POL_MULT_RISK_YN = :P_POL_MULTI_RISK, POL_SI_CURRENCY = :P_POL_SI_CURRENCY,  POL_PREM_CURRENCY = :P_POL_PREM_CURRENCY, POL_ASSR_DOB = TO_DATE(:P_POL_ASSR_DOB,'DD/MM/RRRR'), POL_FM_DT = TO_DATE(:P_POL_FM_DT,'DD/MM/RRRR'), POL_TO_DT = TO_DATE(:P_POL_TO_DT,'DD/MM/RRRR'), POL_SI_CURR_RATE = :P_POL_SI_CURR_RATE, POL_PREM_CURR_RATE = :P_POL_PREM_CURR_RATE WHERE POL_UID = :P_POL_UID";

                int n = DBConnection.ExecuteQuery(paramValues, query);
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdatePremiumToFirePolicy(FirePolicy objFirePolicy)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_UID"] = objFirePolicy.PolUid,
                    ["P_POL_FC_SI"] = Math.Round(objFirePolicy.PolFcSi, 2),
                    ["P_POL_LC_SI"] = Math.Round(objFirePolicy.PolLcSi, 2),
                    ["P_POL_GROSS_FC_PREM"] = Math.Round(objFirePolicy.PolGrossFcPrem, 2),
                    ["P_POL_GROSS_LC_PREM"] = Math.Round(objFirePolicy.PolGrossLcPrem, 2),
                    ["P_POL_VAT_FC_AMT"] = Math.Round(objFirePolicy.PolVatFcAmt, 2),
                    ["P_POL_VAT_LC_AMT"] = Math.Round(objFirePolicy.PolVatLcAmt, 2),
                    ["P_POL_NET_FC_PREM"] = Math.Round(objFirePolicy.PolNetFcPrem, 2),
                    ["P_POL_NET_LC_PREM"] = Math.Round(objFirePolicy.PolNetLcPrem, 2)

                };

                string query = "UPDATE FIRE_POLICY SET POL_FC_SI = :P_POL_FC_SI, POL_LC_SI = :P_POL_LC_SI, POL_GROSS_FC_PREM = :P_POL_GROSS_FC_PREM, POL_GROSS_LC_PREM = :P_POL_GROSS_LC_PREM, POL_VAT_FC_AMT = :P_POL_VAT_FC_AMT, POL_VAT_LC_AMT = :P_POL_VAT_LC_AMT, POL_NET_FC_PREM = :P_POL_NET_FC_PREM, POL_NET_LC_PREM = :P_POL_NET_LC_PREM WHERE POL_UID = :P_POL_UID";

                int n = DBConnection.ExecuteQuery(paramValues, query);
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public FirePolicy PolicyFetchDetails(FirePolicy objFirePolicy)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_UID"] = objFirePolicy.PolUid

                };

                string query = "SELECT * FROM FIRE_POLICY WHERE POL_UID = :P_POL_UID";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                FirePolicy objFirePolicy1 = new FirePolicy();

                objFirePolicy1.PolUid = objFirePolicy.PolUid;
                objFirePolicy1.PolNo = row["POL_NO"].ToString();
                objFirePolicy1.PolIssDt = Convert.ToDateTime(row["POL_ISS_DT"]);
                objFirePolicy1.PolAssrName = row["POL_ASSR_NAME"].ToString();
                objFirePolicy1.PolAssrAddress = row["POL_ASSR_ADDRESS"].ToString();
                objFirePolicy1.PolAssrEmail = row["POL_ASSR_EMAIL"].ToString();
                //objFirePolicy1.PolAssrDob = DateTime.ParseExact(row["POL_ASSR_DOB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime s;
                //if (string.IsNullOrEmpty(row["POL_ASSR_DOB"].ToString()) ? false : true)
                //{
                //    objFirePolicy1.PolAssrDob = Convert.ToDateTime(row["POL_ASSR_DOB"]);
                //}

                objFirePolicy1.PolAssrDob = row["POL_ASSR_DOB"] as DateTime?;
                objFirePolicy1.PolAssrMobile = row["POL_ASSR_MOBILE"].ToString();
                objFirePolicy1.PolAssrCivilId = row["POL_ASSR_CIVIL_ID"].ToString();
                objFirePolicy1.PolAssrType = row["POL_ASSR_TYPE"].ToString();
                objFirePolicy1.PolAssrOccupation = row["POL_ASSR_OCCUPATION"].ToString();
                objFirePolicy1.PolProdCode = row["POL_PROD_CODE"].ToString();
                objFirePolicy1.PolMultRiskYn = row["POL_MULT_RISK_YN"].ToString();
                objFirePolicy1.PolFmDt = Convert.ToDateTime(string.IsNullOrEmpty(row["POL_FM_DT"].ToString()) ? null : row["POL_FM_DT"].ToString());
                objFirePolicy1.PolToDt = Convert.ToDateTime(string.IsNullOrEmpty(row["POL_TO_DT"].ToString()) ? null : row["POL_TO_DT"].ToString());
                objFirePolicy1.PolSiCurrency = row["POL_SI_CURRENCY"].ToString();
                objFirePolicy1.PolSiCurrRate = Convert.ToDouble(string.IsNullOrEmpty(row["POL_SI_CURR_RATE"].ToString()) ? "0" : row["POL_SI_CURR_RATE"].ToString());
                objFirePolicy1.PolPremCurrency = row["POL_PREM_CURRENCY"].ToString();
                objFirePolicy1.PolPremCurrRate = Convert.ToDouble(string.IsNullOrEmpty(row["POL_PREM_CURR_RATE"].ToString()) ? "0" : row["POL_PREM_CURR_RATE"].ToString());
                objFirePolicy1.PolFcSi = Convert.ToDouble(string.IsNullOrEmpty(row["POL_FC_SI"].ToString()) ? "0" : row["POL_FC_SI"].ToString());
                objFirePolicy1.PolLcSi = Convert.ToDouble(string.IsNullOrEmpty(row["POL_LC_SI"].ToString()) ? "0" : row["POL_LC_SI"].ToString());
                objFirePolicy1.PolGrossFcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["POL_GROSS_FC_PREM"].ToString()) ? "0" : row["POL_GROSS_FC_PREM"].ToString());
                objFirePolicy1.PolGrossLcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["POL_GROSS_LC_PREM"].ToString()) ? "0" : row["POL_GROSS_LC_PREM"].ToString());
                objFirePolicy1.PolVatFcAmt = Convert.ToDouble(string.IsNullOrEmpty(row["POL_VAT_FC_AMT"].ToString()) ? "0" : row["POL_VAT_FC_AMT"].ToString());
                objFirePolicy1.PolVatLcAmt = Convert.ToDouble(string.IsNullOrEmpty(row["POL_VAT_LC_AMT"].ToString()) ? "0" : row["POL_VAT_LC_AMT"].ToString());
                objFirePolicy1.PolNetFcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["POL_NET_FC_PREM"].ToString()) ? "0" : row["POL_NET_FC_PREM"].ToString());
                objFirePolicy1.PolNetLcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["POL_NET_LC_PREM"].ToString()) ? "0" : row["POL_NET_LC_PREM"].ToString());
                objFirePolicy1.PolApprStatus = row["POL_APPR_STATUS"].ToString();

                return objFirePolicy1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool FirePolicySave(FirePolicy objFirePolicy)
        {
            try
            {

                //objFirePolicy.PolUid = GetNextPolUid();


                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_UID"] = objFirePolicy.PolUid,
                    ["P_POL_NO"] = objFirePolicy.PolNo,
                    ["P_POL_ASSR_NAME"] = objFirePolicy.PolAssrName,
                    ["P_POL_ASSR_ADDRESS"] = objFirePolicy.PolAssrAddress,
                    ["P_POL_ASSR_DOB"] = objFirePolicy.PolAssrDob,
                    ["P_POL_ASSR_EMAIL"] = objFirePolicy.PolAssrEmail,
                    ["P_POL_ASSR_MOBILE"] = objFirePolicy.PolAssrMobile,
                    ["P_POL_ASSR_CIVIL_ID"] = objFirePolicy.PolAssrCivilId,
                    ["P_POL_ASSR_TYPE"] = objFirePolicy.PolAssrType,
                    ["P_POL_ASSR_OCCUPATION"] = objFirePolicy.PolAssrOccupation,
                    ["P_POL_PROD_CODE"] = objFirePolicy.PolProdCode,
                    ["P_POL_MULTI_RISK"] = objFirePolicy.PolMultRiskYn,
                    ["P_POL_FM_DT"] = objFirePolicy.PolFmDt,
                    ["P_POL_SI_CURRENCY"] = objFirePolicy.PolSiCurrency,
                    ["P_POL_PREM_CURRENCY"] = objFirePolicy.PolPremCurrency,
                    ["P_POL_ISS_DT"] = objFirePolicy.PolIssDt,
                    ["P_POL_SI_CURR_RATE"] = objFirePolicy.PolSiCurrRate,
                    ["P_POL_PREM_CURR_RATE"] = objFirePolicy.PolPremCurrRate,
                    ["P_POL_TO_DT"] = objFirePolicy.PolToDt,
                    ["P_POL_CR_BY"] = objFirePolicy.PolCrBy,
                    ["P_POL_CR_DT"] = objFirePolicy.PolCrDt,
                    ["P_POL_APPR_STATUS"] = objFirePolicy.PolApprStatus


                };
                string query2;

                query2 = "INSERT INTO FIRE_POLICY (POL_CR_BY,POL_CR_DT,POL_TO_DT,POL_ISS_DT, POL_UID, POL_ASSR_NAME, POL_ASSR_ADDRESS, POL_ASSR_DOB, POL_ASSR_EMAIL, POL_ASSR_MOBILE, POL_ASSR_CIVIL_ID, POL_ASSR_TYPE, POL_ASSR_OCCUPATION, POL_PROD_CODE, POL_MULT_RISK_YN, POL_FM_DT, POL_SI_CURRENCY,POL_SI_CURR_RATE, POL_PREM_CURRENCY,POL_PREM_CURR_RATE, POL_NO, POL_APPR_STATUS) VALUES(:P_POL_CR_BY, :P_POL_CR_DT, TO_DATE(:P_POL_TO_DT,'DD/MM/RRRR'),TO_DATE(:P_POL_ISS_DT,'DD/MM/RRRR'),:P_POL_UID, :P_POL_ASSR_NAME, :P_POL_ASSR_ADDRESS, TO_DATE(:P_POL_ASSR_DOB,'DD/MM/RRRR'), :P_POL_ASSR_EMAIL, :P_POL_ASSR_MOBILE, :P_POL_ASSR_CIVIL_ID, :P_POL_ASSR_TYPE, :P_POL_ASSR_OCCUPATION, :P_POL_PROD_CODE, :P_POL_MULTI_RISK, TO_DATE(:P_POL_FM_DT,'DD/MM/RRRR'), :P_POL_SI_CURRENCY,:P_POL_SI_CURR_RATE, :P_POL_PREM_CURRENCY,:P_POL_PREM_CURR_RATE, :P_POL_NO, :P_POL_APPR_STATUS)";

                int n = DBConnection.ExecuteQuery(paramValues, query2);
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ApproveStatusToDB(int pRiskPolUid, string pUser)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_POL_UID"] = pRiskPolUid,
                    ["P_POL_APPR_STATUS"] = "A",
                    ["P_POL_APPR_DT"] = DateTime.Now,
                    ["P_POL_APPR_BY"] = pUser
                };

                string query = "UPDATE FIRE_POLICY SET POL_APPR_STATUS = :P_POL_APPR_STATUS, POL_APPR_DT = :P_POL_APPR_DT, POL_APPR_BY = :P_POL_APPR_BY WHERE POL_UID = :P_POL_UID";

                int n = DBConnection.ExecuteQuery(paramValues, query);
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Listing_Details(string pPolUid)
        {
            DataTable result = null;

            //string query = $"SELECT * FROM TBL_USER WHERE USR_ID ='{userID}'";
            string query = $"SELECT POL_NO, POL_ASSR_NAME, POL_ASSR_ADDRESS, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = POL_ASSR_TYPE AND CM_TYPE = 'ASSR_TYPE') AS Occupation , POL_FM_DT, POL_TO_DT, POL_LC_SI, POL_GROSS_LC_PREM, POL_VAT_LC_AMT, POL_NET_LC_PREM, RISK_ID, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_CLASS AND CM_TYPE = 'RISK_CLASS') AS Risk_Class,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_OCCUP_TYPE AND CM_TYPE = 'OCCUPANCY') AS Occupancy,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_CONSTR_TYPE AND CM_TYPE = 'CONSTR_TYPE') AS Construction,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = POL_PROD_CODE AND CM_TYPE = 'SCHEME') AS Scheme, RISK_LC_SI FROM FIRE_POLICY JOIN FIRE_POLICY_RISK ON POL_UID = RISK_POL_UID WHERE POL_UID = { pPolUid}";

            result = DBConnection.ExecuteDataset(query);
            return result;
        }

        public DataTable FetchDashboardData()
        {
            try
            {
                DataTable result = null;

                string query = "SELECT COUNT(POL_UID) POLICY_COUNT, SUM(POL_LC_SI) SI, SUM(POL_NET_LC_PREM) PREMIUM FROM FIRE_POLICY WHERE POL_APPR_STATUS = 'A'";

                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string TotalPolicyCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM FIRE_POLICY";
                return Convert.ToString(DBConnection.ExecuteScalar(query));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

  
}
