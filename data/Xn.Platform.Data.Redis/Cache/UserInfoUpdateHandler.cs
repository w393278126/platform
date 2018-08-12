using Xn.Platform.Abstractions.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.Redis.Cache
{
    public class UserInfoUpdateHandler : RedisList
    {
        public UserInfoUpdateHandler() : base(RedisKeyDefinition.CacheListUserInfoUpdate)
        {

        }
    }
}
