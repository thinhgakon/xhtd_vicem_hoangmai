using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface ICommonService
    {
        void SendMail(string FromName, string ToEmail, string ToName, string Subject, string Body, string IP);
        void TestLogService();
    }
    public class CommonService : ICommonService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
     (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void SendMail(string FromName, string ToEmail, string ToName, string Subject, string Body, string IP)
        {
            string templateEmail = String.Format(@"<h1>Thông báo có vấn đề xẩy ra</h1>
                Dear anh Trung,
                <br />
                <p>Hệ thống xuất hàng tự động vừa có vấn đề xẩy ra</p>
                <br />
                Lỗi xẩy ra ở thiết bị với IP {0}", IP);

            SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential("chanmenhthientu24001@gmail.com", "Ch@nmenhthientu&1101");
                smtpClient.Timeout = 20000;

                MailAddress FromAddress = new MailAddress("chanmenhthientu24001@gmail.com", "Xuất hàng tự động");
                MailAddress ToAddress = new MailAddress(ToEmail, ToName);
                MailMessage Mailer = new MailMessage(FromAddress, ToAddress);
                Mailer.IsBodyHtml = true;
                Mailer.BodyEncoding = System.Text.Encoding.UTF8;
                Mailer.Subject = Subject;
                Mailer.Body = !String.IsNullOrEmpty(Body) ? Body : templateEmail;
                smtpClient.Send(Mailer);

        }
        public void TestLogService()
        {
            try
            {
                log.Info("==================ok==============");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
