using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using RestSharp;
using XHTD_Schedules.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using XHTD_Schedules.SignalRNotification;
using HMXHTD.Services.Services;
using Topshelf;

namespace XHTD_Schedules.Schedules
{
    public class SignalRJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SignalRJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                SignalRProcess();
            });
        }
        public void SignalRProcess()
        {
        //    log.Info("==============start process SyncOrderProcess ====================");

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
