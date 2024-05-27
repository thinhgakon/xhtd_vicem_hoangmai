using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Monitor_Service.Schedules
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


            //IJobDetail AutoSyncScaledJob = JobBuilder.Create<MonitorStatusJob>().Build();
            //_scheduler.ScheduleJob(AutoSyncScaledJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInSeconds(60 * 5)
            //         .RepeatForever()
            //        )
            //    .Build());

            IJobDetail CPUJob = JobBuilder.Create<CPUJob>().Build();
            _scheduler.ScheduleJob(CPUJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(15)
                     .RepeatForever()
                    )
                .Build());

        }
    }
}
