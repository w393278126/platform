using  Xn.Platform.Infrastructure.Web.Events.Handlers;

namespace Xn.Platform.Infrastructure.Web.Events.Factory
{
    /// <summary>
    /// 事件处理器工厂类接口.
    /// </summary>
    public interface IEventHandlerFactory
    {
    }

    /// <summary>
    /// 事件处理器工厂类接口.
    /// </summary>
    public interface IEventHandlerFactory<TEventData> : IEventHandlerFactory 
        where TEventData : IEventData
    {
        /// <summary>
        /// 获取事件处理器.
        /// </summary>
        IEventHandler<TEventData> GetHandler();

        /// <summary>
        /// 注销事件处理器.
        /// </summary>
        void ReleaseHandler(IEventHandler<TEventData> handler);
    }
}
