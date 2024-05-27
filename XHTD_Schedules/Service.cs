using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using XHTD_Schedules.Schedules;
using XHTD_Schedules.SignalRNotification;

namespace XHTD_Schedules
{
    partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
          
            // TODO: Add code here to start your service.
            Autofac.IContainer container = AutoFacBootstrapper.Init();
            var scheduler = container.Resolve<JobScheduler>();
            scheduler.Start();
            new SignalRServiceNotification().OnStart(null);
            //ConfigSignalR();

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
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
