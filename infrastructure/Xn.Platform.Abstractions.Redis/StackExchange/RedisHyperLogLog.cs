using System.Linq;
using StackExchange.Redis;
using sr = StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisHyperLogLog : RedisStructure, IRedisHyperLogLog
    {
        public RedisHyperLogLog(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HyperLogLogAdd(Key, value, (sr.CommandFlags)commandFlags);
        }
        public bool Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return command.HyperLogLogAdd(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HyperLogLogLength(Key, (sr.CommandFlags)commandFlags);
        }

        public void HyperLogLogMerge(string destination, string[] sourceKeys, CommandFlags flags = CommandFlags.None)
        {
            throw new System.NotImplementedException();
        }
    }
}
