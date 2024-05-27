using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Schedules.Schedules
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


            //IJobDetail IgnoreCallVehicleAndReIndexJob = JobBuilder.Create<IgnoreCallVehicleAndReIndexJob>().Build();
            //_scheduler.ScheduleJob(IgnoreCallVehicleAndReIndexJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

            //#region Hủy trạng thái chờ trước máng khi đã cân ra
            //IJobDetail AutoSyncScaledJob = JobBuilder.Create<AutoSyncScaledJob>().Build();
            //_scheduler.ScheduleJob(AutoSyncScaledJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInSeconds(60)
            //         .RepeatForever()
            //        )
            //    .Build());
            //#endregion

            //#region Hủy gọi loa khi đã cân
            //IJobDetail AutoRemoveCallJob = JobBuilder.Create<AutoRemoveCallJob>().Build();
            //_scheduler.ScheduleJob(AutoRemoveCallJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInSeconds(20)
            //         .RepeatForever()
            //        )
            //    .Build());
            //#endregion

            //open when deploy prod    
            IJobDetail AutoAccomplishedJob = JobBuilder.Create<AutoAccomplishedJob>().Build();
            _scheduler.ScheduleJob(AutoAccomplishedJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInHours(1)
                     .RepeatForever()
                    )
                .Build());

            //open when deploy prod    
            IJobDetail ScaleModuleJob = JobBuilder.Create<ScaleModuleJob>().Build();
            _scheduler.ScheduleJob(ScaleModuleJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInHours(24 * 365 * 10)
                     .RepeatForever()
                    )
                .Build());

            //#region test tách module

            //IJobDetail ScaleModuleCCJob = JobBuilder.Create<ScaleModuleCCJob>().Build();
            //_scheduler.ScheduleJob(ScaleModuleCCJob, TriggerBuilder.Create()
            //    .WithPriority(2)
            //     .StartAt(dateTime.AddSeconds(10))
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInHours(864000)
            //         .RepeatForever()
            //        )
            //    .Build());
            //IJobDetail ScaleModuleCNJob = JobBuilder.Create<ScaleModuleCNJob>().Build();
            //_scheduler.ScheduleJob(ScaleModuleCNJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartAt(dateTime.AddSeconds(5))
            //     .WithSimpleSchedule(x => x
            //     .WithIntervalInHours(864000)
            //         .RepeatForever()
            //        )
            //    .Build());

            //#endregion

            //open when deploy prod     
            IJobDetail SyncScaleJob = JobBuilder.Create<SyncScaleJob>().Build();
            _scheduler.ScheduleJob(SyncScaleJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(60)
                     .RepeatForever()
                    )
                .Build());

            //IJobDetail SyncOrderJob = JobBuilder.Create<SyncOrderJob>().Build();
            //_scheduler.ScheduleJob(SyncOrderJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(300)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail SyncOrderBOOKJob = JobBuilder.Create<SyncOrderBOOKJob>().Build();
            //_scheduler.ScheduleJob(SyncOrderBOOKJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60 * 1)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail SyncOrderFromDbJob = JobBuilder.Create<SyncOrderFromDbJob>().Build();
            //_scheduler.ScheduleJob(SyncOrderFromDbJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60 * 1)
            //        .RepeatForever())
            //    .Build());


            //IJobDetail ConfirmationPointModuleJob = JobBuilder.Create<ConfirmationPointModuleJob>().Build();
            //_scheduler.ScheduleJob(ConfirmationPointModuleJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInHours(1)
            //         .RepeatForever()
            //        )
            //    .Build());

            //IJobDetail GatewayModuleJob = JobBuilder.Create<GatewayModuleJob>().Build();
            //_scheduler.ScheduleJob(GatewayModuleJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInHours(1)
            //         .RepeatForever()
            //        )
            //    .Build());

            //IJobDetail SyncTroughJob = JobBuilder.Create<SyncTroughJob>().Build();
            //_scheduler.ScheduleJob(SyncTroughJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail RealTimeTroughJob = JobBuilder.Create<RealTimeTroughJob>().Build();
            //_scheduler.ScheduleJob(RealTimeTroughJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());


            //#region LED
            //IJobDetail LED12ControlJob = JobBuilder.Create<LED12ControlJob>().Build();
            //_scheduler.ScheduleJob(LED12ControlJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(5)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail ControlLedInTroughJob = JobBuilder.Create<ControlLedInTroughJob>().Build();
            //_scheduler.ScheduleJob(ControlLedInTroughJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

            //IJobDetail LEDMainTroughControlJob = JobBuilder.Create<LEDMainTroughControlJob>().Build();
            //_scheduler.ScheduleJob(LEDMainTroughControlJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(15)
            //        .RepeatForever())
            //    .Build());

            //#endregion

        }
    }
}
