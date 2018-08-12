using  Xn.Platform.Infrastructure.Web.Events.Handlers;

namespace Xn.Platform.Infrastructure.Web.Events.Factory
{
    /// <summary>
    /// 实现 <see cref="IEventHandlerFactory"/> 接口， 使用单例模式返回事件处理器
    /// </summary>
    /// <remarks>
    /// 每次返回同一个事件处理器实例.
    /// </remarks>
    internal class SingleInstanceHandlerFactory<TEventData> : IEventHandlerFactory<TEventData>
         where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理器单例.
        /// </summary>
        public IEventHandler<TEventData> HandlerInstance { get; private set; }

        public SingleInstanceHandlerFactory(IEventHandler<TEventData> handler)
        {
            HandlerInstance = handler;
        }

        /// <summary>
        /// 返回事件处理器单例
        /// </summary>
        public IEventHandler<TEventData> GetHandler()
        {
            return HandlerInstance;
        }

        public void ReleaseHandler(IEventHandler<TEventData> handler)
        {

        }
    }
}
