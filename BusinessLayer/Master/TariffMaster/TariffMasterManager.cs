using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class TariffMasterManager
    {
        public int GetNextTmUid()
        {
            try
            {
                string query = "SELECT TmUidSeq.NEXTVAL FROM DUAL";
                return Convert.ToInt32(DBConnection.ExecuteScalar(query));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable TariffMasterGridBind(int pRiskClass, int pRiskOccup, double? pSI)
        {
            try
            {
                
                DataTable result = null;
               
                string query = $"SELECT TM_UID,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_FM AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_CLASS_TO AND CM_TYPE = 'RISK_CLASS') AS RISK_CLASS_TO,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_FM AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_FM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = TM_RISK_OCCP_TO AND CM_TYPE = 'OCCUPANCY') AS RISK_OCCP_TO, TM_RISK_SI_FM, TM_RISK_SI_TO, TM_RISK_RATE FROM TARIFF_MASTER WHERE 1=1  ";

                //if (pRiskClass != 0)
                //{
                //    query = query.Replace("1=1", "{pRiskClass} BETWEEN TM_RISK_CLASS_FM AND TM_RISK_CLASS_TO AND 1=1");

                //}

                //if (pRiskOccup != 0)
                //{
                //    query = query.Replace("1=1", "{pRiskOccup} BETWEEN TM_RISK_OCCP_FM AND TM_RISK_OCCP_TO AND 1=1");

                //}

                //if (pSI != 0)
                //{
                //    query = query.Replace("1=1", "{pSI} BETWEEN TM_RISK_SI_FM AND TM_RISK_SI_TO");

                //}

                if (pRiskClass != 0)
                {
                    query += $" AND {pRiskClass} BETWEEN TM_RISK_CLASS_FM AND TM_RISK_CLASS_TO";
                }

                if (pRiskOccup != 0)
                {
                    query += $" AND {pRiskOccup} BETWEEN TM_RISK_OCCP_FM AND TM_RISK_OCCP_TO";
                }

                if (pSI.HasValue && pSI.Value != 0)
                {
                    query += $" AND {pSI.Value} BETWEEN TM_RISK_SI_FM AND TM_RISK_SI_TO";
                }

                
                query += " ORDER BY TM_UID";

                result = DBConnection.ExecuteDataset(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteFromTariffMaster(string pTmUid)
        {
            try
            {
                string query = $"DELETE FROM TARIFF_MASTER WHERE TM_UID = '{pTmUid}'";

                int n = DBConnection.ExecuteQuery(query);
                return n;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TariffMaster FetchDetails(TariffMaster objTariffMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_TM_UID"] = objTariffMaster.TmUid,

                };

                string query = "SELECT TM_RISK_CLASS_FM,TM_RISK_CLASS_TO, TM_RISK_OCCP_FM, TM_RISK_OCCP_TO, TM_RISK_SI_FM, TM_RISK_SI_TO, TM_RISK_RATE FROM TARIFF_MASTER WHERE TM_UID = :P_TM_UID";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                TariffMaster objTariffMaster1 = new TariffMaster();
                objTariffMaster1.TmUid = objTariffMaster.TmUid;
                objTariffMaster1.TmRiskClassFm = row["TM_RISK_CLASS_FM"].ToString();
                objTariffMaster1.TmRiskClassTo = row["TM_RISK_CLASS_TO"].ToString();
                objTariffMaster1.TmOccFm = row["TM_RISK_OCCP_FM"].ToString();
                objTariffMaster1.TmOccTo = row["TM_RISK_OCCP_TO"].ToString();
                objTariffMaster1.TmSiFm = Convert.ToDouble(row["TM_RISK_SI_FM"].ToString());
                objTariffMaster1.TmSiTo = Convert.ToDouble(row["TM_RISK_SI_TO"].ToString());
                objTariffMaster1.TmRiskRate = Convert.ToDouble(row["TM_RISK_RATE"].ToString());

                return objTariffMaster1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool TariffMasterSave(TariffMaster objTariffMaster)
        {
            try
            {
                //objTariffMaster.TmUid = GetNextTmUid();

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_TM_UID"] = objTariffMaster.TmUid,
                    ["P_TM_RISK_CLASS_FM"] = objTariffMaster.TmRiskClassFm,
                    ["P_TM_RISK_CLASS_TO"] = objTariffMaster.TmRiskClassTo,
                    ["P_TM_RISK_OCCP_FM"] = objTariffMaster.TmOccFm,
                    ["P_TM_RISK_OCCP_TO"] = objTariffMaster.TmOccTo,
                    ["P_TM_RISK_SI_FM"] = Math.Round(objTariffMaster.TmSiFm, 2),
                    ["P_TM_RISK_SI_TO"] = Math.Round(objTariffMaster.TmSiTo, 2),
                    ["P_TM_RISK_RATE"] = Math.Round(objTariffMaster.TmRiskRate, 2),
                    ["P_TM_CR_BY"] = objTariffMaster.TmCrBy,
                    ["P_TM_CR_DT"] = objTariffMaster.TmCrDt
                };

                string query = "INSERT INTO TARIFF_MASTER (TM_UID, TM_RISK_CLASS_FM, TM_RISK_CLASS_TO, TM_RISK_OCCP_FM, TM_RISK_OCCP_TO, TM_RISK_SI_FM, TM_RISK_SI_TO, TM_RISK_RATE, TM_CR_BY, TM_CR_DT)" +
                    "VALUES (:P_TM_UID,:P_TM_RISK_CLASS_FM,:P_TM_RISK_CLASS_TO,:P_TM_RISK_OCCP_FM,:P_TM_RISK_OCCP_TO, :P_TM_RISK_SI_FM, :P_TM_RISK_SI_TO, :P_TM_RISK_RATE, :P_TM_CR_BY, :P_TM_CR_DT) ";

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

        public bool CheckTmUidExists(string pTmUid)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM TARIFF_MASTER WHERE TM_UID = '{pTmUid}'";

                object obj = DBConnection.ExecuteScalar(query);

                int n = Convert.ToInt32(obj);
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

        public bool TariffMasterUpdate(TariffMaster objTariffMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_TM_UID"] = objTariffMaster.TmUid,
                    ["P_TM_RISK_CLASS_FM"] = objTariffMaster.TmRiskClassFm,
                    ["P_TM_RISK_CLASS_TO"] = objTariffMaster.TmRiskClassTo,
                    ["P_TM_RISK_OCCP_FM"] = objTariffMaster.TmOccFm,
                    ["P_TM_RISK_OCCP_TO"] = objTariffMaster.TmOccTo,
                    ["P_TM_RISK_SI_FM"] = objTariffMaster.TmSiFm,
                    ["P_TM_RISK_SI_TO"] = objTariffMaster.TmSiTo,
                    ["P_TM_RISK_RATE"] = objTariffMaster.TmRiskRate,
                    ["P_TM_UP_BY"] = objTariffMaster.TmUpBy,
                    ["P_TM_UP_DT"] = objTariffMaster.TmUpDt

                };
              
                string query = "UPDATE TARIFF_MASTER SET TM_RISK_CLASS_FM = :P_TM_RISK_CLASS_FM, TM_RISK_CLASS_TO = :P_TM_RISK_CLASS_TO, TM_RISK_OCCP_FM = :P_TM_RISK_OCCP_FM, TM_RISK_OCCP_TO = :P_TM_RISK_OCCP_TO, TM_RISK_SI_FM = :P_TM_RISK_SI_FM, TM_RISK_SI_TO = :P_TM_RISK_SI_TO, TM_RISK_RATE = :P_TM_RISK_RATE, TM_UP_BY  = :P_TM_UP_BY, TM_UP_DT = :P_TM_UP_DT WHERE TM_UID = :P_TM_UID ";

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
    }
}
