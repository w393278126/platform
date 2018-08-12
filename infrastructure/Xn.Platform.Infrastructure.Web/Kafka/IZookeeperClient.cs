using Kafka.Client.Cluster;
using Kafka.Client.Producers;
using System;
using System.Collections.Generic;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public interface IZookeeperClient : IDisposable
    {
        IEnumerable<Broker> GetAllBrokers();
        IProducer<TKey, TMessage> CreateProducer<TKey, TMessage>();
        IProducer<TKey, TMessage> CreateProducer<TKey, TMessage>(ProducerConfig config);
    }
}
