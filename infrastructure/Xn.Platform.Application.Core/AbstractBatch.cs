using Common.Logging;
using System;

namespace Xn.Platform.Application.Core
{
    public abstract class AbstractBatch : IBatch
    {
        /// <summary>
        /// 本地日志
        /// </summary>
        protected readonly ILog Log = LogManager.GetLogger("AbstractBatch");
        protected BatchConfig BatchConfig;

        public void SetBatchConfig(BatchConfig batchConfig)
        {
            BatchConfig = batchConfig;
            Log.Info($"任务{BatchConfig.BatchId}({BatchConfig.BatchName ?? GetType().Name})上次执行时间{BatchConfig.LastSchedulerTime}本次调度时间{BatchConfig.SchedulerTime}本次执行日期{BatchConfig.SchedulerDate}");
        }

        public virtual bool IsRun()
        {
            return true;
        }

        public abstract void Prepare();

        public abstract void Execute();

        public void Clear()
        {
            ClearCore();
            Log.Info($"任务{BatchConfig.BatchId}({BatchConfig.BatchName ?? GetType().Name})执行完成{BatchConfig.SchedulerDate}");
        }

        protected abstract void ClearCore();

        public abstract void UpdateTimeStamp(TimeSpan timeSpan);

        /// <summary>
        /// 返回Sleep时间戳
        /// </summary>
        /// <returns></returns>
        public virtual TimeSpan GetSleepTimeSpan()
        {
            return TimeSpan.FromSeconds(10);
        }
    }
}