using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class BatchTimeStampHandler : RedisHash
    {
        public BatchTimeStampHandler() : base(RedisKeyDefinition.CacheHashBatchTimeStamp)
        {
        }
    }
}
