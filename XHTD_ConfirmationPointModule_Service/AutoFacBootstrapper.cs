using Autofac;
using Autofac.Extras.Quartz;
using HMXHTD.Services;
using HMXHTD.Services.Services;
using System.Collections.Specialized;
using XHTD_ConfirmationPointModule_Service.Schedules;

namespace XHTD_ConfirmationPointModule_Service
{
    public static class AutoFacBootstrapper
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new Database()).As<IDatabase>().InstancePerRequest();
            builder.Register(x => new DatabaseFactory()).As<IDatabaseFactory>().InstancePerRequest();

            builder.RegisterType<ServiceFactory>().As<IServiceFactory>();



            RegisterScheduler(builder);
            return builder.Build();
        }
        private static void RegisterScheduler(ContainerBuilder builder)
        {
            var schedulerConfig = new NameValueCollection {
          {"quartz.threadPool.threadCount", "1"},
          {"quartz.scheduler.threadName", "MyScheduler"}
         };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(ConfirmationPointModule_Job).Assembly));
            builder.RegisterType<JobScheduler>().AsSelf();
        }

    }
}
