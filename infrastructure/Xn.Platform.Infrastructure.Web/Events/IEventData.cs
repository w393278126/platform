using System;

namespace Xn.Platform.Infrastructure.Web.Events
{
    /// <summary>
    /// 事件数据类的接口定义
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件发生时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 事件源
        /// </summary>
        object EventSource { get; set; }
    }
}
