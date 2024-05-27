using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    class Device
    {
        #region method setData
        public int setData(string Code, string Name, string IpAddress, int PortNumber)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "IF NOT EXISTS (SELECT * FROM tblDevice WHERE Code = @Code) ";
                SQL += "BEGIN INSERT INTO tblDevice(Code,Name,IpAddress,PortNumber) VALUES(@Code,@Name,@IpAddress,@PortNumber) END ";
                SQL += "ELSE BEGIN UPDATE tblDevice SET Name = @Name, IpAddress = @IpAddress, PortNumber = @PortNumber WHERE Code = @Code END";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
                Cmd.Parameters.Add("IpAddress", SqlDbType.NVarChar).Value = IpAddress;
                Cmd.Parameters.Add("PortNumber", SqlDbType.Int).Value = PortNumber;
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

        #region method getDataByCode
        public DataTable getDataByCode(string Code)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblDevice WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
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
        public DataTable getDeviceInfoByCode(string Code)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblDeviceOperating WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch(Exception ex)
            {

            }
            return objTable;
        }
        public List<DeviceInfoModel> getAllDeviceInfo()
        {
            var res = new List<DeviceInfoModel>();
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblDeviceOperating";
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
                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    var deviceItem = new DeviceInfoModel();
                    deviceItem.Id = Int32.Parse(objTable.Rows[i]["Id"].ToString());
                    deviceItem.GroupDevice = Int32.Parse(objTable.Rows[i]["GroupDevice"]?.ToString() ?? "0");
                    deviceItem.GroupDeviceCode = objTable.Rows[i]["GroupDeviceCode"]?.ToString();
                    deviceItem.GroupDeviceName = objTable.Rows[i]["GroupDeviceName"]?.ToString();
                    deviceItem.Code = objTable.Rows[i]["Code"]?.ToString();
                    deviceItem.Name = objTable.Rows[i]["Name"]?.ToString();
                    deviceItem.IpAddress = objTable.Rows[i]["IpAddress"]?.ToString();
                    deviceItem.PortNumber = Int32.Parse(objTable.Rows[i]["PortNumber"]?.ToString() ?? "0");
                    deviceItem.State = Boolean.Parse(objTable.Rows[i]["State"]?.ToString() ?? "0");
                    deviceItem.LogHistory = objTable.Rows[i]["LogHistory"]?.ToString();
                    res.Add(deviceItem);
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        #endregion
        public int UpdateDeviceInfo(string Code, string Name, string IpAddress, int PortNumber, string LogHistory)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "UPDATE dbo.tblDeviceOperating SET Name = @Name, IpAddress = @IpAddress, PortNumber = @PortNumber, LogHistory = @LogHistory WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
                Cmd.Parameters.Add("IpAddress", SqlDbType.NVarChar).Value = IpAddress;
                Cmd.Parameters.Add("PortNumber", SqlDbType.Int).Value = PortNumber;
                Cmd.Parameters.Add("LogHistory", SqlDbType.NVarChar).Value = LogHistory;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }

        #region method getConfigByCode
        public string getConfigByCode(string Code)
        {
            string tmpValue = "";
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT IpAddress, PortNumber FROM tblDevice WHERE Code = @Code AND ISNULL(State,0) = 1";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    tmpValue = "protocol=TCP,ipaddress=" + Rd["IpAddress"].ToString() + ",port=" + Rd["PortNumber"].ToString() + ",timeout=2000,passwd=";
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method getIpAddressByCode
        public string getIpAddressByCode(string Code)
        {
            string tmpValue = "";
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT IpAddress FROM tblDevice WHERE Code = @Code AND ISNULL(State,0) = 1";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = Rd["IpAddress"].ToString();
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method setBarieLog
        #region method setBarieLog
        public int setBarieLog(string BarieName, string Note, string UserAction)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "INSERT INTO tblBarieLog(BarieName,Note,TimeAction,UserAction) VALUES(@BarieName,@Note,getdate(),@UserAction)";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("BarieName", SqlDbType.NVarChar).Value = BarieName;
                Cmd.Parameters.Add("Note", SqlDbType.NVarChar).Value = Note;
                Cmd.Parameters.Add("UserAction", SqlDbType.NVarChar).Value = UserAction;
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
        #endregion
    }
    public class DeviceInfoModel
    {
        public int Id { get; set; }
        public int? GroupDevice { get; set; }
        public string GroupDeviceCode { get; set; }
        public string GroupDeviceName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int? PortNumber { get; set; }
        public bool? State { get; set; }
        public DateTime? DayCreate { get; set; }
        public string LogHistory { get; set; }
        public int? Flag { get; set; }

    }
}
