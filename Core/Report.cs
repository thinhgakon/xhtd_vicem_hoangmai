using HMXHTD.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace HMXHTD.Core
{
    class Report
    {
        #region method getBarieLog
        public DataTable getBarieLog(DateTime objDate1, DateTime objDate2, string BarieName)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQL_BarieName = "";
                if (BarieName != "" && BarieName.Trim() != "Chọn Barie")
                {
                    SQL_BarieName = " AND A.BarieName = @BarieName ";
                }
                string SQLQUERY = "SELECT A.*, B.FullName FROM tblBarieLog A, tblAccount B WHERE A.UserAction = B.UserName AND A.TimeAction BETWEEN @objDate1 AND @objDate2 "+ SQL_BarieName + " ORDER BY A.TimeAction DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day,0,0,0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("BarieName",SqlDbType.NVarChar).Value = BarieName;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getConfirm
        public DataTable getConfirm(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQL_SEARCH = "";
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (DeliveryCode LIKE N'%'+@SearchKey+'%'  OR DriverName LIKE N'%'+@SearchKey+'%' OR Vehicle LIKE N'%'+@SearchKey+'%' OR NameDistributor LIKE N'%'+@SearchKey+'%' OR NameProduct LIKE N'%'+@SearchKey+'%') ";
                }
                string SQLQUERY = "SELECT Id, OrderDate, DeliveryCode, Vehicle, DriverName, NameDistributor, NameProduct, NameStore, SumNumber, Confirm1, TimeConfirm1, Confirm2, DriverAccept, TimeConfirm5, TimeConfirm6, TimeConfirm7, TimeConfirm8, TimeConfirm9 FROM tblStoreOrderOperating WHERE ISNULL(Confirm1,0) <> 0 AND TimeConfirm1 BETWEEN @objDate1 AND @objDate2 "+ SQL_SEARCH + " ORDER BY TimeConfirm1 DESC";
                //string SQLQUERY = "SELECT Id, OrderDate, DeliveryCode, Vehicle, DriverName, NameDistributor, NameProduct, NameStore, SumNumber, Confirm1, TimeConfirm1, Confirm2, DriverAccept, TimeConfirm5, TimeConfirm6, TimeConfirm7, TimeConfirm8, TimeConfirm9 FROM tblStoreOrderOperating WHERE 1 = 1 " + SQL_SEARCH + " ORDER BY TimeConfirm1 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 0, 0, 0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getInOut
        public DataTable getInOut(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQL_SEARCH = "";
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (DeliveryCode LIKE N'%'+@SearchKey+'%'  OR DriverName LIKE N'%'+@SearchKey+'%' OR Vehicle LIKE N'%'+@SearchKey+'%' OR NameDistributor LIKE N'%'+@SearchKey+'%' OR NameProduct LIKE N'%'+@SearchKey+'%') ";
                }
                string SQLQUERY = "SELECT Id, OrderDate, DeliveryCode, Vehicle, DriverName, NameDistributor, NameProduct, NameStore, SumNumber, TimeConfirm2, TimeConfirm8 FROM tblStoreOrderOperating WHERE ISNULL(Confirm2,0) <> 0 AND TimeConfirm2 BETWEEN @objDate1 AND @objDate2 "+ SQL_SEARCH + " ORDER BY TimeConfirm2 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 0, 0, 0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getScale
        public DataTable getScale(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            string SQL_SEARCH = "";
            DataTable objTable = new DataTable();
            try
            {
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (Vehicle LIKE N'%'+@SearchKey+'%' OR DeliveryCode LIKE N'%'+@SearchKey+'%' OR DriverName LIKE N'%'+@SearchKey+'%'OR NameProduct LIKE N'%'+@SearchKey+'%') ";
                }
                string SQLQUERY = "SELECT Id, OrderDate, DeliveryCode, Vehicle, DriverName, NameDistributor, NameProduct, NameStore, SumNumber, TimeConfirm3, TimeConfirm7 FROM tblStoreOrderOperating WHERE ISNULL(Confirm3,0) <> 0 AND TimeConfirm3 BETWEEN @objDate1 AND @objDate2 " + SQL_SEARCH + " ORDER BY TimeConfirm3 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 0, 0, 0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getRelease
        public DataTable getRelease(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQL_SEARCH = "";
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (Vehicle LIKE N'%'+@SearchKey+'%' OR DeliveryCode LIKE N'%'+@SearchKey+'%' OR DriverName LIKE N'%'+@SearchKey+'%'OR NameProduct LIKE N'%'+@SearchKey+'%') ";
                }
                string SQLQUERY = "SELECT Id, OrderDate, DeliveryCode, Vehicle, DriverName, NameDistributor, NameProduct, NameStore, SumNumber, TimeConfirm5, TimeConfirm6 FROM tblStoreOrderOperating WHERE ISNULL(Confirm5,0) <> 0 AND TimeConfirm5 BETWEEN @objDate1 AND @objDate2 " + SQL_SEARCH + " ORDER BY TimeConfirm5 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 0, 0, 0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getDriver
        public DataTable getDriver(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            string SQL_FULLNAME = "";
            if (SearchKey.Trim() != "")
            {
                SQL_FULLNAME = " AND UPPER(Full_Name) LIKE N'%' || UPPER(:SearchKey) || '%' ";
            }
            DataTable objTable = new DataTable();
            #region ORC
            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT User_Name, Full_Name, VEHICLE_LIST, 0 AS CountOrder, 0 AS TotalNumber FROM ACCOUNT WHERE TYPE_ID = 4 " + SQL_FULLNAME+ " AND VEHICLE_LIST <> N' ' ORDER BY Full_Name";
                if (SearchKey.Trim() != "")
                {
                    Cmd.Parameters.Add("SearchKey", OracleDbType.NVarchar2).Value = SearchKey;
                }
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                objTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                var str = ex.ToString();
                return null;
            }

            finally
            {
                ORC_CONN.Close();
                ORC_CONN.Dispose();
            }
            #endregion
            //for (int i = 0; i < objTable.Rows.Count; i++)
            //{
            //    objTable.Rows[i]["TotalNumber"] = this.getTotalNumber(objDate1, objDate2, objTable.Rows[i]["User_Name"].ToString());
            //}
            return objTable;
        }
        #endregion

        #region method getCountBill
        public int getCountBill(DateTime objDate1, DateTime objDate2, string UserName)
        {
            int tmpValue = 0;

            string SQLQUERY = "SELECT ISNULL(COUNT(*),0) AS TotalItem FROM tblStoreOrderOperating WHERE ISNULL(Confirm9,0) <> 0 AND OrderDate BETWEEN @objDate1 AND @objDate2 AND DriverUserName = @DriverUserName";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 0, 0, 0);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59);
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = UserName;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                tmpValue = int.Parse(ds.Tables[0].Rows[0]["TotalItem"].ToString());
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method getTotalNumber
        public int getTotalNumber(DateTime objDate1, DateTime objDate2, string UserName)
        {
            int tmpValue = 0;

            string SQLQUERY = "SELECT ISNULL(SUM(SumNumber),0) AS SumNumber FROM tblStoreOrderOperating WHERE ISNULL(Confirm9,0) <> 0 AND OrderDate BETWEEN @objDate1 AND @objDate2 AND DriverUserName = @DriverUserName";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 5, 59, 59);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59).AddDays(6);
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = UserName;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tmpValue = (int)float.Parse(ds.Tables[0].Rows[0]["SumNumber"].ToString());
                }
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method getAllTotalNumber trung test
        public DataTable getAllTotalNumber(DateTime objDate1, DateTime objDate2)
        {
            DataTable objTable = new DataTable();
            string SQLQUERY = "SELECT ISNULL(SUM(SumNumber),0) AS SumNumber, DriverUserName FROM tblStoreOrderOperating WHERE ISNULL(Confirm9,0) <> 0 AND OrderDate BETWEEN @objDate1 AND @objDate2 GROUP BY DriverUserName";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 5, 59, 59);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59).AddDays(6);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return objTable;
        }
        #endregion

        #region method getTotalNumberListOrder
        public DataTable getTotalNumberListOrder(DateTime objDate1, DateTime objDate2, string UserName)
        {
            DataTable objTable = new DataTable();

            string SQLQUERY = "SELECT * FROM tblStoreOrderOperating WHERE ISNULL(Confirm9,0) <> 0 AND OrderDate BETWEEN @objDate1 AND @objDate2 AND DriverUserName = @DriverUserName";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day, 5, 59, 59);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59).AddDays(6);
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = UserName;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return objTable;
        }
        #endregion
    }
}
