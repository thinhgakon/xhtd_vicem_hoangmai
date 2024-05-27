using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class CustomerShippoint
    {
        #region method setData
        public int setData(int CS_ID, int SHIPPOINT_ID, int CUSTOMER_ID)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblCustomerShippoint WHERE CS_ID = @CS_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblCustomerShippoint(CS_ID,SHIPPOINT_ID,CUSTOMER_ID) VALUES(@CS_ID,@SHIPPOINT_ID,@CUSTOMER_ID) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CS_ID", SqlDbType.Int).Value = CS_ID;
                Cmd.Parameters.Add("SHIPPOINT_ID", SqlDbType.Int).Value = SHIPPOINT_ID;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
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
    }
}
