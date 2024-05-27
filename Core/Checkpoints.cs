using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HMXHTD
{
    class Checkpoints
    {
        #region method setData
        public int setData(int CHECKPOINT_ID, string CODE, string NAME)
        {
            int tmpValue = 0;
            try
            {
                string sqlQuery = "";
                sqlQuery = "IF NOT EXISTS (SELECT * FROM IERP_tblCheckpoints WHERE CHECKPOINT_ID = @CHECKPOINT_ID) ";
                sqlQuery += "BEGIN INSERT INTO IERP_tblCheckpoints(CHECKPOINT_ID,CODE,NAME) VALUES(@CHECKPOINT_ID,@CODE,@NAME) END ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CHECKPOINT_ID", SqlDbType.Int).Value = CHECKPOINT_ID;
                Cmd.Parameters.Add("CODE", SqlDbType.NVarChar).Value = CODE;
                Cmd.Parameters.Add("NAME", SqlDbType.NVarChar).Value = NAME;
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
