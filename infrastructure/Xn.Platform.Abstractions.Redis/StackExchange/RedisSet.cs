using System;
using System.Collections.Generic;
using System.Linq;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using StackExchange.Redis;
using sr = StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisSet : RedisStructure, IRedisSet
    {
        public RedisSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetAdd(Key, value, (sr.CommandFlags)commandFlags);
        }
        public long Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = values.Select(x => (RedisValue)x).ToArray();
            return command.SetAdd(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public string[] Combine(SetOperation operation, string[] keys, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keys[0]);
            var command = RedisGroup.GetCommand(true, Key);
            var redisKeys = keys.Select(x => (RedisKey)x).ToArray();
            var values = command.SetCombine((sr.SetOperation)operation, redisKeys, (sr.CommandFlags)flags);
            return values.Select(o => o.ToString()).ToArray();
        }
        public long CombineAndStore(SetOperation operation, string destination, string[] keys,
            CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keys[0]);
            var command = RedisGroup.GetCommand(true, Key);
            var redisKeys = keys.Select(x => (RedisKey)x).ToArray();
            return command.SetCombineAndStore((sr.SetOperation)operation, destination, redisKeys, (sr.CommandFlags)flags);
        }
        public bool Contains(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetContains(Key, value, (sr.CommandFlags)commandFlags);
        }
        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetLength(Key, (sr.CommandFlags)commandFlags);
        }
        public string[] Members(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var r = command.SetMembers(Key, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public bool Move(string keySuffix, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetMove(Key, destination, value, (sr.CommandFlags)flags);
        }
        public string Pop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetPop(Key, (sr.CommandFlags)commandFlags);
        }
        public string RandomMember(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetRandomMember(Key, (sr.CommandFlags)commandFlags);
        }
        public string[] RandomMembers(string keySuffix, long count, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var r = command.SetRandomMembers(Key, count, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SetRemove(Key, member, (sr.CommandFlags)commandFlags);
        }
        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = members.Select(x => (RedisValue)x).ToArray();
            return command.SetRemove(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public IEnumerable<RedisValue> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.SetScan(Key, pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags);
        }
    }

    public class RedisSetTimeStamp : RedisSet
    {
        public readonly string KeySuffix;
        private readonly RedisString _redisString;

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
}
