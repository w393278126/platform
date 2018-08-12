using System;

namespace Xn.Platform.Infrastructure.Web.Events.Factory
{
    /// <summary>
    /// 使用 <see cref="Dispose"/> 方法注销 <see cref="IEventHandlerFactory"/> .
    /// </summary>
    internal class FactoryUnregistrar<TEventData> : IDisposable
         where TEventData : IEventData
    {
        private readonly IEventBus _eventBus;
        private readonly IEventHandlerFactory _factory;

        public FactoryUnregistrar(IEventBus eventBus, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _factory = factory;
        }

        public void Dispose()
        {
            _eventBus.Unregister<TEventData>(_factory);
        }
    }
}
