using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Login
{
    public class PluAuthenticationHandler : RedisSortedSet
    {
        public PluAuthenticationHandler() : base(RedisKeyDefinition.LoginSortedSetPluKey)
        {
        }
    }
}
