using  Xn.Platform.Abstractions.Redis.LuaScripts;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using  Xn.Platform.Extensions.Extensions;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis
{
    public static class RedisClientExtensions
    {
        /// <summary>
        /// 分批次HMGET再合并
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        public static List<string> GetValuesFromHashSafe(this IRedisClient redis, string hashId, string[] keys, int blockSize = 100)
        {
            if (keys == null || keys.Length == 0)
                return new List<string>();

            if (keys.Length <= blockSize)
                return redis.GetValuesFromHash(hashId, keys);

            return keys.ToBlocks(blockSize)
                       .SelectMany(block => redis.GetValuesFromHash(hashId, block))
                       .ToList();
        }
        public static int RemoveEntriesFromHashSafe(this IRedisClient redis, string hashId, string[] fields, int blockSize = 100)
        {
            if (fields == null || fields.Length < 1)
                return 0;
            if (fields.Length <= blockSize)
                return redis.RemoveEntriesFromHash(hashId, fields);

            return fields.ToBlocks(blockSize).Sum(block => redis.RemoveEntriesFromHash(hashId, block));
        }

        /// <summary>
        /// 使用HSCAN方式获取所有键值对
        /// </summary>
        public static Dictionary<string, string> GetAllEntriesFromHashSafe(this IRedisClient redis, string hashId, int pageSize = 500)
        {
            return redis.HashScanAll(hashId, pageSize).ToDictionaryEx(o => o.Key, o => o.Value);
        }

        /// <summary>
        /// 使用HSCAN遍历获取所有键值对
        /// </summary>
        private static IEnumerable<KeyValuePair<string, string>> HashScanAll(this IRedisClient redis, string hashId, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = redis.HashScan(hashId, cursor, pageSize);

                if (resultList == null || resultList.Count == 0)
                {
                    yield break;
                }

                for (var i = 1; i < resultList.Count - 1; i += 2)
                {
                    yield return new KeyValuePair<string, string>(resultList[i], resultList[i + 1]);
                }

                cursor = resultList[0].AsLong();
                if (cursor == 0)
                {
                    yield break;
                }
            } while (true);
        }

        public static List<string> HashScan(this IRedisClient redis, string hashId, long cursor = 0, int pageSize = 500)
        {
            var sha1 = redis.GetSha1(RedisScanLua.HSCAN);
            return redis.ExecLuaShaAsList(sha1, hashId, cursor.ToString(), pageSize.ToString());
        }


        /// <summary>
        /// 使用SSCAN方式获取所有成员
        /// </summary>
        public static HashSet<string> GetAllItemsFromSetSafe(this IRedisClient redis, string setId, int pageSize = 500)
        {
            return redis.SetScanAll(setId, pageSize).ToHashSet();
        }

        /// <summary>
        /// 使用SSCAN遍历获取所有成员
        /// </summary>
        private static IEnumerable<string> SetScanAll(this IRedisClient redis, string setId, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = redis.SetScan(setId, cursor, pageSize);

                if (resultList == null || resultList.Count == 0)
                {
                    yield break;
                }

                for (var i = 1; i < resultList.Count; ++i)
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

        public static List<string> SetScan(this IRedisClient redis, string setId, long cursor = 0, int pageSize = 500)
        {
            var sha1 = redis.GetSha1(RedisScanLua.SSCAN);
            return redis.ExecLuaShaAsList(sha1, setId, cursor.ToString(), pageSize.ToString());
        }

        public static string SortedSetIncrementLimitByMin(this IRedisClient redis, string setId, string member, double value, double min)
        {
            var sha1 = redis.GetSha1(Script.SortedSetIncrementLimitByMin);
            return redis.ExecLuaShaAsString(sha1, new[] { setId }, new[] { member, value.ToString(CultureInfo.InvariantCulture), min.ToString(CultureInfo.InvariantCulture) });
        }

        public static string SortedSetIncrementLimitByMax(this IRedisClient redis, string setId, string member, double value, double max)
        {
            var sha1 = redis.GetSha1(Script.SortedSetIncrementLimitByMax);
            return redis.ExecLuaShaAsString(sha1, new[] { setId }, new[] { member, value.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture) });
        }

        /// <summary>
        /// 使用HSCAN方式获取所有键
        /// </summary>
        public static string[] GetHashKeysSafe(this IRedisClient redis, string hashId, int pageSize = 500)
        {
            return redis.HashKeyScanAll(hashId, pageSize).ToHashSet().ToArray();
        }

        /// <summary>
        /// 使用HSCAN遍历获取所有键
        /// </summary>
        private static IEnumerable<string> HashKeyScanAll(this IRedisClient redis, string hashId, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = redis.HashKeyScan(hashId, cursor, pageSize);

                if (resultList == null || resultList.Count == 0)
                {
                    yield break;
                }

                for (var i = 1; i < resultList.Count; ++i)
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

        public static List<string> HashKeyScan(this IRedisClient redis, string hashId, long cursor = 0, int pageSize = 500)
        {
            var sha1 = redis.GetSha1(RedisScanLua.HKEYSCAN);
            return redis.ExecLuaShaAsList(sha1, hashId, cursor.ToString(), pageSize.ToString());
        }

        /// <summary>
        /// 使用SCAN MATCH pattern获取所有Key
        /// </summary>
        public static string[] KeysSafe(this IRedisClient redis, string pattern, int pageSize = 500)
        {
            return redis.SearchAllKeysSafe(pattern, pageSize).ToHashSet().ToArray();
        }

        private static IEnumerable<string> SearchAllKeysSafe(this IRedisClient redis, string pattern, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = redis.SearchKeysSafe(pattern, cursor, pageSize);

                if (resultList == null || resultList.Count == 0)
                {
                    yield break;
                }

                for (var i = 1; i < resultList.Count; ++i)
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

        public static void SearchKeysSafeDo(this IRedisClient redis, string pattern, Action<string> doString, int pageSize = 500)
        {
            long cursor = 0;

            do
            {
                var resultList = redis.SearchKeysSafe(pattern, cursor, pageSize);

                if (resultList == null || resultList.Count == 0)
                {
                    break;
                }

                for (var i = 1; i < resultList.Count; ++i)
                {
                    doString(resultList[i]);
                }
                cursor = resultList[0].AsLong();
                if (cursor == 0)
                {
                    break;
                }
            } while (true);
        }

        public static List<string> SearchKeysSafe(this IRedisClient redis, string pattern, long cursor = 0, int pageSize = 500)
        {
            var sha1 = redis.GetSha1(RedisScanLua.SCAN);
            return redis.ExecLuaShaAsList(sha1, pattern, cursor.ToString(), pageSize.ToString());
        }

        public static int RemoveKeysByPatternSafe(this IRedisClient redis, string pattern)
        {
            var keys = redis.SearchKeysSafe(pattern);
            foreach (var key in keys)
            {
                redis.RemoveKeySafe(key);
            }
            return keys.Count;
        }

        public static bool RemoveKeySafe(this IRedisClient redis, string key)
        {
            var type = redis.GetEntryType(key);
            switch (type)
            {
                case RedisKeyType.Hash:
                    return redis.RemoveHashSafe(key);
                case RedisKeyType.Set:
                    return redis.RemoveSetSafe(key);
                case RedisKeyType.String:
                case RedisKeyType.List:
                case RedisKeyType.SortedSet:
                    return redis.Remove(key);
                default:
                    return false;
            }
        }

        public static bool RemoveSetSafe(this IRedisClient redis, string setId, int batchSize = 500)
        {
            foreach (var block in redis.SetScanAll(setId, batchSize).ToBlocks(batchSize))
            {
                redis.RemoveItemsFromSet(setId, block);
            }
            return redis.Remove(setId);
        }

        public static bool RemoveHashSafe(this IRedisClient redis, string hashId, int batchSize = 500)
        {
            foreach (var block in redis.HashKeyScanAll(hashId, batchSize).ToBlocks(batchSize))
            {
                redis.RemoveEntriesFromHash(hashId, block);
            }
            return redis.Remove(hashId);
        }

        public static long AddRangeToSetSafe(this IRedisClient redis, string setId, List<string> items, int blockSize = 500)
        {
            if (items == null || items.Count == 0)
                return 0;

            if (items.Count <= blockSize)
            {
                return redis.AddRangeToSetBatch(setId, items);
            }

            return items.ToBlocks(blockSize).Sum(block => redis.AddRangeToSetBatch(setId, block));
        }

        public static long AddRangeToSetBatch(this IRedisClient redis, string setId, List<string> items)
        {
            var sha1 = redis.GetSha1(Script.SADD);
            return redis.ExecLuaShaAsInt(sha1, new []{ setId }, items.ToArray());
        }

        public static void SetRangeInHashSafe(this IRedisClient redis, string hashId, IEnumerable<KeyValuePair<string,string>> keyValuePairs, int blockSize = 500)
        {
            var pairs = keyValuePairs?.ToList();
            if (pairs == null || pairs.Count == 0)
                return;

            if (pairs.Count <= blockSize)
            {
                redis.SetRangeInHash(hashId, pairs);
                return;
            }

            foreach (var block in pairs.ToBlocks(blockSize))
            {
                redis.SetRangeInHash(hashId, block);
            }
        }

        /// <summary>
        /// 需要删除一个大Key时，请用本方法，注意需要定期清除gc:keys:*
        /// </summary>
        public static void RemoveKeyByRename(this IRedisClient redis, string key)
        {
            var sha1 = redis.GetSha1(Script.DeleteKeyByRename);
            redis.ExecLuaShaAsList(sha1, key);
        }


        /// <summary>
        /// 需要覆盖一个大Key时，请用本方法，注意需要定期清除gc:keys:*
        /// </summary>
        public static void RenameFast(this IRedisClient redis, string key, string newKey)
        {
            var sha1 = redis.GetSha1(Script.RenameFast);
            redis.ExecLuaShaAsList(sha1, key, newKey);
        }

        /// <summary>
        /// 设置一个不存在的key并附带过期时间，通常可以用来做锁
        /// </summary>
        public static bool SetEntryIfNotExistsWithExpire(this IRedisClient redis, string key, string value, TimeSpan expiry)
        {
            var sha1 = redis.GetSha1(Script.SETNXEX);
            return redis.ExecLuaShaAsInt(sha1, new[] { key }, new[] { value, expiry.TotalSeconds.ToString() }) == 1;
        }
    }
}
