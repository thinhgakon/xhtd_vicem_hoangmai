using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace HMXHTD
{
    class Vehicle
    {
        #region method UpdateData
        public int UpdateData(string VEHICLE_CODE, string H, string W, string L)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "UPDATE tblVehicle SET HeightVehicle = @HeightVehicle, WidthVehicle = @WidthVehicle, LongVehicle = @LongVehicle WHERE Vehicle = @Vehicle";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = VEHICLE_CODE;
                Cmd.Parameters.Add("HeightVehicle", SqlDbType.NVarChar).Value = H;
                Cmd.Parameters.Add("WidthVehicle", SqlDbType.NVarChar).Value = W;
                Cmd.Parameters.Add("LongVehicle", SqlDbType.NVarChar).Value = L;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                tmpValue = 1;
            }
            catch
            {
                tmpValue = 0;
            }
            return tmpValue;
        }
        #endregion

        #region getDataVihicleORC
        public DataTable getDataVihicleORC(int TYPE, int IS_CONFIRM, int STATUS, string SearchKey)
        {
            string SQL_TYPE = "", SQL_IS_CONFIRM = "", SQL_STATUS = "", SQL_SEARCH = "";

            DataTable objTable = new DataTable();

            if (TYPE > 0)
            {
                SQL_TYPE = " AND ( TYPE = :TYPE ) ";
            }

            if (IS_CONFIRM > 0)
            {
                if (IS_CONFIRM == 1)
                {
                    SQL_IS_CONFIRM = " AND ( IS_CONFIRM = 1 ) ";
                }
                else
                {
                    SQL_IS_CONFIRM = " AND ( IS_CONFIRM = 0 ) ";
                }
            }

            if (STATUS > 0)
            {
                if (STATUS == 1)
                {
                    SQL_STATUS = " AND ( STATUS = 1 ) ";
                }
                else
                {
                    SQL_STATUS = " AND ( STATUS = 0 ) ";
                }
            }

            if (SearchKey.Trim() != "")
            {
                SQL_SEARCH = " AND ( UPPER(CODE) LIKE N'%' || :SearchKey || '%' ) ";
            }

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT ID, CODE, NVL(TYPE,0) AS TYPE, NVL(IS_CONFIRM,0) AS IS_CONFIRM, TONNAGE, NVL(STATUS,0) AS STATUS, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VEHICLE.TYPE,'0','Chưa xác định'),'1','Xe tải'),'2','Xe đầu kéo'),'3','Tàu'),'4','Xà lan'),'5','Tàu hỏa') TYPENAME FROM VEHICLE WHERE NVL(IS_DELETE,0) = 0 " + SQL_TYPE + SQL_SEARCH + SQL_IS_CONFIRM + SQL_STATUS + " ORDER BY CODE";
                if (TYPE > 0)
                {
                    Cmd.Parameters.Add("TYPE", OracleDbType.Int32).Value = TYPE;
                }
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

            //if (SearchKey.Trim() != "")
            //{
            //    QueryVehicle = " AND ((Vehicle LIKE N'%'+@SearchKey+'%') OR (Vehicle IN (SELECT Vehicle FROM tblRFID WHERE Code LIKE N'%'+@SearchKey+'%')) ) ";
            //}
            //DataTable objTable = new DataTable();
            //try
            //{
            //    string SQLQUERY = "SELECT DISTINCT 0 AS TT, IDVehicle, Vehicle, NameDriver, IdCardNumber, Tonnage, TonnageDefault, HeightVehicle, WidthVehicle, LongVehicle, (SELECT Code FROM tblRFID WHERE Vehicle = tblVehicle.Vehicle) AS CardNo " +
            //    " FROM tblVehicle WHERE 1 = 1 " + QueryVehicle + " ORDER BY Vehicle";
            //    SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            //    sqlCon.Open();
            //    SqlCommand Cmd = sqlCon.CreateCommand();
            //    Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
            //    Cmd.CommandText = SQLQUERY;
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

        #region getDataVihicleCustomer
        public DataTable getDataVihicleCustomer(string Vehicle, int CustomerId)
        {
            string SQL_Vehicle = "", SQL_CustomerId = "";

            if (Vehicle != "")
            {
                SQL_Vehicle = " AND UPPER(VEHICLE.CODE) = :Vehicle ";
            }

            if (CustomerId > 0)
            {
                SQL_CustomerId = " AND DISTRIBUTOR_ID_SYN = "+ CustomerId;
            }

            DataTable objTable = new DataTable();

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT VEHICLE.*, STORE.NAME_STORE FROM VEHICLE, STORE WHERE VEHICLE.STORE_ID = STORE.Id AND STORE_ID > 0 AND STORE.CODE_STORE IN (SELECT CODE_STORE FROM DISTRIBUTOR WHERE 1 = 1 "+SQL_CustomerId+") "+SQL_Vehicle;
                if (Vehicle != "")
                {
                    Cmd.Parameters.Add("Vehicle", OracleDbType.Varchar2).Value = Vehicle.ToUpper();
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

            //if (SearchKey.Trim() != "")
            //{
            //    QueryVehicle = " AND ((Vehicle LIKE N'%'+@SearchKey+'%') OR (Vehicle IN (SELECT Vehicle FROM tblRFID WHERE Code LIKE N'%'+@SearchKey+'%')) ) ";
            //}
            //DataTable objTable = new DataTable();
            //try
            //{
            //    string SQLQUERY = "SELECT DISTINCT 0 AS TT, IDVehicle, Vehicle, NameDriver, IdCardNumber, Tonnage, TonnageDefault, HeightVehicle, WidthVehicle, LongVehicle, (SELECT Code FROM tblRFID WHERE Vehicle = tblVehicle.Vehicle) AS CardNo " +
            //    " FROM tblVehicle WHERE 1 = 1 " + QueryVehicle + " ORDER BY Vehicle";
            //    SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            //    sqlCon.Open();
            //    SqlCommand Cmd = sqlCon.CreateCommand();
            //    Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
            //    Cmd.CommandText = SQLQUERY;
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

        #region getDataVihicleStore
        public DataTable getDataVihicleStore(string Vehicle, int StoreId)
        {
            string SQL_Vehicle = "", SQL_StoreId = "";

            if (Vehicle != "")
            {
                SQL_Vehicle = " AND UPPER(VEHICLE.CODE) = :Vehicle ";
            }

            if (StoreId > 0)
            {
                SQL_StoreId = " AND STORE_ID = " + StoreId;
            }

            DataTable objTable = new DataTable();

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT VEHICLE.*, STORE.NAME_STORE FROM VEHICLE, STORE WHERE VEHICLE.STORE_ID = STORE.ID AND STORE.CODE_STORE NOT IN (SELECT CODE_STORE FROM DISTRIBUTOR) " + SQL_StoreId + SQL_Vehicle;
                if (Vehicle != "")
                {
                    Cmd.Parameters.Add("Vehicle", OracleDbType.Varchar2).Value = Vehicle.ToUpper();
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
            return objTable;
        }
        #endregion

        #region getDataVihicleDriver
        public DataTable getDataVihicleDriver(string Vehicle)
        {
            DataTable objTable = new DataTable();

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT USER_NAME, FULL_NAME, VEHICLE_LIST FROM ACCOUNT WHERE TYPE_ID = 4 AND INSTR(VEHICLE_LIST, :Vehicle) > 0 ORDER BY FULL_NAME";
                Cmd.Parameters.Add("Vehicle", OracleDbType.Varchar2).Value = Vehicle.ToUpper();
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
            return objTable;
        }
        #endregion

        #region addData
        public string addData(string CODE, int TONNAGE, int TYPE, int STATUS, int IS_CONFIRM)
        {
            string tmpValue = "";
            int tmpValue1 = 0;
            bool itemExists = false;

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT * FROM VEHICLE WHERE UPPER(CODE) = :CODE";
                Cmd.Parameters.Add("CODE", OracleDbType.Varchar2).Value = CODE;
                OracleDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    itemExists = true;
                }
                Rd.Close();
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

            if (itemExists)
            {
                tmpValue = "Phương tiện \"" + CODE.ToUpper() + "\" đã tồn tại trên hệ thống";
                return tmpValue;
            }

            OracleConnection ORC_CONN1 = new OracleConnection();
            ORC_CONN1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN1.Open();
                OracleGlobalization glob = ORC_CONN1.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN1.SetSessionInfo(glob);
                OracleCommand Cmd1 = ORC_CONN1.CreateCommand();
                Cmd1.CommandText = "INSERT INTO VEHICLE(CODE,TONNAGE,TYPE,STATUS,IS_CONFIRM) VALUES(:CODE,:TONNAGE,:TYPE,:STATUS,:IS_CONFIRM)";
                Cmd1.Parameters.Add("CODE", OracleDbType.Varchar2).Value = CODE;
                Cmd1.Parameters.Add("TONNAGE", OracleDbType.Int32).Value = TONNAGE;
                Cmd1.Parameters.Add("TYPE", OracleDbType.Int32).Value = TYPE;
                Cmd1.Parameters.Add("STATUS", OracleDbType.Int32).Value = STATUS;
                Cmd1.Parameters.Add("IS_CONFIRM", OracleDbType.Int32).Value = IS_CONFIRM;
                tmpValue1 = Cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var str = ex.ToString();
            }

            finally
            {
                ORC_CONN1.Close();
                ORC_CONN1.Dispose();
            }

            if (tmpValue1 > 0)
            {
                tmpValue = "OK";
            }
            else
            {
                tmpValue = "Cập nhật thông tin thất bại!";
            }

            return tmpValue;
        }
        #endregion

        #region editData
        public int editData(int Id, int Type, int TONNAGE, int IS_CONFIRM)
        {
            int tmpValue = 0;

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "UPDATE VEHICLE SET Type = :Type, TONNAGE = :TONNAGE, IS_CONFIRM = :IS_CONFIRM WHERE Id = :Id";
                Cmd.Parameters.Add("Type", OracleDbType.Int32).Value = Type;
                Cmd.Parameters.Add("TONNAGE", OracleDbType.Int32).Value = TONNAGE;
                Cmd.Parameters.Add("IS_CONFIRM", OracleDbType.Int32).Value = IS_CONFIRM;
                Cmd.Parameters.Add("Id", OracleDbType.Int32).Value = Id;
                tmpValue = Cmd.ExecuteNonQuery();
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
            return tmpValue;
        }
        #endregion

        #region delData
        public int delData(int Id, string Code)
        {
            int tmpValue = 0;

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;
            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "UPDATE VEHICLE SET IS_DELETE = 1 WHERE Id = :Id";
                Cmd.Parameters.Add("Id", OracleDbType.Int32).Value = Id;
                tmpValue = Cmd.ExecuteNonQuery();
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
            return tmpValue;
        }
        #endregion

        #region getDataVihicle
        public DataTable getDataVihicle(string SearchKey)
        {
            string QueryVehicle = "";
            if (SearchKey.Trim() != "")
            {
                QueryVehicle = " AND ((Vehicle LIKE N'%'+@SearchKey+'%') OR (Vehicle IN (SELECT Vehicle FROM tblRFID WHERE Code LIKE N'%'+@SearchKey+'%')) ) ";
            }
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT DISTINCT 0 AS TT, IDVehicle, Vehicle, NameDriver, IdCardNumber, Tonnage, TonnageDefault, HeightVehicle, WidthVehicle, LongVehicle, (SELECT top 1 Code FROM tblRFID WHERE Vehicle = tblVehicle.Vehicle) AS CardNo " +
                " FROM tblVehicle WHERE 1 = 1 " + QueryVehicle + " ORDER BY Vehicle";
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

        #region getDataVihicle
        public DataTable getDataVihicleID(string SearchKey)
        {
   
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 1 HeightVehicle, WidthVehicle, LongVehicle " +
                " FROM tblVehicle WHERE Vehicle = @Vehicle";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = SearchKey;
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

        #region method setData
        public int setData(int Id, string Vehicle, string NameDriver, string Tonnage, string IdCardNumber, string HeightVehicle, string WidthVehicle, string LongVehicle)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "IF NOT EXISTS (SELECT * FROM tblVehicle WHERE IDVehicle = @IDVehicle) ";
                SQLQUERY += "BEGIN INSERT INTO tblVehicle(Vehicle,NameDriver,Tonnage,IdCardNumber,HeightVehicle,WidthVehicle,LongVehicle,UserCreate) VALUES(@Vehicle,@NameDriver,@Tonnage,@IdCardNumber,@HeightVehicle,@WidthVehicle,@LongVehicle,@UserCreate) END ";
                SQLQUERY += "ELSE BEGIN UPDATE tblVehicle SET NameDriver = @NameDriver, Tonnage = @Tonnage, IdCardNumber=@IdCardNumber, HeightVehicle=@HeightVehicle, WidthVehicle = @WidthVehicle, LongVehicle = @LongVehicle, DayUpdate = getdate(), UserUpdate = @UserCreate WHERE IDVehicle = @IDVehicle END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("IDVehicle", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("NameDriver", SqlDbType.NVarChar).Value = NameDriver;
                Cmd.Parameters.Add("Tonnage", SqlDbType.NVarChar).Value = Tonnage;
                Cmd.Parameters.Add("IdCardNumber", SqlDbType.NVarChar).Value = IdCardNumber;
                Cmd.Parameters.Add("HeightVehicle", SqlDbType.NVarChar).Value = HeightVehicle;
                Cmd.Parameters.Add("WidthVehicle", SqlDbType.NVarChar).Value = WidthVehicle;
                Cmd.Parameters.Add("LongVehicle", SqlDbType.NVarChar).Value = LongVehicle;
                Cmd.Parameters.Add("UserCreate", SqlDbType.NVarChar).Value = frmMain.UserName;
                Cmd.CommandText = SQLQUERY;
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

        #region method delData
        public int delData(int Id)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY1 = "DELETE tblRFID WHERE Vehicle = ISNULL((SELECT TOP 1 Vehicle FROM tblVehicle WHERE IDVehicle = @Id),N'')";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.CommandText = SQLQUERY1;
                tmpValue = Cmd.ExecuteNonQuery();

                string SQLQUERY2 = "DELETE tblVehicle WHERE IDVehicle = @Id";
                Cmd.CommandText = SQLQUERY2;
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

        #region method updateRFID
        public string updateRFID(string Vehicle, string RFID)
        {
            string msg = "";
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = RFID;
                Cmd.CommandText = "UPDATE tblRFID SET Code = @Code WHERE Vehicle = @Vehicle";
                tmpValue = Cmd.ExecuteNonQuery();

                sqlCon.Close();
                sqlCon.Dispose();
                this.updateRFIDOrderOperating(Vehicle, RFID);
                msg = "Cập nhật thông tin thành công";
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            return msg;
        }
        #endregion
        #region method updateRFIDForOrderOperating
        public string updateRFIDOrderOperating(string Vehicle, string RFID)
        {
            string msg = "";
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = RFID;

                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET CardNo = @Code WHERE Vehicle = @Vehicle";
                Cmd.ExecuteNonQuery();

                sqlCon.Close();
                sqlCon.Dispose();

                msg = "Cập nhật thông tin thành công";
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            return msg;
        }
        #endregion
    }
}
