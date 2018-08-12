namespace Xn.Platform.Infrastructure.Web.Events
{
    public enum ZeroPubEventType
    {
        NotSet = 0,
        LiveStarted = 1,
        LiveEnded = 2
    }

    public class ZeroPubEventData : EventData
    {
        public ZeroPubEventType EventType { get; set; }
        public object Data { get; set; }
    }
}
