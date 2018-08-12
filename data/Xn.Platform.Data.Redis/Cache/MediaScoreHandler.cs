using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache视频标签名
    /// </summary>
    public class MediaTagHandler : RedisHash
    {
        public MediaTagHandler() : base(RedisKeyDefinition.CacheHashMediaTag)
        {
        }
    }
}
