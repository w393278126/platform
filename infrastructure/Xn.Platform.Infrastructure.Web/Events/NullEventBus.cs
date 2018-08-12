using System;
using System.Threading.Tasks;
using  Xn.Platform.Infrastructure.Web.Events.Factory;
using  Xn.Platform.Infrastructure.Web.Events.Handlers;
using  Xn.Platform.Infrastructure.Web.Utils;

namespace Xn.Platform.Infrastructure.Web.Events
{
    /// <summary>
    /// 默认消息总线实现（空对象模式）
    /// </summary>
    public sealed class NullEventBus : IEventBus
    {
        /// <summary>
        /// 获取 <see cref="NullEventBus"/> 单例.
        /// </summary>
        public static NullEventBus Instance { get; } = new NullEventBus();

        private NullEventBus()
        {
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler<TEventData>, new()
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
        }
        
        /// <inheritdoc/>
        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
        }
        
        /// <inheritdoc/>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
        }
        
        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return new Task(() => { });
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            return new Task(() => { });
        }
    }
}
