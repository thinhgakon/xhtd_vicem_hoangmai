using HMXHTD.Data.Models;
using HMXHTD.Services.Services;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD_CallVoiceService.Schedules
{
    public class VehicleVoiceJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string PathAudio = $@"D:/VICEM_HOANGMAI/xuathangtudong/AudioCallVehicle";
        protected readonly IServiceFactory _serviceFactory;
        public VehicleVoiceJob(IServiceFactory serviceFactory)
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
                ProcessCreateVoice();
            });
        }
        public void ProcessCreateVoice()
        {
            try
            {
                var lst = new List<string>() { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                foreach (var c in lst)
                {
                    CreateVoiceForVehicle(c.ToString());
                }
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        //UpdateCreatedVoiceById
        public bool CreateVoiceForVehicle(string inputText)
        {
            try
            {
                var voiceText = $@"{inputText}";

                var client = new RestClient("https://tts.mobifone.ai/");

                var request = new RestRequest("api/tts");

                RequestVoiceTTS requestTTS = new RequestVoiceTTS();
                requestTTS.app_id = "2f85f86b7632bf3ac13dc46c";
                requestTTS.key = "pOprjmfRLyjMJC7k";
                requestTTS.voice = "hn_female_maiphuong_news_48k-d";
                requestTTS.user_id = "48380";
                requestTTS.rate = "0.7";
                requestTTS.time = "1533523698753";
                requestTTS.input_text = voiceText;

                request.AddJsonBody(requestTTS);

                request.AddHeader("Content-Type", "application/json");

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
        public void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
