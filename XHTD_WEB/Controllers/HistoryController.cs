using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Models;

namespace XHTD_WEB.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index(string keyword, int? step)
        {
            step = step ?? 1;
            ViewBag.Keyword = keyword ?? "";
            ViewBag.Step = step ?? 1;
            var orders = new List<tblStoreOrderOperating>();
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = step == -1 ?
                                $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Vehicle like @keyword AND Step > @Step order by Id desc"
                                : $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Vehicle like @keyword AND Step = @Step order by Id desc";
                var sqlInLed = $@"SELECT COUNT(DISTINCT Vehicle) FROM dbo.tblStoreOrderOperating WHERE Step IN (1,4)";
                var sqlInGate = $@"SELECT COUNT(DISTINCT Vehicle) FROM dbo.tblStoreOrderOperating WHERE Step IN (2,3,4)";
                var sqlConfigMaxVehicle = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'MaxVehicleInTrough'";
                var sqlConfigReIndex = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'MaxReIndex'";
                var sqlConfigIsCall = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'IsCall'";
                orders = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect, new SqlParameter("@keyword", $@"%{keyword}%"), new SqlParameter("@step", step)).ToList();

                ViewBag.CountVehicleLed = db.Database.SqlQuery<int>(sqlInLed).SingleOrDefault();
                ViewBag.CountVehicleInGate = db.Database.SqlQuery<int>(sqlInGate).SingleOrDefault();
                ViewBag.MaxVehicleInTrough = db.Database.SqlQuery<int>(sqlConfigMaxVehicle).SingleOrDefault();
                ViewBag.MaxReIndex = db.Database.SqlQuery<int>(sqlConfigReIndex).SingleOrDefault();
                ViewBag.IsCall = db.Database.SqlQuery<int>(sqlConfigIsCall).SingleOrDefault();
            }
            ViewBag.Count = orders.Count;
            return View(orders);
        }

        [HttpGet]
        public JsonResult GetHistoryCall(int id)
        {
            var historyString = "";
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = "SELECT TOP 100 * FROM dbo.tblCallVehicleStatus WHERE StoreOrderOperatingId = @id";
                var histories = db.Database.SqlQuery<tblCallVehicleStatu>(sqlSelect, new SqlParameter("@id", id)).ToList();
                foreach (var history in histories)
                {
                    historyString += "\n " + history.LogCall;
                }
                return Json(historyString, JsonRequestBehavior.AllowGet);
            }
        }
        public class StepItem
        {
            public int Step { get; set; }
            public string Description { get; set; }

        }
    }
}