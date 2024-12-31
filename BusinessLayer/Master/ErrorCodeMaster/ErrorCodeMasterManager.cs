using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ErrorCodeMasterManager
    {
        public string FetchMessage(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_ERR_CODE"] = objErrorCodeMaster.ErrCode
                };
                string query = "SELECT ERR_DESC FROM ERROR_CODE_MASTER WHERE ERR_CODE = :P_ERR_CODE";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                string message = dt.Rows[0]["ERR_DESC"].ToString();

                return message;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ErrorCodeMaster FetchDetails(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_ERR_CODE"] = objErrorCodeMaster.ErrCode,

                };

                string query = "SELECT ERR_DESC,ERR_TYPE FROM ERROR_CODE_MASTER WHERE ERR_CODE = :P_ERR_CODE";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                ErrorCodeMaster objErrorCodeMaster2 = new ErrorCodeMaster();
                objErrorCodeMaster2.ErrCode = objErrorCodeMaster.ErrCode;
                objErrorCodeMaster2.ErrType = row["ERR_TYPE"].ToString();
                objErrorCodeMaster2.ErrDesc = row["ERR_DESC"].ToString();

                return objErrorCodeMaster2;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ErrorCodeMasterSave(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_ERR_CODE"] = objErrorCodeMaster.ErrCode,
                    ["P_ERR_TYPE"] = objErrorCodeMaster.ErrType,
                    ["P_ERR_DESC"] = objErrorCodeMaster.ErrDesc,
                    ["P_ERR_CR_BY"] = objErrorCodeMaster.ErrCrBy,
                    ["P_ERR_CR_DT"] = objErrorCodeMaster.ErrCrDt
                };

                string query = "INSERT INTO ERROR_CODE_MASTER (ERR_CODE,ERR_TYPE,ERR_DESC,ERR_CR_BY,ERR_CR_DT)" +
                    "VALUES (:P_ERR_CODE,:P_ERR_TYPE,:P_ERR_DESC,:P_ERR_CR_BY,:P_ERR_CR_DT) ";

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

        public bool ErrorCodeMasterUpdate(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_ERR_CODE"] = objErrorCodeMaster.ErrCode,
                    ["P_ERR_TYPE"] = objErrorCodeMaster.ErrType,
                    ["P_ERR_DESC"] = objErrorCodeMaster.ErrDesc,
                    ["P_ERR_UP_BY"] = objErrorCodeMaster.ErrUpBy,
                    ["P_ERR_UP_DT"] = objErrorCodeMaster.ErrUpDt

                };

                string query = "UPDATE ERROR_CODE_MASTER SET ERR_TYPE = :P_ERR_TYPE, ERR_DESC = :P_ERR_DESC, ERR_UP_BY = :P_ERR_UP_BY, ERR_UP_DT = :P_ERR_UP_DT WHERE ERR_CODE = :P_ERR_CODE ";

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

        public bool CheckCodeTypeExists(string pErrCode)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{pErrCode}'";

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

        public DataTable CodesMasterGridBind(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_ERR_TYPE"] = objErrorCodeMaster.ErrType
                };

                DataTable result = null;
                string query = "SELECT * FROM ERROR_CODE_MASTER WHERE 1=1 ORDER BY ERR_TYPE,ERR_CODE";

                if (!string.IsNullOrEmpty(objErrorCodeMaster.ErrType))
                {
                    query = query.Replace("1=1", "ERR_TYPE = :P_ERR_TYPE");

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

        public int DeleteFromCodeMaster(string pErrCode)
        {
            try
            {
                string query = $"DELETE FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{pErrCode}'";

                int n = DBConnection.ExecuteQuery(query);
                return n;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
