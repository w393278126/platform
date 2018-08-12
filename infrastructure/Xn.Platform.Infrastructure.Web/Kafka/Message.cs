namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class Message
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public Compression Codec { get; set; }
    }
}
