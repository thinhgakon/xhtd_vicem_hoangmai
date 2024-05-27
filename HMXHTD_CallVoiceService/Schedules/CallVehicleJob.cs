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
using System.Data.SqlClient;
using RoundRobin;

namespace HMXHTD_CallVoiceService.Schedules
{
    public class CallVehicleJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string PathAudio = $@"D:/VICEM_HOANGMAI/xuathangtudong/AudioNew";
        protected readonly IServiceFactory _serviceFactory;
        private static bool IsCallClinker = true;
        private static bool IsCallXMB = false;
        
        public CallVehicleJob(IServiceFactory serviceFactory)
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
                log.Info("==============start service ====================");
               CallVehicleProcess();
            });
        }
        public void TestCallWav()
        {
            try
            {
                //Thread.Sleep(5000);
                //string PathAudio2 = $@"D:/VICEM_HOANGMAI/xuathangtudong/AudioNew";
                string PathAudio2 = $@"./AudioNormal";
                string VoiceFileStart = $@"{PathAudio2}/audio_generer/VicemBegin.wav";
                string VoiceFileInvite = $@"{PathAudio2}/audio_generer/moixe.wav";
                string VoiceFileInOut = $@"{PathAudio2}/audio_generer/vaonhanhang.wav";
                string VoiceFileVehicle = $@"{PathAudio2}/37C21836.wav";
                string VoiceFileEnd = $@"{PathAudio2}/audio_generer/VicemEnd.wav";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = VoiceFileStart;
                wplayer.controls.play();
                Thread.Sleep(5000);

                // mời xe
                wplayer.URL = VoiceFileInvite;
                wplayer.controls.play();
                Thread.Sleep(2000);
                // đọc biển số
                wplayer.URL = VoiceFileVehicle;
                wplayer.controls.play();
                Thread.Sleep(3500);
                // vào or ra
                wplayer.URL = VoiceFileInOut;
                wplayer.controls.play();

                Thread.Sleep(5000);

                wplayer.URL = VoiceFileEnd;
                wplayer.controls.play();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public tblCallVehicleStatu GetVehicleToCall()
        {
            var callVehicleItem = new tblCallVehicleStatu();
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var typeCurrent = Program.roundRobinList.Next();
                        callVehicleItem = db.tblCallVehicleStatus.Where(x => x.IsDone == false && x.CountTry < 3 && x.TypeProduct.Equals(typeCurrent)).OrderBy(x => x.Id).FirstOrDefault();
                        if (callVehicleItem != null && callVehicleItem.Id > 0) return callVehicleItem;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return callVehicleItem;
        }
        public bool CheckAvaiableCall()
        {
            bool avaiableCall = true;
            try
            {
                if (_serviceFactory.ConfigOperating.GetValueByCode("IsCheckMaxVehicleBetweenGetwayAndScale") == 0) return true;
                var maxVehicleCurrentBetweenGetwayAndScale = _serviceFactory.ConfigOperating.GetValueByCode("MaxVehicleBetweenGetwayAndScale");
                log.Info($@"====maxVehicleCurrentBetweenGetwayAndScale====={maxVehicleCurrentBetweenGetwayAndScale}");
                var currentVehicleCurrentBetweenGetwayAndScale = _serviceFactory.StoreOrderOperating.CountVehicleBetweenGetwayAndScale();
                log.Info($@"====currentVehicleCurrentBetweenGetwayAndScale====={currentVehicleCurrentBetweenGetwayAndScale}");
                if (currentVehicleCurrentBetweenGetwayAndScale > maxVehicleCurrentBetweenGetwayAndScale)
                {
                    log.Info($@"====stop call by limit vehicle====={currentVehicleCurrentBetweenGetwayAndScale} > {maxVehicleCurrentBetweenGetwayAndScale}");
                    avaiableCall = false;
                }
                else
                {
                    avaiableCall = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return avaiableCall;
        }
        public void CallVehicleProcess()
        {
            log.Info("==============start process CallVehicleProcess ====================");
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            // check thêm điều kiện max số xe nằm ở khu vực cổng 3 và trạm cân
            if(!CheckAvaiableCall())
            {
                log.Info("=========cancel call vehicle by max vehicle between getway and scale");
                return;
            }

            try
            {
                //var typeCurrent = Program.roundRobinList.Next();
                //log.Info($@"{DateTime.Now} - {typeCheck}");
                //return;
                var isWillCall = false;
                var vehiceCode = "";
                using (var db = new HMXuathangtudong_Entities())
                {
                    var callVehicleItem = new tblCallVehicleStatu();
                    var typeCurrent = Program.roundRobinList.Next();

                    callVehicleItem = GetVehicleToCall();
                    if (callVehicleItem == null || callVehicleItem.Id < 1) return;
                    // check xem trong bảng tblStoreOrderOperating xem đơn hàng có đang yêu cầu gọi vào không
                    var storeOrderOperating = db.tblStoreOrderOperatings.FirstOrDefault(x=>x.Id == callVehicleItem.StoreOrderOperatingId);
                    log.Error($@"======1==call vehicle {storeOrderOperating.Vehicle} ==============");
                    if(storeOrderOperating.CountReindex != null && (int)storeOrderOperating.CountReindex >= 3)
                    {
                        log.Error($@"======2==call vehicle {storeOrderOperating.Vehicle} quá 3 lần xoay vòng gọi -- hủy lốt==============");
                        if (storeOrderOperating.Step == 1 || storeOrderOperating.Step == 4)
                        {
                            var sql = $@"UPDATE dbo.tblStoreOrderOperating SET IndexOrder = 0, Confirm1 = 0, TimeConfirm1 = NULL, Step = 0, IndexOrder2 = 0, DeliveryCodeParent = NULL, LogProcessOrder = CONCAT(LogProcessOrder, N'#Quá 3 lần xoay vòng lốt mà xe không vào, hủy lốt lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) WHERE  Step IN (1,4) AND ISNULL(DriverUserName,'') <> '' AND (DeliveryCode = @DeliveryCode OR DeliveryCodeParent = @DeliveryCode)";
                            db.Database.ExecuteSqlCommand(sql, new SqlParameter("@DeliveryCode", storeOrderOperating.DeliveryCode));
                            var sqlDelete = $@"UPDATE dbo.tblCallVehicleStatus SET IsDone = 1 WHERE StoreOrderOperatingId = @StoreOrderOperatingId";
                            db.Database.ExecuteSqlCommand(sql, new SqlParameter("@StoreOrderOperatingId", storeOrderOperating.Id));
                            return;
                        }
                    }
                    var vehicleWaitingCall = db.tblCallVehicleStatus.FirstOrDefault(x=>x.Id == callVehicleItem.Id);
                    if (vehicleWaitingCall == null) return;
                    if (storeOrderOperating == null || storeOrderOperating.Step != 4)
                    {
                        vehicleWaitingCall.ModifiledOn = DateTime.Now;
                        vehicleWaitingCall.IsDone = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        storeOrderOperating.LogProcessOrder = storeOrderOperating.LogProcessOrder + $@" #Gọi xe vào lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                        isWillCall = true;
                        vehiceCode = storeOrderOperating.Vehicle;
                        vehicleWaitingCall.ModifiledOn = DateTime.Now;
                        vehicleWaitingCall.CountTry = vehicleWaitingCall.CountTry + 1;
                        vehicleWaitingCall.LogCall = $@"{vehicleWaitingCall.LogCall} # Gọi xe {vehiceCode} vào lúc {DateTime.Now}";
                        db.SaveChanges();
                    }
                }
                if (isWillCall)
                {
                    // tiến hành gọi xe
                   // CallVoiceVehicle(vehiceCode);
                    CallBySystem(vehiceCode);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }
        public void CallVoiceVehicle(string vehicleCode)
        {
            try
            {
                //Thread.Sleep(5000);
                var isExistFile = CreateVoiceForVehicle(vehicleCode);
                if (!isExistFile) return;

                string VoiceFileStart = $@"{PathAudio}/audio_generer/VicemBegin.mp3";
                string VoiceFileInvite = $@"{PathAudio}/audio_generer/moixe_.mp3";
                string VoiceFileInOut = $@"{PathAudio}/audio_generer/vaonhanhang.mp3";
                string VoiceFileVehicle  = $@"{PathAudio}/{vehicleCode}.mp3";
                string VoiceFileEnd = $@"{PathAudio}/audio_generer/VicemEnd.mp3";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = VoiceFileStart;
                wplayer.settings.volume = 60;
                wplayer.controls.play();
                Thread.Sleep(7000);

                // mời xe
                wplayer.URL = VoiceFileInvite;
                wplayer.settings.volume = 100;
                wplayer.controls.play();
                Thread.Sleep(2000);
                // đọc biển số
                wplayer.URL = VoiceFileVehicle;
                wplayer.settings.volume = 100;
                wplayer.controls.play();
                Thread.Sleep(4000);
                // vào or ra
                wplayer.URL = VoiceFileInOut;
                wplayer.settings.volume = 60;
                wplayer.controls.play();

                Thread.Sleep(7000);

                wplayer.URL = VoiceFileEnd;
                wplayer.controls.play();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public bool CreateVoiceForVehicle(string vehicleCode)
        {
            
            if (File.Exists($@"{PathAudio}/{vehicleCode}.mp3"))
            {
                return true;
            }
            else
            {
                var res = "no no no";
            }
               
            try
            {
                log.Info($@"create file mp3 {vehicleCode} start {DateTime.Now}");
                var voiceText = $@"Mời xe {vehicleCode} cân vào";

                var client = new RestClient("https://tts.mobifone.ai/");

                var request = new RestRequest("api/tts");

                RequestVoiceTTS requestTTS = new RequestVoiceTTS();
                requestTTS.app_id = "0a7ffab8a3e323713780f6f9";
                requestTTS.key = "VPv7P0TxnFmRgRYY";
                //requestTTS.voice = "hn_male_xuantin_vdts_48k-hsmm";
                //requestTTS.voice = "hn_female_xuanthu_news_48k-hsmm";
                //requestTTS.voice = "hn_female_thutrang_phrase_48k-hsmm";
             //   requestTTS.voice = "sg_male_xuankien_vdts_48k-hsmm";
                requestTTS.voice = "hn_male_xuantin_vdts_48k-hsmm";
                //requestTTS.voice = "sg_female_xuanhong_vdts_48k-hsmm";
                requestTTS.user_id = "48380";
                requestTTS.rate = "0.5";
                requestTTS.time = "1533523698753";
                requestTTS.input_text = voiceText;

                // or just whitelisted properties
                request.AddJsonBody(requestTTS);

                // easily add HTTP Headers
                request.AddHeader("Content-Type", "application/json");

                // execute the request
                var response = client.Post(request);
                if (response.ContentType.Equals("audio/x-wav"))
                {
                    var byteArray = response.RawBytes;
                    Stream stream = new MemoryStream(byteArray);

                    using (Stream file = File.Create($@"{PathAudio}/{vehicleCode}.mp3"))
                    {
                        CopyStream(stream, file);
                    }
                }
                else
                {
                    return false;
                }
                log.Info($@"created file mp3 {vehicleCode} end at {DateTime.Now}");
                return true;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            return false;
        }
        public  void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
        // test

        public bool CreateVoiceFreeText(string inputText)
        {
            try
            {
                //if (inputText.Length < 7) return false;
                var voiceText = $@"{inputText}";

                var client = new RestClient("https://tts.mobifone.ai/");

                var request = new RestRequest("api/tts");

                RequestVoiceTTS requestTTS = new RequestVoiceTTS();
                requestTTS.app_id = "2f85f86b7632bf3ac13dc46c";
                requestTTS.key = "pOprjmfRLyjMJC7k";
                requestTTS.voice = "hn_female_maiphuong_news_48k-d";
                requestTTS.user_id = "48380";
                requestTTS.rate = "0.5";
                requestTTS.time = "1533523698753";
                requestTTS.input_text = voiceText;

                // or just whitelisted properties
                request.AddJsonBody(requestTTS);

                // easily add HTTP Headers
                request.AddHeader("Content-Type", "application/json");

                // execute the request
                var response = client.Post(request);
                if (response.ContentType.Equals("audio/x-wav"))
                {
                    var byteArray = response.RawBytes;
                    Stream stream = new MemoryStream(byteArray);

                    using (Stream file = File.Create($@"{PathAudio}/{inputText}.wav"))
                    {
                        CopyStream(stream, file);
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return false;
        }
        public void CallBySystem(string vehicle)
        {
            try
            {
                var PathAudioLib = $@"D:/ThuVienGoiLoa/AudioNormal";
               // var PathAudioLib = $@"./AudioNormal";
                string VoiceFileStart = $@"{PathAudioLib}/audio_generer/VicemBegin.wav";
                string VoiceFileInvite = $@"{PathAudioLib}/audio_generer/moixe.wav";
                string VoiceFileInOut = $@"{PathAudioLib}/audio_generer/vaonhanhang.wav";
                string VoiceFileEnd = $@"{PathAudioLib}/audio_generer/VicemEnd.wav";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                //wplayer.URL = VoiceFileStart;
                //wplayer.settings.volume = 100;
                //wplayer.controls.play();
                //Thread.Sleep(5000);

                wplayer.URL = VoiceFileInvite;
                wplayer.settings.volume = 100;
                wplayer.controls.play();
                Thread.Sleep(1500);
                var count = 0;
                foreach (char c in vehicle)
                {
                    count++;
                    wplayer.URL = $@"{PathAudioLib}/{c}.wav";
                    wplayer.settings.volume = 100;
                    wplayer.controls.play();
                    if(count < 3)
                    {
                        Thread.Sleep(700);
                    }else if(count == 3)
                    {
                        Thread.Sleep(1200);
                    }
                    else
                    {
                        Thread.Sleep(700);
                    }    
                }
               
                wplayer.URL = VoiceFileInOut;
                wplayer.settings.volume = 100;
                wplayer.controls.play();

                //Thread.Sleep(5000);

                //wplayer.URL = VoiceFileEnd;
                //wplayer.controls.play();
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public void PreCreateVoiceMoiXe()
        {
            try
            {
                _serviceFactory.VehicleVoice.InsertVehicleVoice("11C01925");
                using (var db = new HMXuathangtudong_Entities())
                {
                    var listString = new List<string>()
                    {
                        "17C07746","29H01411","34C17128","37C00703","37C14983","37C15579","37C19000","37C21442","37C24708","37C29425","37C31616","37C39230","37CH00816","37H00863","37H00930","37H01173","38H00304","47C09071","47C10371","49C05417","49C09053","51C53875","51D19276","51R16321","63C04456","64C07693","66C02447","74C03438","74C03822","74C04690","74C05618","77C06293","77C07194","77C07515","77C08051","77C09149","77C09490","77C10042","77C10629","77C11045","77C12493","77C14345","77C14718","77C15124","77C15169","77C16314","77C17000","77C17168","77C17570","77C17825","77C18229","77C18413","77C19803","77H00140","77H00466","78C03917","78C04065","78C04172","78C07473","81C04726","81C07936","81C11595","81C12817","81C14549","81C16838","81H00151","81H00246","85C00876","85C02704","85H00066","86C09115","86C09393","86C12021","86H00102","86H3664"
                    };
                    var lst = new List<tblVehicleVoice>();
                    foreach (var item in listString)
                    {
                        //Console.WriteLine(item);
                        //CreateVoiceFreeText(item.ToUpper());
                        var newCallVoice = new tblVehicleVoice
                        {
                            Vehicle = item,
                            CreatedOn = DateTime.Now,
                            Flag = 0,
                            IsCreatedVoice = false
                        };
                        lst.Add(newCallVoice);
                    }
                    db.tblVehicleVoices.AddRange(lst);
                    db.SaveChanges();
                   // var vehicles  = db.Database.SqlQuery<VehicleAudio>("SELECT * FROM tmp_vehicle_audio WHERE IsCreatedAudio = 0 ORDER BY Id DESC").ToList();
                    //var vehicles = db.Database.SqlQuery<VehicleAudio>("SELECT * FROM tmp_vehicle_audio WHERE IsCreatedAudio = 0 AND Id IN (823,822,564,171)").ToList();
                    //var count = 1;
                    //foreach (var vehicle in vehicles)
                    //{
                    //    var IsSuccessced = CreateVoiceFreeText(vehicle.Vehicle.Trim().Replace(" ","").Replace("_","").Replace(":","").ToUpper());
                    //    if (IsSuccessced)
                    //    {
                    //        db.Database.ExecuteSqlCommand($@"UPDATE dbo.tmp_vehicle_audio SET IsCreatedAudio = 1 WHERE Id = {vehicle.Id}");
                    //    }
                    //    else
                    //    {

                    //    }
                    //    count++;
                    //}
                }
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

    }
}
