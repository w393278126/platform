using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using sr = StackExchange.Redis;
using StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisHash : RedisStructure, IRedisHash
    {
        public RedisHash(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
            
        }
        public bool Delete(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HashDelete(Key, field, (sr.CommandFlags)commandFlags);
        }
        public long Delete(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var hashFields = fields.Select(x => (RedisValue)x).ToArray();
            return command.HashDelete(Key, hashFields, (sr.CommandFlags)commandFlags);
        }
        public bool Exists(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.HashExists(Key, field, (sr.CommandFlags)commandFlags);
        }
        public string Get(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.HashGet(Key, field, (sr.CommandFlags)commandFlags);  
        }
        public IDictionary<string, string> Get(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var hashFields = fields.Select(x => (RedisValue)x).ToArray();
            var rValues = command.HashGet(Key, hashFields, (sr.CommandFlags)commandFlags);
            var result = fields.Zip(rValues, (key, x) =>
            {
                if (!x.HasValue) return new { key, rValue = string.Empty };
                return new { key, rValue = (string)x };
            })
                    .ToDictionary(x => x.key, x => x.rValue);
            return result;
        }
        public IDictionary<string, string> GetAll(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var hashEntries = command.HashGetAll(Key, (sr.CommandFlags)commandFlags);
            var result = new Dictionary<string, string>();
            foreach (var hashEntry in hashEntries)
            {
                result[hashEntry.Name] = hashEntry.Value;
            }
            return result;
        }
        public long Increment(string keySuffix, string field, long value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HashIncrement(Key, field, value, (sr.CommandFlags)commandFlags);
        }
        public double Increment(string keySuffix, string field, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HashIncrement(Key, field, value, (sr.CommandFlags)commandFlags);
        }
        public string[] Keys(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var r = command.HashKeys(Key, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.HashLength(Key, (sr.CommandFlags)commandFlags);
        }

        //public IEnumerable<HashEntry> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        //{
        //    InitRedisKey(keySuffix);
        //    var command = RedisGroup.GetCommand(false, Key);
        //    return command.HashScan(Key, pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags);
        //}
        public bool Set(string keySuffix, string field, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.HashSet(Key, field, value, (sr.When)when, (sr.CommandFlags)commandFlags);
        }
        public void Set(string keySuffix, IDictionary<string, string> values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var hashFields = values.Select(x => new HashEntry(x.Key, x.Value)).ToArray();
            command.HashSet(Key, hashFields, (sr.CommandFlags)commandFlags);
        }
        public string[] Values(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var r = command.HashValues(Key, (sr.CommandFlags)commandFlags);
            return r.Select(o => (string)o).ToArray();
        }
        public long DecrementLimitByMin(string keySuffix, string field, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var left = command.HashDecrement(Key, field, value, (sr.CommandFlags)commandFlags);
            if (left < 0)
            {
                command.HashIncrement(Key, field, value, (sr.CommandFlags)commandFlags);
            }
            return left;
        }
        public long IncrementLimitByMax(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (long)command.ScriptEvaluate(Script.HashIncrementLimitByMax, new[] { Key, (RedisKey)field }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags);
        }
        public double IncrementLimitByMax(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (double)command.ScriptEvaluate(Script.HashIncrementFloatLimitByMax, new[] { Key, (RedisKey)field }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags);
        }
        public long IncrementLimitByMin(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (long)command.ScriptEvaluate(Script.HashIncrementLimitByMin, new[] { Key, (RedisKey)field }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags);
        }
        public double IncrementLimitByMin(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (double)command.ScriptEvaluate(Script.HashIncrementFloatLimitByMin, new[] { Key, (RedisKey)field }, new RedisValue[] { value, max }, (sr.CommandFlags)commandFlags);
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

    public class RedisHashTimeStamp : RedisHash
    {
        private readonly string _keySuffix;
        private readonly RedisString _redisString;

        public RedisHashTimeStamp(string[] serverKeyPrefixFormat) : base(new[] { serverKeyPrefixFormat[0], RedisKeyDefinition.ConfigHashList })
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
    }
}