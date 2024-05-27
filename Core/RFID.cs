using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    class RFID
    {
        #region method getRFID
        public DataTable getRFID(string SearchKey)
        {
            string QueryVehicle = "";
            if (SearchKey.Trim() != "")
            {
                QueryVehicle = " AND ( (Vehicle LIKE N'%'+@SearchKey+'%') OR (Code LIKE N'%'+@SearchKey+'%') )";
            }
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblRFID WHERE 1 = 1 " + QueryVehicle + " ORDER BY DayCreate DESC";
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

        #region method setData
        public int setData(int Id, string Code, string Vehicle, DateTime DayReleased, DateTime DayExpired, string UserReleased, bool State, ref string strMsg)
        {
            int tmpValue = 0;
            int tmpValue1 = 0;
            try
            {
                string SQLQUERY = "IF NOT EXISTS (SELECT * FROM tblRFID WHERE Id = @Id) ";
                SQLQUERY += "BEGIN INSERT INTO tblRFID(Code,Vehicle,DayReleased,DayExpired,UserReleased,State,UserCreate) VALUES(@Code,@Vehicle,@DayReleased,@DayExpired,@UserReleased,@State,@UserCreate) END ";
                SQLQUERY += "ELSE BEGIN UPDATE tblRFID SET DayExpired = @DayExpired, State = @State, DayUpdate = getdate(), UserUpdate = @UserUpdate, Code = @Code, Vehicle = @Vehicle WHERE Id = @Id END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("DayReleased", SqlDbType.DateTime).Value = DayReleased;
                Cmd.Parameters.Add("DayExpired", SqlDbType.DateTime).Value = DayExpired;
                Cmd.Parameters.Add("UserReleased", SqlDbType.NVarChar).Value = UserReleased;
                Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
                Cmd.Parameters.Add("UserCreate", SqlDbType.NVarChar).Value = frmMain.UserName;
                Cmd.Parameters.Add("UserUpdate", SqlDbType.NVarChar).Value = frmMain.UserName;
                Cmd.CommandText = SQLQUERY;
                tmpValue = Cmd.ExecuteNonQuery();

                SQLQUERY = "UPDATE tblStoreOrderOperating SET CardNo = @Code WHERE ISNULL(CardNo,N'') = N'' AND Vehicle = @Vehicle AND Step IN (0,1,2,3)";
                Cmd.CommandText = SQLQUERY;
                tmpValue1 = Cmd.ExecuteNonQuery();

                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue == 1)
                {
                    strMsg = "Cập nhật thông tin thành công";
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
            }
            return tmpValue;
        }
        #endregion

        #region method CheckVihecleExit
        public bool CheckVehicleExit(string Code, string Vehicle)
        {
            bool sCheck = false;
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblRFID WHERE Code = @Code OR Vehicle = @Vehicle";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                if(objTable.Rows.Count > 0)
                {
                    sCheck = true;
                }
            }
            catch
            {

            }
            return sCheck;
        }
        #endregion

        #region method getVehicleByCardNo
        public string getVehicleByCardNo(string Code)
        {
            string tmpValue = "";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "SELECT * FROM tblRFID WHERE Code = @Code";
                sqlCon.Open();
                
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    tmpValue = Rd["Vehicle"].ToString().ToUpper().Trim();
                }
                Rd.Close();

                if (tmpValue.Trim() != "")
                {
                    this.setLastEnterByCardNo(Code);
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

        #region method setLastEnterByCardNo
        public int setLastEnterByCardNo(string Code)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "UPDATE tblRFID SET LastEnter = getdate() WHERE Code = @Code";
                
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.CommandText = SQLQUERY;
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

        #region method saveRfidToBillOrder
        public int saveRfidToBillOrder(string CardNo, string Vehicle)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("CardNo", SqlDbType.NVarChar).Value = CardNo;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET CardNo = @CardNo WHERE Vehicle = @Vehicle AND Step < 8";
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

    }
}
