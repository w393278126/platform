using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Xn.Platform.Core.Extensions;
using ZeroMQ;

namespace Xn.Platform.Infrastructure.Web.Events.Handlers
{
    public class ZeroPubEventHandler : IEventHandler<ZeroPubEventData>
    {
        public string Endpoint { get; }

        private BlockingCollection<ZeroPubEventData> PushQueue { get; }

        public ZeroPubEventHandler(string endpoint)
        {
            Endpoint = endpoint;

            PushQueue = new BlockingCollection<ZeroPubEventData>(new ConcurrentQueue<ZeroPubEventData>());

            Task.Factory.StartNew(() =>
            {
                using (var publisher = new ZSocket(ZSocketType.PUB))
                {
                    publisher.Linger = TimeSpan.Zero;
                    publisher.Bind(Endpoint);

                    foreach (var eventData in PushQueue.GetConsumingEnumerable())
                    {
                        using (var message = new ZMessage())
                        {
                            message.Add(new ZFrame($"{eventData.EventType} {eventData.Data.ToJson()}"));
                            publisher.Send(message);
                        }
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void HandleEvent(ZeroPubEventData eventData)
        {
            PushQueue.Add(eventData);
        }
    }
}
