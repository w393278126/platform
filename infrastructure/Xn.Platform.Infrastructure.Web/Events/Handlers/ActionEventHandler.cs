using System;

namespace Xn.Platform.Infrastructure.Web.Events.Handlers
{
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData>
    {
        public Action<TEventData> Action { get; private set; }

        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}
