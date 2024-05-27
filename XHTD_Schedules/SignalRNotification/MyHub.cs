using Autofac;
using HMXHTD.Data.DataEntity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_Schedules.BarrierLib;
using XHTD_Schedules.Models;
using XHTD_Schedules.ScaleBusiness;

namespace XHTD_Schedules.SignalRNotification
{
    public class MyHub : Hub
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static int MaxItemCheck = 10;
        private static int AmplitudeOfOscillationAllow = 25;

        public void Send(string name, string message)
        {
            try
            {
                Console.WriteLine("Send: " + message);
                var broadcast = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                broadcast.Clients.All.SendMessage(name, message);
            }
            catch (Exception ex)
            {

            }
        }
        public void SendScaleInfo1(DateTime datetime, string message)
        {
             ProcessScaleReceived1(datetime, message);
        }
        public void SendScaleInfo2(DateTime datetime, string message)
        {
            ProcessScaleReceived2(datetime, message);
        }
        public void ProcessScaleReceived1(DateTime dateTime, string message)
        {
            // cân 150T - cân nổi
            try
            {
                Int32.TryParse(ConfigurationManager.AppSettings.Get("AmplitudeOfOscillationAllow").ToString(), out AmplitudeOfOscillationAllow);
                int tmp = 0;
                Int32.TryParse(message, out tmp);
                Send("Scale1_Current", tmp.ToString());
                log.Info($"========weight realtime========{tmp.ToString()}");
                if (tmp < 1000)
                {
                    Program.listScale1.Clear();
                    return;
                }
                
                var preItem = Program.listScale1.LastOrDefault();
                var newScaleInfo = new ScaleInfoModel
                {
                    CreatedOn = dateTime,
                    WeightCurrent = tmp,
                    AmplitudeOfOscillation = preItem == null ? 0 : (tmp - preItem.WeightCurrent)
                };
                Program.listScale1.Add(newScaleInfo);
                if (Program.IsScallingCN)
                {
                    MaxItemCheck = GetMaxItemCheck();
                    if (Program.listScale1.Count > MaxItemCheck)
                    {
                        var top10ItemLastest = Program.listScale1.Skip(Math.Max(0, Program.listScale1.Count() - MaxItemCheck));
                        
                        foreach (var item in top10ItemLastest)
                        {
                            if (item.AmplitudeOfOscillation > AmplitudeOfOscillationAllow || item.AmplitudeOfOscillation < -AmplitudeOfOscillationAllow) return;
                        }
                        log.Info($"========weight====Canbang===={newScaleInfo.WeightCurrent}");
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var scaleInfo = db.tblScaleOperatings.FirstOrDefault(x => x.ScaleCode == "CN");
                            if ((bool)scaleInfo.IsScaling)
                            {
                                var logScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == scaleInfo.DeliveryCode && x.IsSynced == false);
                                var orderDetails = db.tblStoreOrderOperatings.FirstOrDefault(x=>x.DeliveryCode == scaleInfo.DeliveryCode);
                                if (logScale != null)
                                {
                                    Send("Scale1_Balance", newScaleInfo.WeightCurrent.ToString());
                                    if (orderDetails.IsScaleAuto != null && !(bool)orderDetails.IsScaleAuto) return;
                                    if (((bool)scaleInfo.ScaleIn) && logScale.WeightScaleIn == null)
                                    {
                                        logScale.WeightScaleIn = newScaleInfo.WeightCurrent;
                                        db.SaveChanges();
                                        AutoFacBootstrapper.Init().Resolve<UnladenWeightBusiness>().InsertOrUpdate(scaleInfo.DeliveryCode);
                                        AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().ProcessOffBarrierScale(true);
                                        var scaleInfoResult = AutoFacBootstrapper.Init().Resolve<DesicionScaleBusiness>().MakeDecisionScaleIn(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, true);
                                        log.Info($"=============MakeDecisionScaleIn=CN====={scaleInfo.DeliveryCode}===={scaleInfoResult.code}====={scaleInfoResult.message}");
                                        if (scaleInfoResult.code == "01")
                                        { 
                                            AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().OnOffBarrierScale(true, true, true);
                                        }
                                       
                                        AutoFacBootstrapper.Init().Resolve<WeightScaleBusiness>().UpdateWeight(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, true);
                                        Send("Scale_CN_IN_Desision", newScaleInfo.WeightCurrent.ToString());
                                        ReleaseScale("CN");
                                        Program.IsScallingCN = false;
                                    }
                                    if (((bool)scaleInfo.ScaleOut) && logScale.WeightScaleOut == null)
                                    {
                                        logScale.WeightScaleOut = newScaleInfo.WeightCurrent;
                                        db.SaveChanges();
                                        AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().ProcessOffBarrierScale(true);
                                        var scaleInfoResult = AutoFacBootstrapper.Init().Resolve<DesicionScaleBusiness>().MakeDecisionScaleOut(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, true);
                                        log.Info($"=============MakeDecisionScaleOut=CN====={scaleInfo.DeliveryCode}===={scaleInfoResult.code}====={scaleInfoResult.message}");
                                        if (scaleInfoResult.code == "01")
                                        {
                                            AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().OnOffBarrierScale(true, true, true);
                                            
                                        }
                                        AutoFacBootstrapper.Init().Resolve<WeightScaleBusiness>().UpdateWeight(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, false);
                                        Send("Scale_CN_OUT_Desision", newScaleInfo.WeightCurrent.ToString());
                                        ReleaseScale("CN");
                                        Program.IsScallingCN = false;

                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    if(Program.listScale1.Count > 6)
                    {
                        var top10ItemLastest = Program.listScale1.Skip(Math.Max(0, Program.listScale1.Count() - 6));
                        var IsNotIn = top10ItemLastest.Any(x => x.AmplitudeOfOscillation < 20);
                        if (!IsNotIn)
                        {
                            Send("Scale_CN_Warning", "Không xác định được biển số xe");
                        }
                        Program.listScale1.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($@"Error {ex.Message} ===== {ex.StackTrace}");
            }
        }
        public void ProcessScaleReceived2(DateTime dateTime, string message)
        {
            // cân 100T - cân chìm
            try
            {
                Int32.TryParse(ConfigurationManager.AppSettings.Get("AmplitudeOfOscillationAllow").ToString(), out AmplitudeOfOscillationAllow);
                int tmp = 0;
                Int32.TryParse(message, out tmp);
                Send("Scale2_Current", tmp.ToString());
                if (tmp < 1000)
                {
                    Program.listScale2.Clear();
                    return;
                }
                var preItem = Program.listScale2.LastOrDefault();
                var newScaleInfo = new ScaleInfoModel
                {
                    CreatedOn = dateTime,
                    WeightCurrent = tmp,
                    AmplitudeOfOscillation = preItem == null ? 0 : (tmp - preItem.WeightCurrent)
                };
                Program.listScale2.Add(newScaleInfo);
                if (Program.IsScallingCC)
                {
                    MaxItemCheck = GetMaxItemCheck();
                    if (Program.listScale2.Count > MaxItemCheck)
                    {
                        var top10ItemLastest = Program.listScale2.Skip(Math.Max(0, Program.listScale2.Count() - MaxItemCheck));
                        foreach (var item in top10ItemLastest)
                        {
                            if (item.AmplitudeOfOscillation > AmplitudeOfOscillationAllow || item.AmplitudeOfOscillation < -AmplitudeOfOscillationAllow) return;
                        }

                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var scaleInfo = db.tblScaleOperatings.FirstOrDefault(x => x.ScaleCode == "CC");
                            if ((bool)scaleInfo.IsScaling)
                            {
                                var logScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == scaleInfo.DeliveryCode && x.IsSynced == false);
                                var orderDetails = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == scaleInfo.DeliveryCode);
                                if (logScale != null)
                                {
                                    Send("Scale2_Balance", newScaleInfo.WeightCurrent.ToString());
                                    if (orderDetails.IsScaleAuto != null && !(bool)orderDetails.IsScaleAuto) return;
                                    if (((bool)scaleInfo.ScaleIn) && logScale.WeightScaleIn == null)
                                    {
                                        logScale.WeightScaleIn = newScaleInfo.WeightCurrent;
                                        db.SaveChanges();
                                        AutoFacBootstrapper.Init().Resolve<UnladenWeightBusiness>().InsertOrUpdate(scaleInfo.DeliveryCode);
                                        AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().ProcessOffBarrierScale(false);
                                        var scaleInfoResult = AutoFacBootstrapper.Init().Resolve<DesicionScaleBusiness>().MakeDecisionScaleIn(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, false);
                                        log.Info($"=============MakeDecisionScaleIn=CC====={scaleInfo.DeliveryCode}===={scaleInfoResult.code}====={scaleInfoResult.message}");
                                        if (scaleInfoResult.code == "01")
                                        {
                                            AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().OnOffBarrierScale(false, true, true);

                                        } 
                                        AutoFacBootstrapper.Init().Resolve<WeightScaleBusiness>().UpdateWeight(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, true);
                                        Send("Scale_CC_IN_Desision", newScaleInfo.WeightCurrent.ToString());
                                        ReleaseScale("CC");
                                        Program.IsScallingCC = false;
                                    }
                                    if (((bool)scaleInfo.ScaleOut) && logScale.WeightScaleOut == null)
                                    {
                                        logScale.WeightScaleOut = newScaleInfo.WeightCurrent;
                                        db.SaveChanges();
                                        AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().ProcessOffBarrierScale(false);
                                       var scaleInfoResult =  AutoFacBootstrapper.Init().Resolve<DesicionScaleBusiness>().MakeDecisionScaleOut(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, false);
                                        log.Info($"=============MakeDecisionScaleOut=CC====={scaleInfo.DeliveryCode}===={scaleInfoResult.code}====={scaleInfoResult.message}");
                                        if (scaleInfoResult.code == "01")
                                        {
                                            AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().OnOffBarrierScale(false, true, true);
                                        }
                                        AutoFacBootstrapper.Init().Resolve<WeightScaleBusiness>().UpdateWeight(scaleInfo.DeliveryCode, newScaleInfo.WeightCurrent, false);
                                        Send("Scale_CC_OUT_Desision", newScaleInfo.WeightCurrent.ToString());
                                        ReleaseScale("CC");
                                        Program.IsScallingCC = false;

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Program.listScale2.Count > 6)
                    {
                        var top10ItemLastest = Program.listScale2.Skip(Math.Max(0, Program.listScale2.Count() - 6));
                        var IsNotIn = top10ItemLastest.Any(x => x.AmplitudeOfOscillation < 20);
                        if (!IsNotIn)
                        {
                            Send("Scale_CC_Warning", "Không xác định được biển số xe");
                        }
                        Program.listScale2.Clear();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public void ConfirmPointModule(string cardNo, DateTime timeReceived)
        {
            log.Info($@"==========================={cardNo}==========={timeReceived}");
        }

        public Task JoinGroup(string groupId)
        {
            return Groups.Add(Context.ConnectionId, groupId);
        }
        public void SendToGroup(string groupId, string data)
        {
            Clients.Group(groupId).SendDataLed12(groupId, data);
        }
        public int GetMaxItemCheck()
        {
            int maxItem = 10;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlConfigReIndex = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = 'MaxItemCheckBalance'";
                    maxItem = db.Database.SqlQuery<int>(sqlConfigReIndex).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return maxItem;
        }
        public void ReleaseScale(string scaleCode)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlUpdate = "UPDATE dbo.tblScaleOperating SET IsScaling = 0, Vehicle = '', DeliveryCode = '', ScaleIn = 0, ScaleOut = 0, TimeIn = NULL WHERE ScaleCode = @ScaleCode";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@ScaleCode", scaleCode));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public override Task OnConnected()
        {
            Console.WriteLine("login" + DateTime.Now);
            return base.OnConnected();
        }
    }
}
