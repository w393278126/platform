using Xn.Platform.Abstractions.Redis;
using System;

namespace Xn.Platform.Infrastructure.Web.Events
{
    /// <summary>
    /// 实现 <see cref="IEventData"/> 接口作为事件数据类的基类.
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// 时间发生时间.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 事件源 (可选).
        /// </summary>
        public object EventSource { get; set; }

        protected EventData()
        {
            EventTime = DateTimeHelper.GetRedisDateTime();
        }
    }
}
