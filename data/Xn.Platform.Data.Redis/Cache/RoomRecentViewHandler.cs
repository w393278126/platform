using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache房间最近7天视频播放量
    /// </summary>
    public class RoomRecentViewHandler : RedisSortedSet
    {
        public RoomRecentViewHandler() : base(RedisKeyDefinition.CacheSortedSetRoomMediaRecentView)
        {
        }
    }
}
