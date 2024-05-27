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

namespace XHTD_Call_Service.Schedules
{
    public class PushToDbCallClinkerJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        //const int LimitVehicle = 12;
        public PushToDbCallClinkerJob(IServiceFactory serviceFactory)
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
                log.Info($@"============start PushToDbCallClinkerJob=============");
                PushToDbCallClinkerProccesss();
            });
        }
        public void PushToDbCallClinkerProccesss()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            try
            {
                var LimitVehicle = 5;
                var IsCall = true;
                using (var db = new HMXuathangtudong_Entities())
                {
                    var configs = db.tblConfigOperatings.ToList();
                    var configMaxInTrough = configs.FirstOrDefault(x => x.Code == "MaxVehicleClinker");
                    LimitVehicle = (int)configMaxInTrough.Value;
                    IsCall = configs.FirstOrDefault(x => x.Code == "IsCall").Value == 1 ? true : false;
                }
                ProcessPushToDBCall( LimitVehicle, IsCall);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void ProcessPushToDBCall(int LimitVehicle, bool IsCall)
        {
            try
            {
                //get sl xe trong bãi chờ máng ứng với sp
                var vehicleFrontClinkerYard = _serviceFactory.StoreOrderOperating.CountStoreOrderWaitingIntoTroughByType("CLINKER");
                if (vehicleFrontClinkerYard >= LimitVehicle)
                {

                }
                else
                {
                    ProcessUpdateStepIntoClinkerYard(LimitVehicle - vehicleFrontClinkerYard, IsCall);

                }
            }
            catch (Exception ex)
            {
                log.Error($@"SyncTrough1 {ex.Message}");
            }
        }
        public void ProcessUpdateStepIntoClinkerYard(int topX, bool IsCall)
        {
            try
            {
                //typeProduct = "PCB40";
                //topX = 4;
                if (!IsCall) return;
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 1 && x.TypeProduct.Equals("CLINKER") && x.IndexOrder2 == 0 && (x.DriverUserName ?? "") != "").OrderBy(x => x.IndexOrder).Take(topX).ToList();
                    foreach (var order in orders)
                    {
                        var dateTimeCall = DateTime.Now.AddMinutes(-2);
                        if (order.TimeConfirm1 > dateTimeCall) continue;
                        var sqlUpdate = "UPDATE tblStoreOrderOperating SET Step =  4,  TimeConfirm4 = ISNULL(TimeConfirm4, GETDATE()), LogProcessOrder = CONCAT(LogProcessOrder, N'#Đưa vào hàng đợi mời xe vào lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) WHERE OrderId = @OrderId AND ISNULL(Step,0) <> 4";
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
                                TypeProduct = order.TypeProduct,
                                CreatedOn = DateTime.Now,
                                ModifiledOn = DateTime.Now,
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
        public void UpdateStepByVehicle(string deliveryCode, bool isReceived)
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
        
    }
}
