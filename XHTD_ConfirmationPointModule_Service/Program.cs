using Autofac;
using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XHTD_ConfirmationPointModule_Service.Models;
using XHTD_ConfirmationPointModule_Service.Schedules;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_ConfirmationPointModule_Service
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
           // Console.WriteLine("Starting Scheduler");
            log.Error("Starting Scheduler");
            //IContainer container = AutoFacBootstrapper.Init();
            //var scheduler = container.Resolve<JobScheduler>();
            //scheduler.Start();
            // container.Resolve<GatewayModule>().AuthenticateGatewayModule();
            // container.Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: true, contentComeIn: "37C0000");
            // container.Resolve<LEDGetwayBehindControl>().SendLedyBehindAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: false, contentComeIn: "37C0000");
            // ConfigSignalR();


            //  AutoFacBootstrapper.Init().Resolve<RealTimeTroughJob>().ProcessSyncTrough();


            // AutoSyncScaleProcess
            //   AutoFacBootstrapper.Init().Resolve<SyncOrderJob>().SyncOrderProcess();


            //AutoFacBootstrapper.Init().Resolve<SyncOrderJob>().SyncOrderProcess();
            // AutoFacBootstrapper.Init().Resolve<SyncOrderBOOKJob>().SyncOrderProcess();
            //AutoFacBootstrapper.Init().Resolve<ReIndexOrderJob>().ProcessReIndex();
            // AutoFacBootstrapper.Init().Resolve<RealTimeTroughJob>().ProcessSyncTrough();
            // AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().CheckValidationOrder("400139-21");
            //AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().ScaleIn("400139-21", 122);
            //AutoFacBootstrapper.Init().Resolve<ConfirmationPointModule_Job>().TestTele();
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
