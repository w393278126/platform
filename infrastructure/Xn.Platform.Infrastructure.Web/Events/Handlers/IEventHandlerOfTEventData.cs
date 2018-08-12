namespace Xn.Platform.Infrastructure.Web.Events.Handlers
{
    /// <summary>
    /// 事件处理类接口，处理 <see cref="TEventData"/> 对应的事件.
    /// </summary>
    /// <typeparam name="TEventData">需要处理的事件类型</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// 通过该接口处理 <see cref="TEventData"/> 对应的事件.
        /// </summary>
        /// <param name="eventData">事件相关数据</param>
        void HandleEvent(TEventData eventData);
    }
}
