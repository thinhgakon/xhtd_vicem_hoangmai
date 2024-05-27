using Autofac;
using HMXHTD_CallVoiceService.Schedules;
using RoundRobin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace HMXHTD_CallVoiceService
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly RoundRobinList<string> roundRobinList = new RoundRobinList<string>(
                    new List<string>{
                        "PCB40", "PCB30","ROI", "CLINKER","XK"
                    }
                );
        static void Main(string[] args)
        {
            log.Error("Starting Scheduler");
            //AutoFacBootstrapper.Init().Resolve<JobScheduler>().Start();
            //for (int i = 0; i < 100; i++)
            //{
            //    AutoFacBootstrapper.Init().Resolve<CallVehicleJob>().CallVehicleProcess();
            //}
            //Console.ReadKey();

            var s = new Service();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                s
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
