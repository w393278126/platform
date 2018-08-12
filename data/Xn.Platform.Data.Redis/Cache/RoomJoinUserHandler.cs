using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{

    /// <summary>
    /// 不显示用户总榜集合
    /// </summary>
    public class NotShowTotalHandler : RedisSet
    {
        public NotShowTotalHandler() : base(RedisKeyDefinition.CacheSetNotShowTotal)
        {
        }
    }
}
