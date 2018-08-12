using Xn.Platform.Abstractions.Redis.RedisCluster;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain.Sports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Data.Redis.Sports
{
    public class SportStarRecommendHandler : RedisHash
    {
        
        public SportStarRecommendHandler() : base(RedisKeyDefinition.SportStarRecommend)
        {
        }
    }
}
