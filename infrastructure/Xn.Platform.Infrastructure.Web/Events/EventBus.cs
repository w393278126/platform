using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using  Xn.Platform.Infrastructure.Web.Events.Factory;
using  Xn.Platform.Infrastructure.Web.Events.Handlers;
using Common.Logging;

namespace Xn.Platform.Infrastructure.Web.Events
{
    /// <summary>
    /// 消息总线类（单例模式）
    /// </summary>
    public class EventBus : IEventBus
    {
        private static readonly ILog log = LogManager.GetLogger("EventBus");
        /// <summary>
        /// 获取 <see cref="EventBus"/> 单例.
        /// </summary>
        public static EventBus Default { get; } = new EventBus();

        /// <summary>
        /// 所有注册的事件处理器工厂.
        /// Key: 事件类型
        /// Value: 事件处理器工厂列表
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<IEventHandlerFactory>> _handlerFactories;

        /// <summary>
        /// 创建 <see cref="EventBus"/> 实例.
        /// 可以使用 <see cref="Default"/> 获取全局 <see cref="EventBus"/> 实例.
        /// </summary>
        public EventBus()
        {
            _handlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
        }

        #region Register

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Register(new ActionEventHandler<TEventData>(action));
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register<TEventData>(new SingleInstanceHandlerFactory<TEventData>(handler));
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler<TEventData>, new()
        {
            return Register<TEventData>(new TransientEventHandlerFactory<TEventData, THandler>());
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories<TEventData>().Add(handlerFactory);
            return new FactoryUnregistrar<TEventData>(this, handlerFactory);
        }

        #endregion

        #region Unregister

        /// <inheritdoc/>
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories<TEventData>()
                .RemoveAll(
                    factory =>
                    {
                        var singleInstanceFactory = factory as SingleInstanceHandlerFactory<TEventData>;
                        if (singleInstanceFactory != null)
                        {
                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEventData>;
                            if (actionHandler != null)
                            {
                                if (actionHandler.Action == action)
                                {
                                    return true;
                                }
                            }
                        }

                        return false;
                    });
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories<TEventData>().RemoveAll(factory => factory is SingleInstanceHandlerFactory<TEventData> && ((SingleInstanceHandlerFactory<TEventData>)factory).HandlerInstance == handler);
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories<TEventData>().Remove(factory);
        }

        /// <inheritdoc/>
        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            GetOrCreateHandlerFactories<TEventData>().Clear();
        }

        #endregion

        #region Trigger

        /// <inheritdoc/>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Trigger(null, eventData);
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            eventData.EventSource = eventSource;
            var triggers = GetHandlerFactories<TEventData>();
            if(triggers.Count == 0)
            {
                log.Error("EventHandler Factory is Null, Use Register Method Before");
                return;
            }
            foreach (var factoryToTrigger in triggers)
            {
                var eventHandler = factoryToTrigger.GetHandler();
                if (eventHandler == null)
                {
                    continue;
                }
                try
                {
                    eventHandler.HandleEvent(eventData);
                }
                finally
                {
                    factoryToTrigger.ReleaseHandler(eventHandler);
                }
            }
        }

        private List<IEventHandlerFactory<TEventData>> GetHandlerFactories<TEventData>() where TEventData : IEventData
        {
            List<IEventHandlerFactory> handlerFactoryList;
            if (_handlerFactories.TryGetValue(typeof(TEventData), out handlerFactoryList))
            {
                return handlerFactoryList.OfType<IEventHandlerFactory<TEventData>>().ToList();
            }
            return new List<IEventHandlerFactory<TEventData>>();
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync((object)null, eventData);
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            var flowControl = ExecutionContext.SuppressFlow();

            var task = Task.Run(() => Trigger(eventSource, eventData));

            flowControl.Undo();

            return task;
        }

        #endregion

        #region Private methods

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories<TEventData>() where TEventData : IEventData
        {
            return _handlerFactories.GetOrAdd(typeof(TEventData), type => new List<IEventHandlerFactory>());
        }

        #endregion

    }
}
