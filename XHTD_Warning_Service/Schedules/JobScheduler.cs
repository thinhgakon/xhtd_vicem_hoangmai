using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Warning_Service.Schedules
{
    public class JobScheduler
    {
        private readonly IScheduler _scheduler;

        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start();
            var dateTime = DateTime.Now;

         
            IJobDetail PingStatusJob = JobBuilder.Create<WarningScale>().Build();
            _scheduler.ScheduleJob(PingStatusJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInHours(24*365)
                     .RepeatForever()
                    )
                .Build());

        }
    }
}
