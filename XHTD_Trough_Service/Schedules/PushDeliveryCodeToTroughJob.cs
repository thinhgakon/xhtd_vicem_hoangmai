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
using XHTD_Trough_Service.Models;
using XHTD_Trough_Service.Models.TroughApiModels;
namespace XHTD_Trough_Service.Schedules
{
    public class PushDeliveryCodeToTroughJob : IJob
    {
        private static string LinkAPI_Trough = ConfigurationManager.AppSettings.Get("LinkAPI_Trough")?.ToString();//"http://192.168.158.19/WebCounter";
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public PushDeliveryCodeToTroughJob(IServiceFactory serviceFactory)
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
                PushDeliveryCodeToTroughProcess();
            });
        }
        public void PushDeliveryCodeToTroughProcess()
        {
            try
            {
                if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
                var deliveryCodes = new List<tblDeliveryCodeTroughOperating>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    deliveryCodes = db.tblDeliveryCodeTroughOperatings.Where(x => x.IsSentTrough == false).ToList();
                }
                if (deliveryCodes.Count < 1) return;
                foreach (var item in deliveryCodes)
                {
                    ProcessOrderToTrough(item.DeliveryCode, item.Vehicle);
                }
            }
            catch (Exception ex)
            {
                log.Error($@"PushDeliveryCodeToTroughProcess {ex.Message}");
            }
        }
        public void ProcessOrderToTrough(string deliveryCode, string vehicle)
        {
            try
            {
                //UpdateDeliveryCodeTrough(deliveryCode); //sau nay bo di
                //log.Info($@"{vehicle} is receiving"); // sau nay bo di
                //return; // sau nay bo di

                var isOverTime = CheckOverTime(deliveryCode);
                if (isOverTime) return;

                var orderWeight = _serviceFactory.StoreOrderOperating.GetScaleInfoByDeliveryCode(deliveryCode);
                log.Info($@"======{orderWeight.WEIGHTNULL}=========={orderWeight.STATUS}======={deliveryCode}");
                if (orderWeight.WEIGHTNULL < 1) return;
                if (orderWeight.STATUS != "RECEIVING") return;
                // bổ sung luồng cho clinker và xuất khẩu để step = 5 khi đã cân vào
                using (var db = new HMXuathangtudong_Entities())
                {
                    var order = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (order.LocationCode.Contains(".XK") || order.TypeProduct == "CLINKER" || order.TypeProduct == "ROI")
                    {
                        var queryUpdateDeliveryTrough = $@"UPDATE dbo.tblDeliveryCodeTroughOperating SET IsSentTrough = 1, TimeSendTrough = GETDATE(), LogHistory = N'CLINKER OR XK OR XMR, not push to trough' WHERE DeliveryCode = @DeliveryCode";
                        var ResponseUpdateDeliveryTrough = db.Database.ExecuteSqlCommand(queryUpdateDeliveryTrough, new SqlParameter("@DeliveryCode", deliveryCode));
                    }
                    var queryUpdateOrder = $@"UPDATE dbo.tblStoreOrderOperating SET Step = 5, Confirm4 = 1, TimeConfirm4 = ISNULL(TimeConfirm4, GETDATE()) , Confirm5 = 1, TimeConfirm5 = ISNULL(TimeConfirm5, GETDATE()), TimeConfirm6 = ISNULL(TimeConfirm6, GETDATE()) WHERE DeliveryCode = @DeliveryCode";
                    var ResponseUpdateOrder = db.Database.ExecuteSqlCommand(queryUpdateOrder, new SqlParameter("@DeliveryCode", deliveryCode));
                    if (order.TypeProduct == "CLINKER" || order.TypeProduct == "ROI")
                    {
                        return;
                    }
                }
                var res = SendToTroughAPI(deliveryCode);
                log.Info($@"push delivery code : {deliveryCode}, statusCode: {res.Code}");
                if (res.Code == 200)
                {
                    UpdateDeliveryCodeTrough(deliveryCode);
                }
                // phần này lưu log
                using (var db = new HMXuathangtudong_Entities())
                {
                    var troughOrder = db.tblTroughOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (troughOrder == null)
                    {
                        var newTroughOrder = new tblTroughOrderOperating
                        {
                            DeliveryCode = deliveryCode,
                            Vehicle = vehicle,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now,
                            Status = 1,
                            LogResponse = ""
                        };
                        if (res.Code != 200)
                        {
                            newTroughOrder.Status = 4;
                            newTroughOrder.LogResponse = newTroughOrder.LogResponse + "," + res.Mesage;
                        }
                        db.tblTroughOrderOperatings.Add(newTroughOrder);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (res.Code != 200)
                        {
                            troughOrder.Status = 4;
                            troughOrder.ModifiedOn = DateTime.Now;
                            troughOrder.LogResponse = troughOrder.LogResponse + "," + res.Mesage;
                        }
                        else
                        {
                            troughOrder.Status = 1;
                            troughOrder.ModifiedOn = DateTime.Now;
                            troughOrder.LogResponse = troughOrder.LogResponse + "," + res.Mesage;
                        }
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
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
        private bool CheckOverTime(string deliveryCode)
        {
            bool isOverTime = false;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var deliveryCodeTrough = db.tblDeliveryCodeTroughOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if(deliveryCodeTrough.CreatedOn <  DateTime.Now.AddMinutes(-30))
                    {
                        db.tblDeliveryCodeTroughOperatings.Remove(deliveryCodeTrough);
                        deliveryCodeTrough.IsSentTrough = true;
                        deliveryCodeTrough.LogHistory = "Quá thời gian 30 sau khi cân không đẩy thành công";
                        deliveryCodeTrough.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("CheckOverTime" + ex.Message);
            }
            return isOverTime;
        }
        private bool CheckOrderHasWeight(string deliveryCode)
        {
            bool isHasWeightIn = false;
            try
            {
                strToken = getToken();
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
               
                if (item.LOADWEIGHTNULL != null)
                {
                    isHasWeightIn =  true;
                }
                
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor" + ex.Message);
            }
            return isHasWeightIn;
        }
        public void UpdateDeliveryCodeTrough(string deliveryCode)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var query = $@"UPDATE dbo.tblDeliveryCodeTroughOperating SET IsSentTrough = 1, TimeSendTrough = GETDATE(), LogHistory = N'Push By System' WHERE DeliveryCode = @DeliveryCode";
                    var InsertResponse = db.Database.ExecuteSqlCommand(query, new SqlParameter("@DeliveryCode", deliveryCode));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public ResponseApiSendToTroughModel SendToTroughAPI(string deliveryCode)
        {
            
            var responseData = new ResponseApiSendToTroughModel
            {
                Code = 200,
                Mesage = "OK"
            };
            try
            {
                var client = new RestClient(LinkAPI_Trough + $"/api/Delivery?DeliveryCode={deliveryCode}");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                responseData = JsonConvert.DeserializeObject<ResponseApiSendToTroughModel>(content);
            }
            catch (Exception ex)
            {
                log.Error($@"SendToTrough  {deliveryCode}, {ex.Message}");
            }
            log.Info($@"push delivery code : {deliveryCode}, status: {responseData.Mesage}");
            return responseData;
        }
        #region khong su dung nua
        private string GetToken()
        {
            if (!String.IsNullOrEmpty(strToken) && expireTimeToken > DateTime.Now.AddMinutes(60 * 5))
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
        private bool CheckIsReceivingByDeliveryCode(string deliveryCode)
        {
            try
            {
                strToken = GetToken();
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor" + ex.Message);
            }
            return false;
        }
        #endregion
    }
}
