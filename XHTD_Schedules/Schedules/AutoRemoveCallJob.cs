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
    public class AutoRemoveCallJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public AutoRemoveCallJob(IServiceFactory serviceFactory)
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
                AutoRemoveCallProcess();
            });
        }
        public void AutoRemoveCallProcess()
        {
            getToken();
            SyncOrderCallVoice();
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

        private void SyncOrderCallVoice()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 4 || x.Step == 1) && (x.DriverUserName ?? "") != "").ToList();

                    //var sqlSelect = "SELECT TOP 10000 Step,* FROM dbo.tblStoreOrderOperating WHERE ISNULL(DriverUserName,'') = '' AND Step > 0 AND Step <8";
                    //var orders = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect).ToList();

                    if (orders.Count < 1) return;
                    foreach (var order in orders)
                    {
                        ProcessOrder(order.DeliveryCode);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        private void ProcessOrder(string deliveryCode)
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
                if (stateId == 5 && item.LOADWEIGHTNULL != null)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        orderExist.Confirm2 = 1;
                        orderExist.TimeConfirm2 = DateTime.Now;
                        orderExist.Confirm3 = 1;
                        orderExist.Step = 3;
                        orderExist.TimeConfirm3 = orderExist?.TimeConfirm3 ?? DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autoremovecalljob state5 ";
                        db.SaveChanges();
                    }
                }
                //else if(stateId == 2)
                //{
                //    using (var db = new hmxuathangtudong_entities())
                //    {
                //        var orderexist = db.tblstoreorderoperatings.firstordefault(x => x.deliverycode == deliverycode);
                //        db.tblstoreorderoperatings.remove(orderexist);
                //        db.savechanges();
                //    }
                //}
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
