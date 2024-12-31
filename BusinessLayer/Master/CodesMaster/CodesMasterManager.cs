using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class CodesMasterManager
    {
        public bool CodeMasterSave(CodesMaster objCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_CM_CODE"] = objCodesMaster.CmCode,
                    ["P_CM_TYPE"] = objCodesMaster.CmType,
                    ["P_CM_VALUE"] = objCodesMaster.CmValue,
                    ["P_CM_DESC"] = objCodesMaster.CmDesc,
                    ["P_CM_ACTIVE_YN"] = objCodesMaster.CmActiveYn,
                    ["P_CM_CR_BY"] = objCodesMaster.CmCrBy,
                    ["P_CM_CR_DT"] = objCodesMaster.CmCrDt
                };

                string query = "INSERT INTO CODES_MASTER (CM_CODE,CM_TYPE,CM_DESC,CM_VALUE,CM_ACTIVE_YN,CM_CR_BY,CM_CR_DT)" +
                    "VALUES (:P_CM_CODE,:P_CM_TYPE,:P_CM_DESC,:P_CM_VALUE,:P_CM_ACTIVE_YN,:P_CM_CR_BY,:P_CM_CR_DT) ";

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

        public CodesMaster FetchDetails(CodesMaster objCodesMaster1)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_CM_CODE"] = objCodesMaster1.CmCode,
                    ["P_CM_TYPE"] = objCodesMaster1.CmType
                };

                string query = "SELECT CM_DESC,CM_VALUE,CM_ACTIVE_YN FROM CODES_MASTER WHERE CM_CODE = :P_CM_CODE AND CM_TYPE = :P_CM_TYPE";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                CodesMaster objCodesMaster2 = new CodesMaster();
                objCodesMaster2.CmCode = objCodesMaster1.CmCode;
                objCodesMaster2.CmType = objCodesMaster1.CmType;
                objCodesMaster2.CmDesc = row["CM_DESC"].ToString();
                objCodesMaster2.CmValue = row["CM_VALUE"].ToString();
                objCodesMaster2.CmActiveYn = row["CM_ACTIVE_YN"].ToString();

                return objCodesMaster2;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CodeMasterUpdate(CodesMaster objCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_CM_CODE"] = objCodesMaster.CmCode,
                    ["P_CM_TYPE"] = objCodesMaster.CmType,
                    ["P_CM_VALUE"] = objCodesMaster.CmValue,
                    ["P_CM_DESC"] = objCodesMaster.CmDesc,
                    ["P_CM_ACTIVE_YN"] = objCodesMaster.CmActiveYn,
                    ["P_CM_UP_BY"] = objCodesMaster.CmUpBy,
                    ["P_CM_UP_DT"] = objCodesMaster.CmUpDt

                };

                string query = "UPDATE CODES_MASTER SET CM_VALUE = :P_CM_VALUE, CM_DESC = :P_CM_DESC, CM_ACTIVE_YN = :P_CM_ACTIVE_YN, CM_UP_BY = :P_CM_UP_BY, CM_UP_DT = :P_CM_UP_DT WHERE CM_CODE = :P_CM_CODE AND CM_TYPE = :P_CM_TYPE ";

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

        public int DeleteFromCodeMaster(string pCmCode, string pCmType)
        {
            try
            {
                string query = $"DELETE FROM CODES_MASTER WHERE CM_CODE = '{pCmCode}' AND CM_TYPE = '{pCmType}'";

                int n = DBConnection.ExecuteQuery(query);
                return n;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable CodesMasterGridBind(CodesMaster objCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {

                    ["P_CM_TYPE"] = objCodesMaster.CmType
                };

                DataTable result = null;
                string query = "SELECT * FROM CODES_MASTER WHERE 1=1 ORDER BY CM_TYPE, CM_CODE";

                if (!string.IsNullOrEmpty(objCodesMaster.CmType))
                {
                    query = query.Replace("1=1", "CM_TYPE = :P_CM_TYPE");

                }

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues,query);
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckCodeTypeExists(string pCmCode, string pCmType)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM CODES_MASTER WHERE CM_CODE = '{pCmCode}' AND CM_TYPE = '{pCmType}'";

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

        public DataTable DropDownFilling(CodesMaster ObjCodesMaster)
        {
            try
            {
                string query = $"SELECT CM_CODE CODE, CM_CODE || ' - ' || CM_DESC TEXT FROM CODES_MASTER WHERE CM_TYPE = '{ObjCodesMaster.CmType}' AND CM_ACTIVE_YN = 'Y' ORDER BY CM_CODE";

                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DropDownFillingCM()
        {
            try
            {
                string query = $"SELECT DISTINCT(CM_TYPE) TYPE FROM CODES_MASTER";

                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string FetchCurrencyValue(CodesMaster objcodesMaster)
        {
            try
            {
                string query = $"SELECT CM_VALUE FROM CODES_MASTER WHERE CM_CODE = '{objcodesMaster.CmCode}' AND CM_TYPE = '{objcodesMaster.CmType}'";

                object obj = DBConnection.ExecuteScalar(query);
                string cmValue = Convert.ToString(obj);
                return cmValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public double ReturnVal(string pCmCode, string pCmType)
        {
            try
            {
                string query = $"SELECT CM_VALUE FROM CODES_MASTER WHERE CM_TYPE = '{pCmType}' AND CM_CODE = '{pCmCode}'";

                object obj = DBConnection.ExecuteScalar(query);
                return (Convert.ToDouble(obj));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
