using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XHTD_Led_Service.Schedules;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_Led_Service
{
    
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static String Led12Point = "";
        public static String LedMainTroughPoint = "";
        static void Main()
        {
            //AutoFacBootstrapper.Init().Resolve<LED12ControlJob>().ShowLed12Process();

           // AutoFacBootstrapper.Init().Resolve<LedHPTestXibao>().ShowLed12Process();
            AutoFacBootstrapper.Init().Resolve<LedHPTestXiroi>().ShowLed12Process();
            Console.ReadKey();

            //var s = new Service();
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    s
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
