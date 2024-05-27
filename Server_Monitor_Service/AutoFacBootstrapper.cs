using Autofac;
using Autofac.Extras.Quartz;
using System.Collections.Specialized;
using Server_Monitor_Service.Schedules;

namespace Server_Monitor_Service
{
    public static class AutoFacBootstrapper
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();

            //builder.Register(x => new Database()).As<IDatabase>().InstancePerRequest();
            //builder.Register(x => new DatabaseFactory()).As<IDatabaseFactory>().InstancePerRequest();

            //builder.RegisterType<ServiceFactory>().As<IServiceFactory>();

            RegisterScheduler(builder);
            return builder.Build();
        }
        private static void RegisterScheduler(ContainerBuilder builder)
        {
            var schedulerConfig = new NameValueCollection {
          {"quartz.threadPool.threadCount", "20"},
          {"quartz.scheduler.threadName", "JobScheduler"}
         };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(CPUJob).Assembly));
            builder.RegisterType<JobScheduler>().AsSelf();
        }

    }
}
