using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XHTD_WEB.Controllers
{
    public class ScaleController : Controller
    {
        // GET: Scale
        public ActionResult Index()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"SELECT TOP 100 * FROM dbo.tblScaleLogOperating ORDER BY ModifieldOn DESC";
                var orders = db.Database.SqlQuery<ScaleLog>(sqlSelect).ToList();
                return View(orders);
            }
            
        }
    }
    public class ScaleLog
    {
        public int Id { get; set; }
        public string Vehicle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? WeightScaleIn { get; set; }
        public double? WeightScaleOut { get; set; }
        public double? LoadWeightNull { get; set; }
        public double? LoadWeightFull { get; set; }
        public DateTime? TimeInScale { get; set; }
        public DateTime? TimeOutScale { get; set; }
    }
}