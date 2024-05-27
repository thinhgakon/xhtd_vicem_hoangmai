using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class Shippoints
    {
        #region method setData
        public int setData(int SHIPPOINT_ID, string SHIPPOINT_CODE, string SHIPPOINT_NAME)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblShippoints WHERE SHIPPOINT_ID = @SHIPPOINT_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblShippoints(SHIPPOINT_ID,SHIPPOINT_CODE,SHIPPOINT_NAME) VALUES(@SHIPPOINT_ID,@SHIPPOINT_CODE,@SHIPPOINT_NAME) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("SHIPPOINT_ID", SqlDbType.Int).Value = SHIPPOINT_ID;
                Cmd.Parameters.Add("SHIPPOINT_CODE", SqlDbType.NVarChar).Value = SHIPPOINT_CODE;
                Cmd.Parameters.Add("SHIPPOINT_NAME", SqlDbType.NVarChar).Value = SHIPPOINT_NAME;
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
