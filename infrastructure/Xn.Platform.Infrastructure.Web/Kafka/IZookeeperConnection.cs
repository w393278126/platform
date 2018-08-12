using Kafka.Client.Helper;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public interface IZookeeperConnection
    {
        IZookeeperClient CreateClient();
        IConsumerConnector CreateConsumerConnector(ConsumerOptions options);
        KafkaSimpleManager<string, Message> CreateSimpleManager();
    }
}
