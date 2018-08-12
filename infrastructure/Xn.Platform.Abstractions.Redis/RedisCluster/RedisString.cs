using StackExchange.Redis;
using System;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public class RedisString: RedisStructure, IRedisString
    {
        public RedisString(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public long Append(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringAppend(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }

        public long BitCount(string keySuffix, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long BitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = new RedisKey(), CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long BitPosition(string keySuffix, bool bit, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string Get(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringGet(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }
        public string GetSet(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringGetSet(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }
        public bool Set(string keySuffix, string value, TimeSpan? expiry = null, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringSet(InitRedisKey(keySuffix), value, expiry, (sr.When)when, (sr.CommandFlags)commandFlags);
        }

        public long Increment(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringIncrement(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }
        public double Increment(string keySuffix, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringIncrement(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }

        public long Decrement(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringDecrement(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }

        public double Decrement(string keySuffix, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringDecrement(InitRedisKey(keySuffix), value, (sr.CommandFlags)commandFlags);
        }
        public bool SetBit(string keySuffix, long offset, bool bit, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringSetBit(InitRedisKey(keySuffix), offset, bit, (sr.CommandFlags)commandFlags);
        }

        public bool GetBit(string keySuffix, long offset, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.StringGetBit(InitRedisKey(keySuffix), offset, (sr.CommandFlags)commandFlags);
        }

        public long IncrementLimitByMax(string keySuffix, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            var left = Client.StringIncrement(InitRedisKey(keySuffix), value);
            if (left > max)
            {
                Client.StringSet(InitRedisKey(keySuffix), max);
                return max;
            }
            return left;
        }
        public long DecrementLimitByMin(string keySuffix, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            var left = Client.StringDecrement(InitRedisKey(keySuffix), value);
            if (left <0)
            {
                Client.StringIncrement(InitRedisKey(keySuffix),value);
            }
            return left;
        }

        public long IncrementLimitByMin(string keySuffix, long value, long min, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double IncrementLimitByMax(string keySuffix, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double IncrementLimitByMin(string keySuffix, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }
    }
}
