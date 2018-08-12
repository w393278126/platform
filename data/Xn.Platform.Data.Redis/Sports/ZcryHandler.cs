using Xn.Platform.Abstractions.Redis.RedisCluster;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain.Sports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Data.Redis.Sports
{
    public class ZcryRankHandler : RedisSortedSet
    {
        
        public ZcryRankHandler() : base(RedisKeyDefinition.ZcryRankSortSet)
        {
        }
    }

    public class ZcryObjectTypeHandler : RedisHash
    {
        public ZcryObjectTypeHandler() : base(RedisKeyDefinition.ZcryObjectTypeRedisHash)
        {
        }
    }

    public class ZcryActivityHandler : RedisHash
    {
        public ZcryActivityHandler() : base(RedisKeyDefinition.ZcrySprinFinalRedisHash)
        {
        }
    }
}
