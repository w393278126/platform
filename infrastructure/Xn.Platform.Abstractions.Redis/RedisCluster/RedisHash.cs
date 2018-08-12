using Xn.Platform.Core.Extensions;
using  Xn.Platform.Extensions.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public class RedisHash : RedisStructure, IRedisHash
    {
        public RedisHash(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Delete(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashDelete(InitRedisKey(keySuffix), field, (sr.CommandFlags)commandFlags);
        }

        public long Delete(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            var hashFields = fields.Select(x => (RedisValue)x).ToArray();
            return Client.HashDelete(InitRedisKey(keySuffix), hashFields, (sr.CommandFlags)commandFlags);
        }

        public long DeleteAllSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return DeleteAllEntriesFromHashSafe(keySuffix);
        }

        private long DeleteAllEntriesFromHashSafe(string keySuffix, int batchSize = 500)
        {
            var keysAll = HashKeyScanAll(InitRedisKey(keySuffix), batchSize);
            long count = 0;
            foreach (var block in keysAll.ToBlocks(batchSize))
            {
                Delete(keySuffix, block);

                count += block.Length;
            }

            return count;
        }

        private IEnumerable<string> HashKeyScanAll(string hashId, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = HashKeyScan(hashId, cursor, pageSize);

                if (resultList == null || resultList.Length == 0)
                {
                    yield break;
                }

                for (var i = 1; i < resultList.Length; ++i)
                {
                    yield return resultList[i];
                }

                cursor = resultList[0].AsLong();
                if (cursor == 0)
                {
                    yield break;
                }
            } while (true);
        }

        private string[] HashKeyScan(string hashId, long cursor = 0, int pageSize = 500)
        {
            return (string[])Client.ScriptEvaluate(Script.HKEYSCAN, new[] { (RedisKey)hashId }, new RedisValue[] {cursor,pageSize });
        }

        public bool Exists(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashExists(InitRedisKey(keySuffix), field, (sr.CommandFlags)commandFlags);
        }

        public string Get(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashGet(InitRedisKey(keySuffix), field, (sr.CommandFlags)commandFlags);
        }

        public IDictionary<string, string> Get(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            var hashFields = fields.Select(x => (RedisValue)x).ToArray();
            var rValues = Client.HashGet(InitRedisKey(keySuffix), hashFields, (sr.CommandFlags)commandFlags);
            var result = fields.Zip(rValues, (key, x) =>
                {
                    if (!x.HasValue) return new { key, rValue = string.Empty };
                    return new { key, rValue = (string)x };
                })
                .ToDictionary(x => x.key, x => x.rValue);
            return result;
        }

        public IDictionary<string, string> GetAll(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            var hashEntries = Client.HashGetAll(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
            var result = new Dictionary<string, string>();
            foreach (var hashEntry in hashEntries)
            {
                result[hashEntry.Name] = hashEntry.Value;
            }
            return result;
        }

        /// <summary>
        /// 使用HSCAN方式获取所有键值对
        /// </summary>
        public Dictionary<string, string> GetAllSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return GetAllEntriesFromHashSafe(InitRedisKey(keySuffix));
        }

        private Dictionary<string, string> GetAllEntriesFromHashSafe(string hashId, int pageSize = 500)
        {
            return HashScanAll(hashId, pageSize).ToDictionaryEx(o => o.Key, o => o.Value);
        }

        /// <summary>
        /// 使用HSCAN遍历获取所有键值对
        /// </summary>
        private IEnumerable<KeyValuePair<string, string>> HashScanAll(string hashId, int pageSize = 500)
        {
            var resultList = Client.HashScan(hashId, pageSize: pageSize).ToList();

            if (resultList.Count == 0)
            {
                yield break;
            }

            foreach (var entry in resultList)
            {
                yield return new KeyValuePair<string, string>(entry.Name, entry.Value);
            }
        }

        public long Increment(string keySuffix, string field, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashIncrement(InitRedisKey(keySuffix), field, value, (sr.CommandFlags)commandFlags);
        }

        public double Increment(string keySuffix, string field, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashIncrement(InitRedisKey(keySuffix), field, value, (sr.CommandFlags)commandFlags);
        }

        public string[] Keys(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.HashKeys(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashLength(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }
        
        public bool Set(string keySuffix, string field, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.HashSet(InitRedisKey(keySuffix), field, value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }

        public void Set(string keySuffix, IDictionary<string, string> values, CommandFlags commandFlags = CommandFlags.None)
        {
            var hashFields = values.Select(x => new HashEntry(x.Key, x.Value)).ToArray();
            Client.HashSet(InitRedisKey(keySuffix), hashFields, (sr.CommandFlags)commandFlags);
        }

        public string[] Values(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.HashValues(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
            return r.Select(o => (string)o).ToArray();
        }

        public long DecrementLimitByMin(string keySuffix, string field, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            var left = Client.HashDecrement(InitRedisKey(keySuffix), field, value, (sr.CommandFlags)commandFlags);
            if (left < 0)
            {
                Client.HashIncrement(InitRedisKey(keySuffix), field, value, (sr.CommandFlags)commandFlags);
            }
            return left;
        }

        public long IncrementLimitByMax(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            var left = Client.HashIncrement(InitRedisKey(keySuffix), field, value);
            if (left > max)
            {
                Client.HashSet(InitRedisKey(keySuffix), field, max.ToString());
                return max;
            }
            return left;
        }

        public double IncrementLimitByMax(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long IncrementLimitByMin(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double IncrementLimitByMin(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }
    }
}