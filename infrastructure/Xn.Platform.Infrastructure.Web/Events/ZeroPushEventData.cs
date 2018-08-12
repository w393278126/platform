namespace Xn.Platform.Infrastructure.Web.Events
{
    public enum ZeroPushEventType
    {
        NotSet = 0,
        LiveStarted = 1,
        LiveEnded = 2
    }

    public class ZeroPushEventData : EventData
    {
        public ZeroPushEventType EventType { get; set; }
        public object Data { get; set; }
    }
}
