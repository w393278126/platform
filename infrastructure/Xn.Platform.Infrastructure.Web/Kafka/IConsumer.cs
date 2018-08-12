using System;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public interface IConsumer<out T> : IDisposable
    {
        void Start(
            Action<T> dataSubscriber,
            Action<Exception> errorSubscriber = null,
            Action closeAction = null);
        void Shutdown();
    }
}