using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;
using System.Threading;
using System.IO;
using RestSharp;
using HMXHTD.Data.Models;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Media;
using SoxSharp;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace HMXHTD_CallVoiceService.Schedules
{
    public class TestJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string PathAudio = $@"D:/VICEM_HOANGMAI/xuathangtudong/Audio";
        protected readonly IServiceFactory _serviceFactory;
        List<string> lstCardNos = new List<string>() { "9048713", "1048706", "1048692", "1048711", "1048710", "1048709", "1048708", "1048704", "1048707", "1048697", "1048696", "1048695", "1048694", "1048691", "1048693", "1048690", "1048688", "1048681", "1048680", "1048676", "1048675", "1048665", "1048672", "1048674", "1048656", "1048673", "1048633", "1048601", "1048616", "1048600", "1048657", "1048648", "1048644", "1048646", "1048645", "1048599", "1048597", "1048596", "1048595", "1048593", "1048592", "1048582", "1048662", "1048581", "1048632", "1048712" };
        List<(string, string)> mylist = new List<(string, string)>();
        public TestJob(IServiceFactory serviceFactory)
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
                // TestProcess();;
                // sentNotificationV1("3954c47d-9a8a-4dc6-859b-84d84e09bc79", "Lái xe 1", "Mời xe vào lấy hàng", "thông báo", "383700");
                // TestVBee();
                // TestAmplifier();
                // TestSyncTonageDefault();
            });
        }
        public void TestSyncTonageDefault()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var vehicles = db.tblVehicles.Where(x => x.TonnageDefault == null && x.Vehicle.Length > 6).Take(100).ToList();
                foreach (var vehicle in vehicles)
                {
                    var sqlCount = "SELECT TOP 1 DeliveryCode FROM dbo.tblStoreOrder WHERE IDVehicle = @IDVehicle";
                    var deliveryCode = db.Database.SqlQuery<string>(sqlCount, new SqlParameter("@IDVehicle", vehicle.IDVehicle)).SingleOrDefault();
                    if (String.IsNullOrEmpty(deliveryCode))
                    {
                        var sqlUpdate = "UPDATE dbo.tblVehicle SET TonnageDefault = 0 WHERE IDVehicle = @IDVehicle";
                        db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@IDVehicle", vehicle.IDVehicle));
                    }
                    else
                    {
                        var loadWeightNull = GetLoadWeightNullByDeliveryCode(deliveryCode);
                        if(loadWeightNull < 1)
                        {
                            var sqlUpdate = "UPDATE dbo.tblVehicle SET TonnageDefault = 0 WHERE IDVehicle = @IDVehicle";
                            db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@IDVehicle", vehicle.IDVehicle));
                        }
                        else
                        {
                            var sqlUpdate = "UPDATE dbo.tblVehicle SET TonnageDefault = @TonnageDefault WHERE IDVehicle = @IDVehicle";
                            db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@IDVehicle", vehicle.IDVehicle), new SqlParameter("@TonnageDefault", loadWeightNull));
                        }
                    }
                }

            }
        }
        public int GetLoadWeightNullByDeliveryCode(string deliveryCode)
        {
            var weight = 0;
            try
            {
                var strToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzMDIiLCJuYW1laWQiOiIzOTExIiwianRpIjoiNzExYzcyOGItOTZjNC00MjQ4LWFiYjMtYmY0YTlhMWUwOGM2IiwibmJmIjoxNjE4MDI4NDg1LCJleHAiOjE2MTgxMTQ4ODUsImlzcyI6ImllcnAudm4iLCJhdWQiOiJpRVJQIn0.n5teHg6wiOGHBHETQjlPj8MxnWz3J82gZWwrbCWWQn4";
                var client = new RestClient( $@"http://api.ximanghoangmai.vn:8099/api/order-detail/{deliveryCode}");
                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", "Bearer " + strToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<List<DetailOrderResponse>>(content).SingleOrDefault();
                if(responseData?.LOADWEIGHTNULL != null) {
                    weight = (int)(responseData.LOADWEIGHTNULL * 1000) + 70;
                }
            }
            catch (Exception ex)
            {

            }
            return weight;
        }
        public void TestAmplifier()
        {
            string finalFileName = "D:/VICEM_HOANGMAI/xuathangtudong/Audio_Test/37C23593_f2.wav";
            string finalDirectory = "D:/VICEM_HOANGMAI/xuathangtudong/Audio_Test/Amplifier";
            string tmpFileName = "D:/VICEM_HOANGMAI/xuathangtudong/Audio_Test/37C23593.wav";
            string soxEXE = @"sox.exe";
            string soxArgs = "-v v14.2.0";
            //try
            //{
            //    using (Sox sox = new Sox("sox.exe"))
            //    {
            //        sox.CustomArgs = string.Format("{0} {1} {2}",
            //                             soxArgs, tmpFileName, finalFileName);
            //        AudioInfo wavInfo = sox.GetInfo(tmpFileName);
            //        Console.WriteLine(wavInfo);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex);
            //}
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = "sox.exe";
                startInfo.Arguments = $@" -v 6.0 {tmpFileName} -r 16000 {finalFileName}";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                using (Process soxProc = Process.Start(startInfo))
                {
                    soxProc.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = soxEXE;
                process.StartInfo.Arguments = string.Format("{0} {1} {2}",
                                         soxArgs, tmpFileName, finalFileName);
                process.Start();
                process.WaitForExit();
                int exitCode = process.ExitCode;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@finalFileName);
                simpleSound.PlaySync();

                FileInfo readFile = new FileInfo(finalFileName);
                string finalDestination = finalDirectory + "/" + readFile.Name;
                readFile.MoveTo(finalDestination);
            }
            catch (Exception e)
            {
                string errmsg = e.Message;
            }
        }
        public void TestProcess()
        {
            try
            {
                mylist.Add(("9048713", "37C28467"));
                mylist.Add(("1048706", "37N9324"));
                mylist.Add(("1048692", "36C31823"));
                mylist.Add(("1048711", "37C09557"));
                mylist.Add(("1048710", "37H00200"));
                mylist.Add(("1048709", "36C31935"));
                mylist.Add(("1048708", "36C31893"));
                mylist.Add(("1048704", "36C09558"));
                mylist.Add(("1048707", "37S5886"));
                mylist.Add(("1048697", "37C03301"));
                mylist.Add(("1048696", "38C05645"));
                mylist.Add(("1048695", "24C01486"));
                mylist.Add(("1048694", "38H00343"));
                mylist.Add(("1048691", "37C10559"));
                mylist.Add(("1048693", "36C28005"));
                mylist.Add(("1048690", "38N4903"));
                mylist.Add(("1048688", "37H00584"));
                mylist.Add(("1048681", "38N4912"));
                mylist.Add(("1048680", "37C19903"));
                mylist.Add(("1048676", "37S6074"));
                mylist.Add(("1048675", "37C07939"));
                mylist.Add(("1048665", "38C11272"));
                mylist.Add(("1048672", "37H00997"));
                mylist.Add(("1048674", "37C09829"));
                mylist.Add(("1048656", "37C19594"));
                mylist.Add(("1048673", "37C22575"));
                mylist.Add(("1048633", "37C14956"));
                mylist.Add(("1048601", "37C24385"));
                mylist.Add(("1048616", "37C14155"));
                mylist.Add(("1048600", "37C35472"));
                mylist.Add(("1048657", "37C30344"));
                mylist.Add(("1048648", "38C14366"));
                mylist.Add(("1048644", "51D44419"));
                mylist.Add(("1048646", "37C12217"));
                mylist.Add(("1048645", "37C29404"));
                mylist.Add(("1048599", "30M2856"));
                mylist.Add(("1048597", "37H00742"));
                mylist.Add(("1048596", "36C15789"));
                mylist.Add(("1048595", "37V0047"));
                mylist.Add(("1048593", "37S3871"));
                mylist.Add(("1048592", "37C05801"));
                mylist.Add(("1048582", "37C15481"));
                mylist.Add(("1048662", "37C01798"));
                mylist.Add(("1048581", "37C08827"));
                mylist.Add(("1048632", "38H00357"));
                mylist.Add(("1048712", "37C30722"));
                var cardInDbs = new List<tblRFID>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    foreach (var card in mylist)
                    {
                        var cardInDB = db.tblRFIDs.FirstOrDefault(x => x.Code == card.Item1);
                        if (cardInDB == null) {
                            var newRFID = new tblRFID
                            {
                                Code = card.Item1,
                                Vehicle = card.Item2
                            };
                            db.tblRFIDs.Add(newRFID);
                            db.SaveChanges();
                        };
                        
                    }
                }
                var res = "OK";
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }

        #region method sentNotificationV1
        public void sentNotificationV1(string deviceId, string UserName, string content, string type, string orderid)
        {
            try
            {
                var data = new NotificationContent
                {
                    app_id = "7666b94e-e453-44fa-aec4-992e65af8e23",
                    include_player_ids = new List<string>() { deviceId },
                    data = new DataNotification
                    {
                        orderid = orderid,
                        type_ntf = type
                    },
                    headings = new HeadingsNotification
                    {
                        en = "VICEM HOANG MAI"
                    },
                    contents = new HeadingsNotification
                    {
                        en = content
                    }
                };
                var jsonSerialize = JsonConvert.SerializeObject(data);
                //Android
                var clientAndroid = new RestClient("https://onesignal.com/api/v1/notifications");
                var requestAndroid = new RestRequest(Method.POST);
                requestAndroid.AddHeader("Content-type", "application/json");

                requestAndroid.AddHeader("Authorization", "Basic NDI2YWEyNzAtYWEyYy00YTk0LThhNDEtMzc0ZmJjNjJkZGVh");
                requestAndroid.AddParameter("application/json", jsonSerialize, ParameterType.RequestBody);

                IRestResponse responseAndroid = clientAndroid.Execute(requestAndroid);
                string dataAndroid = responseAndroid.Content;

                //Ios
                data.app_id = "60eb9e95-d58f-405c-b5ec-bdeb0d323bb7";
                var clientIOS = new RestClient("https://onesignal.com/api/v1/notifications");
                var requestIOS = new RestRequest(Method.POST);
                requestIOS.AddHeader("Content-type", "application/json");

                requestIOS.AddHeader("Authorization", "Basic YmE1NTQ2OTUtMjhjNS00NGQ4LWJjOGYtMWE0YWExZDRjOTAz");
                requestIOS.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);

                IRestResponse responseIOS = clientIOS.Execute(requestIOS);
                string dataIOS = responseIOS.Content;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        #endregion

    }
    public class NotificationContent
    {
        public string app_id { get; set; }
        public List<string> include_player_ids { get; set; }
        public DataNotification data { get; set; }
        public HeadingsNotification headings { get; set; }
        public HeadingsNotification contents { get; set; }
        public NotificationContent()
        {
            data = new DataNotification();
            headings = new HeadingsNotification();
            contents = new HeadingsNotification();
        }
    }
    public class DataNotification
    {
        public string type_ntf { get; set; }
        public string orderid { get; set; }

    }
    public class HeadingsNotification
    {
        public string en { get; set; }
    }
    public class Content
    {
        public string en { get; set; }
    }
    public class DetailOrderResponse
    {
        public double LOADWEIGHTNULL { get; set; }
    }
}
