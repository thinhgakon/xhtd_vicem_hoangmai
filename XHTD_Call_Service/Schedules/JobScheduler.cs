using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Call_Service.Schedules
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
            DateTime startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 17, 59, 59, 999);


            IJobDetail IgnoreCallVehicleAndReIndexJob = JobBuilder.Create<IgnoreCallVehicleAndReIndexJob>().Build();
            _scheduler.ScheduleJob(IgnoreCallVehicleAndReIndexJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());

            IJobDetail RealTimeTroughJob = JobBuilder.Create<RealTimeTroughJob>().Build();
            _scheduler.ScheduleJob(RealTimeTroughJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(15)
                     .RepeatForever()
                    )
                .Build());

            IJobDetail PushToDbCallClinkerJob = JobBuilder.Create<PushToDbCallClinkerJob>().Build();
            _scheduler.ScheduleJob(PushToDbCallClinkerJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60)
                     .RepeatForever()
                    )
                .Build());
            IJobDetail PushToDbCallOrderExportJob = JobBuilder.Create<PushToDbCallOrderExportJob>().Build();
            _scheduler.ScheduleJob(PushToDbCallOrderExportJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60)
                     .RepeatForever()
                    )
                .Build());
        }
    }
}
