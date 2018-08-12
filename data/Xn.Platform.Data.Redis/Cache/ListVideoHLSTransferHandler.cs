using System;
using System.Collections.Generic;
using System.Linq;
using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class ListVideoHLSTransferHandler : RedisList
    {
        public ListVideoHLSTransferHandler() : base(RedisKeyDefinition.CacheListVideoHlsTransfer)
        {

        }

        public void Push(int cloudMediaId)
        {
            this.LeftPush(null, cloudMediaId.ToString());
        }

        public List<int> GetALL()
        {
            string[] val = this.Range(null, 0, -1);
            if (val == null || val.Length <= 0) { return new List<int>(); }
            return System.Array.ConvertAll<string, int>(val, x => Convert.ToInt32(x)).ToList();
        }
        public List<int> GetALL(int count)
        {
            string[] val = this.Range(null, 0, count);
            if (val == null || val.Length <= 0) { return new List<int>(); }
            return System.Array.ConvertAll<string, int>(val, x => Convert.ToInt32(x)).ToList();
        }

    }
}
