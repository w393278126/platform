using System;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public interface IKafkaClient : IDisposable
    {
        IKafkaTopic Topic(string name);
        IKafkaConsumer Consumer(string groupName);
        IKafkaConsumer Consumer(ConsumerOptions options);
        IKafkaSimpleConsumer SimpleConsumer();
    }
}
