using System;
using  Xn.Platform.Infrastructure.Web.Events.Handlers;

namespace Xn.Platform.Infrastructure.Web.Events.Factory
{
    /// <summary>
    /// 实现 <see cref="IEventHandlerFactory"/> 接口. 
    /// </summary>
    /// <remarks>
    /// 每次返回一个新的事件处理器实例.
    /// </remarks>
    internal class TransientEventHandlerFactory<TEventData, THandler> : IEventHandlerFactory<TEventData>
        where TEventData : IEventData
        where THandler : IEventHandler<TEventData>, new()
    {
        /// <summary>
        /// 返回一个新的事件处理器.
        /// </summary>
        public IEventHandler<TEventData> GetHandler()
        {
            return new THandler();
        }

        /// <summary>
        /// 如事件处理器实现了 <see cref="IDisposable"/> , 调用该方法释放资源
        /// </summary>
        /// <param name="handler">需要释放资源的事件处理器</param>
        public void ReleaseHandler(IEventHandler<TEventData> handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}
