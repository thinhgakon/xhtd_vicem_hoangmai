using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD_CallVoiceService.Schedules
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


            IJobDetail CallVehicleJob = JobBuilder.Create<CallVehicleJob>().Build();
            _scheduler.ScheduleJob(CallVehicleJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());


            //IJobDetail ProcessVehicleIgnoreJob = JobBuilder.Create<ProcessVehicleIgnoreJob>().Build();
            //_scheduler.ScheduleJob(ProcessVehicleIgnoreJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail VehicleVoiceJob = JobBuilder.Create<VehicleVoiceJob>().Build();
            //_scheduler.ScheduleJob(VehicleVoiceJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInHours(60)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail TestJob = JobBuilder.Create<TestJob>().Build();
            //_scheduler.ScheduleJob(TestJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

        }
    }
}
