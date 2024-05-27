using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class ItemList
    {
        #region method setData
        public int setData(int INVENTORY_ITEM_ID, string DESCRIPTION, string PRIMARY_UOM_CODE)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblItemList WHERE INVENTORY_ITEM_ID = @INVENTORY_ITEM_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblItemList(INVENTORY_ITEM_ID,DESCRIPTION,PRIMARY_UOM_CODE) VALUES(@INVENTORY_ITEM_ID,@DESCRIPTION,@PRIMARY_UOM_CODE) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("INVENTORY_ITEM_ID", SqlDbType.Int).Value = INVENTORY_ITEM_ID;
                Cmd.Parameters.Add("DESCRIPTION", SqlDbType.NVarChar).Value = DESCRIPTION;
                Cmd.Parameters.Add("PRIMARY_UOM_CODE", SqlDbType.NVarChar).Value = PRIMARY_UOM_CODE;
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
