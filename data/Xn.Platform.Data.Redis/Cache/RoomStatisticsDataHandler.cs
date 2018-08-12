using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache
    /// </summary>
    public class RoomStatisticsChatDataHandler : RedisHash
    {
        public RoomStatisticsChatDataHandler() : base(RedisKeyDefinition.CacheHashRoomStatisticsChat)
        {
        }
    }
}
