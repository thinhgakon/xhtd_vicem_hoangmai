using HMXHTD.Data.DataEntity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace HMXHTD.Services.Services
{
    public interface INotificationService : IBaseService<tblRFID>
    {
        Task SendNotificationToTelegram(string message);
        Task SendNotificationToTelegramMonitor(string message);

    }
    public class NotificationService : BaseService<tblRFID>, INotificationService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NotificationService()
        {
        }
        public async Task SendNotificationToTelegram(string message)
        {
            try
            {
                var client = new RestClient($"http://192.168.0.10:8018/api/Notification/notification-rfid?message={message}");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                //string apiToken = "1847865636:AAEi3f9oUTqQGqqjM785rPZn51P3mmu_pxg";
                //string chatId = "-506163941";
                //var bot = new TelegramBotClient(apiToken);
                //var s = await bot.SendTextMessageAsync(chatId, message);

                //string apiToken = "1766003770:AAHwQLUXmSTHDUOpsavkwxi156kXlw1SP_s";
                //string chatId = "-582738115";
                //var bot = new TelegramBotClient(apiToken);
                //var s = await bot.SendTextMessageAsync(chatId, message);

                //string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                //string apiToken = "1766003770:AAHwQLUXmSTHDUOpsavkwxi156kXlw1SP_s";
                //string chatId = "-582738115";
                //string text = message;
                //urlString = String.Format(urlString, apiToken, chatId, text);
                //WebRequest request = WebRequest.Create(urlString);
                //await request.GetResponseAsync();
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
        public async Task SendNotificationToTelegramMonitor(string message)
        {
            try
            {
                var client = new RestClient($"http://192.168.0.10:8018/api/Notification/notification-monitor?message={message}");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
    }
}
