using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    public partial class DataUnit
    {
        #region ExeSQLQuery
        public static bool ExeSQLQuery(string sqlQuery)
        {
            bool result = false;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    result = true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region ExeProcedure
        public static bool ExeProcedure(string spName, System.Data.SqlClient.SqlParameter[] param)
        {
            bool result = false;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                            cmd.Parameters.AddRange(param);
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                    result = true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region GetDataWithTotalRecord
        public static DataTable GetDataWithTotalRecord(string spName, SqlParameter[] param, out int totalRecord, string parameterOutPut)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            DataTable result = null;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                            cmd.Parameters.AddRange(param);
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        result = ds.Tables[0];
                        totalRecord = Convert.ToInt32(cmd.Parameters[parameterOutPut].Value);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region GetData (By StoreProcedure)
        public static DataTable GetData(string spName, SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            DataTable result = null;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                            cmd.Parameters.AddRange(param);
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        result = ds.Tables[0];
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region GetData (By SQLQuery)
        public static DataTable GetData(string sqlQuery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            DataTable result = null;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        result = ds.Tables[0];
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region GetFieldValue (By table name, column name and condition)
        public static string GetFieldValue(string tableName, string columnName, string condition)
        {
            string result = "";
            string sqlQuery = "select top 1 " + columnName + " from " + tableName + " where " + condition;
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteScalar()?.ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return result;
            }
        }
        #endregion

        #region GetFieldValue (By SQLQuery)
        public static string GetFieldValue(string sqlQuery)
        {
            string result = "";
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return result;
            }
        }
        #endregion

        #region GetFieldValue (By StoreProcedure)
        public static string GetFieldValue(string spName, SqlParameter[] param)
        {
            string result = "";
            using (SqlConnection cnn = new SqlConnection(TVSOracle.SQL_Con))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(spName, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                            cmd.Parameters.AddRange(param);
                        result = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return result;
            }
        }
        #endregion

        #region Method Create By AnhLT-10/11/2020
        /// <summary>
        /// Brings a SqlCommand object to be able to add some parameters in it. After you send this to Execute method.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlCommand GetCommand(string sql)
        {
            SqlConnection conn = new SqlConnection(TVSOracle.SQL_Con);
            SqlCommand sqlCmd = new SqlCommand(sql, conn);
            return sqlCmd;
        }

        /// <summary>
        /// Execute Insert Update
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlCommand command)
        {
            command.Connection.Open();
            int result = command.ExecuteNonQuery();
            command.Connection.Close();
            return result;
        }
        #endregion
    }
}
