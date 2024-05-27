using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using RestSharp;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using HMXHTD.Services.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace XHTD_Call_Service.Schedules
{
    public class RealTimeTroughJob : IJob
    {
        private static string LinkAPI_Trough = ConfigurationManager.AppSettings.Get("LinkAPI_Trough")?.ToString();//"http://192.168.158.19/WebCounter";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        //const int LimitVehicle = 12;
        public RealTimeTroughJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                log.Info($@"============start ProcessSyncTrough=============");
                ProcessSyncTrough();
            });
        }
        public void Test()
        {
           var count =  _serviceFactory.StoreOrderOperating.CountVehicleBetweenGetwayAndScale();
        }
        public void ProcessSyncTrough()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            try
            {
                var LimitVehicle = 12;
                var IsCall = true;
                var configs = new List<tblConfigOperating>();
                var configTypeProduct = new List<tblConfigOperating>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    configs = db.tblConfigOperatings.ToList();
                    IsCall = configs.FirstOrDefault(x => x.Code == "IsCall").Value == 1 ? true : false;
                    configTypeProduct = configs.Where(x => x.Code == "PushToDbCallOrderPCB30" || x.Code == "PushToDbCallOrderPCB40" || x.Code == "PushToDbCallOrderROI").ToList();
                }
                SyncTrough("M1",IsCall, configTypeProduct);
                SyncTrough("M2", IsCall, configTypeProduct);
                SyncTrough("M3", IsCall, configTypeProduct);
                SyncTrough("M4", IsCall, configTypeProduct);
                SyncTrough("M5", IsCall, configTypeProduct);
                SyncTrough("M6", IsCall, configTypeProduct);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public int GetMaxVehicleInTroughByType(string typeProduct)
        {
            int Limit = 10;
            try
            {
                var limitCode = "MaxVehicleInTrough_PCB40";
                switch (typeProduct)
                {
                    case "PCB30":
                        limitCode = "MaxVehicleInTrough_PCB30";
                            break;
                    case "PCB40":
                        limitCode = "MaxVehicleInTrough_PCB40";
                        break;
                    case "ROI":
                        limitCode = "MaxVehicleInTrough_ROI";
                        break;
                    default:
                        break;
                }
                using (var db = new HMXuathangtudong_Entities())
                {
                    var configs = db.tblConfigOperatings.ToList();
                    var configMaxInTrough = configs.FirstOrDefault(x => x.Code == limitCode);
                    Limit = (int)configMaxInTrough.Value;
                }
            }
            catch (Exception ex)
            {

            }
            return Limit;
        }
        public void UpdateLineTroughForOrder(string deliveryCode, string lineCode)
        {
            try
            { 
                using (var db = new HMXuathangtudong_Entities())
                {
                    var order = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (order == null) return;
                    order.TroughLineCode = lineCode;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void SyncTrough(string trough, bool IsCall, List<tblConfigOperating> configTypeProduct)
        {
            try
            {
                var LimitVehicle = 10;
                // getListTypeProductInDb
                var typeProducts = new List<string>();
                var troughInfoDb = new tblTrough();
                using (var db = new HMXuathangtudong_Entities())
                {
                    troughInfoDb = db.tblTroughs.FirstOrDefault(x => x.LineCode == trough);
                    if (troughInfoDb == null) return;
                    UpdateLineTroughForOrder(troughInfoDb.DeliveryCodeCurrent, trough);
                    //if (!(bool)troughInfoDb.State) return; // check máng hỏng thì bỏ qua, sau này mở ra
                    typeProducts = troughInfoDb.ProductTypes.Split(',').ToList();
                }

                foreach (var type in typeProducts)
                {
                    LimitVehicle = GetMaxVehicleInTroughByType(type);
                    if (!configTypeProduct.Exists(x => x.Code.Contains(type) && x.Value == 1)) continue;
                    //get sl xe trong bãi chờ máng ứng với sp
                    var vehicleFrontTrough = _serviceFactory.StoreOrderOperating.CountStoreOrderWaitingIntoTroughByType(type);
                    if (vehicleFrontTrough >= LimitVehicle)
                    {
                        if (!String.IsNullOrEmpty(troughInfoDb.DeliveryCodeCurrent))
                        {
                            var isAlmostDone = (troughInfoDb.CountQuantityCurrent / troughInfoDb.PlanQuantityCurrent) > 0.8 ? true : false;
                            if (isAlmostDone)
                            {
                                // cập nhật đơn hàng này thành đã lấy hàng
                                UpdateStepByVehicle(troughInfoDb.DeliveryCodeCurrent, true, trough);
                            }
                            else
                            {
                                // cập nhật đang lấy hàng
                                UpdateStepByVehicle(troughInfoDb.DeliveryCodeCurrent, false, trough);
                            }
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(troughInfoDb.DeliveryCodeCurrent))
                        {
                            // sẽ gọi xe tiếp theo vào
                            ProcessUpdateStepIntoTrough(type, LimitVehicle - vehicleFrontTrough, IsCall);
                            //_storeOrderOperatingService.UpdateStepIntoTrough(type, 2 - vehicleFrontTrough);
                        }
                        else
                        {
                            var isAlmostDone = (troughInfoDb.CountQuantityCurrent / troughInfoDb.PlanQuantityCurrent) > 0.8 ? true : false;

                            if (isAlmostDone)
                            {
                                ProcessUpdateStepIntoTrough(type, LimitVehicle - vehicleFrontTrough, IsCall);
                                //_storeOrderOperatingService.UpdateStepIntoTrough(type, 2 - vehicleFrontTrough);

                                // cập nhật đơn hàng này thành đã lấy hàng
                                UpdateStepByVehicle(troughInfoDb.DeliveryCodeCurrent, true, trough);
                            }
                            else
                            {
                                ProcessUpdateStepIntoTrough(type, LimitVehicle - vehicleFrontTrough, IsCall);
                                // cập nhật đang lấy hàng
                                UpdateStepByVehicle(troughInfoDb.DeliveryCodeCurrent, false, trough);
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($@"SyncTrough1 {ex.Message}");
            }
        }
        public void UpdateStepByVehicle(string deliveryCode, bool isReceived, string trough)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var order = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (order == null) return;
                    if (isReceived)
                    {
                        if (order.Step == 6) return;
                        order.IndexOrder = 0;
                        order.Confirm1 = 1;
                        order.Confirm2 = 1;
                        order.Confirm3 = 1;
                        order.Confirm4 = 1;
                        order.Confirm5 = 1;
                        order.Confirm6 = 1;
                        order.TimeConfirm6 = DateTime.Now;
                        order.Step = 6;
                        order.TroughLineCode = trough;
                        order.LogProcessOrder = order.LogProcessOrder + $@" #xuất hàng xong lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ";
                        db.SaveChanges();
                    }
                    else
                    {
                        if (order.Step == 5) return;
                        order.IndexOrder = 0;
                        order.Confirm1 = 1;
                        order.Confirm2 = 1;
                        order.Confirm3 = 1;
                        order.Confirm4 = 1;
                        order.Confirm5 = 1;
                        order.TimeConfirm5 = DateTime.Now;
                        order.Step = 5;
                        order.TroughLineCode = trough;
                        order.LogProcessOrder = order.LogProcessOrder + $@" #xuất hàng lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ";
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void ProcessUpdateStepIntoTrough(string typeProduct, int topX, bool IsCall)
        {
            try
            {
                //typeProduct = "PCB40";
                //topX = 4;
                if (!IsCall) return;
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 1 && x.TypeProduct.Equals(typeProduct) && x.IndexOrder2 == 0 && (x.DriverUserName ?? "") != "").OrderBy(x => x.IndexOrder).Take(topX).ToList();
                    foreach (var order in orders)
                    {
                        var dateTimeCall = DateTime.Now.AddMinutes(-2);
                        if (order.TimeConfirm1 > dateTimeCall) continue;
                        var sqlUpdate = "UPDATE tblStoreOrderOperating SET Step =  4, Confirm4 = 1,  TimeConfirm4 = ISNULL(TimeConfirm4, GETDATE()), LogProcessOrder = CONCAT(LogProcessOrder, N'#Đưa vào hàng đợi mời xe vào lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) WHERE OrderId = @OrderId AND ISNULL(Step,0) <> 4";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@OrderId", order.OrderId));
                        if (updateResponse > 0)
                        {
                            // xử lý nghiệp vụ đẩy vào db để xử lý gọi loa
                            var tblCallVehicleStatusDb = db.tblCallVehicleStatus.FirstOrDefault(x => x.StoreOrderOperatingId == order.Id && x.IsDone == false);
                            if (tblCallVehicleStatusDb != null) continue;
                            var logString = $@"Xe được mời vào lúc {DateTime.Now} .";
                            var newTblVehicleStatus = new tblCallVehicleStatu
                            {
                                StoreOrderOperatingId = order.Id,
                                CountTry = 0,
                                CreatedOn = DateTime.Now,
                                ModifiledOn = DateTime.Now,
                                TypeProduct = order.TypeProduct,
                                LogCall = logString,
                                IsDone = false
                            };
                            db.tblCallVehicleStatus.Add(newTblVehicleStatus);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
