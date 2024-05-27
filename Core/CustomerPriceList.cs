using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class CustomerPriceList
    {
        #region method setData
        public int setData(int CUSTOMER_ID, int LIST_HEADER_ID, string LIST_NUMBER, string LIST_NAME, string CURRENCY_CODE, DateTime START_DATE_EFFECTIVE, DateTime END_DATE_EFFECTIVE, int INVENTORY_ITEM_ID, float OPERAND, float PRICE_VAT, int SHIPPOINT_ID)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM DEV_tblCustomerPriceList WHERE CUSTOMER_ID = @CUSTOMER_ID AND LIST_HEADER_ID = @LIST_HEADER_ID AND LIST_NUMBER = @LIST_NUMBER AND LIST_NAME = @LIST_NAME AND INVENTORY_ITEM_ID = @INVENTORY_ITEM_ID AND OPERAND = @OPERAND AND SHIPPOINT_ID = @SHIPPOINT_ID) ";
                sqlQuery += "BEGIN INSERT INTO DEV_tblCustomerPriceList(CUSTOMER_ID,LIST_HEADER_ID,LIST_NUMBER,LIST_NAME,CURRENCY_CODE,START_DATE_EFFECTIVE,END_DATE_EFFECTIVE,INVENTORY_ITEM_ID,OPERAND,PRICE_VAT,SHIPPOINT_ID) VALUES(@CUSTOMER_ID,@LIST_HEADER_ID,@LIST_NUMBER,@LIST_NAME,@CURRENCY_CODE,@START_DATE_EFFECTIVE,@END_DATE_EFFECTIVE,@INVENTORY_ITEM_ID,@OPERAND,@PRICE_VAT,@SHIPPOINT_ID) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
                Cmd.Parameters.Add("LIST_HEADER_ID", SqlDbType.Int).Value = LIST_HEADER_ID;
                Cmd.Parameters.Add("LIST_NUMBER", SqlDbType.NVarChar).Value = LIST_NUMBER;
                Cmd.Parameters.Add("LIST_NAME", SqlDbType.NVarChar).Value = LIST_NAME;
                Cmd.Parameters.Add("CURRENCY_CODE", SqlDbType.NVarChar).Value = CURRENCY_CODE;
                Cmd.Parameters.Add("START_DATE_EFFECTIVE", SqlDbType.DateTime).Value = START_DATE_EFFECTIVE;
                Cmd.Parameters.Add("END_DATE_EFFECTIVE", SqlDbType.DateTime).Value = END_DATE_EFFECTIVE;
                Cmd.Parameters.Add("INVENTORY_ITEM_ID", SqlDbType.Int).Value = INVENTORY_ITEM_ID;
                Cmd.Parameters.Add("OPERAND", SqlDbType.Float).Value = OPERAND;
                Cmd.Parameters.Add("PRICE_VAT", SqlDbType.Float).Value = PRICE_VAT;
                Cmd.Parameters.Add("SHIPPOINT_ID", SqlDbType.Int).Value = SHIPPOINT_ID;
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
