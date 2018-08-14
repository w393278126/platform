using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Login
{
    public class XnAuthenticationHandler : RedisSortedSet
    {
        public XnAuthenticationHandler() : base(RedisKeyDefinition.LoginSortedSetXnKey)
        {
        }
    }
}
