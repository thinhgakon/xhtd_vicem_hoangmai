using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OK RUN");
            //Test();
            // TestToken();
            TestTokenScale();
            Console.ReadKey();
        }
        public static void TestTokenScale()
        {
          

            var requestData = new TokenScaleRequestModel
            {
                client_id = "weight-api",
                client_secret = System.Configuration.ConfigurationSettings.AppSettings.Get("client_secret_scale_prod").ToString(),
                grant_type = "client_credentials"
            };
            try
            {
                var client = new RestClient(System.Configuration.ConfigurationSettings.AppSettings.Get("LinkAPI_Scale_Prod").ToString() + "/connect/token");
                var request = new RestRequest(Method.POST);

                //  request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
                //  request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/x-www-form-urlencoded", $"client_id={requestData.client_id}&client_secret={requestData.client_secret}&grant_type={requestData.grant_type}", ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<TokenScaleResponseModel>(content);
                var strToken = responseData.access_token;
                Console.WriteLine($"====token=={strToken}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("getToken scale" + ex.StackTrace);
            }
        }
        public static void TestToken()
        {
            try
            {
                var client = new RestClient(System.Configuration.ConfigurationSettings.AppSettings["LinkAPI_DatHang"].ToString() + "/connect/token");
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "multipart/form-data");
                request.Parameters.Clear();
                request.AddParameter("grant_type", "password");
                request.AddParameter("client_secret", System.Configuration.ConfigurationSettings.AppSettings["client_secret"].ToString());
                request.AddParameter("username", System.Configuration.ConfigurationSettings.AppSettings["username"].ToString());
                request.AddParameter("password", System.Configuration.ConfigurationSettings.AppSettings["password"].ToString());
                request.AddParameter("client_id", System.Configuration.ConfigurationSettings.AppSettings["client_id"].ToString());
                var response = client.Execute(request);
                string data = response.Content;
                if (string.IsNullOrEmpty(data))
                {
                    Console.WriteLine($"==data null=={data}");
                }
                Console.WriteLine("=====data=======" + data);
                var jo = JObject.Parse(response.Content);
               var token = jo["access_token"].ToString();
                Console.WriteLine($"==token==={token}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex====={ex.Message}");
            }
        }
        public static void Test()
        {
            string url = "https://api.telegram.org/bot1847865636:AAEi3f9oUTqQGqqjM785rPZn51P3mmu_pxg/sendMessage?chat_id=-506163941&text=sdffffdsd";
            ServicePointManager.Expect100Continue  = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15000;
            request.Method = "GET";
                Console.WriteLine("OK RUN 1");
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        Console.WriteLine("OK RUN 2" + response.StatusCode);
                        }
                        else
                        {
                        Console.WriteLine("OK RUN 2" + response.StatusCode);
                    }
                    }
                }
                catch (Exception e)
                {
                Console.WriteLine("OK RUN 3" + e.StackTrace);
                Console.WriteLine("OK RUN 3" + e.Message);
            }
        }
    }
    public class TokenScaleRequestModel
    {
        public string grant_type { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class TokenScaleResponseModel
    {
        public string access_token { get; set; }
        public double expires_in { get; set; }
    }
    public class RequestUpdateWeightModel
    {
        public string deliveryCode { get; set; }
        public int weight { get; set; }
    }
    public class ScaleResponseModel
    {
        public string code { get; set; }
        public string message { get; set; }

    }
    public class LogicErrors
    {
        public List<string> LogicError { get; set; }
    }
}
