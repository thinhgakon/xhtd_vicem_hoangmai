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
using XHTD_Schedules.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using XHTD_Schedules.SignalRNotification;
using HMXHTD.Services.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace XHTD_Schedules.Schedules
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
            getToken();
            syncScale();
        }
        private string getToken()
        {
            if (!String.IsNullOrEmpty(strToken) && expireTimeToken > DateTime.Now.AddMinutes(60))
            {
                return strToken;
            }


            var requestData = new TokenRequestModel
            {
                userName = ConfigurationManager.AppSettings.Get("userNameAPI").ToString(),
                password = ConfigurationManager.AppSettings.Get("passwordAPI").ToString(),
                OUId = "302"
            };
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_WebSale").ToString() + "/api/get-token");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<TokenResponseModel>(content);
                strToken = responseData.token;
                expireTimeToken = responseData.expires;

                return responseData.token;
            }
            catch (Exception ex)
            {
                log.Error("getToken" + ex.Message);
                return "";
            }
        }

        private void syncScale()
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
        private void GetOrderInfo(string deliveryCode, int step)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_WebSale").ToString() + $"/api/order-detail/{deliveryCode}");
                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", "Bearer " + strToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<List<ResponseOrderItem>>(content);
                var item = responseData.FirstOrDefault();
                var stateId = 0;
                var statusName = "";
                switch (item.STATUS.ToUpper())
                {
                    case "BOOKED":
                        switch (item.PRINT_STATUS.ToUpper())
                        {
                            case "BOOKED":
                            case "APPROVED":
                            case "PENDING":
                                stateId = 1;
                                statusName = "Đã xác nhận";
                                break;
                            case "PRINTED":
                                stateId = 4;
                                statusName = "Đã in phiếu";
                                break;
                        }
                        break;
                    case "PRINTED":
                        stateId = 4;
                        statusName = "Đã in phiếu";
                        break;
                    case "VOIDED":
                        stateId = 2;
                        statusName = "Đã hủy";
                        break;
                    case "RECEIVING":
                        stateId = 5;
                        statusName = "Đang lấy hàng";
                        break;
                    case "RECEIVED":
                        stateId = 6;
                        statusName = "Đã xuất hàng";
                        break;
                }
                if(step == 2)
                {
                    if (stateId == 5 && item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL == null)
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
                //08827
                if (step == 3)
                {
                    if (stateId == 6 && item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL != null)
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
                    else if (item.LOADWEIGHTNULL == null && item.LOADWEIGHTFULL == null)
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
                    
                    return;
                }
                if (step == 5)
                {
                    if (stateId == 6 && item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL != null)
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
                if (step == 7 && stateId == 6 && item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL != null)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        orderExist.Confirm8 = 1;
                        orderExist.TimeConfirm8 = DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.Step = 8;
                        orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autosyncscale step 7 and stateId 6";
                        db.SaveChanges();
                    }
                    return;
                }

                if (stateId == 2)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        db.tblStoreOrderOperatings.Remove(orderExist);
                        db.SaveChanges();
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
