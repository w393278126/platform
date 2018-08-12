using Kafka.Client.Cfg;
using Kafka.Client.Cluster;
using Kafka.Client.Producers;
using Kafka.Client.Utils;
using Kafka.Client.ZooKeeperIntegration;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class ZookeeperClient : IZookeeperClient
    {
        private readonly ZooKeeperClient _client;

        public ZookeeperClient(string zkConnect)
        {
            _client = new ZooKeeperClient(
                zkConnect,
                ZooKeeperConfiguration.DefaultSessionTimeout,
                ZooKeeperStringSerializer.Serializer
                );
            _client.Connect();
        }

        public IEnumerable<Broker> GetAllBrokers()
        {
            return ZkUtils.GetAllBrokersInCluster(_client);
        }

        public IProducer<TKey, TMessage> CreateProducer<TKey, TMessage>()
        {
            return CreateProducer<TKey, TMessage>(ProducerConfig.Default());
        }

        public IProducer<TKey, TMessage> CreateProducer<TKey, TMessage>(ProducerConfig config)
        {
            var producerConfiguration = new ProducerConfiguration(
                GetAllBrokers()
                    .Select(b => new BrokerConfiguration
                    {
                        BrokerId = b.Id,
                        Host = b.Host,
                        Port = b.Port
                    }).ToList()
                )
            {
                RequiredAcks = config.Acks,
                ProducerRetries = 5
            };

            producerConfiguration.PartitionerClass = " Xn.Platform.Infrastructure.Web.Kafka.CustomStringPartitioner`1,  Xn.Platform.Infrastructure.Web";
            return new Producer<TKey, TMessage>(producerConfiguration);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
