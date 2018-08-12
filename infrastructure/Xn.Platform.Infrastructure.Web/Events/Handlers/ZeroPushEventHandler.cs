using System.Collections.Concurrent;
using System.Threading.Tasks;
using Xn.Platform.Core.Extensions;
using ZeroMQ;

namespace Xn.Platform.Infrastructure.Web.Events.Handlers
{
    public class ZeroPushEventHandler : IEventHandler<ZeroPushEventData>
    {
        public string Endpoint { get; }

        private BlockingCollection<ZeroPushEventData> PushQueue { get; }

        public ZeroPushEventHandler(string endpoint)
        {
            Endpoint = endpoint;

            PushQueue = new BlockingCollection<ZeroPushEventData>(new ConcurrentQueue<ZeroPushEventData>());

            Task.Factory.StartNew(() =>
            {
                using (var publisher = new ZSocket(ZSocketType.PUSH))
                {
                    publisher.Connect(Endpoint);

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

        public void HandleEvent(ZeroPushEventData eventData)
        {
            PushQueue.Add(eventData);
        }
    }
}
