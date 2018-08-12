using Kafka.Client.Producers;
using Logging.Client;
using System;
using System.Linq;
using KafkaMessage = Kafka.Client.Messages.Message;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class KafkaTopic : IKafkaTopic
    {
        private readonly IZookeeperConnection _zkConnect;
        private readonly string _name;
        private readonly IProducer<string, KafkaMessage> _producer;
        private readonly IZookeeperClient _zkClient;
        private readonly ILog _logger = LogManager.GetLogger(typeof(KafkaTopic));

        public KafkaTopic(IZookeeperConnection zkConnect, string name)
        {
            _zkConnect = zkConnect;
            _name = name;
            _zkClient = zkConnect.CreateClient();
            _producer = _zkClient.CreateProducer<string, KafkaMessage>();
        }

        public void Send(params Message[] messages)
        {
            _producer.Send(messages.Select(m => m.AsProducerData(_name)));
        }

        public TopicMetadata GetMetadata()
        {
            return _zkConnect.CreateSimpleManager()
                .RefreshMetadata(
                    KafkaConfig.VersionId,
                    KafkaConfig.ClientId,
                    KafkaConfig.NextCorrelationId(),
                    _name,
                    false);
        }

        public void Dispose()
        {
            _producer.Dispose();
            _zkClient.Dispose();
        }
    }
}
