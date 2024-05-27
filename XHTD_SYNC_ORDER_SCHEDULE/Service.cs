using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XHTD_SYNC_ORDER_SCHEDULE.Schedules;

namespace XHTD_SYNC_ORDER_SCHEDULE
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Autofac.IContainer container = AutoFacBootstrapper.Init();
            var scheduler = container.Resolve<JobScheduler>();
            scheduler.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
