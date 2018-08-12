using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// 通知TGP Redis缓存
    /// </summary>
    public class NoticeTgpRedisHandler : RedisSet
    {
        public NoticeTgpRedisHandler() : base(RedisKeyDefinition.CacheSetNoticeTgpRoomStatus)
        {

        }
    }

    public class ZhimaNumberRedisHandler : RedisString
    {
        public ZhimaNumberRedisHandler() : base(RedisKeyDefinition.CacheStringZhimaNumber)
        {

        }
    }

}
