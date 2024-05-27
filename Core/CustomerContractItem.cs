using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class CustomerContractItem
    {
        #region method setData
        public int setData(int CUSTOMER_ID, int HEADER_ID, int INVENTORY_ITEM_ID, int LINE_ID, int LINE_NUMBER)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM DEV_tblCustomerContractItem WHERE CUSTOMER_ID = @CUSTOMER_ID AND HEADER_ID = @HEADER_ID AND INVENTORY_ITEM_ID = @INVENTORY_ITEM_ID) ";
                sqlQuery += "BEGIN INSERT INTO DEV_tblCustomerContractItem(CUSTOMER_ID,HEADER_ID,INVENTORY_ITEM_ID,LINE_ID,LINE_NUMBER) VALUES(@CUSTOMER_ID,@HEADER_ID,@INVENTORY_ITEM_ID,@LINE_ID,@LINE_NUMBER) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
                Cmd.Parameters.Add("HEADER_ID", SqlDbType.Int).Value = HEADER_ID;
                Cmd.Parameters.Add("INVENTORY_ITEM_ID", SqlDbType.Int).Value = INVENTORY_ITEM_ID;
                Cmd.Parameters.Add("LINE_ID", SqlDbType.Int).Value = LINE_ID;
                Cmd.Parameters.Add("LINE_NUMBER", SqlDbType.Int).Value = LINE_NUMBER;
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
    }
}
