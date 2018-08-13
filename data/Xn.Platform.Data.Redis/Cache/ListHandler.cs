using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class ListKafkaEventDataMissedHandler : RedisList
    {
        public ListKafkaEventDataMissedHandler() : base(RedisKeyDefinition.LogListKafkaEventDataMissed)
        {
        }
    }
}
