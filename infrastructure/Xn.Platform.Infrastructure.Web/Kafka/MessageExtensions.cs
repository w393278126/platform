using Kafka.Client.Messages;
using Kafka.Client.Producers;
using System.Text;
using KafkaMessage = Kafka.Client.Messages.Message;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public static class MessageExtensions
    {
        public static ProducerData<string, KafkaMessage> AsProducerData(this Message message, string topic)
        {
            return new ProducerData<string, KafkaMessage>(
                topic,
                message.Key,
                new KafkaMessage(
                    Encoding.UTF8.GetBytes(message.Value),
                    Encoding.UTF8.GetBytes(message.Key),
                    (CompressionCodecs)message.Codec)
                );
        }

        public static ConsumedMessage AsConsumedMessage(this KafkaMessage message)
        {
            return new ConsumedMessage
            {
                Partition = message.PartitionId ?? 0,
                Offset = message.Offset,
                Key = message.Key.Decode(),
                Value = message.Payload.Decode(),
                Codec = (Compression)message.CompressionCodec
            };
        }

        public static byte[] Encode(this string value)
        {
            return value == null ? null : Encoding.UTF8.GetBytes(value);
        }

        public static string Decode(this byte[] value)
        {
            return value == null ? null : Encoding.UTF8.GetString(value);
        }
    }
}
