using Xn.Platform.Abstractions.Redis;
using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.Redis.Cache
{
    public class CertificationHashHandler: RedisHash
    {
        public CertificationHashHandler() : base(RedisKeyDefinition.CacheSetCertificationHash)
        {

        }

        public bool Set(int userId)
        {
            return Set(null,userId.ToString(), DateTime.UtcNow.GetUnixTimeStamp().ToString());
        }

        public bool IsExistUserHash(int userId)
        {
            return Exists(null, userId.ToString());
        }

        public string Get(int userId)
        {
            return Get(null, userId.ToString());
        }

    }
}
