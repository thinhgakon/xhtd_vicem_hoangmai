using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class CustomerCheckpointItem
    {
        #region method setData
        public int setData(int CCI_ID, int CUSTOMER_CHECKPOINT_ID, int ITEM_ID)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblCustomerCheckpointItem WHERE CCI_ID = @CCI_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblCustomerCheckpointItem(CCI_ID,CUSTOMER_CHECKPOINT_ID,ITEM_ID) VALUES(@CCI_ID,@CUSTOMER_CHECKPOINT_ID,@ITEM_ID) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CCI_ID", SqlDbType.Int).Value = CCI_ID;
                Cmd.Parameters.Add("CUSTOMER_CHECKPOINT_ID", SqlDbType.Int).Value = CUSTOMER_CHECKPOINT_ID;
                Cmd.Parameters.Add("ITEM_ID", SqlDbType.Int).Value = ITEM_ID;
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
