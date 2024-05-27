using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace HMXHTD.Core
{
    class Driver
    {
        #region method setData
        public int setData(int Id, string FullName, string Phone, string IdCard, string Address, string UserName)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "IF NOT EXISTS (SELECT * FROM tblDriver WHERE Id = @Id) ";
                SQLQUERY += "BEGIN INSERT INTO tblDriver(FullName,Phone,IdCard,Address,UserName) VALUES(@FullName,@Phone,@IdCard,@Address,@UserName) END ";
                SQLQUERY += "ELSE BEGIN UPDATE tblDriver SET FullName = @FullName, Phone = @Phone, IdCard = @IdCard, Address = @Address WHERE Id = @Id END ";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("FullName", SqlDbType.NVarChar).Value = FullName;
                Cmd.Parameters.Add("Phone", SqlDbType.NVarChar).Value = Phone;
                Cmd.Parameters.Add("IdCard", SqlDbType.NVarChar).Value = IdCard;
                Cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = Address;
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.CommandText = SQLQUERY;
                tmpValue = Cmd.ExecuteNonQuery();

                #region Tạo tài khoản cho lái xe
                try
                {
                    if (Id == 0)
                    {
                        if(checkExistAccount(UserName) > 0)
                        {
                            return -2;
                        }

                        #region Oracle
                        string sqlQuery = "";
                        string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                        sqlQuery = "INSERT INTO ACCOUNT(USER_NAME,FULL_NAME,GROUP_ID,TYPE,TYPE_ID,PASSWORD,STATUS) VALUES(:USER_NAME,:FULL_NAME,1,0,4,:PASSWORD,1)";
                        using (OracleConnection ORC_CONN = new OracleConnection(strConString))
                        {
                            OracleCommand ORC_Cmd = new OracleCommand(sqlQuery, ORC_CONN);
                            ORC_CONN.Open();
                            ORC_Cmd.Parameters.Add("USER_NAME", OracleDbType.NVarchar2).Value = UserName;
                            ORC_Cmd.Parameters.Add("FULL_NAME", OracleDbType.NVarchar2).Value = FullName;
                            ORC_Cmd.Parameters.Add("PASSWORD", OracleDbType.NVarchar2).Value = this.CryptographyMD5("123");
                            ORC_Cmd.CommandText = sqlQuery;
                            ORC_Cmd.ExecuteNonQuery();
                            ORC_CONN.Close();
                            ORC_CONN.Dispose();
                        }
                        #endregion

                        int tmpValue1 = 0;
                        SQLQUERY = "INSERT INTO tblAccount(TypeId,TypeName,UserName,FullName,Phone,IdCard,PassWord,State) VALUES(@TypeId,@TypeName,@UserName,@FullName,@Phone,@IdCard,@PassWord,1)";
                        Cmd.Parameters.Add("TypeName", SqlDbType.NVarChar).Value = "Lái xe";
                        Cmd.Parameters.Add("TypeId", SqlDbType.Int).Value = 4;
                        Cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = this.CryptographyMD5("123");
                        Cmd.CommandText = SQLQUERY;
                        tmpValue1 = Cmd.ExecuteNonQuery();

                        this.InsertLocationInfoAccountConfig(UserName, FullName);

                        //if (tmpValue1 > 0)
                        //{
                        //    SQLQUERY = "UPDATE tblDriver SET AccId = ()";
                        //}
                    }
                }
                catch (Exception Ex)
                {
                    tmpValue = -1;
                }
                #endregion
            }
            catch (Exception  Ex)
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
        public int checkExistAccount(string UserName)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
               
                string SQLQUERY = "SELECT COUNT(*) FROM tblAccount WHERE UserName = @UserName";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.CommandText = SQLQUERY;
                tmpValue = (int)Cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        public void InsertLocationInfoAccountConfig(string username, string fullname)
        {
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = @"IF NOT EXISTS ( SELECT  *
                            FROM    tblLocationInfoAccountConfig
                            WHERE   UserName = @UserName )
                            BEGIN
                                INSERT  INTO dbo.tblLocationInfoAccountConfig
                                        ( UserName ,
                                          FullName ,
                                          LastSent ,
                                          Latitude ,
                                          Longitude ,
                                          TypeId ,
                                          Active
                                        )
                                VALUES  ( @UserName ,
                                          @FullName ,
                                          GETDATE() ,
                                          N'' ,
                                          N'' ,
                                          4 ,
                                          1
                                        );
                            END;";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = username;
                Cmd.Parameters.Add("FullName", SqlDbType.NVarChar).Value = fullname;
                Cmd.CommandText = SQLQUERY;
                Cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }
        #region checkAccount

        #endregion

        #region method getData
        public DataTable getData(string SearchKey)
        {
            string QueryFullName = "";
            if (SearchKey.Trim() != "")
            {
                QueryFullName = " AND (A.FullName LIKE N'%'+@SearchKey+'%' OR A.UserName LIKE N'%'+@SearchKey+'%') ";
            }
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT *, ISNULL((SELECT UserName FROM tblAccount WHERE Id = A.AccId),N'') AS UserName1 FROM tblDriver A WHERE ISNULL(A.FullName,N'') <> N'' "+ QueryFullName + " ORDER BY A.FullName ";
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
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getDataAsDriver
        public DataTable getDataAsDriver(string SearchKey)
        {
            string SQL_FULLNAME = "";
            if (SearchKey.Trim() != "")
            {
                SQL_FULLNAME = " AND (UPPER(Full_Name) LIKE N'%' || :SearchKey || '%' OR UPPER(User_Name) LIKE N'%' || :SearchKey || '%'  OR UPPER(Vehicle_List) LIKE N'%' || :SearchKey || '%')";
            }

            DataTable objTable = new DataTable();

            #region ORC
            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;

            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT Id, User_Name AS UserName, Full_Name AS FullName, Phone, Vehicle_List AS VehicleList FROM Account WHERE Type_Id = 4 AND NVL(STATUS,0) = 1 " + SQL_FULLNAME + " ORDER BY Full_Name";
                if (SearchKey.Trim() != "")
                {
                    Cmd.Parameters.Add("SearchKey", OracleDbType.Varchar2).Value = SearchKey.ToUpper();
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
            }

            finally
            {
                ORC_CONN.Close();
                ORC_CONN.Dispose();
            }
            #endregion

            //string SQL_FULLNAME = "";
            //if (SearchKey.Trim() != "")
            //{
            //    SQL_FULLNAME = " AND FullName LIKE N'%'+@SearchKey+'%' ";
            //}
            //DataTable objTable = new DataTable();
            //try
            //{
            //    SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            //    sqlCon.Open();
            //    SqlCommand Cmd = sqlCon.CreateCommand();
            //    Cmd.CommandText = "SELECT Id, UserName, FullName, Phone, VehicleList FROM tblAccount WHERE TypeId = 4 AND ISNULL(State,0) = 1 " + SQL_FULLNAME + " ORDER BY FullName";
            //    Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
            //    SqlDataAdapter da = new SqlDataAdapter();
            //    da.SelectCommand = Cmd;
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    sqlCon.Close();
            //    sqlCon.Dispose();
            //    objTable = ds.Tables[0];
            //}
            //catch
            //{

            //}
            return objTable;
        }
        #endregion

        #region method setOrderTracking
        public string setOrderTracking(int Id, string DriverUserName, string DriverFullName, string Vehicle, DateTime? TimeIn21)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";

                bool DriverTrackinging = false;
                string tmpDeliveryCode = "";
                sqlQuery = "SELECT A.DeliveryCode, B.* FROM tblStoreOrder A, tblStoreOrderVehicle B WHERE A.Id = B.OrderId AND A.DriverUserName = @DriverUserName AND REPLACE(REPLACE(B.Vehicle,N' ',N''),N'-',N'') <> @Vehicle AND B.State = 1 AND A.DayFinish IS NULL";
                SqlConnection sqlCon1 = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon1.Open();
                SqlCommand Cmd1 = sqlCon1.CreateCommand();
                Cmd1.CommandText = sqlQuery;
                Cmd1.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd1.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle.Replace(" ", "").Replace("-", "");
                Cmd1.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                SqlDataReader Rd1 = Cmd1.ExecuteReader();
                while (Rd1.Read())
                {
                    tmpDeliveryCode = Rd1["DeliveryCode"].ToString();
                    DriverTrackinging = true;
                }
                Rd1.Close();
                sqlCon1.Close();
                sqlCon1.Dispose();

                if (DriverTrackinging)
                {
                    return "Lái xe đang giao đơn hàng có mã số giao hàng: " + tmpDeliveryCode;
                }

                sqlQuery = "UPDATE tblStoreOrder SET DriverUserName = @DriverUserName, TimeIn21 = @TimeIn21, Tracking = 1 WHERE Id = @Id AND ISNULL(Tracking,0) = 0";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                if (TimeIn21 != null)
                {
                    Cmd.Parameters.Add("TimeIn21", SqlDbType.DateTime).Value = TimeIn21.Value;
                }
                else
                {
                    Cmd.Parameters.Add("TimeIn21", SqlDbType.DateTime).Value = DBNull.Value;
                }
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue > 0)
                {
                    #region Cập nhật thông tin vào bảng xe, đơn hàng
                    SqlConnection sqlConVehicle = new SqlConnection(TVSOracle.SQL_Con);
                    sqlConVehicle.Open();
                    SqlCommand CmdVehicle = sqlConVehicle.CreateCommand();
                    CmdVehicle.CommandText = "UPDATE tblStoreOrderVehicle SET NameDriver = @NameDriver WHERE OrderId = @OrderId AND ISNULL(State,0) = 1";
                    CmdVehicle.Parameters.Add("NameDriver", SqlDbType.NVarChar).Value = DriverFullName;
                    CmdVehicle.Parameters.Add("OrderId", SqlDbType.Int).Value = Id;
                    CmdVehicle.ExecuteNonQuery();
                    sqlConVehicle.Close();
                    sqlConVehicle.Dispose();
                    #endregion

                    //#region Gửi tin nhắn cho lái xe
                    //try
                    //{
                    //    Subjects objSubject = new Subjects();
                    //    SqlConnection sqlConNotification = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                    //    sqlConNotification.Open();
                    //    SqlCommand CmdNotification = sqlConNotification.CreateCommand();
                    //    CmdNotification.CommandText = "SELECT A.UserName, A.FullName, A.DeviceId FROM tblAccount A WHERE A.UserName = @UserName AND ISNULL(DeviceId,N'') <> N'' ORDER BY DeviceIdDayUpdate DESC";
                    //    CmdNotification.Parameters.Add("UserName", SqlDbType.NVarChar).Value = DriverUserName;
                    //    SqlDataReader RdNotification = CmdNotification.ExecuteReader();
                    //    while (RdNotification.Read())
                    //    {
                    //        if (TimeIn21 != null)
                    //        {
                    //            objSubject.sentNotificationV1(RdNotification["DeviceId"].ToString(), RdNotification["UserName"].ToString(), "Bạn có đơn hàng mới cần xác nhận vận chuyển tại Nhà máy xi măng Hoàng Mai, số hiệu xe: " + Vehicle + ". Thời điểm vào lấy hàng lúc " + TimeIn21.Value.ToString("HH:mm dd/MM/yyyy") + ". Vui lòng mở ứng dụng VICEM để xử lý. Trân trọng!");
                    //        }
                    //        else
                    //        {
                    //            objSubject.sentNotificationV1(RdNotification["DeviceId"].ToString(), RdNotification["UserName"].ToString(), "Bạn có đơn hàng mới cần xác nhận vận chuyển tại Nhà máy xi măng Hoàng Mai, số hiệu xe: " + Vehicle + ". Vui lòng mở ứng dụng VICEM để xử lý. Trân trọng!");
                    //        }

                    //        #region Luu tin nhan vao Inbox
                    //        SqlConnection sqlCon02 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                    //        sqlCon02.Open();
                    //        SqlCommand Cmd02 = sqlCon02.CreateCommand();
                    //        Cmd02.CommandText = "INSERT INTO tblNotification(RootId,UserNameSent,UserNameReceive,NotificationContent) VALUES(@RootId,@UserNameSent,@UserNameReceive,@NotificationContent)";
                    //        Cmd02.Parameters.Add("RootId", SqlDbType.Int).Value = 0;
                    //        Cmd02.Parameters.Add("UserNameSent", SqlDbType.NVarChar).Value = "admin";
                    //        Cmd02.Parameters.Add("UserNameReceive", SqlDbType.NVarChar).Value = RdNotification["UserName"].ToString();
                    //        Cmd02.Parameters.Add("NotificationContent", SqlDbType.NVarChar).Value = "Bạn có đơn hàng mới cần xác nhận vận chuyển tại Nhà máy xi măng Hoàng Mai, số hiệu xe: " + Vehicle + ". Vui lòng mở ứng dụng VICEM để xử lý. Trân trọng!";
                    //        Cmd02.ExecuteNonQuery();
                    //        sqlCon02.Close();
                    //        sqlCon02.Dispose();
                    //        #endregion
                    //    }
                    //    RdNotification.Close();
                    //    sqlConNotification.Close();
                    //    sqlConNotification.Dispose();
                    //}
                    //catch
                    //{

                    //}
                    //#endregion

                    return "OK";
                }
            }
            catch
            {
                tmpValue = 0;
            }
            return "";
        }
        #endregion

        #region method delOrderTracking
        public int delOrderTracking(int Id)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";

                sqlQuery = "UPDATE tblStoreOrder SET DriverUserName = N'', Tracking = 0, TrackingBegin = null WHERE Id = @Id";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }
            return tmpValue;
        }
        #endregion

        #region method getStoreOrderInfo
        public DataTable getStoreOrderInfo(int OrderId)
        {
            DataTable objTable = new DataTable();
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT *, (SELECT TOP 1 NameDistributor FROM tblDistributor WHERE IDDistributorSyn = tblStoreOrder.IDDistributorSyn) AS NameDistributor FROM tblStoreOrder WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = OrderId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
            return objTable;
        }
        #endregion

        #region method delData
        public int delData(int Id)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.CommandText = "DELETE tblDriver WHERE Id = @Id";
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

        #region method updateDriverUserName
        public int updateDriverUserName(int Id, int AccId, string UserName, string FullName)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("AccId", SqlDbType.Int).Value = AccId;
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                Cmd.Parameters.Add("FullName", SqlDbType.NVarChar).Value = FullName;
                Cmd.CommandText = "UPDATE tblDriver SET AccId = @AccId, UserName = @UserName, FullName = @FullName WHERE Id = @Id";
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
