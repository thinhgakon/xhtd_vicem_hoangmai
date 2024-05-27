using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_Schedules.SignalRNotification;

namespace XHTD_Schedules.Schedules
{
    public class NotificationSignalRJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NotificationSignalRJob()
        {
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                OperatingTest();
            });
        }
        public void OperatingTest()
        {
            new MyHub().Send("AuthenticateOperating", "test message " + DateTime.Now.ToString());

        }

    }
}
