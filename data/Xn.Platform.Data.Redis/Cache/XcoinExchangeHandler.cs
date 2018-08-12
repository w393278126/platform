using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class XcoinExchangeHandler : RedisString
    {
        public XcoinExchangeHandler() : base(RedisKeyDefinition.Cache2StringXcoinExchangePasswordError)
        {
        }
    }

    public class XcoinExchangeTokenHandler : RedisString
    {
        public XcoinExchangeTokenHandler() : base(RedisKeyDefinition.Cache2StringXcoinExchangeToken)
        {
        }
    }
}
