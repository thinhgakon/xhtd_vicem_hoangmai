using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XHTD_Extension_Service.Schedules;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_Extension_Service
{
    
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main()
        {
            AutoFacBootstrapper.Init().Resolve<Test>().TestConnect();
            // AutoFacBootstrapper.Init().Resolve<FixBugJob>().ReIndexByTypeProduct("PCB40");

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
