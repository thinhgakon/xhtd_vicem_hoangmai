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
using XHTD_Getway_Service.Schedules;

namespace XHTD_Getway_Service
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
            //ConfigSignalR();

        }
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
