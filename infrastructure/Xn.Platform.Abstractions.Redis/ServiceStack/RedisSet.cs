using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis
{
    public class RedisSet : RedisStructure, IRedisSet
    {
        public RedisSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SAdd
                var result = ((RedisNativeClient)redis).SAdd(InitRedisKey(keySuffix), value.ToUtf8Bytes());
                return result > 0;
            }
        }

        public void AddPush(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SAdd
                redis.AddItemToSet(InitRedisKey(keySuffix), value);
            }
        }

        public long Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SAdd
                return redis.AddRangeToSetSafe(InitRedisKey(keySuffix), values.ToList());
            }
        }

        public string[] Combine(SetOperation operation, string[] keys, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (operation == SetOperation.Intersect)
                {
                    //SInter
                    return redis.GetIntersectFromSets(keys).ToArray();
                }
                if (operation == SetOperation.Union)
                {
                    //SUnion
                    return redis.GetUnionFromSets(keys).ToArray();
                }
                //SDiff
                string fromSetId = keys[0];
                string[] withSetIds = new string[keys.Length - 1];
                for (int i = 1; i < keys.Length; i++)
                {
                    withSetIds[i - 1] = keys[i];
                }
                return redis.GetDifferencesFromSet(fromSetId, withSetIds).ToArray();
            }
        }

        public long CombineAndStore(SetOperation operation, string destination, string[] keys,
            CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (operation == SetOperation.Intersect)
                {
                    //SInterStore
                    redis.StoreIntersectFromSets(destination, keys);
                }
                else if (operation == SetOperation.Union)
                {
                    //SUnionStore
                    redis.StoreUnionFromSets(destination, keys);
                }
                else
                {
                    //SDiffStore
                    string fromSetId = keys[0];
                    string[] withSetIds = new string[keys.Length - 1];
                    for (int i = 1; i < keys.Length; i++)
                    {
                        withSetIds[i - 1] = keys[i];
                    }
                    redis.StoreDifferencesFromSet(destination, fromSetId, withSetIds);
                }
            }
            return 1;
        }

        public bool Contains(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //SIsMember
                return redis.SetContainsItem(InitRedisKey(keySuffix), value);
            }
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //SCard
                return redis.GetSetCount(InitRedisKey(keySuffix));
            }
        }

        public string[] Members(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //SMembers
                return redis.GetAllItemsFromSet(InitRedisKey(keySuffix)).ToArray();
            }
        }

        /// <summary>
        /// 使用SSCAN方式获取所有成员
        /// </summary>
        public string[] MembersSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                return redis.GetAllItemsFromSetSafe(InitRedisKey(keySuffix)).ToArray();
            }
        }

        public bool Move(string keySuffix, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SMove
                redis.MoveBetweenSets(InitRedisKey(keySuffix), destination, value);
            }
            return true;
        }

        public string Pop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SPop
                return redis.PopItemFromSet(InitRedisKey(keySuffix));
            }
        }

        public string RandomMember(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //SRandMember
                return redis.GetRandomItemFromSet(InitRedisKey(keySuffix));
            }
        }

        public string[] RandomMembers(string keySuffix, long count, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SRem
                var result = ((RedisNativeClient)redis).SRem(InitRedisKey(keySuffix), member.ToUtf8Bytes());
                return result > 0;
            }
        }

        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SRem
                var result = ((RedisNativeClient)redis).SRem(InitRedisKey(keySuffix), members.Select(o => o.ToUtf8Bytes()).ToArray());
                return result;
            }
        }

        public IEnumerable<RedisValue> Scan(string keySuffix, RedisValue pattern = new RedisValue(), int pageSize = 10, long cursor = 0,
            int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }
    }

    public class RedisSetTimeStamp : RedisSet
    {
        public readonly string KeySuffix;
        protected readonly RedisString _redisString;

        public RedisSetTimeStamp(string[] serverKeyPrefixFormat) : base(new[] { serverKeyPrefixFormat[0], RedisKeyDefinition.ConfigHashList })
        {
            KeySuffix = serverKeyPrefixFormat[1];
            _redisString = new RedisString(new[] { serverKeyPrefixFormat[0], RedisKeyDefinition.ConfigStringTimestamp });
        }
        public void Clear()
        {
            Delete(KeySuffix);
            _redisString.Set(KeySuffix, DateTime.Now.Ticks.ToString());
        }

        public void SetEntryAndSetTimestamp(string value)
        {
            Add(KeySuffix, value);
            Update();
        }

        public void RemoveEntryAndSetTimestamp(string value)
        {
            Remove(KeySuffix, value);
            Update();
        }

        public void Update()
        {
            _redisString.Set(KeySuffix, DateTime.Now.Ticks.ToString());
        }

        public DateTime GetKeyOfListTimestamp()
        {
            var ticket = _redisString.Get(KeySuffix);
            return new DateTime(ticket.AsLong());
        }
    }

    public class RedisSetTimeStampExt : RedisSetTimeStamp
    {
        public RedisSetTimeStampExt(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public void SetEntryAndSetTimestampForGo(string value, bool useRedisTime = false)
        {
            Add(KeySuffix, value);
            Update(true, useRedisTime);
        }

        public void RemoveEntryAndSetTimestampForGo(string value, bool useRedisTime = false)
        {
            Remove(KeySuffix, value);
            Update(true, useRedisTime);
        }

        public void SetEntryAndSetTimestampForMinStr(string value, bool useRedisTime = false)
        {
            AddForMinStr(KeySuffix, value);
            Update(true, useRedisTime);
        }

        public void RemoveEntryAndSetTimestampForMinStr(string value, bool useRedisTime = false)
        {
            RemoveForMinStr(KeySuffix, value);
            Update(true, useRedisTime);
        }

        public void AddForMinStr(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                ((RedisNativeClient)redis).Set(InitRedisKey(keySuffix), value.ToUtf8Bytes());
            }
        }

        public bool RemoveForMinStr(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //SRem
                var result = ((RedisNativeClient)redis).Del(InitRedisKey(keySuffix));
                return result > 0;
            }
        }

        public void Update(bool timeStamp, bool useRedisTime = false)
        {
            var time = GetTimeStamp();
            if (useRedisTime)
            {
                time = DateTimeHelper.GetRedisTimestamp().ToString();
            }
            _redisString.Set(KeySuffix, time);
        }

        public static string GetTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
