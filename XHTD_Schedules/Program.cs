using Autofac;
using HMXHTD.Data.DataEntity;
using HMXHTD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using XHTD_Schedules.ApiScales;
using XHTD_Schedules.ApiTroughs;
using XHTD_Schedules.AuthenticateOperating;
using XHTD_Schedules.BarrierLib;
using XHTD_Schedules.LEDControl;
using XHTD_Schedules.Models;
using XHTD_Schedules.Schedules;
using XHTD_Schedules.SignalRNotification;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_Schedules
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<ScaleInfoModel> listScale1 = new List<ScaleInfoModel>();
        public static List<ScaleInfoModel> listScale2 = new List<ScaleInfoModel>();
        public static bool IsScallingCN = false;
        public static bool IsScallingCC = false;
        public static ScaleInfoRealTimeModel ScaleInfoCN = new ScaleInfoRealTimeModel();
        public static ScaleInfoRealTimeModel ScaleInfoCC = new ScaleInfoRealTimeModel();

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
            //AutoFacBootstrapper.Init().Resolve<AutoRemoveCallJob>().AutoRemoveCallProcess();
            //



            //AutoFacBootstrapper.Init().Resolve<AutoSyncScaledJob>().AutoSyncScaleProcess();



            //AutoFacBootstrapper.Init().Resolve<FixStepJob>().AutoSyncScaleProcess();


            //AutoFacBootstrapper.Init().Resolve<SyncOrderJob>().SyncOrderProcess();
            //AutoFacBootstrapper.Init().Resolve<ReIndexOrderJob>().ProcessReIndex();

            //AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().ProcessOffBarrierScale(true);
            // AutoFacBootstrapper.Init().Resolve<BarrierScaleBusiness>().OnOffBarrierScale(false, false, false);
            //AutoFacBootstrapper.Init().Resolve<ScaleBusiness.DesicionScaleBusiness>().MakeDecisionScaleIn("459879-22", 26920, true);

            //Console.ReadKey();

            var s = new Service();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                s
            };
            ServiceBase.Run(ServicesToRun);
        }
        static void ConfigSignalR()
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.Service<SignalRServiceNotification>(serviceInstance =>
                {
                    serviceConfig.UseNLog();

                    serviceInstance.ConstructUsing(
                        () => new SignalRServiceNotification());

                    serviceInstance.WhenStarted(
                        execute => execute.OnStart(null));

                    serviceInstance.WhenStopped(
                        execute => execute.OnStop());
                });

                //TimeSpan delay = new TimeSpan(0, 0, 0, 60);
                //serviceConfig.EnableServiceRecovery(recoveryOption =>
                //{
                //    recoveryOption.RestartService(delay);
                //    recoveryOption.RestartService(delay);
                //    recoveryOption.RestartComputer(delay,
                //       System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                //       " computer reboot"); // All subsequent failures
                //});

                //serviceConfig.SetServiceName
                //  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                //serviceConfig.SetDisplayName
                //  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                //serviceConfig.SetDescription
                //  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                //   " is a simple web chat application.");

                serviceConfig.StartAutomatically();
            });
        }
    }
}
