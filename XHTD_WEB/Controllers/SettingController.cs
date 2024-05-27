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
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index(string keyword, int? step)
        {
            step = step ?? 1;
            ViewBag.Keyword = keyword ?? "";
            ViewBag.Step = step ?? 1;
             var orders = new List<tblStoreOrderOperating>();
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = step == -1 ?
                                $@"SELECT TOP 10 * FROM dbo.tblStoreOrderOperating WHERE Vehicle like @keyword AND Step > @Step order by Id desc"
                                : $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Vehicle like @keyword AND Step = @Step order by Id desc";
                var sqlInLed = $@"SELECT COUNT(DISTINCT Vehicle) FROM dbo.tblStoreOrderOperating WHERE Step IN (1,4)";
                var sqlInGate = $@"SELECT COUNT(DISTINCT Vehicle) FROM dbo.tblStoreOrderOperating WHERE Step IN (2,3,4,5) AND TypeProduct != 'CLINKER' AND TypeProduct != 'ROI'";
                var sqlConfigMaxVehicle = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'MaxVehicleInTrough'";
                var sqlConfigReIndex = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'MaxReIndex'";
                var sqlConfigIsCall = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'IsCall'";
                var sqlConfigIsAutoScale = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'IsAutoScale'";
                orders = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect, new SqlParameter("@keyword", $@"%{keyword}%"), new SqlParameter("@step", step)).ToList();

                ViewBag.CountVehicleLed = db.Database.SqlQuery<int>(sqlInLed).SingleOrDefault();
                ViewBag.CountVehicleInGate = db.Database.SqlQuery<int>(sqlInGate).SingleOrDefault();
                ViewBag.MaxVehicleInTrough = db.Database.SqlQuery<int>(sqlConfigMaxVehicle).SingleOrDefault();
                ViewBag.MaxReIndex = db.Database.SqlQuery<int>(sqlConfigReIndex).SingleOrDefault();
                ViewBag.IsCall = db.Database.SqlQuery<int>(sqlConfigIsCall).SingleOrDefault();
                ViewBag.IsAutoScale = db.Database.SqlQuery<int>(sqlConfigIsAutoScale).SingleOrDefault();
            }
            ViewBag.Count = orders.Count;
            return View(orders);
        }

        [HttpPost]
        public JsonResult SetMaxVehicleInTrough(string value)
        {
            if (String.IsNullOrEmpty(value)) return Json("Chưa nhập giá trị", JsonRequestBehavior.AllowGet);
            var maxValue = Int32.Parse(value);
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblConfigOperating SET Value = @value WHERE Code = 'MaxVehicleInTrough'";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@value", maxValue));
                if(updateResponse > 0)
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Cập nhật không thành công", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetMaxReIndex(string value)
        {
            if (String.IsNullOrEmpty(value)) return Json("Chưa nhập giá trị", JsonRequestBehavior.AllowGet);
            var maxValue = Int32.Parse(value);
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblConfigOperating SET Value = @value WHERE  Code = 'MaxReIndex'";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@value", maxValue));
                if (updateResponse > 0)
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Cập nhật không thành công", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetIsCall(bool isCall)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblConfigOperating SET Value = @value WHERE  Code = 'IsCall'";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@value", isCall ? 1 : 0));
                if (updateResponse > 0)
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Cập nhật không thành công", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetIsAutoScale(bool isAutoScale)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblConfigOperating SET Value = @value WHERE  Code = 'IsAutoScale'";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@value", isAutoScale ? 1 : 0));
                if (updateResponse > 0)
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Cập nhật không thành công", JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult SetStepOrder(int id, int step)
        {
            
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "";
                switch (step)
                {
                    case -1: // hủy nhận đơn
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     
                                                Step = 0 ,
                                                Confirm1 = 0 ,
                                                TimeConfirm1 = NULL ,
                                                Confirm2 = 0 ,
                                                TimeConfirm2 = NULL ,
                                                Confirm3 = 0 ,
                                                TimeConfirm3 = NULL ,
                                                Confirm4 = 0 ,
                                                TimeConfirm4 = NULL ,
                                                Confirm5 = 0 ,
                                                TimeConfirm5 = NULL ,
                                                Confirm6 = 0 ,
                                                TimeConfirm6 = NULL ,
                                                Confirm7 = 0 ,
                                                TimeConfirm7 = NULL ,
                                                Confirm8 = 0 ,
                                                TimeConfirm8 = NULL ,
		                                        Confirm9 = 0,
		                                        TimeConfirm9 = NULL,
                                                IndexOrder = 0 ,
		                                        DeliveryCodeParent = NULL,
		                                        IndexOrder2 = 0,
                                                IndexOrder1 = 0,
		                                        DriverUserName = NULL,
                                                 DriverAccept = NULL
                                        WHERE   Id = @Id";
                        break;
                    case 0: // đặt về lúc chưa xác thực
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     
                                                Step = 0 ,
                                                Confirm1 = 0 ,
                                                TimeConfirm1 = NULL ,
                                                Confirm2 = 0 ,
                                                TimeConfirm2 = NULL ,
                                                Confirm3 = 0 ,
                                                TimeConfirm3 = NULL ,
                                                Confirm4 = 0 ,
                                                TimeConfirm4 = NULL ,
                                                Confirm5 = 0 ,
                                                TimeConfirm5 = NULL ,
                                                Confirm6 = 0 ,
                                                TimeConfirm6 = NULL ,
                                                Confirm7 = 0 ,
                                                TimeConfirm7 = NULL ,
                                                Confirm8 = 0 ,
                                                TimeConfirm8 = NULL ,
		                                        Confirm9 = 0,
		                                        TimeConfirm9 = NULL,
                                                IndexOrder = 0 ,
		                                        DeliveryCodeParent = NULL,
		                                        IndexOrder2 = 0,
                                                IndexOrder1 = 0
                                        WHERE   Id = @Id";
                        break;
                    case 1: 
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 1 ,
                                                Confirm1 = 1 ,
                                                TimeConfirm1 = GETDATE() ,
                                                LogHistory = CONCAT(LogHistory,N', Xác thực bằng tay lúc ', GETDATE()) ,
                                                IndexOrder = (SELECT MAX(IndexOrder) FROM tblStoreOrderOperating WHERE TypeProduct = (SELECT TOP 1 TypeProduct FROM dbo.tblStoreOrderOperating WHERE DeliveryCode =  @DeliveryCode)) + 1
                                        WHERE   Id = @Id";
                        break;
                    case 2: 
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 2 ,
                                                Confirm1 = 1 ,
                                                Confirm2 = 1 ,
                                                TimeConfirm2 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 3: 
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 3 ,
                                                Confirm1 = 1 ,
                                                Confirm2 = 1 ,
                                                Confirm3 = 1 ,
                                                TimeConfirm3 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 4:
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 4 ,
                                               IndexOrder = IndexOrder1
                                        WHERE   Id = @Id";
                        break;
                    case 5:
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 5 ,
                                                Confirm1 = 1 ,
                                                Confirm2 = 1 ,
                                                Confirm3 = 1 ,
                                                Confirm4 = 1 ,
                                                Confirm5 = 1 ,
                                                TimeConfirm5 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 6:
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 6 ,
                                                Confirm1 = 1 ,
                                                Confirm2 = 1 ,
                                                Confirm3 = 1 ,
                                                Confirm4 = 1 ,
                                                Confirm5 = 1 ,
                                                Confirm6 = 1 ,
                                                TimeConfirm6 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 7: 
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 7 ,
                                                Confirm7 = 1 ,
                                                TimeConfirm7 = GETDATE() ,
                                                IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 8: 
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 8 ,
                                                Confirm1 = 1 ,
                                                TimeConfirm1 = GETDATE() ,
                                                Confirm2 = 1 ,
                                                TimeConfirm2 = GETDATE() ,
                                                Confirm3 = 1 ,
                                                TimeConfirm3 = GETDATE() ,
                                                Confirm4 = 1 ,
                                                TimeConfirm4 = GETDATE() ,
                                                Confirm5 = 1 ,
                                                TimeConfirm5 = GETDATE() ,
                                                Confirm6 = 1 ,
                                                TimeConfirm6 = GETDATE() ,
                                                Confirm7 = 1 ,
                                                TimeConfirm7 = GETDATE() ,
                                               Confirm8 = 1 ,
                                               TimeConfirm8 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 9:
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 9 ,
                                                Confirm1 = 1 ,
                                                TimeConfirm1 = GETDATE() ,
                                                Confirm2 = 1 ,
                                                TimeConfirm2 = GETDATE() ,
                                                Confirm3 = 1 ,
                                                TimeConfirm3 = GETDATE() ,
                                                Confirm4 = 1 ,
                                                TimeConfirm4 = GETDATE() ,
                                                Confirm5 = 1 ,
                                                TimeConfirm5 = GETDATE() ,
                                                Confirm6 = 1 ,
                                                TimeConfirm6 = GETDATE() ,
                                                Confirm7 = 1 ,
                                                TimeConfirm7 = GETDATE() ,
                                               Confirm8 = 1 ,
                                               TimeConfirm8 = GETDATE() ,
                                                Confirm9 = 1 ,
                                               TimeConfirm9 = GETDATE() ,
                                               IndexOrder = 0
                                        WHERE   Id = @Id";
                        break;
                    case 11:
                        sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     Step = 1 ,
                                                Confirm1 = 1 ,
                                                TimeConfirm1 = GETDATE() ,
                                                Confirm2 = 0 ,
                                                TimeConfirm2 = NULL ,
                                               IndexOrder = IndexOrder1
                                        WHERE   Id = @Id";
                        break;
                    default:
                        break;
                }
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@Id", id));
                if (updateResponse > 0)
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Cập nhật không thành công", JsonRequestBehavior.AllowGet);
        }
        public class StepItem
        {
            public int Step { get; set; }
            public string Description { get; set; }

        }
    }
}