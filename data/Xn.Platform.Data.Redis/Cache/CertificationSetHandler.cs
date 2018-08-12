using Xn.Platform.Abstractions.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.Redis.Cache
{
     public class CertificationSetHandler: RedisSet
    {
        public CertificationSetHandler() : base(RedisKeyDefinition.CacheSetCertificationSet)
        {
           
        }

        public bool Set(int userId)
        {
           return  Add(null, userId.ToString());
        }
        public bool IsExistUserSet(int userId)
        {
            return Contains(null, userId.ToString());
        }
    }
}
