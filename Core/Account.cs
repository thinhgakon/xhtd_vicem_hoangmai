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
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;

namespace HMXHTD.Core
{
    class Account
    {
        #region method setData
        public int setData(string UserName, bool SysOperating, bool SysAccount, bool TaskOperating, bool TaskConfirm, bool TaskInOut, bool TaskScale, bool TaskRelease, bool TaskRelease2, bool TaskDbet,
            bool ReportConfirm, bool ReportInOut, bool ReportScale, bool ReportRelease, bool DirTrough, bool DirRFID, bool DirDevice, bool DirVehicle, bool DirDriver, bool DirDriverAccount, int HomePage, bool AdminKCS, bool ViewKCS)
        {
            int tmpValue = 0;

            try
            {
                string SQLQUERY = "IF NOT EXISTS (SELECT * FROM tblAccountOperating WHERE UserName = @UserName) ";
                SQLQUERY += "BEGIN INSERT INTO tblAccountOperating(UserName,SysOperating,SysAccount,TaskOperating,TaskConfirm,TaskInOut,TaskScale,TaskRelease,TaskRelease2,TaskDbet,ReportConfirm,ReportInOut,ReportScale,ReportRelease,DirTrough,DirRFID,DirDevice,DirVehicle,DirDriver,DirDriverAccount,HomePage, AdminKCS, ViewKCS) VALUES(@UserName,@SysOperating,@SysAccount,@TaskOperating,@TaskConfirm,@TaskInOut,@TaskScale,@TaskRelease,@TaskRelease2,@TaskDbet,@ReportConfirm,@ReportInOut,@ReportScale,@ReportRelease,@DirTrough,@DirRFID,@DirDevice,@DirVehicle,@DirDriver,@DirDriverAccount,@HomePage, @AdminKCS, @ViewKCS) END ";
                SQLQUERY += "ELSE BEGIN UPDATE tblAccountOperating SET SysOperating = @SysOperating, SysAccount = @SysAccount, TaskOperating = @TaskOperating, TaskConfirm = @TaskConfirm, TaskInOut = @TaskInOut, TaskScale = @TaskScale, TaskRelease = @TaskRelease, TaskRelease2 = @TaskRelease2, TaskDbet = @TaskDbet, ReportConfirm = @ReportConfirm, ReportInOut = @ReportInOut, ReportScale = @ReportScale, ReportRelease = @ReportRelease, DirTrough = @DirTrough, DirRFID = @DirRFID, DirDevice = @DirDevice, DirVehicle = @DirVehicle, DirDriver = @DirDriver, DirDriverAccount = @DirDriverAccount, HomePage = @HomePage, AdminKCS = @AdminKCS, ViewKCS = @ViewKCS, DayUpdate = getdate() WHERE UserName = @UserName END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("SysOperating", SqlDbType.Bit).Value = SysOperating;
                Cmd.Parameters.Add("SysAccount", SqlDbType.Bit).Value = SysAccount;

                Cmd.Parameters.Add("TaskOperating", SqlDbType.Bit).Value = TaskOperating;
                Cmd.Parameters.Add("TaskConfirm", SqlDbType.Bit).Value = TaskConfirm;
                Cmd.Parameters.Add("TaskInOut", SqlDbType.Bit).Value = TaskInOut;
                Cmd.Parameters.Add("TaskScale", SqlDbType.Bit).Value = TaskScale;
                Cmd.Parameters.Add("TaskRelease", SqlDbType.Bit).Value = TaskRelease;
                Cmd.Parameters.Add("TaskRelease2", SqlDbType.Bit).Value = TaskRelease2;
                Cmd.Parameters.Add("TaskDbet", SqlDbType.Bit).Value = TaskDbet; 

                Cmd.Parameters.Add("ReportConfirm", SqlDbType.Bit).Value = ReportConfirm;
                Cmd.Parameters.Add("ReportInOut", SqlDbType.Bit).Value = ReportInOut;
                Cmd.Parameters.Add("ReportScale", SqlDbType.Bit).Value = ReportScale;
                Cmd.Parameters.Add("ReportRelease", SqlDbType.Bit).Value = ReportRelease;

                Cmd.Parameters.Add("DirTrough", SqlDbType.Bit).Value = DirTrough;
                Cmd.Parameters.Add("DirRFID", SqlDbType.Bit).Value = DirRFID;
                Cmd.Parameters.Add("DirDevice", SqlDbType.Bit).Value = DirDevice;
                Cmd.Parameters.Add("DirVehicle", SqlDbType.Bit).Value = DirVehicle;
                Cmd.Parameters.Add("DirDriver", SqlDbType.Bit).Value = DirDriver;
                Cmd.Parameters.Add("DirDriverAccount", SqlDbType.Bit).Value = DirDriverAccount; 
                Cmd.Parameters.Add("HomePage", SqlDbType.Int).Value = HomePage;

                Cmd.Parameters.Add("AdminKCS", SqlDbType.Int).Value = AdminKCS;
                Cmd.Parameters.Add("ViewKCS", SqlDbType.Int).Value = ViewKCS;

                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method setDataDriverVehicle
        public int setDataDriverVehicle(string UserName, string VehicleList)
        {
            int tmpValue = 0;

            try
            {

                #region Oracle
                string sqlQuery = "";
                VehicleList = VehicleList.Replace(" ","");
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                sqlQuery = "update Account set VEHICLE_LIST = :VEHICLE_LIST where USER_NAME = :USER_NAME";
                using (OracleConnection ORC_CONN = new OracleConnection(strConString))
                {
                    OracleCommand ORC_Cmd = new OracleCommand(sqlQuery, ORC_CONN);
                    ORC_Cmd.CommandTimeout = 10;
                    ORC_CONN.Open();
                    
                    ORC_Cmd.Parameters.Add("VEHICLE_LIST", OracleDbType.NVarchar2).Value = VehicleList.ToUpper();
                    ORC_Cmd.Parameters.Add("USER_NAME", OracleDbType.NVarchar2).Value = UserName;
                    
                    ORC_Cmd.CommandText = sqlQuery;
                    ORC_Cmd.ExecuteNonQuery();
                    ORC_CONN.Close();
                    ORC_CONN.Dispose();
                }
                #endregion


                string SQLQUERY = "UPDATE tblAccount SET LogProcess = CONCAT(LogProcess,N'# Bsx cũ ', VehicleList, N', bsx mới ', @VehicleList , N', Chỉnh sửa bsx lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) ,VehicleList = @VehicleList WHERE UserName = @UserName";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("VehicleList", SqlDbType.NVarChar).Value = VehicleList;
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {

            }
            return tmpValue;
        }
        #endregion

        #region method getData
        public DataTable getData(string SearchKey)
        {
            string QueryFullName = "";
            if (SearchKey.Trim() != "")
            {
                QueryFullName = " AND FULL_NAME LIKE '%'+@SearchKey+'%' ";
                //QueryFullName = " AND FULL_NAME LIKE %'+:SearchKey+'% ";
            }
            DataTable objTable = new DataTable();
            try
            {
                #region SQL
                string SQLQUERY = "SELECT * FROM tblAccount WHERE GroupId = 1 AND TypeId IN (11) " + QueryFullName + " ORDER BY FullName";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                #endregion

                //#region ORCACLE
                //string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                //string sqlQuery = "SELECT * FROM ACCOUNT WHERE GROUP_ID = 1 AND TYPE_ID IN (11) " + QueryFullName + " ORDER BY FULL_NAME";
                //using (OracleConnection ORC_CONN = new OracleConnection(strConString))
                //{
                //    OracleCommand Cmd = new OracleCommand(sqlQuery, ORC_CONN);
                //    ORC_CONN.Open();
                //    Cmd.Parameters.Add("SearchKey", OracleDbType.NVarchar2).Value = SearchKey;
                //    OracleDataAdapter da = new OracleDataAdapter();
                //    da.SelectCommand = Cmd;
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);
                //    objTable = ds.Tables[0];
                //    ORC_CONN.Close();
                //    ORC_CONN.Dispose();
                //}
                //#endregion
            }
            catch 
            {

            }
            return objTable;
        }
        #endregion

        #region method getDataStockInfo
        public DataTable getDataStockInfo(string SearchKey)
        {
            string QueryFullName = "";
            if (SearchKey.Trim() != "")
            {
                //QueryFullName = " AND FULL_NAME LIKE '%'+@SearchKey+'%' ";
                QueryFullName = " AND FULL_NAME LIKE %'+:SearchKey+'% ";
            }
            DataTable objTable = new DataTable();
            try
            {
                #region ORCACLE
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                string sqlQuery = "SELECT * FROM ACCOUNT WHERE TYPE_ID IN (13) " + QueryFullName + " ORDER BY FULL_NAME";
                using (OracleConnection ORC_CONN = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, ORC_CONN);
                    ORC_CONN.Open();
                    Cmd.Parameters.Add("SearchKey", OracleDbType.NVarchar2).Value = SearchKey;
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = Cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    objTable = ds.Tables[0];
                    ORC_CONN.Close();
                    ORC_CONN.Dispose();
                }
                #endregion
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getDataTrough
        public DataTable getDataTrough()
        {            
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT 0 AS 'Select', [Name],[LineCode] FROM [tblTrough] ORDER BY [Name]";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
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

        #region method getDataByUserName
        public DataTable getDataByUserName(string UserName)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblAccountOperating WHERE UserName = @UserName";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.CommandText = SQLQUERY;
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

        #region method getAccountOrderLineCode
        public bool getAccountOrderLineCode(string UserName, string LineCode)
        {
            bool tmpValue = false;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT * FROM tblAccountOrderLineCode WHERE UserName = @UserName AND LineCode = @LineCode";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("LineCode", SqlDbType.NVarChar).Value = LineCode;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = true;
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
                tmpValue = false;
            }
            return tmpValue;
        }
        #endregion

        #region method setAccountOrderLineCode
        public int setAccountOrderLineCode(string UserName, string LineCode)
        {
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "INSERT INTO tblAccountOrderLineCode(UserName,LineCode) VALUES(@UserName,@LineCode)";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("LineCode", SqlDbType.NVarChar).Value = LineCode;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
                tmpValue = 0;
            }
            return tmpValue;
        }
        #endregion

        #region method delAccountOrderLineCode
        public void delAccountOrderLineCode(string UserName)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "DELETE tblAccountOrderLineCode WHERE UserName = @UserName";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region method getAccountOrderTypeProduct
        public bool getAccountOrderTypeProduct(string UserName, string TypeProduct)
        {
            bool tmpValue = false;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT * FROM tblAccountOrderTypeProduct WHERE UserName = @UserName AND TypeProduct = @TypeProduct";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = true;
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
                tmpValue = false;
            }
            return tmpValue;
        }
        #endregion

        #region method setAccountOrderTypeProduct
        public int setAccountOrderTypeProduct(string UserName, string TypeProduct)
        {
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "INSERT INTO tblAccountOrderTypeProduct(UserName,TypeProduct) VALUES(@UserName,@TypeProduct)";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
                tmpValue = 0;
            }
            return tmpValue;
        }
        #endregion

        #region method delAccountOrderTypeProduct
        public void delAccountOrderTypeProduct(string UserName)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "DELETE tblAccountOrderTypeProduct WHERE UserName = @UserName";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region method login
        public bool login(string UserName, string PassWord, ref string FullName)
        {
            bool tmpValue = false;
            try
            {
                #region SQL
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT UserName, PassWord, FullName FROM tblAccount WHERE UserName = @UserName AND PassWord = @PassWord AND ISNULL(State,0) = 1";
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = this.CryptographyMD5(PassWord);
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = true;
                    FullName = Rd["FullName"].ToString();
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
                #endregion

                //#region Oracle
                //string sqlQuery = "";
                //string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                //sqlQuery = "SELECT USER_NAME, PASSWORD, FULL_NAME FROM ACCOUNT WHERE USER_NAME = :UserName AND PASSWORD = :PassWord AND STATUS = 1";
                //using (OracleConnection connection = new OracleConnection(strConString))
                //{
                //    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                //    connection.Open();
                //    Cmd.Parameters.Add("UserName", OracleDbType.Varchar2).Value = UserName;
                //    Cmd.Parameters.Add("PassWord", OracleDbType.NVarchar2).Value = this.CryptographyMD5(PassWord);
                //    using (OracleDataReader Rd = Cmd.ExecuteReader())
                //    {
                //        while (Rd.Read())
                //        {
                //            tmpValue = true;
                //            FullName = Rd["FULL_NAME"].ToString();
                //        }
                //    }
                //}
                //#endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tmpValue;
        }
        #endregion

        #region method CryptographyMD5
        public string CryptographyMD5(string source)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider objMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] bytHash = objMD5.ComputeHash(buffer);
            string result = "";
            foreach (byte a in bytHash)
            {
                result += int.Parse(a.ToString(), System.Globalization.NumberStyles.HexNumber).ToString();
            }
            return result;
        }
        #endregion
    }
}
