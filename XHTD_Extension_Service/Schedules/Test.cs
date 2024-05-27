using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Microsoft.AspNet.SignalR.Client;
using Quartz;
using RoundRobin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;

namespace XHTD_Extension_Service.Schedules
{
    public class Test : IJob
    {
        // Scanner config
        private static CZKEM zk;
        public string ip_address;
        public int port = 4370;
        private HubConnection Connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public Test(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(async () =>
            {
                TestConnect();
            });
        }
        public void TestConnect()
        {
            try
            {
                //192.168.22.16
                // INITIALZE FINGERPRINT SCANNER & CONFIG
                zk = new CZKEM();
                ip_address = "192.168.22.16";
                port = 4370;

                // INITIALIZE CONNECT ON CREATE
                if (zk.Connect_Net(ip_address, port))
                {

                    // REGISTER FINGERPRINT SCANNER EVENTS
                    //if (zk.RegEvent(zk.MachineNumber, 1))
                    //{
                    //    zk.OnAttTransactionEx += AttendanceTransactionHandler;
                    //}
                    if (zk.ReadRTLog(2))
                    {

                        while (true)
                        {
                            var s = zk.GetRTLog(2);
                            Console.WriteLine("======get=====" + s);
                            Console.WriteLine("======1=====" + zk.CardNumber[0].ToString());
                            Console.WriteLine("======2=====" + zk.CardNumber[1].ToString());
                            Console.WriteLine("======3=====" + zk.CardNumber[2].ToString());
                            Console.WriteLine("======4=====" + zk.CardNumber[3].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private async void AttendanceTransactionHandler(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            StringBuilder sb = new StringBuilder();

            

            // PRINT NEW EMPLOYEE ATTENDANCE TO LOGS
            Console.WriteLine(sb.ToString());

        }
    }
}
