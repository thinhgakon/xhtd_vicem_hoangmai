using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class CustomerCheckpoint
    {
        #region method setData
        public int setData(int CUSTOMER_CHECKPOINT_ID, int CUSTOMER_ID, int CHECKPOINT_ID)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblCustomerCheckpoint WHERE CUSTOMER_CHECKPOINT_ID = @CUSTOMER_CHECKPOINT_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblCustomerCheckpoint(CUSTOMER_CHECKPOINT_ID,CUSTOMER_ID,CHECKPOINT_ID) VALUES(@CUSTOMER_CHECKPOINT_ID,@CUSTOMER_ID,@CHECKPOINT_ID) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CUSTOMER_CHECKPOINT_ID", SqlDbType.Int).Value = CUSTOMER_CHECKPOINT_ID;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
                Cmd.Parameters.Add("CHECKPOINT_ID", SqlDbType.Int).Value = CHECKPOINT_ID;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue < 0)
                {
                    tmpValue = 0;
                }
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
