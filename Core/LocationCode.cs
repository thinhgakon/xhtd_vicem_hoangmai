using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class LocationCode
    {
        #region method setData
        public int setData(int CODE_ID, int SHIPPOINT_ID, int CUSTOMER_ID, int AREA_ID, string LOCATION_CODE)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblLocationCode WHERE CODE_ID = @CODE_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblLocationCode(CODE_ID,SHIPPOINT_ID,CUSTOMER_ID,AREA_ID,LOCATION_CODE) VALUES(@CODE_ID,@SHIPPOINT_ID,@CUSTOMER_ID,@AREA_ID,@LOCATION_CODE) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CODE_ID", SqlDbType.Int).Value = CODE_ID;
                Cmd.Parameters.Add("SHIPPOINT_ID", SqlDbType.Int).Value = SHIPPOINT_ID;
                Cmd.Parameters.Add("CUSTOMER_ID", SqlDbType.Int).Value = CUSTOMER_ID;
                Cmd.Parameters.Add("AREA_ID", SqlDbType.Int).Value = AREA_ID;
                Cmd.Parameters.Add("LOCATION_CODE", SqlDbType.NVarChar).Value = LOCATION_CODE;
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
