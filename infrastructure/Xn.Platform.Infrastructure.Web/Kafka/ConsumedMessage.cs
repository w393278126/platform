namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class ConsumedMessage : Message
    {
        public int Partition { get; set; }
        public long Offset { get; set; }
    }
}
