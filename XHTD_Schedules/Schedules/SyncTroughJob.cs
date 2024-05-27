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
    public class SyncTroughJob : IJob
    {
        private static string LinkAPI_Trough = ConfigurationManager.AppSettings.Get("LinkAPI_Trough")?.ToString();//"http://192.168.158.19/WebCounter";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncTroughJob(IServiceFactory serviceFactory)
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
                SyncTrough();
            });
        }
        public void SyncTrough()
        {
          //  log.Info("==============start process SyncTrough ====================");
            SyncTroughToDb();
            
        }
        public void SyncTroughToDb()
        {
            SyncSingleTroughToDb("M1");
            SyncSingleTroughToDb("M2");
            SyncSingleTroughToDb("M3");
            SyncSingleTroughToDb("M4");
            SyncSingleTroughToDb("M5");
            SyncSingleTroughToDb("M6");
        }
        public void SyncSingleTroughToDb(string trough)
        {
            try
            {
                var troughInfo = GetTroughQuantityRealTime(trough);
                var troughLineTranInfo = GetTroughLineTranRealTime(trough);
                var checkInTrough = String.IsNullOrEmpty(troughInfo.DeliveryCode) ? false : true;
                var checkInviteComeInLineTran = String.IsNullOrEmpty(troughLineTranInfo.TransportName) ? false : true;

                using (var db = new HMXuathangtudong_Entities())
                {
                    var troughDb = db.tblTroughs.FirstOrDefault(x => x.LineCode == trough);
                    if (troughDb == null) return;
                    troughDb.DeliveryCodeCurrent = checkInTrough ? troughInfo.DeliveryCode : "";
                    troughDb.PlanQuantityCurrent = checkInTrough ? troughInfo.PlanQuantity : 0;
                    troughDb.CountQuantityCurrent = checkInTrough ? troughInfo.CountQuantity : 0;
                    troughDb.IsPicking = checkInTrough;

                    troughDb.TransportNameCurrent = checkInviteComeInLineTran ? troughLineTranInfo.TransportName : "";
                    troughDb.CheckInTimeCurrent = checkInviteComeInLineTran ? troughLineTranInfo.CheckInTime : troughDb.CheckInTimeCurrent;
                    troughDb.IsInviting = checkInviteComeInLineTran;
                    db.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                log.Error($@"SyncSingleTroughToDb at {trough}, {ex.Message}");
            }
        }
        public TroughQuantityModel GetTroughQuantityRealTime(string trough)
        {
            var responseData = new TroughQuantityModel();
            try
            {
                var client = new RestClient(LinkAPI_Trough + $"/api/LineCount?LineCode={trough}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                responseData = JsonConvert.DeserializeObject<TroughQuantityModel>(content);
            }
            catch (Exception ex)
            {
                log.Error($@"GetTroughQuantityRealTime at {trough}, {ex.Message}");
            }
            return responseData;
        }
        public TroughLineTranModel GetTroughLineTranRealTime(string trough)
        {
            var responseData = new TroughLineTranModel();
            try
            {
                var client = new RestClient(LinkAPI_Trough + $"/api/LineTran?LineCode={trough}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                responseData = JsonConvert.DeserializeObject<TroughLineTranModel>(content);
            }
            catch (Exception ex)
            {
                log.Error($@"GetTroughLineTranRealTime at {trough}, {ex.Message}");
            }
            return responseData;
        }
    }
}
