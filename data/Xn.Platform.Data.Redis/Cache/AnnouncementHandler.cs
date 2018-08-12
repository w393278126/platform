using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class AnnouncementHandler : RedisHash
    {
        public AnnouncementHandler() : base(RedisKeyDefinition.CacheHashAnnouncement)
        {
        }
    }

    /// <summary>
    /// 需要下线的轮询公告
    /// </summary>
    public class AnnouncementCancelSetHandler : RedisSet
    {
        public AnnouncementCancelSetHandler() : base(RedisKeyDefinition.CacheSetAnnouncementCancel)
        {
        }
    }
}
