using System.Linq;
using StackExchange.Redis;
using sr = StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisList : RedisStructure, IRedisList
    {
        public RedisList(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public string GetByIndex(string keySuffix, long index, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListGetByIndex(Key, index, (sr.CommandFlags)commandFlags);
        }
        public long InsertAfter(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListInsertAfter(Key, pivot, value, (sr.CommandFlags)commandFlags);
        }
        public long InsertBefore(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListInsertBefore(Key, pivot, value, (sr.CommandFlags)commandFlags);
        }
        public string LeftPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListLeftPop(Key, (sr.CommandFlags)commandFlags);
        }
        public long LeftPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListLeftPush(Key, value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }
        public long LeftPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return command.ListLeftPush(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListLength(Key, (sr.CommandFlags)commandFlags);
        }
        public string[] Range(string keySuffix, long start = 0, long stop = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var r = command.ListRange(Key, start, stop, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public long Remove(string keySuffix, string value, long count = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListRemove(Key, value, count, (sr.CommandFlags)commandFlags);
        }
        public string RightPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListRightPop(Key, (sr.CommandFlags)commandFlags);
        }
        public string RightPopLeftPush(string keySuffix, string destination, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListRightPopLeftPush(Key, destination, (sr.CommandFlags)flags);
        }
        public long RightPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.ListRightPush(Key, value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }
        public long RightPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return command.ListRightPush(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public void SetByIndex(string keySuffix, int index, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            command.ListSetByIndex(Key, index, value, (sr.CommandFlags)commandFlags);
        }
        public void Trim(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            command.ListTrim(Key, start, stop, (sr.CommandFlags)commandFlags);
        }
    }
}
