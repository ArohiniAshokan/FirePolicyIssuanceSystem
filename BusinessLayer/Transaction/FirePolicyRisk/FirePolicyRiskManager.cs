using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class FirePolicyRiskManager
    {
        public double? GetPremiumRate(FirePolicyRisk objFirePolicyRisk)
        {
            try
            {
                string query = $"SELECT TM_RISK_RATE FROM TARIFF_MASTER WHERE '{objFirePolicyRisk.RiskClass}' BETWEEN TM_RISK_CLASS_FM AND TM_RISK_CLASS_TO AND '{objFirePolicyRisk.RiskOccupType}' BETWEEN TM_RISK_OCCP_FM AND TM_RISK_OCCP_TO AND {objFirePolicyRisk.RiskLcSi} BETWEEN TM_RISK_SI_FM AND TM_RISK_SI_TO";

                object obj = DBConnection.ExecuteScalar(query);

                return obj == null ? (double?)null : Convert.ToDouble(obj);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetNextRiskUid()
        {
            try
            {
                string query = "SELECT RiskUidSeq.NEXTVAL FROM DUAL";
                return Convert.ToInt32(DBConnection.ExecuteScalar(query));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool FirePolicyRiskSave(FirePolicyRisk objFirePolicyRisk)
        {
            try
            {
                objFirePolicyRisk.RiskUid = GetNextRiskUid();

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_RISK_UID"] = objFirePolicyRisk.RiskUid,
                    ["P_RISK_POL_UID"] = objFirePolicyRisk.RiskPolUid,
                    ["P_RISK_CLASS"] = objFirePolicyRisk.RiskClass,
                    ["P_RISK_OCCUP_TYPE"] = objFirePolicyRisk.RiskOccupType,
                    ["P_RISK_CONSTR_TYPE"] = objFirePolicyRisk.RiskConstrType,
                    ["P_RISK_LOCATION"] = objFirePolicyRisk.RiskLocation,
                    ["P_RISK_DESC"] = objFirePolicyRisk.RiskDesc,
                    ["P_RISK_FC_SI"] = Math.Round(objFirePolicyRisk.RiskFcSi, 2),
                    ["P_RISK_LC_SI"] = Math.Round(objFirePolicyRisk.RiskLcSi, 2),
                    ["P_RISK_PREM_RATE"] = Math.Round(objFirePolicyRisk.RiskPremRate, 2),
                    ["P_RISK_FC_PREM"] = Math.Round(objFirePolicyRisk.RiskFcPrem, 2),
                    ["P_RISK_LC_PREM"] = Math.Round(objFirePolicyRisk.RiskLcPrem, 2),
                    ["P_RISK_CR_BY"] = objFirePolicyRisk.RiskCrBy,
                    ["P_RISK_CR_DT"] = objFirePolicyRisk.RiskCrDt

                };

                string sqlQuery = "SELECT COALESCE(MAX(RISK_ID), 0) ID FROM FIRE_POLICY_RISK WHERE RISK_POL_UID = :P_RISK_POL_UID";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, sqlQuery);
                DataTable dt = ds.Tables[0];

                string query;
                if (dt.Rows.Count > 0)
                {
                    int riskId = Convert.ToInt32(dt.Rows[0]["ID"].ToString());

                    query = $"INSERT INTO FIRE_POLICY_RISK(RISK_UID, RISK_ID, RISK_POL_UID, RISK_CLASS, RISK_OCCUP_TYPE, RISK_CONSTR_TYPE, RISK_LOCATION, RISK_DESC, RISK_FC_SI, RISK_LC_SI, RISK_PREM_RATE, RISK_FC_PREM, RISK_LC_PREM, RISK_CR_BY, RISK_CR_DT) VALUES (:P_RISK_UID, {riskId + 1}, :P_RISK_POL_UID, :P_RISK_CLASS, :P_RISK_OCCUP_TYPE, :P_RISK_CONSTR_TYPE, :P_RISK_LOCATION, :P_RISK_DESC, :P_RISK_FC_SI, :P_RISK_LC_SI, :P_RISK_PREM_RATE, :P_RISK_FC_PREM, :P_RISK_LC_PREM, :P_RISK_CR_BY, :P_RISK_CR_DT)";
                }
                else
                {
                    query = $"INSERT INTO FIRE_POLICY_RISK(RISK_UID, RISK_ID, RISK_POL_UID, RISK_CLASS, RISK_OCCUP_TYPE, RISK_CONSTR_TYPE, RISK_LOCATION, RISK_DESC, RISK_FC_SI, RISK_LC_SI, RISK_PREM_RATE, RISK_FC_PREM, RISK_LC_PREM, RISK_CR_BY, RISK_CR_DT) VALUES (:P_RISK_UID, {1}, :P_RISK_POL_UID, :P_RISK_CLASS, :P_RISK_OCCUP_TYPE, :P_RISK_CONSTR_TYPE, :P_RISK_LOCATION, :P_RISK_DESC, :P_RISK_FC_SI, :P_RISK_LC_SI, :P_RISK_PREM_RATE, :P_RISK_FC_PREM, :P_RISK_LC_PREM, :P_RISK_CR_BY, :P_RISK_CR_DT)";
                }


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

        public int DeleteFromRisk(string pRiskUid, string pRiskPolUid)
        {
            try
            {
                string query = $"DELETE FROM FIRE_POLICY_RISK WHERE RISK_UID = '{pRiskUid}' AND RISK_POL_UID = '{pRiskPolUid}'";

                int n = DBConnection.ExecuteQuery(query);
                return n;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool FirePolicyRiskUpdate(FirePolicyRisk objFirePolicyRisk)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_RISK_POL_UID"] = objFirePolicyRisk.RiskPolUid,
                    ["P_RISK_UID"] = objFirePolicyRisk.RiskUid,
                    ["P_RISK_CLASS"] = objFirePolicyRisk.RiskClass,
                    ["P_RISK_OCCUP_TYPE"] = objFirePolicyRisk.RiskOccupType,
                    ["P_RISK_CONSTR_TYPE"] = objFirePolicyRisk.RiskConstrType,
                    ["P_RISK_LOCATION"] = objFirePolicyRisk.RiskLocation,
                    ["P_RISK_DESC"] = objFirePolicyRisk.RiskDesc,
                    ["P_RISK_FC_SI"] = Math.Round(objFirePolicyRisk.RiskFcSi,2),
                    ["P_RISK_LC_SI"] = Math.Round(objFirePolicyRisk.RiskLcSi,2),
                    ["P_RISK_PREM_RATE"] = Math.Round(objFirePolicyRisk.RiskPremRate,2),
                    ["P_RISK_FC_PREM"] = Math.Round(objFirePolicyRisk.RiskFcPrem,2),
                    ["P_RISK_LC_PREM"] = Math.Round(objFirePolicyRisk.RiskLcPrem,2),
                    ["P_RISK_UP_BY"] = objFirePolicyRisk.RiskUpBy,
                    ["P_RISK_UP_DT"] = objFirePolicyRisk.RiskUpDt

                };

                string query = "UPDATE FIRE_POLICY_RISK SET RISK_CLASS = :P_RISK_CLASS, RISK_OCCUP_TYPE = :P_RISK_OCCUP_TYPE, RISK_CONSTR_TYPE = :P_RISK_CONSTR_TYPE, RISK_LOCATION = :P_RISK_LOCATION, RISK_DESC = :P_RISK_DESC, RISK_FC_SI = :P_RISK_FC_SI, RISK_LC_SI = :P_RISK_LC_SI, RISK_PREM_RATE = :P_RISK_PREM_RATE, RISK_FC_PREM = :P_RISK_FC_PREM, RISK_LC_PREM = :P_RISK_LC_PREM, RISK_UP_BY = :P_RISK_UP_BY, RISK_UP_DT = :P_RISK_UP_DT WHERE RISK_UID = :P_RISK_UID AND RISK_POL_UID = :P_RISK_POL_UID";

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

        public DataTable RiskDataGridBind(string pRiskPolUid)
        {
            try
            {
                DataTable result = null;
                //string query = $"SELECT * FROM FIRE_POLICY_RISK WHERE RISK_POL_UID = '{pRiskPolUid}' ORDER BY RISK_ID";
                string query = $"SELECT RISK_UID, RISK_ID, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_CLASS AND CM_TYPE = 'RISK_CLASS') R_CLASS, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_OCCUP_TYPE AND CM_TYPE = 'OCCUPANCY') R_OCCUP, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = RISK_CONSTR_TYPE AND CM_TYPE = 'CONSTR_TYPE') R_CONSTR, RISK_FC_SI, RISK_LC_SI, RISK_PREM_RATE, RISK_FC_PREM, RISK_LC_PREM FROM FIRE_POLICY_RISK  WHERE RISK_POL_UID = '{pRiskPolUid}' ORDER BY RISK_ID";

                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public FirePolicyRisk FetchDetails(FirePolicyRisk objFirePolicyRisk)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_RISK_UID"] = objFirePolicyRisk.RiskUid,
                    ["P_RISK_POL_UID"] = objFirePolicyRisk.RiskPolUid
                };

                string query = "SELECT * FROM FIRE_POLICY_RISK WHERE RISK_UID = :P_RISK_UID AND RISK_POL_UID = :P_RISK_POL_UID";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                FirePolicyRisk objFirePolicyRisk1 = new FirePolicyRisk();
                objFirePolicyRisk1.RiskUid = objFirePolicyRisk.RiskUid;
                objFirePolicyRisk1.RiskClass = row["RISK_CLASS"].ToString();
                objFirePolicyRisk1.RiskOccupType = row["RISK_OCCUP_TYPE"].ToString();
                objFirePolicyRisk1.RiskConstrType = row["RISK_CONSTR_TYPE"].ToString();
                objFirePolicyRisk1.RiskLocation = row["RISK_LOCATION"].ToString();
                objFirePolicyRisk1.RiskDesc = row["RISK_DESC"].ToString();
                objFirePolicyRisk1.RiskFcSi = Convert.ToDouble(row["RISK_FC_SI"]);
                objFirePolicyRisk1.RiskLcSi = Convert.ToDouble(row["RISK_LC_SI"]);
                objFirePolicyRisk1.RiskPremRate = Convert.ToDouble(row["RISK_PREM_RATE"]);
                objFirePolicyRisk1.RiskFcPrem = Convert.ToDouble(row["RISK_FC_PREM"]);
                objFirePolicyRisk1.RiskLcPrem = Convert.ToDouble(row["RISK_LC_PREM"]);

                return objFirePolicyRisk1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public FirePolicy FetchRiskPremiumDetails(string pRiskPolUid)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_RISK_POL_UID"] = pRiskPolUid
                };

                string query = "SELECT SUM(RISK_FC_SI) FC_SI, SUM(RISK_LC_SI) LC_SI, SUM(RISK_FC_PREM) FC_PREM, SUM(RISK_LC_PREM) LC_PREM FROM FIRE_POLICY_RISK WHERE RISK_POL_UID = :P_RISK_POL_UID";

                FirePolicy objFirePolicy = new FirePolicy();

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                
                objFirePolicy.PolFcSi = Convert.ToDouble(string.IsNullOrEmpty(row["FC_SI"].ToString()) ? "0" : row["FC_SI"].ToString());
                objFirePolicy.PolLcSi = Convert.ToDouble(string.IsNullOrEmpty(row["LC_SI"].ToString()) ? "0" : row["LC_SI"].ToString());
                objFirePolicy.PolGrossFcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["FC_PREM"].ToString()) ? "0" : row["FC_PREM"].ToString());
                objFirePolicy.PolGrossLcPrem = Convert.ToDouble(string.IsNullOrEmpty(row["LC_PREM"].ToString()) ? "0" : row["LC_PREM"].ToString());

                return objFirePolicy;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int RiskRowCount(int riskPolUid)
        {
            try
            {

                string query = $"SELECT COUNT(*) FROM FIRE_POLICY_RISK WHERE RISK_POL_UID = {riskPolUid}";

                int n = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return n;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
