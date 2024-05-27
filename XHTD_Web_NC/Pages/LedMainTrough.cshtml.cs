using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XHTD_Web_NC.Pages
{
    public class LedMainTroughModel : PageModel
    {
        public List<StoreOrderForLEDTrough> Orders { get; set; } = new List<StoreOrderForLEDTrough>();
        public void OnGet()
        {
            try
            {
                string SQLQUERY = "SELECT DISTINCT Vehicle, Step FROM dbo.tblStoreOrderOperating WHERE Step IN (3,5) AND ISNULL(IndexOrder2, 0) = 0 ORDER BY Step DESC";
                SqlConnection sqlCon = new SqlConnection("Server = '192.168.158.19\\XHTD'; Uid = 'hmxhtd'; pwd = 'bh123!@#'; Database = 'QLBanhang_Test'");
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {

                    StoreOrderForLEDTrough entity = new StoreOrderForLEDTrough();
                    entity.Vehicle = Rd["Vehicle"].ToString();
                    entity.Step = Int32.Parse(Rd["Step"].ToString());
                    Orders.Add(entity);
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
        }
    }
    public class StoreOrderForLEDTrough
    {
        public int Step { get; set; }
        public string Vehicle { get; set; }

    }
}
