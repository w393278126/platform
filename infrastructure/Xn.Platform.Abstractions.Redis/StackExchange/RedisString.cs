using System;
using StackExchange.Redis;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisString : RedisStructure, IRedisString
    {
        public RedisString(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public long Append(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringAppend(Key, value, (sr.CommandFlags)commandFlags);
        }
        public long BitCount(string keySuffix, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.StringBitCount(Key, start, end, (sr.CommandFlags)commandFlags);
        }

        public long BitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = new RedisKey(),
            CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long BitPosition(string keySuffix, bool bit, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.StringBitPosition(Key, bit, start, end, (sr.CommandFlags)commandFlags);
        }
        public string Get(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.StringGet(Key, (sr.CommandFlags)commandFlags);
        }
        public string GetSet(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringGetSet(Key, value, (sr.CommandFlags)commandFlags);
        }
        public bool Set(string keySuffix, string value, TimeSpan? expiry = null, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringSet(Key, value, expiry, (sr.When)when, (sr.CommandFlags)commandFlags);
        }
        public long Increment(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringIncrement(Key, value, (sr.CommandFlags)commandFlags);
        }
        public double Increment(string keySuffix, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringIncrement(Key, value, (sr.CommandFlags)commandFlags);
        }
        public long Decrement(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringDecrement(Key, value, (sr.CommandFlags)commandFlags);
        }
        public double Decrement(string keySuffix, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringDecrement(Key, value, (sr.CommandFlags)commandFlags);
        }
        public bool SetBit(string keySuffix, long offset, bool bit, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.StringSetBit(Key, offset, bit, (sr.CommandFlags)commandFlags);
        }
        public bool GetBit(string keySuffix, long offset, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.StringGetBit(Key, offset, (sr.CommandFlags)commandFlags);
        }
        public long IncrementLimitByMax(string keySuffix, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (long)command.ScriptEvaluate(Script.StringIncrementLimitByMax, new[] { Key }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags);
        }
        public long IncrementLimitByMin(string keySuffix, long value, long min, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (long)command.ScriptEvaluate(Script.StringIncrementLimitByMin, new[] { Key }, new RedisValue[] { value, min }, (sr.CommandFlags)commandFlags);
        }
        public double IncrementLimitByMax(string keySuffix, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return double.Parse((string)command.ScriptEvaluate(Script.StringIncrementFloatLimitByMax, new[] { Key }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags));
        }
        public double IncrementLimitByMin(string keySuffix, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return double.Parse((string)command.ScriptEvaluate(Script.StringIncrementFloatLimitByMin, new[] { Key }, new RedisValue[] { value, min }, (sr.CommandFlags)commandFlags));
        }
    }
}
