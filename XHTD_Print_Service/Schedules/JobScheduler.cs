using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Print_Service.Schedules
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


            IJobDetail CPUJob = JobBuilder.Create<PrintJob>().Build();
            _scheduler.ScheduleJob(CPUJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(20)
                     .RepeatForever()
                    )
                .Build());

        }
    }
}
