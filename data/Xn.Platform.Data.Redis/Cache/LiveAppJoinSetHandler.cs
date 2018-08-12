using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache 全民直播JoinRoom观看人数
    /// </summary>
    public class LiveAppJoinSetHandler : RedisSet
    {
        public LiveAppJoinSetHandler() : base(RedisKeyDefinition.CacheSetLiveAppJoin)
        {
        }
    }
}
