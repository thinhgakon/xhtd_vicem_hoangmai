using Autofac;
using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using XHTD_Getway_In_Service.LEDControl;
using XHTD_Getway_In_Service.Models;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_Getway_In_Service
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
           // Console.WriteLine("Starting Scheduler");
            log.Error("Starting Scheduler");
            //for (int i = 0; i < 10; i++)
            //{
            //    AutoFacBootstrapper.Init().Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: false, contentComeIn: "");
            //    Thread.Sleep(2000);
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
