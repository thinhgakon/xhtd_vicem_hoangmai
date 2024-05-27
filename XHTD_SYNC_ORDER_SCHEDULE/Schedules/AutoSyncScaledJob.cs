using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_SYNC_ORDER_SCHEDULE.Models;

namespace XHTD_SYNC_ORDER_SCHEDULE.Schedules
{
    public class AutoSyncScaledJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public AutoSyncScaledJob(IServiceFactory serviceFactory)
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
                AutoSyncScaleProcess();
            });
        }
        public void AutoSyncScaleProcess()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            SyncScale();
        }
        private void SyncScale()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 2 || x.Step == 3 || x.Step == 5 || x.Step == 6 || x.Step == 7).ToList();
                    // var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 3).ToList();
                    if (orders.Count < 1) return;
                    foreach (var order in orders)
                    {
                        GetOrderInfo(order.DeliveryCode, (int)order.Step);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        public void GetOrderInfo(string deliveryCode, int step)
        {
            try
            {


                var scaleOrderInfo = _serviceFactory.StoreOrderOperating.GetScaleInfoByDeliveryCode(deliveryCode);

                if (step == 2)
                {
                    if (scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL == null)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            orderExist.Confirm3 = 1;
                            orderExist.TimeConfirm3 = orderExist?.TimeConfirm3 ?? DateTime.Now;
                            orderExist.IndexOrder = 0;
                            orderExist.Step = 3;
                            orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 2 ";
                            db.SaveChanges();
                        }
                    }
                    return;
                }
                if (step == 3)
                {
                    if (scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL > 0)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            orderExist.Confirm3 = 1;
                            orderExist.TimeConfirm3 = orderExist?.TimeConfirm3 ?? DateTime.Now;
                            orderExist.Confirm4 = 1;
                            orderExist.TimeConfirm4 = orderExist?.TimeConfirm4 ?? DateTime.Now;
                            orderExist.Confirm5 = 1;
                            orderExist.TimeConfirm5 = orderExist?.TimeConfirm5 ?? DateTime.Now;
                            orderExist.Confirm6 = 1;
                            orderExist.TimeConfirm6 = orderExist?.TimeConfirm6 ?? DateTime.Now;
                            orderExist.IndexOrder = 0;
                            orderExist.Step = 6;
                            orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 3 ";
                            db.SaveChanges();
                        }
                    }
                    else if (scaleOrderInfo.WEIGHTNULL == null && scaleOrderInfo.WEIGHTFULL == null)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            if (orderExist.TimeConfirm3 == null || orderExist.TimeConfirm3 > DateTime.Now.AddMinutes(-60)) return;
                            orderExist.Confirm1 = 0;
                            orderExist.Confirm2 = 0;
                            orderExist.Confirm3 = 0;
                            orderExist.TimeConfirm1 = null;
                            orderExist.TimeConfirm2 = null;
                            orderExist.TimeConfirm3 = null;
                            orderExist.IndexOrder = 0;
                            orderExist.IndexOrder2 = 0;
                            orderExist.Step = 0;
                            orderExist.DriverUserName = null;
                            orderExist.DriverAccept = null;
                            orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale chưa cân ";
                            db.SaveChanges();
                        }

                    }
                    else if (scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL == null)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            if(orderExist.LocationCode.Contains(".XK") || orderExist.TypeProduct == "CLINKER" || orderExist.TypeProduct == "ROI")
                            {
                                orderExist.Confirm5 = 1;
                                orderExist.TimeConfirm5 = DateTime.Now;
                                orderExist.IndexOrder = 0;
                                orderExist.Step = 5;
                                orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale da can vao ";
                                db.SaveChanges();
                            }
                            
                        }

                    }

                    return;
                }
                if (step == 5)
                {
                    if (scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL > 0)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            orderExist.Confirm3 = 1;
                            orderExist.TimeConfirm3 = orderExist?.TimeConfirm3 ?? DateTime.Now;
                            orderExist.Confirm4 = 1;
                            orderExist.TimeConfirm4 = orderExist?.TimeConfirm4 ?? DateTime.Now;
                            orderExist.Confirm5 = 1;
                            orderExist.TimeConfirm5 = orderExist?.TimeConfirm5 ?? DateTime.Now;
                            orderExist.Confirm6 = 1;
                            orderExist.TimeConfirm6 = orderExist?.TimeConfirm6 ?? DateTime.Now;
                            orderExist.IndexOrder = 0;
                            orderExist.Step = 6;
                            orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 5 and stateId 6 ";
                            db.SaveChanges();
                        }
                    }

                    return;
                }
                if (step == 6 && scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL > 0)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        if (orderExist.TimeConfirm6 == null || orderExist.TimeConfirm6 > DateTime.Now.AddMinutes(-30)) return;
                        orderExist.TimeConfirm7 = DateTime.Now;
                        orderExist.Confirm7 = 1;
                        orderExist.TimeConfirm8 = DateTime.Now;
                        orderExist.Confirm8 = 1;
                        orderExist.TimeConfirm8 = DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.Step = 8;
                        orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 6 and stateId 6";
                        db.SaveChanges();
                    }
                    return;
                }
                // tạm thời khi rfid chưa chuyển ở luồng ra
                if (step == 7 && scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL > 0)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {

                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        var timeDecision = orderExist.TimeConfirm7 ?? orderExist.TimeConfirm6;
                        if (timeDecision == null || timeDecision > DateTime.Now.AddMinutes(-30)) return;
                        orderExist.Confirm8 = 1;
                        orderExist.TimeConfirm8 = DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.Step = 8;
                        orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 7 and stateId 6";
                        db.SaveChanges();
                    }
                    return;
                }
                if (step == 7 && scaleOrderInfo.WEIGHTNULL > 0 && scaleOrderInfo.WEIGHTFULL > 0)
                {
                    if (_serviceFactory.ConfigOperating.GetValueByCode("ActiveBarrierGateway") != 1)
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {

                            var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                            if (orderExist.LocationCode.Contains(".XK") || orderExist.TypeProduct == "CLINKER" || orderExist.TypeProduct == "ROI")
                            {
                                if (_serviceFactory.ConfigOperating.GetValueByCode("ActiveBarrierGateway") != 1)
                                {
                                    orderExist.Confirm8 = 1;
                                    orderExist.TimeConfirm8 = DateTime.Now;
                                    orderExist.IndexOrder = 0;
                                    orderExist.Step = 8;
                                    orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale da can ra ";
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                if (orderExist.TimeConfirm6 == null || orderExist.TimeConfirm6 > DateTime.Now.AddMinutes(-30)) return;
                                orderExist.Confirm8 = 1;
                                orderExist.TimeConfirm8 = DateTime.Now;
                                orderExist.IndexOrder = 0;
                                orderExist.Step = 8;
                                orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 7 and stateId 6";
                                db.SaveChanges();
                            }
                        }
                        return;
                    }
                }
                if (step == 7 && scaleOrderInfo.WEIGHTNULL == 0 && scaleOrderInfo.WEIGHTFULL == 0)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                       var sqlUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
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
                                                DriverAccept = NULL,
                                                LogJobAttach = N'Hủy nhận đơn do vào mà không lấy hàng'
                                        WHERE   DeliveryCode = @DeliveryCode";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", deliveryCode));
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor" + ex.Message);
            }
        }

    }
}
