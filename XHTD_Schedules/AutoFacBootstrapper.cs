using Autofac;
using Autofac.Extras.Quartz;
using HMXHTD.Services;
using HMXHTD.Services.Services;
using System.Collections.Specialized;
using System.Reflection;
using XHTD_Schedules.ApiScales;
using XHTD_Schedules.ApiTroughs;
using XHTD_Schedules.AuthenticateOperating;
using XHTD_Schedules.BarrierLib;
using XHTD_Schedules.LEDControl;
using XHTD_Schedules.ScaleBusiness;
using XHTD_Schedules.Schedules;

namespace XHTD_Schedules
{
    public static class AutoFacBootstrapper
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new Database()).As<IDatabase>().InstancePerRequest();
            builder.Register(x => new DatabaseFactory()).As<IDatabaseFactory>().InstancePerRequest();

            builder.RegisterType<ServiceFactory>().As<IServiceFactory>();

            builder.RegisterType<GatewayModule>().AsSelf();
            builder.RegisterType<LEDGetwayFrontControl>().AsSelf();
            builder.RegisterType<LEDGetwayBehindControl>().AsSelf();
            builder.RegisterType<ScaleApiLib>().AsSelf();
            builder.RegisterType<DesicionScaleBusiness>().AsSelf();
            builder.RegisterType<WeightScaleBusiness>().AsSelf();
            builder.RegisterType<UnladenWeightBusiness>().AsSelf();
            builder.RegisterType<TroughApiLib>().AsSelf();
            builder.RegisterType<BarrierScaleBusiness>().AsSelf();
            //builder.RegisterType<StoreOrderOperatingService>().As<IStoreOrderOperatingService>();
            //builder.RegisterType<LogStoreOrderOperatingService>().As<ILogStoreOrderOperatingService>();
            //builder.RegisterType<DistributorService>().As<IDistributorService>();


            RegisterScheduler(builder);
            return builder.Build();
        }
        private static void RegisterScheduler(ContainerBuilder builder)
        {
            var schedulerConfig = new NameValueCollection {
          {"quartz.threadPool.threadCount", "20"},
          {"quartz.scheduler.threadName", "MyScheduler"}
         };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(ScaleModuleJob).Assembly));
            builder.RegisterType<JobScheduler>().AsSelf();
        }

    }
}
