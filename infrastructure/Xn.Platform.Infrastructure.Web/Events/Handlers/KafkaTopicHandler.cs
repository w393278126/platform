using Common.Logging;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis.Cache;
using  Xn.Platform.Infrastructure.Web.Kafka;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.Events.Handlers
{
    public class KafkaTopicHandler : IEventHandler<KafkaTopicEventData>
    {
        private IKafkaClient Client { get; }

        private BlockingCollection<KafkaTopicEventData> PushQueue { get; }
        private ConcurrentDictionary<KafkaTopicType, IKafkaTopic> Topics { get; }

        private readonly ListKafkaEventDataMissedHandler _listKafkaEventDataMissedHandler = new ListKafkaEventDataMissedHandler();

        private readonly ILog _logger = LogManager.GetLogger(typeof(KafkaTopicHandler));

        public KafkaTopicHandler(string zkConnectionStr)
        {
            Client = new KafkaClient(zkConnectionStr);
            Topics = new ConcurrentDictionary<KafkaTopicType, IKafkaTopic>();

            PushQueue = new BlockingCollection<KafkaTopicEventData>(new ConcurrentQueue<KafkaTopicEventData>());

            Task.Factory.StartNew(() =>
            {
                foreach (var eventData in PushQueue.GetConsumingEnumerable())
                {
                    try
                    {
                        var topic = Topics.GetOrAdd(eventData.TopicType, type => Client.Topic(type.ToString().ToLowerInvariant()));
                        var data = eventData.Data as string ?? eventData.Data.ToNewtonsoftJson();
                        topic.Send(new Message
                        {
                            Key = eventData.Key ?? string.Empty,
                            Value = data,
                            Codec = eventData.Codec
                        });
                    }
                    catch (Exception e)
                    {
                        // 时长事件不补发
                        if (eventData.TopicType != KafkaTopicType.MissionEventOnline)
                        {
                            _listKafkaEventDataMissedHandler.LeftPush(null, eventData.ToJson());
                        }
                        _logger.Error("KafkaTopicHandler", e);
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void HandleEvent(KafkaTopicEventData eventData)
        {
            PushQueue.Add(eventData);
        }
    }
}
