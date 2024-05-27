using HMXHTD.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public class FileKCS
    {
        #region method getTableKCS
        public DataTable GetTableKCS(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            string SQL_SEARCH = "";
            if (SearchKey.Trim() != "")
            {
                SQL_SEARCH = " AND B.KCSCode LIKE N'%'+@SearchKey+'%' ";
            }

            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 100 * FROM dbo.tblKCSOperating B WHERE B.CreatedOn BETWEEN @objDate1 AND @objDate2  " + SQL_SEARCH + " ORDER BY B.Id DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = objDate1;
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = objDate2;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion
    }
}
