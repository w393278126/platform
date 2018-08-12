namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class ProducerConfig
    {
        public const short DefaultAcks = 0;

        public short Acks { get; set; }

        public static ProducerConfig Default()
        {
            return new ProducerConfig
            {
                Acks = DefaultAcks
            };
        }
    }
}