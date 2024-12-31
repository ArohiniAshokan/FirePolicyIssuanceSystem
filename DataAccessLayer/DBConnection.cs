using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DBConnection
    {
        private static string ConnString = GetConnectionString("ConnectionString", string.Empty);
        public static string GetConnectionString(string key, string defaultValue)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(defaultValue))
            {
                value = defaultValue;
            }
            try
            {
                //value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                value = "Data Source=ORCL19C;User ID=E0170;Password=e0170;Unicode=True";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error reading webconfig : " + ex.ToString());
            }
            return value;
        }
        public static OracleConnection OpenConnection()
        {
            try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = ConnString;
                con.Open();
                return con;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {

            }
        }

        public static void CloseConnection(OracleConnection connection)
        {
            try
            {
                if (connection != null)
                    connection.Close();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
            }
        }

        public static int ExecuteQuery(string sql)
        {
            OracleConnection connection = null;
            try
            {
                OracleCommand cmd = new OracleCommand();
                connection = OpenConnection();
                cmd.CommandText = sql;
                cmd.Connection = connection;
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {

                throw err;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        public static DataTable ExecuteDataset(string query)
        {
            OracleCommand cmd;
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd = new OracleCommand(query, connection);
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static object ExecuteScalar(string query)
        {
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                OracleCommand objcmd = new OracleCommand(query, connection);
                object outcount = objcmd.ExecuteScalar();
                return outcount;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static int ExecuteQuery(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            int rval;
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                rval = cmd.ExecuteNonQuery();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return rval;

        }


        public static DataSet ExecuteQuerySelect(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            DataSet Dset = new DataSet();
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return Dset;

        }

        public static int ApprovingBid(string pVbUid)
        {
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection(); 
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection; cmd.CommandText = "DPRC_VEH_AUCTION_BIDDING"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_VAB_UID", OracleType.VarChar).Value = pVbUid;
                cmd.Parameters.Add("P_STATUS", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int op = Convert.ToInt32(cmd.Parameters["P_STATUS"].Value.ToString());
                return op;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string ExecuteFunction(string year, string prodCode, string polUid)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd.CommandText = "DFUN_GENERATE_POLICY_NUMBER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_YEAR", OracleType.VarChar).Value = year;
                cmd.Parameters.Add("P_PRODUCT_CODE", OracleType.VarChar).Value = prodCode;
                cmd.Parameters.Add("P_UNIQUE_NUMBER", OracleType.VarChar).Value = polUid;
                //cmd.Parameters.Add("P_POL_NO", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
                OracleParameter outputParam = new OracleParameter("P_POL_NO", OracleType.VarChar, 24);
                outputParam.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(outputParam);
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                string policyId = outputParam.Value.ToString();

                return policyId;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }


        }

        public static (int newPolUid, int status, string errMsg) ExecuteCopyPolicyProcedure(int originalPolUid)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd.CommandText = "DPRC_COPY_POLICY";
                cmd.CommandType = CommandType.StoredProcedure;

                // Define input parameter
                cmd.Parameters.Add("P_POL_UID", OracleType.Int32).Value = originalPolUid;

                // Output parameters
                cmd.Parameters.Add("P_NEW_POL_UID", OracleType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_STATUS", OracleType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ERR_MSG", OracleType.VarChar, 200).Direction = ParameterDirection.Output;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                // Retrieve output parameters
                int newPolUid = Convert.ToInt32(cmd.Parameters["P_NEW_POL_UID"].Value);
                int status = Convert.ToInt32(cmd.Parameters["P_STATUS"].Value);
                string errMsg = cmd.Parameters["P_ERR_MSG"].Value.ToString();

                return (newPolUid, status, errMsg);
            }
            catch (Exception ex)
            {
                return (0, 0, "Error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

    }
}

