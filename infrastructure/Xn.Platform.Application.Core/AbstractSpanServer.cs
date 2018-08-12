using System;
using System.Threading;
using Common.Logging;

namespace Xn.Platform.Application.Core
{
    public class AbstractSpanServer<T> : IServer where T : IBatch, new()
    {
        protected readonly ILog Log = LogManager.GetLogger("AbstractSpanServer");
        private bool _isRunning;
        private readonly BatchConfig _batchConfig;
        public AbstractSpanServer()
        {
            _batchConfig = new BatchConfig { BatchName = typeof(T).Name };

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += (sender, args) =>
            {
                var ex = (AggregateException)args.ExceptionObject;
                Log.Error(string.Format("Runtime terminating: {0}", args.IsTerminating));
                EmailErr(ex);
            };
        }

        private void EmailErr(AggregateException ex)
        {
            Log.Error(string.Format("{0}出现错误{1}", _batchConfig.BatchName, ex.Flatten().ToString()));
        }

        public void OnStart()
        {
            var workThread = new Thread(DoTask);
            _isRunning = true;
            workThread.Start();
        }

        private void DoTask()
        {
            while (_isRunning)
            {
                _batchConfig.SetSchedulerTime(DateTime.Now);
                Log.Info(string.Format("{0}当前执行{1}", _batchConfig.BatchName, _batchConfig.SchedulerTime));
                try
                {
                    var batch = new T();
                    BatchExtension.RunBatch(batch, _batchConfig);
                    var timeSpan = batch.GetSleepTimeSpan();
                    batch.UpdateTimeStamp(timeSpan);
                    Thread.Sleep(timeSpan);
                }
                catch (Exception ex)
                {
                    EmailErr(new AggregateException("执行错误", ex));
                }
                Log.Info(string.Format("{0}执行完成{1}", _batchConfig.BatchName, _batchConfig.SchedulerTime));
            }
        }

        public void OnStop()
        {
            _isRunning = false;
            Log.Info(string.Format("{0}服务关闭", _batchConfig.BatchName));
        }
    }
}