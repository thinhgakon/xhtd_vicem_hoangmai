using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_Schedules.Models.TroughApiModels;

namespace XHTD_Schedules.ApiTroughs
{
    public class TroughApiLib
    {
        private static string LinkAPI_Trough = ConfigurationManager.AppSettings.Get("LinkAPI_Trough")?.ToString();//"http://192.168.158.19/WebCounter";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public TroughApiLib(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public void SendOrderToTrough(string cardNo)
        {
            try
            {
                var orders = _serviceFactory.StoreOrderOperating.GetAllOrderReceivingByCardNo(cardNo);
                foreach (var order in orders)
                {
                    //if (order.LocationCode.Contains(".XK") || order.TypeProduct == "CLINKER")
                    //{

                    //}else 
                   // if(order.TypeProduct == "PCB40" || order.TypeProduct == "PCB30")
                    //{
                        //ProcessOrderToTrough(order.DeliveryCode, order.Vehicle);
                        InsertOrUpdateDeliveryCodeTrough(order.DeliveryCode, order.Vehicle);
                   // }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void InsertOrUpdateDeliveryCodeTrough(string deliveryCode, string vehicle)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var query = $@"if not exists(SELECT * from dbo.tblDeliveryCodeTroughOperating where DeliveryCode = @DeliveryCode)            
                                    BEGIN            
                                     INSERT INTO dbo.tblDeliveryCodeTroughOperating
                                            ( DeliveryCode ,
                                              Vehicle ,
                                              IsSentTrough ,
                                              CreatedOn ,
                                              ModifiedOn ,
                                              TimeSendTrough ,
                                              LogHistory ,
                                              Flag
                                            )
                                    VALUES  ( @DeliveryCode , -- DeliveryCode - varchar(50)
                                              @Vehicle , -- Vehicle - varchar(50)
                                              0 , -- IsSentTrough - bit
                                              GETDATE() , -- CreatedOn - datetime
                                              GETDATE() , -- ModifiedOn - datetime
                                              NULL , -- TimeSendTrough - datetime
                                              N'' , -- LogHistory - nvarchar(500)
                                              0  -- Flag - int
                                            )
                                    End ";
                    var InsertResponse = db.Database.ExecuteSqlCommand(query, new SqlParameter("@DeliveryCode", deliveryCode), new SqlParameter("@Vehicle", vehicle));
                }
            }
            catch (Exception ex)
            {
                log.Error($@"InsertOrUpdateDeliveryCodeTrough {ex.Message}");
            }
        }
        public void ProcessOrderToTrough(string deliveryCode, string vehicle)
        {
            try
            {
                var res = SendToTroughAPI(deliveryCode);
                using (var db = new HMXuathangtudong_Entities())
                {
                    var troughOrder = db.tblTroughOrderOperatings.FirstOrDefault(x=>x.DeliveryCode == deliveryCode);
                    if(troughOrder == null)
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
                        if (res.code != 200)
                        {
                            newTroughOrder.Status = 4;
                            newTroughOrder.LogResponse = res.message;
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        if (res.code != 200)
                        {
                            troughOrder.Status = 4;
                            troughOrder.LogResponse = res.message;
                        }
                        else
                        {
                            troughOrder.Status = 1;
                            troughOrder.LogResponse = res.message;
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
        public ResponseApiSendToTroughModel SendToTroughAPI(string deliveryCode)
        {
            var responseData = new ResponseApiSendToTroughModel 
            {
                code = 200,
                message = "OK"
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
                if(content == "true")
                {

                }
                else
                {
                    responseData.code = 422;
                    responseData.message = "Không thành công";
                }
                // responseData = JsonConvert.DeserializeObject<TroughLineTranModel>(content);
            }
            catch (Exception ex)
            {
                log.Error($@"SendToTrough  {deliveryCode}, {ex.Message}");
            }
            return responseData;
        }
    }
}
