using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Led_Service.Schedules
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


            #region LED
            IJobDetail LED12ControlJob = JobBuilder.Create<LED12ControlJob>().Build();
            _scheduler.ScheduleJob(LED12ControlJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build());

            IJobDetail ControlLedInTroughJob = JobBuilder.Create<ControlLedInTroughJob>().Build();
            _scheduler.ScheduleJob(ControlLedInTroughJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());

            IJobDetail LEDMainTroughControlJob = JobBuilder.Create<LEDMainTroughControlJob>().Build();
            _scheduler.ScheduleJob(LEDMainTroughControlJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());

            #endregion

        }
    }
}
