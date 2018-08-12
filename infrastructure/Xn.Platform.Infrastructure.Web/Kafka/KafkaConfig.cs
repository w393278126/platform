namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public static class KafkaConfig
    {
        private static readonly object Lock = new object();

        static KafkaConfig()
        {
            ClientId = "kafka.basic.dotnet";
        }
        public static string ClientId { get; }
        public const short VersionId = 0;
        public static int CorrelationId { get; private set; }

        public static int NextCorrelationId()
        {
            lock (Lock)
            {
                return CorrelationId++;
            }
        }
    }
}
