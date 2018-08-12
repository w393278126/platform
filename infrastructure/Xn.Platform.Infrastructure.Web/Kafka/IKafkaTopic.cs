using System;
using Kafka.Client.Producers;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public interface IKafkaTopic : IDisposable
    {
        void Send(params Message[] messages);
        TopicMetadata GetMetadata();
    }
}
