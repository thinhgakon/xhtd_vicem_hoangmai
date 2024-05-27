using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XHTD_WEB.Controllers
{
    public class AreaVehicleController : Controller
    {
        // GET: AreaVehilce
        public ActionResult Index()
        {
            var model = new AreaVehicleModel();
            model.T1 = GetVehicleByType("NULL,0");
            model.T2 = GetVehicleByType("1,4");
            model.T3 = GetVehicleByType("2,3");
            model.T4 = GetVehicleByType("5,6");
            model.T5 = GetVehicleByType("7");
            return View(model);
        }
        public List<tblStoreOrderOperating> GetVehicleByType(string steps)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Step IN ({steps})  AND ISNULL(DriverUserName,'') != ''";
                var vehicles = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect).ToList();
                return vehicles;
            }
        }
    }
    public class AreaVehicleModel
    {
        public List<tblStoreOrderOperating> T1 { get; set; }
        public List<tblStoreOrderOperating> T2 { get; set; }
        public List<tblStoreOrderOperating> T3 { get; set; }
        public List<tblStoreOrderOperating> T4 { get; set; }
        public List<tblStoreOrderOperating> T5 { get; set; }
        public AreaVehicleModel()
        {
            T1 = new List<tblStoreOrderOperating>();
            T2 = new List<tblStoreOrderOperating>();
            T3 = new List<tblStoreOrderOperating>();
            T4 = new List<tblStoreOrderOperating>();
            T5 = new List<tblStoreOrderOperating>();
        }
    }
}