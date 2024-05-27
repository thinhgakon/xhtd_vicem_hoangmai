using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Trough_Service.Schedules
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

         
            IJobDetail PushDeliveryCodeToTroughJob = JobBuilder.Create<PushDeliveryCodeToTroughJob>().Build();
            _scheduler.ScheduleJob(PushDeliveryCodeToTroughJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(15)
                     .RepeatForever()
                    )
                .Build());

            IJobDetail PushDeliveryCodeOnDemandToTrough = JobBuilder.Create<PushDeliveryCodeOnDemandToTrough>().Build();
            _scheduler.ScheduleJob(PushDeliveryCodeOnDemandToTrough, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(15)
                     .RepeatForever()
                    )
                .Build());

            //IJobDetail RealTimeTroughJob = JobBuilder.Create<RealTimeTroughJob>().Build();
            //_scheduler.ScheduleJob(RealTimeTroughJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInSeconds(15)
            //         .RepeatForever()
            //        )
            //    .Build());

            IJobDetail SyncTroughJob = JobBuilder.Create<SyncTroughJob>().Build();
            _scheduler.ScheduleJob(SyncTroughJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());

        }
    }
}
