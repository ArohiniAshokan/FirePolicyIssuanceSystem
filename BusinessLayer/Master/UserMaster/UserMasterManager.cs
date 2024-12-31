using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class UserMasterManager
    {
        public bool Login(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_USER_NAME"] = objUserMaster.UserName,
                    ["P_USER_PASSWORD"] = objUserMaster.UserPassword
                };
                string query = $"SELECT 1 FROM USER_MASTER WHERE USER_NAME = :P_USER_NAME AND USER_PASSWORD = :P_USER_PASSWORD AND USER_ACTIVE_YN = 'Y'";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                int rows = dt.Rows.Count;

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUserIDExists(string pUSerId)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM USER_MASTER WHERE USER_ID = '{pUSerId}'";

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

        public bool UserMasterSave(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_USER_ID"] = objUserMaster.UserId,
                    ["P_USER_NAME"] = objUserMaster.UserName,
                    ["P_USER_PASSWORD"] = objUserMaster.UserPassword,
                    ["P_USER_ACTIVE_YN"] = objUserMaster.UserActiveYn,
                    ["P_USER_CR_BY"] = objUserMaster.UserCrBy,
                    ["P_USER_CR_DT"] = objUserMaster.UserCrDt
                };

                string query = "INSERT INTO USER_MASTER (USER_ID, USER_NAME, USER_PASSWORD, USER_ACTIVE_YN, USER_CR_BY, USER_CR_DT)" +
                    "VALUES (:P_USER_ID,:P_USER_NAME,:P_USER_PASSWORD,:P_USER_ACTIVE_YN,:P_USER_CR_BY, :P_USER_CR_DT) ";

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

        public UserMaster FetchDetails(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_USER_ID"] = objUserMaster.UserId,

                };

                string query = "SELECT USER_NAME,USER_PASSWORD, USER_ACTIVE_YN FROM USER_MASTER WHERE USER_ID = :P_USER_ID";

                DataSet ds = DBConnection.ExecuteQuerySelect(paramValues, query);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                UserMaster objUserMaster1 = new UserMaster();
                objUserMaster1.UserId = objUserMaster.UserId;
                objUserMaster1.UserPassword = row["USER_PASSWORD"].ToString();
                objUserMaster1.UserActiveYn = row["USER_ACTIVE_YN"].ToString();
                objUserMaster1.UserName = row["USER_NAME"].ToString();

                return objUserMaster1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UserMasterUpdate(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_USER_ID"] = objUserMaster.UserId,
                    ["P_USER_NAME"] = objUserMaster.UserName,
                    ["P_USER_PASSWORD"] = objUserMaster.UserPassword,
                    ["P_USER_ACTIVE_YN"] = objUserMaster.UserActiveYn,
                    ["P_USER_UP_BY"] = objUserMaster.UserUpBy,
                    ["P_USER_UP_DT"] = objUserMaster.UserUpDt

                };

                string query = "UPDATE USER_MASTER SET USER_NAME = :P_USER_NAME, USER_PASSWORD = :P_USER_PASSWORD, USER_ACTIVE_YN = :P_USER_ACTIVE_YN, USER_UP_BY = :P_USER_UP_BY, USER_UP_DT = :P_USER_UP_DT WHERE USER_ID = :P_USER_ID ";

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

        public DataTable UserMasterGridBind(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["P_USER_NAME"] = objUserMaster.UserName

                };

                DataTable result = null;
                string query = "SELECT USER_ID, USER_NAME, USER_PASSWORD, USER_ACTIVE_YN FROM USER_MASTER WHERE 1=1 ORDER BY USER_NAME";

                if (!string.IsNullOrEmpty(objUserMaster.UserName))
                {
                    query = query.Replace("1=1", "UPPER(USER_NAME) = UPPER(:P_USER_NAME)");

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

        public int DeleteFromUserMaster(string pUserId)
        {
            try
            {
                string query = $"DELETE FROM USER_MASTER WHERE USER_ID = '{pUserId}'";

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
