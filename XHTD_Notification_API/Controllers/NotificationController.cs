using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace XHTD_Notification_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        [Route("notification-monitor")]
        public async Task<Object> SendNotificationMonitor(string message)
        {
            try
            {
                string apiToken = "1847865636:AAEi3f9oUTqQGqqjM785rPZn51P3mmu_pxg";
                string chatId = "-506163941";
                var bot = new TelegramBotClient(apiToken);
                var s = await bot.SendTextMessageAsync(chatId, message);

            }
            catch (Exception ex)
            {
                return new
                {
                    StackTrace = ex.StackTrace,
                    Message = ex.Message
                };
            }
            return Ok();
        }
        [HttpPost]
        [Route("notification-rfid")]
        public async Task<Object> SendNotificationRFID(string message)
        {
            try
            {
                return Ok();
                string apiToken = "1766003770:AAHwQLUXmSTHDUOpsavkwxi156kXlw1SP_s";
                string chatId = "-582738115";
                var bot = new TelegramBotClient(apiToken);
                var s = await bot.SendTextMessageAsync(chatId, message);

            }
            catch (Exception ex)
            {
                return new
                {
                    StackTrace = ex.StackTrace,
                    Message = ex.Message
                };
            }
            return Ok();
        }
    }
}
