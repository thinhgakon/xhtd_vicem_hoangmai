using HMXHTD.Services.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Schedules.ApiScales
{
    public class ScaleApiLib
    {
        //http://upwebsale.ximanghoangmai.vn:5555/api-weight/swagger/index.html
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public ScaleApiLib(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        private string GetToken()
        {
            if (!String.IsNullOrEmpty(strToken) && expireTimeToken > DateTime.Now.AddMinutes(30))
            {
                return strToken;
            }


            var requestData = new TokenScaleRequestModel
            {
                client_id = "weight-api",
                client_secret = ConfigurationManager.AppSettings.Get("client_secret_scale_prod").ToString(),
                grant_type = "client_credentials"
            };
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_Scale_Prod").ToString() + "/connect/token");
                var request = new RestRequest(Method.POST);

              //  request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
              //  request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/x-www-form-urlencoded", $"client_id={requestData.client_id}&client_secret={requestData.client_secret}&grant_type={requestData.grant_type}", ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                log.Error("getToken =============" + content);
                var responseData = JsonConvert.DeserializeObject<TokenScaleResponseModel>(content);
                strToken = responseData.access_token;
                expireTimeToken = DateTime.Now.AddSeconds(responseData.expires_in);
                return responseData.access_token;
            }
            catch (Exception ex)
            {
                log.Error("getToken scale" + ex.StackTrace);
                return "";
            }
        }
        public ScaleResponseModel CheckValidationOrder(string deliveryCode)
        {
            var responseModel = new ScaleResponseModel();
            try
            {
                var accessToken = GetToken();
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_Scale_Prod").ToString() + $@"/api-weight/{deliveryCode}");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Authorization", "Bearer " + accessToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                responseModel = JsonConvert.DeserializeObject<ScaleResponseModel>(content);
            }
            catch (Exception ex)
            {
                log.Error("CheckValidationOrder error: " + ex.StackTrace);
            }
            return responseModel;
        }
        public ScaleResponseModel ScaleIn(string deliveryCode, int weight)
        {
            var responseModel = new ScaleResponseModel();
            try
            {
                if (CheckTimeStay())
                {
                    responseModel.code = "02";
                    responseModel.message = "Thời điểm này không được phép cân để xử lý các tiến trình của hóa đơn điện tử";
                    return responseModel;
                }
                var accessToken = GetToken();
                var requestData = new RequestUpdateWeightModel
                {
                    deliveryCode = deliveryCode,
                    weight = weight
                };
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_Scale_Prod").ToString() + $@"/api-weight/update-weight-in");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestData);
                request.AddHeader("Authorization", "Bearer " + accessToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                log.Warn($@"===========response ScaleIn========{content}");
                if (string.IsNullOrEmpty(content))
                {
                    responseModel.code = "02";
                    responseModel.message = "Lỗi khi gửi cân sang IERP cân vào";
                }
                responseModel = JsonConvert.DeserializeObject<ScaleResponseModel>(content);
            }
            catch (Exception ex)
            {
                log.Error("ScaleIn error: " + ex.StackTrace);
                responseModel.code = "02";
                responseModel.message = "Lỗi khi gửi cân sang IERP";
            }
            return responseModel;
        }
        public ScaleResponseModel ScaleOut(string deliveryCode, int weight)
        {
            var responseModel = new ScaleResponseModel();
            try
            {
                if (CheckTimeStay())
                {
                    responseModel.code = "02";
                    responseModel.message = "Thời điểm này không được phép cân để xử lý các tiến trình của hóa đơn điện tử";
                    return responseModel;
                }
                var accessToken = GetToken();
                var requestData = new RequestUpdateWeightModel
                {
                    deliveryCode = deliveryCode,
                    weight = weight
                };
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_Scale_Prod").ToString() + $@"/api-weight/update-weight-out");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestData);
                log.Warn($@"===========response ScaleOut Object========{requestData.deliveryCode}======{requestData.weight}");
                var jsonLog = JsonConvert.SerializeObject(requestData);
                log.Warn($@"===========response ScaleOut ObjectJson========{jsonLog}===={DateTime.Now}");
                request.AddHeader("Authorization", "Bearer " + accessToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                log.Warn($@"===========response ScaleOut========{content}");
                if (string.IsNullOrEmpty(content))
                {
                    responseModel.code = "02";
                    responseModel.message = "Lỗi khi gửi cân sang IERP cân ra";
                }
                responseModel = JsonConvert.DeserializeObject<ScaleResponseModel>(content);
            }
            catch (Exception ex)
            {
                log.Error("ScaleIn error: " + ex.StackTrace);
                responseModel.code = "02";
                responseModel.message = "Lỗi khi gửi cân sang IERP";
            }
            return responseModel;
        }
        public bool CheckTimeStay()
        {
            DateTime ts = DateTime.Now;
            DateTime ts2 = DateTime.Now.AddDays(1);
            var startStay = new DateTime(ts.Year, ts.Month, ts.Day, 23, 49, 30);
            var endStay = new DateTime(ts2.Year, ts2.Month, ts2.Day, 00, 00, 10);
            if (ts > startStay && ts < endStay) return true;
            return false;
        }
    }
}
