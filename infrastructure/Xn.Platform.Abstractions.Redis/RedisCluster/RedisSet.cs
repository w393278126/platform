using Xn.Platform.Core.Extensions;
using  Xn.Platform.Extensions.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public class RedisSet : RedisStructure, IRedisSet
    {
        public RedisSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetAdd(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }

        public long Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return Client.SetAdd(InitRedisKey(keySuffix), redisValues, (sr.CommandFlags)commandFlags);
        }


        public string[] Combine(SetOperation operation, string[] keys, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long CombineAndStore(SetOperation operation, string destination, string[] keys, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetContains(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetLength(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string[] Members(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.SetMembers(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }


        public string[] MembersSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return SetScanAll(InitRedisKey(keySuffix)).ToHashSet().ToArray();
        }

        public HashSet<string> MembersSafeHash(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return SetScanAll(InitRedisKey(keySuffix)).ToHashSet();
        }

        /// <summary>
        /// 使用SSCAN遍历获取所有成员
        /// </summary>
        private IEnumerable<string> SetScanAll(string setId, int pageSize = 500)
        {
            var resultList = Client.SetScan(setId, pageSize: pageSize).ToList();

            if (resultList.Count == 0)
            {
                yield break;
            }

            foreach (var entry in resultList)
            {
                yield return entry.ToString();
            }
        }

        public bool Move(string keySuffix, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None)
        {
            return Client.SetMove(InitRedisKey(keySuffix), destination, value, (sr.CommandFlags)flags);
        }

        public string Pop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetPop(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string RandomMember(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetRandomMember(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string[] RandomMembers(string keySuffix, long count, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.SetRandomMembers(InitRedisKey(keySuffix), count, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SetRemove(InitRedisKey(keySuffix), member, (sr.CommandFlags)commandFlags);
        }

        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValues = members.Select(x => (RedisValue)x).ToArray();
            return Client.SetRemove(InitRedisKey(keySuffix), redisValues, (sr.CommandFlags)commandFlags);
        }

        public long RemoveSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return RemoveSetAllSafe(keySuffix);
        }

        private long RemoveSetAllSafe(string keySuffix, int batchSize = 500)
        {
            long count = 0;
            var setId = InitRedisKey(keySuffix);
            foreach (var block in SetScanAll(setId, batchSize).ToBlocks(batchSize))
            {
                Remove(keySuffix, block);
                count += block.Length;
            }

            return count;
        }

        public IEnumerable<RedisValue> Scan(string keySuffix, RedisValue pattern = new RedisValue(), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            return Client.SetScan(InitRedisKey(keySuffix), pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags);
        }
    }
}
