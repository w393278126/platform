using StackExchange.Redis;
using System.Linq;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public class RedisList : RedisStructure, IRedisList
    {
        public RedisList(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public string GetByIndex(string keySuffix, long index, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListGetByIndex(InitRedisKey(keySuffix), index, (sr.CommandFlags)commandFlags);
        }

        public long InsertAfter(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListInsertAfter(InitRedisKey(keySuffix), pivot, value, (sr.CommandFlags)commandFlags);
        }

        public long InsertBefore(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListInsertBefore(InitRedisKey(keySuffix), pivot, value, (sr.CommandFlags)commandFlags);
        }

        public string LeftPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListLeftPop(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public long LeftPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListLeftPush(InitRedisKey(keySuffix), value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }

        public long LeftPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return Client.ListLeftPush(InitRedisKey(keySuffix), redisValues, (sr.CommandFlags)commandFlags);
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListLength(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string[] Range(string keySuffix, long start = 0, long stop = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.ListRange(InitRedisKey(keySuffix), start, stop, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public long Remove(string keySuffix, string value, long count = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListRemove(InitRedisKey(keySuffix), value, count, (sr.CommandFlags)commandFlags);
        }

        public string RightPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListRightPop(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string RightPopLeftPush(string keySuffix, string destination, CommandFlags flags = CommandFlags.None)
        {
            return Client.ListRightPopLeftPush(InitRedisKey(keySuffix), destination, (sr.CommandFlags)flags);
        }

        public long RightPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.ListRightPush(InitRedisKey(keySuffix), value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }

        public long RightPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return Client.ListRightPush(InitRedisKey(keySuffix), redisValues, (sr.CommandFlags)commandFlags);
        }

        public void SetByIndex(string keySuffix, int index, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            Client.ListSetByIndex(InitRedisKey(keySuffix), index, value, (sr.CommandFlags)commandFlags);
        }

        public void Trim(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            Client.ListTrim(InitRedisKey(keySuffix), start, stop, (sr.CommandFlags)commandFlags);
        }
    }
}
