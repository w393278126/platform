using Xn.Platform.Abstractions.Redis;
using RedisCluster = Xn.Platform.Abstractions.Redis.RedisCluster;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using System;

namespace Xn.Platform.Data.Redis.Cache
{
    public class LimitGiftUserTimesHandler : RedisHash
    {
        public LimitGiftUserTimesHandler() : base(RedisKeyDefinition.CacheHashUserLimitTimes)
        {
        }

        public string GetkeySuffix(int userId, string itemName)
        {
            return $"{userId}:{itemName}";
        }
    }
}
