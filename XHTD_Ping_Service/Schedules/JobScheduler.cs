using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Ping_Service.Schedules
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

         
            IJobDetail PingStatusJob = JobBuilder.Create<PingStatusJob>().Build();
            _scheduler.ScheduleJob(PingStatusJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60*30)
                     .RepeatForever()
                    )
                .Build());

            IJobDetail PingApiJob = JobBuilder.Create<PingApiJob>().Build();
            _scheduler.ScheduleJob(PingApiJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60*10)
                     .RepeatForever()
                    )
                .Build());

        }
    }
}
