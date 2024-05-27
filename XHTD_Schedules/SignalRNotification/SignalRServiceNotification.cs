using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Schedules.SignalRNotification
{
    public partial class SignalRServiceNotification : IDisposable
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SignalRServiceNotification()
        {
        }

        public void OnStart(string[] args)
        {
            log.InfoFormat("SignalRServiceChat: In OnStart");

            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
             string url = "http://192.168.0.10:8091";// khi đẩy lên server server 0.10
           // string url = "http://192.168.158.55:8091";// khi đẩy lên server server 55

            //   string url = "http://127.0.0.1:8091"; 
            WebApp.Start(url);
        }

        public void OnStop()
        {
            log.InfoFormat("SignalRServiceChat: In OnStop");
        }

        public void Dispose()
        {
        }
    }
}
