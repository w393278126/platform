using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Xn.Platform.Core.Logs
{
    public class LocalLogger
    {
        private static object _Sync = new object();
        private static Queue<LogData> _Queue = new Queue<LogData>();
        static bool bIsInited = false;
        static bool _Running = false;

        public static void Log(string message, LogLevel level = LogLevel.Debug)
        {
            #if !DEBUG

                if (level < LogLevel.Info)
                    return;

            #endif

            lock (_Queue)
            {
                _Queue.Enqueue(new LogData(message, level, DateTime.Now));
                if (!bIsInited)
                {
                    bIsInited = true;
                    _Running = true;
                    Task.Factory.StartNew(WriteLog);
                }
            }
        }

        /// <summary>
        /// 写日志函数
        /// </summary>
        private static void WriteLog()
        {
            while (_Running)
            {
                try
                {
                    LogData data = null;
                    lock (_Sync)
                    {
                        data = Dequeue();
                    }
                    if (data != null)
                    {
                        StreamWriter writer = new StreamWriter(GetWriteLogPath(data.Level), true);
                        try
                        {
                            writer.WriteLine("\r\nDatetime:{0}", data.CreateDateTime.ToString("yyyyMMdd HH:mm:ss.fff"));
                            writer.WriteLine("Message:{0}", data.Message);
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine(ex.ToString());
                        }
                        finally
                        {
                            writer.Close();
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch
                {
                }
            }

        }

        //获得日志文件路径
        private static string GetWriteLogPath(LogLevel level)
        {
            string path = string.Empty;

            //path = string.Format(@"{3}:\logs\Redis\{0}\{1}\{2}\", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, "D");
            string identity = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "_").Replace(":", "_");

            path = string.Format(@"d:\log\locallog\{3}\{0}\{1}\{2}\", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, identity);
            //path = "d:\\log\\locallogger\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + string.Format("{0}-{1}.log", DateTime.Now.ToString("HH"), level.ToString());

            return path;
        }

        //出队列
            #region 出队列
        /// <summary>
        /// 出队列
        /// </summary>
        /// <returns>返回日志信息</returns>
        private static LogData Dequeue()
        {
            if (_Queue.Count > 0)
            {
                lock (_Sync)
                {
                    if (_Queue.Count > 0)
                    {
                        return _Queue.Dequeue();
                    }
                }
            }
            return null;
        }
            #endregion

    }
    class LogData
    {
        public string Message
        {
            get;
            set;
        }

        public LogLevel Level
        {
            get;
            set;
        }

        public DateTime CreateDateTime
        {
            get;
            set;
        }
        public LogData(string message, LogLevel level, DateTime createDateTime)
        {
            Message = message;
            Level = level;
            CreateDateTime = createDateTime;

        }
    }
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }
}
