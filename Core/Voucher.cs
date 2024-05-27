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

namespace HMXHTD.Core
{
    public class Voucher
    {
        #region method getBillToVoucher
        public DataTable getBillToVoucher(string SearchKey)
        {
            string SQL_SEARCH = "";
            DataTable objTable = new DataTable();
            try
            {
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (DeliveryCode LIKE N'%'+@SearchKey+'%'  OR DriverName LIKE N'%'+@SearchKey+'%' OR Vehicle LIKE N'%'+@SearchKey+'%' OR NameDistributor LIKE N'%'+@SearchKey+'%' OR NameProduct LIKE N'%'+@SearchKey+'%') ";
                }
                string SQLQUERY = "SELECT TOP 200 *, 0 AS 'Select' FROM tblStoreOrderOperating WHERE ISNULL(DriverUserName,N'') <> N'' AND Step IN (1,2,3,4,5,6) "+ SQL_SEARCH;
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
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

        #region method getVoucher
        public DataTable getVoucher(DateTime objDate1, DateTime objDate2, int State, string SearchKey)
        {
            string SQL_SEARCH = "", SQL_STATE = "";
            DataTable objTable = new DataTable();
            try
            {
                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (A.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR A.DriverFullName LIKE N'%'+@SearchKey+'%' OR A.Vehicle LIKE N'%'+@SearchKey+'%' OR A.DeliveryCode LIKE N'%'+@SearchKey+'%') ";
                }
                //string SQLQUERY = "SELECT TOP 200 *, 0 AS 'Select' FROM tblDriverVoucher WHERE DayCreate BETWEEN @objDate1 AND @objDate2 " + SQL_SEARCH+ " ORDER BY DayCreate DESC";

                if (State == 1)
                {
                    SQL_STATE = " AND ISNULL(StateUsed,0) = 0 AND ISNULL(Cancel,0) = 0 ";
                }
                else if (State == 2)
                {
                    SQL_STATE = " AND ISNULL(StateUsed,0) = 1 ";
                }
                else if (State == 3)
                {
                    SQL_STATE = " AND ISNULL(Cancel,0) = 1 ";
                }

                string SQLQUERY = "SELECT 0 AS 'Select', A.*, B.TimeConfirm3, B.TimeConfirm7, B.NameDistributor FROM[tblDriverVoucher] A, [tblStoreOrderOperating] B WHERE A.DeliveryCode = B.DeliveryCode AND A.DayCreate BETWEEN @objDate1 AND @objDate2 " + SQL_SEARCH + SQL_STATE + " ORDER BY A.DayCreate DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = new DateTime(objDate1.Year, objDate1.Month, objDate1.Day,0,0,0).AddHours(6);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = new DateTime(objDate2.Year, objDate2.Month, objDate2.Day, 23, 59, 59).AddHours(6);
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

        #region method setData
        public int setData(string DriverUserName, string DriverFullName, string DeliveryCode, string Vehicle)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "INSERT INTO tblDriverVoucher(DriverUserName,DriverFullName,DeliveryCode,Vehicle,UserCreate) VALUES(@DriverUserName,@DriverFullName,@DeliveryCode,@Vehicle,@UserCreate)";
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd.Parameters.Add("DriverFullName", SqlDbType.NVarChar).Value = DriverFullName;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle; 
                Cmd.Parameters.Add("UserCreate", SqlDbType.NVarChar).Value = HMXHTD.frmMain.UserName;
                tmpValue = Cmd.ExecuteNonQuery();
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

        #region method setDataAuto
        public int setDataAuto(string DriverUserName, string DriverFullName, string DeliveryCode, string Vehicle)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQL = "IF NOT EXISTS (SELECT * FROM tblDriverVoucher WHERE DriverUserName = @DriverUserName AND Vehicle = @Vehicle AND ISNULL(StateUsed,0) = 0 AND ISNULL(Cancel,0) = 0 ) ";
                SQL += "BEGIN INSERT INTO tblDriverVoucher(DriverUserName,DriverFullName,DeliveryCode,Vehicle,UserCreate) VALUES(@DriverUserName,@DriverFullName,@DeliveryCode,@Vehicle,@UserCreate) END ";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd.Parameters.Add("DriverFullName", SqlDbType.NVarChar).Value = DriverFullName;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("UserCreate", SqlDbType.NVarChar).Value = HMXHTD.frmMain.UserName;
                tmpValue = Cmd.ExecuteNonQuery();
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

        #region method setDataCancel
        public int setDataCancel(int Id)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "UPDATE tblDriverVoucher SET Cancel = 1, CancelDay = getdate(), UserCancel = @UserCancel WHERE Id = @Id AND ISNULL(Cancel,0) = 0";
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("UserCancel", SqlDbType.NVarChar).Value = HMXHTD.frmMain.UserName;
                tmpValue = Cmd.ExecuteNonQuery();
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

        #region method setDataCancelBySystem
        public int setDataCancelBySystem(int Id)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "UPDATE tblDriverVoucher SET Cancel = 1, CancelDay = getdate(), UserCancel = N'system' WHERE Id = @Id AND ISNULL(Cancel,0) = 0";
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                tmpValue = Cmd.ExecuteNonQuery();
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

        #region method setDataConfig
        public int setDataConfig(bool AutoRelease, int TimeCancel, int Shifts1, int Shifts2, int Shifts3)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "UPDATE tblDriverVoucherConfig SET AutoRelease = @AutoRelease, TimeCancel = @TimeCancel, Shifts1 = @Shifts1, Shifts2 = @Shifts2, Shifts3 = @Shifts3, DayUpdate = getdate(), UserUpdate = @UserUpdate WHERE Id = 1";
                Cmd.Parameters.Add("AutoRelease", SqlDbType.Bit).Value = AutoRelease;
                Cmd.Parameters.Add("TimeCancel", SqlDbType.NVarChar).Value = TimeCancel;
                Cmd.Parameters.Add("Shifts1", SqlDbType.NVarChar).Value = Shifts1;
                Cmd.Parameters.Add("Shifts2", SqlDbType.NVarChar).Value = Shifts2;
                Cmd.Parameters.Add("Shifts3", SqlDbType.NVarChar).Value = Shifts3;
                Cmd.Parameters.Add("UserUpdate", SqlDbType.NVarChar).Value = HMXHTD.frmMain.UserName;
                tmpValue = Cmd.ExecuteNonQuery();
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

        #region method getDataConfig
        public DataTable getDataConfig()
        {
            DataTable objTable = new DataTable();
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT * FROM tblDriverVoucherConfig WHERE Id = 1"; ;
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
    }
}
