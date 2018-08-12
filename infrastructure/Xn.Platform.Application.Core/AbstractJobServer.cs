using System;
using System.Threading;
using Common.Logging;

namespace Xn.Platform.Application.Core
{
    /// <summary>
    /// 只创建一个对象
    /// Prepare方法只执行一次
    /// 执行对象中的Execute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AbstractJobServer<T> : IServer where T : IBatch, new()
    {
        protected readonly ILog Log = LogManager.GetLogger("AbstractJobServer");
        private readonly TimeSpan _timeSpan;
        private bool _isRunning;
        private readonly BatchConfig _batchConfig;
        private IBatch _batch;
        public AbstractJobServer()
        {
            _batch = new T();
            _timeSpan = _batch.GetSleepTimeSpan();
            _batchConfig = new BatchConfig { BatchName = typeof(T).Name };

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += (sender, args) =>
            {
                var ex = (AggregateException)args.ExceptionObject;
                Log.Error($"Runtime terminating: {args.IsTerminating}");
                EmailErr(ex);
            };
        }

        private void EmailErr(AggregateException ex)
        {
            Log.Error($"{_batchConfig.BatchName}出现错误{ex.Flatten().ToString()}");
        }

        public void OnStart()
        {
            var workThread = new Thread(DoTask);
            _isRunning = true;
            workThread.Start();
        }

        private void DoTask()
        {
            try
            {
                _batchConfig.SetSchedulerTime(DateTime.Now);
                _batch.SetBatchConfig(_batchConfig);
                _batch.Prepare();
                while (_isRunning)
                {
                    if (!_batch.IsRun()) return;

                    Log.Info($"{_batchConfig.BatchName}当前执行{_batchConfig.SchedulerTime}");

                    _batch.Execute();
                    _batch.Clear();
                    Log.Info($"{_batchConfig.BatchName}执行完成{_batchConfig.SchedulerTime}");
                    _batch.UpdateTimeStamp(_timeSpan);
                    Thread.Sleep(_timeSpan);
                }
            }
            catch (Exception ex)
            {
                EmailErr(new AggregateException("执行错误", ex));
            }
        }
        public void OnStop()
        {
            _isRunning = false;
            Log.Info($"{_batchConfig.BatchName}服务关闭");
        }
    }
}
