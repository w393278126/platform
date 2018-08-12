using System;
using Xn.Platform.Data.Redis;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis
{
    /// <summary>
    /// MSET key value [key value ...] SetAll
    /// Substr GetSubstring
    /// </summary>
    public class RedisString : RedisStructure, IRedisString
    {
        public RedisString(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public long Append(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Append
                return redis.AppendToValue(InitRedisKey(keySuffix), value);
            }
        }

        public long BitCount(string keySuffix, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long BitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = new RedisKey(),
            CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long BitPosition(string keySuffix, bool bit, long start = 0, long end = -1,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string Get(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //Get
                return redis.GetValue(InitRedisKey(keySuffix));
                //MGet GetValues\GetValuesMap

            }
        }

        public string GetSet(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //GetSet
                return redis.GetAndSetEntry(InitRedisKey(keySuffix), value);
            }
        }

        public bool Set(string keySuffix, string value, TimeSpan? expiry = null, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (!expiry.HasValue)
                {
                    if (when == When.Always)
                    {
                        //Set
                        redis.SetEntry(key, value);
                    }
                    else if (when == When.NotExists)
                    {
                        //SetNx
                        redis.SetEntryIfNotExists(key, value);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    if (when == When.Always)
                    {
                        //SetEx
                        redis.SetEntry(key, value, expiry.Value);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            return true;
        }

        public long Increment(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (value == 1)
                {
                    //Incr
                    return redis.IncrementValue(key);
                }
                //IncrBy
                return redis.IncrementValueBy(key, (int)value);
            }
        }

        public double Increment(string keySuffix, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long Decrement(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (value == 1)
                {
                    //Decr
                    return redis.DecrementValue(key);
                }
                //DecrBy
                return redis.DecrementValueBy(key, (int)value);
            }
        }
        public long DecrementLimitByMin(string keySuffix, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.DecrementValueBy(key, (int)value);
                if (left < 0)
                {
                    redis.IncrementValueBy(key, (int)value);
                }
                return left;
            }
        }

        public double Decrement(string keySuffix, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool SetBit(string keySuffix, long offset, bool bit, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool GetBit(string keySuffix, long offset, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long IncrementLimitByMax(string keySuffix, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueBy(key, (int)value);
                if (left > max)
                {
                    redis.SetEntry(key, max.ToString());
                    return max;
                }
                return left;
            }
        }

        public long IncrementLimitByMin(string keySuffix, long value, long min, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                var left = (int)redis.IncrementValueBy(key, (int)value);
                if (left < min)
                {
                    redis.SetEntry(key, min.ToString());
                    return min;
                }
                return left;
            }
        }

        public double IncrementLimitByMax(string keySuffix, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double IncrementLimitByMin(string keySuffix, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool SetEntryIfNotExistsWithExpire(string keySuffix, string value, TimeSpan expiry)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.SetEntryIfNotExistsWithExpire(InitRedisKey(keySuffix), value, expiry);
            }
        }
    }
}
