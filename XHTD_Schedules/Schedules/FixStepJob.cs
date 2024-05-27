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

namespace XHTD_Schedules.Schedules
{
    public class FixStepJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public FixStepJob(IServiceFactory serviceFactory)
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
                    var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 1).ToList();

                   // var orders = db.tblStoreOrderOperatings.Where(x => x.Step < 8 && x.Step > 0 && (x.DriverUserName ?? "") == "").ToList();

                    if (orders.Count < 1) return;
                    foreach (var order in orders)
                    {
                        GetOrderInfo(order.DeliveryCode);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        private void GetOrderInfo(string deliveryCode)
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
                //if (item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL != null)
                //{
                //    using (var db = new HMXuathangtudong_Entities())
                //    {
                //        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                //        orderExist.Confirm1 = 1;
                //        orderExist.Confirm2 = 1;
                //        orderExist.Confirm3 = 1;
                //        orderExist.Confirm4 = 1;
                //        orderExist.Confirm5 = 1;
                //        orderExist.Confirm6 = 1;
                //        orderExist.IndexOrder = 0;
                //        orderExist.Step = 6;
                //        db.SaveChanges();
                //    }
                //}
                //else if (stateId == 2)
                //{
                //    using (var db = new HMXuathangtudong_Entities())
                //    {
                //        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                //        db.tblStoreOrderOperatings.Remove(orderExist);
                //        db.SaveChanges();
                //    }
                //}
                ////else
                var s = item;
                if (stateId == 1 || stateId == 4)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        //if (orderExist.TimeConfirm1 == null) return;
                        //if (orderExist.TimeConfirm1 < DateTime.Now.AddMinutes(-60)) return;
                        orderExist.Confirm1 = 0;
                        orderExist.Confirm2 = 0;
                        orderExist.Confirm3 = 0;
                        orderExist.TimeConfirm1 = null;
                        orderExist.TimeConfirm2 = null;
                        orderExist.TimeConfirm3 = null;
                        orderExist.IndexOrder = 0;
                        orderExist.IndexOrder2 = 0;
                        orderExist.Step = 0;
                        db.SaveChanges();
                    }
                }
                //else 
                if (stateId == 6 && item.LOADWEIGHTNULL != null && item.LOADWEIGHTFULL != null)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        //if (orderExist.TimeConfirm6 == null || orderExist.TimeConfirm6 > DateTime.Now.AddMinutes(-30)) return;
                        orderExist.Confirm1 = 1;
                        orderExist.Confirm2 = 1;
                        orderExist.Confirm3 = 1;
                        orderExist.Confirm4 = 1;
                        orderExist.Confirm5 = 1;
                        orderExist.Confirm6 = 1;
                        orderExist.Confirm7 = 1;
                        orderExist.Confirm8 = 1;
                        orderExist.TimeConfirm7 = DateTime.Now;
                        orderExist.TimeConfirm8 = DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.IndexOrder2 = 0;
                        orderExist.Step = 8;
                        db.SaveChanges();
                    }
                }
                if(stateId == 2)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        //if (orderExist.TimeConfirm6 == null || orderExist.TimeConfirm6 > DateTime.Now.AddMinutes(-30)) return;
                        orderExist.Confirm1 = 1;
                        orderExist.Confirm2 = 1;
                        orderExist.Confirm3 = 1;
                        orderExist.Confirm4 = 1;
                        orderExist.Confirm5 = 1;
                        orderExist.Confirm6 = 1;
                        orderExist.Confirm7 = 1;
                        orderExist.Confirm8 = 1;
                        orderExist.Confirm9 = 1;
                        orderExist.IndexOrder = 0;
                        orderExist.IndexOrder2 = 0;
                        orderExist.Step = 9;
                        orderExist.IsVoiced = true;

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
