using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_SYNC_ORDER_SCHEDULE.Schedules
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

            //IJobDetail AutoFinishedOrderOnOppoJob = JobBuilder.Create<AutoFinishedOrderOnOppoJob>().Build();
            //_scheduler.ScheduleJob(AutoFinishedOrderOnOppoJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInMinutes(15)
            //        .RepeatForever())
            //    .Build());

            //Đang thử bỏ đi
            //IJobDetail SyncOrderBookJob = JobBuilder.Create<SyncOrderBookJob>().Build();
            //_scheduler.ScheduleJob(SyncOrderBookJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60)
            //        .RepeatForever())
            //    .Build());

            //Đang thử bỏ đi
            //IJobDetail SyncOrderJob = JobBuilder.Create<SyncOrderJob>().Build();
            //_scheduler.ScheduleJob(SyncOrderJob, TriggerBuilder.Create()
            //    .WithPriority(1)
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(60 * 5)
            //        .RepeatForever())
            //    .Build());

            IJobDetail SyncOrderFromDbJob = JobBuilder.Create<SyncOrderFromDbJob>().Build();
            _scheduler.ScheduleJob(SyncOrderFromDbJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build());

            IJobDetail SyncOrderVoidedJob = JobBuilder.Create<SyncOrderVoidedJob>().Build();
            _scheduler.ScheduleJob(SyncOrderVoidedJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInMinutes(60)
                    .RepeatForever())
                .Build());

            IJobDetail SyncOrderVoidedQuickJob = JobBuilder.Create<SyncOrderVoidedQuickJob>().Build();
            _scheduler.ScheduleJob(SyncOrderVoidedQuickJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInMinutes(10)
                    .RepeatForever())
                .Build());

            #region Đồng bộ package
            IJobDetail SyncPackageNumberOrderJob = JobBuilder.Create<SyncPackageNumberOrderJob>().Build();
            _scheduler.ScheduleJob(SyncPackageNumberOrderJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60 * 5)
                     .RepeatForever()
                    )
                .Build());
            #endregion

            #region Hủy trạng thái chờ trước máng khi đã cân ra
            IJobDetail AutoSyncScaledJob = JobBuilder.Create<AutoSyncScaledJob>().Build();
            _scheduler.ScheduleJob(AutoSyncScaledJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(60)
                     .RepeatForever()
                    )
                .Build());
            #endregion

            #region Hủy gọi loa khi đã cân
            IJobDetail AutoRemoveCallJob = JobBuilder.Create<AutoRemoveCallJob>().Build();
            _scheduler.ScheduleJob(AutoRemoveCallJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(20)
                     .RepeatForever()
                    )
                .Build());
            #endregion

            #region Luồng backup đẩy order vào máng
            IJobDetail SyncOrderSendTroughJob = JobBuilder.Create<SyncOrderSendTroughJob>().Build();
            _scheduler.ScheduleJob(SyncOrderSendTroughJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(15)
                     .RepeatForever()
                    )
                .Build());
            #endregion

            #region Luồng backup kết thúc đơn do không lấy hàng qua hệ thống xhtd
            IJobDetail SyncOrderScaleOutJob = JobBuilder.Create<SyncOrderScaleOutJob>().Build();
            _scheduler.ScheduleJob(SyncOrderScaleOutJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInMinutes(3)
                     .RepeatForever()
                    )
                .Build());
            #endregion

            #region Hủy lốt do không lấy hàng sau 5h
            IJobDetail AutoCancelOrderLongTimeJob = JobBuilder.Create<AutoCancelOrderLongTimeJob>().Build();
            _scheduler.ScheduleJob(AutoCancelOrderLongTimeJob, TriggerBuilder.Create()
                .WithPriority(1)
                 .StartNow()
                 .WithSimpleSchedule(x => x
                 .WithIntervalInMinutes(15)
                     .RepeatForever()
                    )
                .Build());
            #endregion

        }
    }
}
