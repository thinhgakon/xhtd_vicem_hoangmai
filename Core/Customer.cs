using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class Customer
    {
        #region method setData
        public int setData(int CUSTOMER_ID, int ADDRESS_ID, string CUSTOMER_NAME, string CUSTOMER_ADDRESS)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblCustomer WHERE CUSTOMER_ID = @CUSTOMER_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblCustomer(CUSTOMER_ID,ADDRESS_ID,CUSTOMER_NAME,CUSTOMER_ADDRESS) VALUES(@CUSTOMER_ID,@ADDRESS_ID,@CUSTOMER_NAME,@CUSTOMER_ADDRESS) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
                Cmd.Parameters.Add("ADDRESS_ID", SqlDbType.Int).Value = ADDRESS_ID;
                Cmd.Parameters.Add("CUSTOMER_NAME", SqlDbType.NVarChar).Value = CUSTOMER_NAME;
                Cmd.Parameters.Add("CUSTOMER_ADDRESS", SqlDbType.NVarChar).Value = CUSTOMER_ADDRESS;
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

        #region method getDistributorData
        public DataTable getDistributorData()
        {

            DataTable objTable = new DataTable();
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT IDDistributor,CodeStore,IDDistributorSyn,CodeDistributor,NameDistributor FROM tblDistributor WHERE [State]=1";

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("getDistributorData-" + ex.Message);
            }
            return objTable;
        }
        #endregion
    }
}
