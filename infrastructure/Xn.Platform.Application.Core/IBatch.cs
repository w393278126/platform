using System;
using System.Threading;

namespace Xn.Platform.Application.Core
{
    public interface IBatch
    {
        void SetBatchConfig(BatchConfig batchConfig);
        bool IsRun();
        void Prepare();
        void Execute();
        void Clear();
        void UpdateTimeStamp(TimeSpan timeSpan);
        TimeSpan GetSleepTimeSpan();
    }

    public static class BatchExtension
    {
        /// <summary>
        /// 调度框架使用
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="config"></param>
        public static void RunBatch(IBatch batch, BatchConfig config)
        {
            batch.SetBatchConfig(config);
            if (!batch.IsRun()) return;
            batch.Prepare();
            batch.Execute();
            batch.Clear();
        }

        /// <summary>
        /// 单独运行使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        public static void RunBatch<T>(BatchConfig config)
            where T : IBatch, new()
        {
            IBatch batch = new T();
            batch.SetBatchConfig(config);
            if (!batch.IsRun()) return;
            batch.Prepare();
            batch.Execute();
            batch.Clear();
            var timeSpan = batch.GetSleepTimeSpan();
            batch.UpdateTimeStamp(timeSpan);
        }
    }
}