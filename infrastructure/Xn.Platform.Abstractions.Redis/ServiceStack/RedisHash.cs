using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Xn.Platform.Abstractions.Redis
{
    public class RedisHash : RedisStructure, IRedisHash
    {
        public RedisHash(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Delete(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //HDel
                return redis.RemoveEntryFromHash(InitRedisKey(keySuffix), field);
            }
        }

        public long Delete(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.RemoveEntriesFromHash(InitRedisKey(keySuffix), fields);
            }
        }

        public bool Exists(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HExists
                return redis.HashContainsEntry(InitRedisKey(keySuffix), field);
            }
        }

        public string Get(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HGet
                return redis.GetValueFromHash(InitRedisKey(keySuffix), field);
            }
        }

        public IDictionary<string, string> Get(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            List<string> values;
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HMGet
                values = redis.GetValuesFromHashSafe(InitRedisKey(keySuffix), fields);
            }
            IDictionary<string, string> results = new Dictionary<string, string>(fields.Length);
            for (int i = 0; i < fields.Length; i++)
            {
                results[fields[i]] = values[i];
            }
            return results;
        }

        public IDictionary<string, string> GetAll(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HGetAll
                return redis.GetAllEntriesFromHash(InitRedisKey(keySuffix));
            }
        }

        /// <summary>
        /// 使用HSCAN方式获取所有键值对
        /// </summary>
        public IDictionary<string, string> GetAllSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                return redis.GetAllEntriesFromHashSafe(InitRedisKey(keySuffix));
            }
        }

        public long Increment(string keySuffix, string field, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            if (value == 0)
            {
                return Get(keySuffix, field, commandFlags).AsLong();
            }
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //HIncrby
                var left = redis.IncrementValueInHash(InitRedisKey(keySuffix), field, (int)value);
                return left;
            }
        }

        public double Increment(string keySuffix, string field, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            //HIncrbyFloat 使用HIncrby
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueInHash(InitRedisKey(keySuffix), field, (int)value);
                return left;
            }
        }

        public string[] Keys(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HKeys
                return redis.GetHashKeys(InitRedisKey(keySuffix)).ToArray();
            }
        }

        /// <summary>
        /// 使用HSCAN方式获取所有键
        /// </summary>
        public string[] KeysSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                return redis.GetHashKeysSafe(InitRedisKey(keySuffix));
            }
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HLen
                return redis.GetHashCount(InitRedisKey(keySuffix));
            }
        }

        //public IEnumerable<HashEntry> Scan(string keySuffix, RedisValue pattern = new RedisValue(), int pageSize = 10, long cursor = 0,
        //    int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Set(string keySuffix, string field, string value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (when == When.NotExists)
                {
                    //HSetNX
                    return redis.SetEntryInHashIfNotExists(key, field, value);
                }
                //HSet
                return redis.SetEntryInHash(key, field, value);
            }
        }

        public void Set(string keySuffix, IDictionary<string, string> values, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //HMSet
                redis.SetRangeInHash(InitRedisKey(keySuffix), values);
            }
        }

        public void SetSafe(string keySuffix, IDictionary<string, string> values, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                redis.SetRangeInHashSafe(InitRedisKey(keySuffix), values);
            }
        }

        public string[] Values(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //HVals
                return redis.GetHashValues(InitRedisKey(keySuffix)).ToArray();
            }
        }

        public long DecrementLimitByMin(string keySuffix, string field, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueInHash(key, field, (int)value * -1);
                if (left < 0)
                {
                    redis.IncrementValueInHash(key, field, (int)value);
                }
                return left;
            }
        }

        public long IncrementLimitByMax(string keySuffix, string field, long value, long max,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueInHash(key, field, (int)value);
                if (left > max)
                {
                    redis.SetEntryInHash(key, field, max.ToString());
                    return max;
                }
                return left;
            }
        }
        
        public long IncrementLimitByMaxReturnDiff(string keySuffix, string field, long value, long max)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.ExecLuaAsInt(Script.IncrementLimitByMaxReturnDiffScript, InitRedisKey(keySuffix), field, value.ToString(), max.ToString());
            }
        }

        public double IncrementLimitByMax(string keySuffix, string field, double value, double max,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long IncrementLimitByMin(string keySuffix, string field, long value, long min,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueInHash(key, field, (int)value);
                if (left < min)
                {
                    redis.SetEntryInHash(key, field, min.ToString());
                    return min;
                }
                return left;
            }
        }

        public double IncrementLimitByMin(string keySuffix, string field, double value, double max,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }
    }

    public class RedisHash<TEntity> : RedisHash, IRedisHash<TEntity>
    {
        public RedisHash(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public string Get(Expression<Func<TEntity, object>> expression, int field, CommandFlags commandFlags = CommandFlags.None)
        {
            var keySuffix = TypeExtensions.DecodeMemberAccessExpressionOf(expression).Name.ToLower();
            return Get(keySuffix, field.ToString(), commandFlags);
        }
        public IDictionary<string, string> GetAll(Expression<Func<TEntity, object>> expression, CommandFlags commandFlags = CommandFlags.None)
        {
            var keySuffix = TypeExtensions.DecodeMemberAccessExpressionOf(expression).Name.ToLower();
            return GetAllSafe(keySuffix);
        }
        public bool Set(Expression<Func<TEntity, object>> expression, int field, string value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var keySuffix = TypeExtensions.DecodeMemberAccessExpressionOf(expression).Name.ToLower();
            return Set(keySuffix, field.ToString(), value, when, commandFlags);
        }
        public long Increment(Expression<Func<TEntity, object>> expression, int field, long value = 1,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var keySuffix = TypeExtensions.DecodeMemberAccessExpressionOf(expression).Name.ToLower();
            return Increment(keySuffix, field.ToString(), value, commandFlags);
        }
        public bool Delete(Expression<Func<TEntity, object>> expression, int field, CommandFlags commandFlags = CommandFlags.None)
        {
            var keySuffix = TypeExtensions.DecodeMemberAccessExpressionOf(expression).Name.ToLower();
            return Delete(keySuffix, field.ToString(), commandFlags);
        }
    }

    public class RedisHashSuffix : RedisHash
    {
        public RedisHashSuffix(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        protected override RedisKey InitRedisKey(string keySuffix)
        {
            return string.Format(KeyPrefixFormat, keySuffix);
        }
    }

    public class RedisHashTimeStamp : RedisHash
    {
        private readonly string _keySuffix;
        private readonly RedisString _redisString;
        /// <summary>
        /// 默认key规则："{0}:list"和"{0}:timestamp"
        /// haveList参数false 默认key规则："{0}"和"{0}:timestamp"
        /// </summary>
        /// <param name="serverKeyPrefixFormat"></param>
        /// <param name="haveList"></param>
        public RedisHashTimeStamp(string[] serverKeyPrefixFormat, bool haveList = true) : base(new[] { serverKeyPrefixFormat[0], haveList ? RedisKeyDefinition.ConfigHashList : "{0}" })
        {
            _keySuffix = serverKeyPrefixFormat[1];
            _redisString = new RedisString(new[] { serverKeyPrefixFormat[0], RedisKeyDefinition.ConfigStringTimestamp });
        }
        public void Clear()
        {
            Delete(_keySuffix);
            _redisString.Set(_keySuffix, DateTime.Now.Ticks.ToString());
        }

        public void SetEntryAndSetTimestamp(string field, string value)
        {
            Set(_keySuffix, field, value);
            _redisString.Set(_keySuffix, DateTime.Now.Ticks.ToString());
        }

        public void RemoveEntryAndSetTimestamp(string field)
        {
            Delete(_keySuffix, field);
            _redisString.Set(_keySuffix, DateTime.Now.Ticks.ToString());
        }

        public DateTime GetKeyOfListTimestamp()
        {
            var ticket = _redisString.Get(_keySuffix);
            return new DateTime(ticket.AsLong());
        }

        public long GetKeyOfListTicket()
        {
            return _redisString.Get(_keySuffix).AsLong();
        }
    }
}
