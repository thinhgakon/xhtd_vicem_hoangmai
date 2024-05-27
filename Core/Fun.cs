using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class Fun
    {
        #region method GetDeviceInfo
        public DataTable GetDeviceInfo(string Door)
        {  
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 1 * FROM tblDeviceInfo WHERE Door = @Door";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Door", SqlDbType.NVarChar).Value = Door;
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

        #region method UpdateDeviceInfo
        public bool UpdateDeviceInfo(string Door, int OperID, int DoorOrAuxoutID, int OutputAddrType, int DoorAction, string Ipaddress, string Port)
        {
            bool tmp = false;
            try
            {
                string SQLQUERY = "UPDATE tblDeviceInfo SET OperID = @OperID, DoorOrAuxoutID = @DoorOrAuxoutID, OutputAddrType=@OutputAddrType, DoorAction=@DoorAction, Ipaddress=@Ipaddress, Port=@Port WHERE Door = @Door";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Door", SqlDbType.NVarChar).Value = Door;
                Cmd.Parameters.Add("OperID", SqlDbType.Int).Value = OperID;
                Cmd.Parameters.Add("DoorOrAuxoutID", SqlDbType.Int).Value = DoorOrAuxoutID;
                Cmd.Parameters.Add("OutputAddrType", SqlDbType.Int).Value = OutputAddrType;
                Cmd.Parameters.Add("DoorAction", SqlDbType.Int).Value = DoorAction;
                Cmd.Parameters.Add("Ipaddress", SqlDbType.NVarChar).Value = Ipaddress;
                Cmd.Parameters.Add("Port", SqlDbType.NVarChar).Value = Port;
                Cmd.CommandText = SQLQUERY;
                Cmd.ExecuteNonQuery();
                tmp = true;
            }
            catch
            {

            }
            return tmp;
        }
        #endregion  

        #region method setConfigOperating
        public int setConfigOperating(string Code, int Value)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "UPDATE tblConfigOperating SET Value = @Value WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Value", SqlDbType.Int).Value = Value;
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
        #region method setConfigOperating string
        public int setConfigOperatingString(string Code, string Value)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "UPDATE tblConfigOperating SET ValueString = @Value WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.Parameters.Add("Value", SqlDbType.NVarChar).Value = Value;
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

        #region method getConfigOperating
        public int getConfigOperating(string Code)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "SELECT Value FROM tblConfigOperating WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code",SqlDbType.NVarChar).Value = Code;
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    if (Rd["Value"].ToString() != "")
                    {
                        tmpValue = int.Parse(Rd["Value"].ToString());
                    }
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
        #region method getConfigOperating string
        public string getStringConfigOperating(string Code)
        {
            string tmpValue = "";
            try
            {
                string SQLQUERY = "SELECT ValueString FROM tblConfigOperating WHERE Code = @Code";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    if (Rd["ValueString"].ToString() != "")
                    {
                        tmpValue =  Rd["ValueString"].ToString();
                    }
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

        #region method getCustomers
        public DataTable getCustomers(string SearchKey)
        {
            string SQl_SEARCH = "";
            if (SearchKey.Trim() != "")
            {
                SQl_SEARCH = " AND NameDistributor LIKE N'%'+@SearchKey+'%'";
            }
            DataTable objTable = new DataTable();
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "SELECT * FROM tblDistributor WHERE 1 = 1 "+ SQl_SEARCH + " ORDER BY LockInDbet DESC, NameDistributor";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
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

        #region method updateCustomerLockInDbet
        public int updateCustomerLockInDbet(int IDDistributorSyn, bool LockInDbet)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "UPDATE tblDistributor SET LockInDbet = @LockInDbet WHERE IDDistributorSyn = @IDDistributorSyn";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("IDDistributorSyn", SqlDbType.Int).Value = IDDistributorSyn;
                Cmd.Parameters.Add("LockInDbet", SqlDbType.Bit).Value = !LockInDbet;
                tmpValue = Cmd.ExecuteNonQuery();

                if (tmpValue > 0)
                {
                    SQLQUERY = "UPDATE tblStoreOrderOperating SET LockInDbet = @LockInDbet WHERE Step NOT IN (3,5,6,7,8,9) AND IDDistributorSyn = @IDDistributorSyn";
                    Cmd.CommandText = SQLQUERY;
                    tmpValue = Cmd.ExecuteNonQuery();
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

        #region method setAppConfig
        public int setAppConfig(int TimeFinishAllow)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "UPDATE tblAppConfig SET TimeFinishAllow = @TimeFinishAllow WHERE Id = 1";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("TimeFinishAllow", SqlDbType.Int).Value = TimeFinishAllow;
                Cmd.CommandText = SQLQUERY;
                tmpValue = Cmd.ExecuteNonQuery();
            }
            catch
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method getAppConfig
        public int getAppConfig()
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "SELECT TimeFinishAllow FROM tblAppConfig WHERE Id = 1";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = int.Parse(Rd["TimeFinishAllow"].ToString());
                }
                Rd.Close();
            }
            catch
            {
                tmpValue = 0;
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
