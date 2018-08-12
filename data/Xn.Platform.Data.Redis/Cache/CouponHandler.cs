using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class CouponHandler : RedisString
    {
        public CouponHandler() : base(RedisKeyDefinition.CacheStringCouponExchangeTimes)
        {
        }

    }
}
