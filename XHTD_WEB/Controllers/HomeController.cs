using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XHTD_WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var orders = new List<StoreOrderForLED12>();
            //using (var db = new HMXuathangtudong_Entities())
            //{
            //    var sqlSelect = "SELECT TOP 8 B.Id, B.IndexOrder, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Chờ vào cổng'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE Step IN (0,1,4) AND B.IndexOrder > 0 AND ISNULL(IndexOrder2,0) = 0 ORDER BY B.Step DESC";
            //    orders = db.Database.SqlQuery<StoreOrderForLED12>(sqlSelect).ToList();
            //}
            try
            {
                string SQLQUERY = "SELECT TOP 100 B.Id, B.IndexOrder, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar), N'0', N'Chưa xác thực'), N'1', N'Chờ vào cổng'), N'2', N'Đã vào cổng'), N'3', N'Đã cân vào'), N'4', N'Mời xe vào'), N'5', N'Đang lấy hàng'), N'6', N'Đã lấy hàng') AS State1, Vehicle, DriverName, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE Step IN(0, 1, 4) AND B.IndexOrder > 0 AND ISNULL(IndexOrder2,0) = 0 ORDER BY B.IndexOrder ASC";
                SqlConnection sqlCon = new SqlConnection("Server = '192.168.158.19\\XHTD'; Uid = 'hmxhtd'; pwd = 'bh123!@#'; Database = 'QLBanhang_Test'");
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {

                    StoreOrderForLED12 entity = new StoreOrderForLED12();
                    entity.Vehicle = Rd["Vehicle"].ToString();
                    entity.State1 = Rd["State1"].ToString();
                    orders.Add(entity);
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return View(orders);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    public class StoreOrderForLED12
    {
        public int Id { get; set; }
        public int IndexOrder { get; set; }
        public string State1 { get; set; }
        public string Vehicle { get; set; }
        public string DriverName { get; set; }
        public string NameDistributor { get; set; }
        public string NameProduct { get; set; }
        public int? IndexOrder1 { get; set; }

    }
}