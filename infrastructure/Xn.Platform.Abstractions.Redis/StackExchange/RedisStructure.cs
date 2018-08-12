using System;
using System.Linq;
using System.Net;
using  Xn.Platform.Abstractions.Redis.Configuration;
using StackExchange.Redis;
using sr = StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public abstract class RedisStructure : IRedisStructure
    {
        protected string KeyPrefixFormat { get; set; }
        protected RedisKey Key { get; set; }
        protected RedisGroup RedisGroup { get; set; }

        protected RedisStructure(string[] serverKeyPrefixFormat)
        {
            RedisGroup = RedisServer.ConfigDict[serverKeyPrefixFormat[0]];
            KeyPrefixFormat = serverKeyPrefixFormat[1];
        }

        protected virtual void InitRedisKey(string keySuffix)
        {
            Key = string.IsNullOrEmpty(keySuffix) ? KeyPrefixFormat : string.Format(KeyPrefixFormat, keySuffix);
        }

        public string[] Keys(int database = 0, string pattern = null, int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            var server = RedisGroup.GetServer(true, string.Empty);
            return server.Keys(database, pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags).Select(o=>o.ToString()).ToArray();
        }

        public bool Delete(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyDelete(Key, (sr.CommandFlags)commandFlags);
        }
        public byte[] Dump(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyDump(Key, (sr.CommandFlags)commandFlags);
        }
        public bool Exists(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyExists(Key, (sr.CommandFlags)commandFlags);
        }
        public bool Expire(string keySuffix, DateTime expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyExpire(Key, expiry, (sr.CommandFlags)commandFlags);
        }
        public bool Expire(string keySuffix, TimeSpan expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyExpire(Key, expiry, (sr.CommandFlags)commandFlags);
        }
        public void Migrate(string keySuffix, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            command.KeyMigrate(Key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, (sr.CommandFlags)flags);
        }
        public bool Move(string keySuffix, int database, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyMove(Key, database, (sr.CommandFlags)flags);
        }
        public bool Persist(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyPersist(Key, (sr.CommandFlags)commandFlags);
        }
        public string Random(CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(null);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyRandom((sr.CommandFlags)commandFlags);
        }
        public bool Rename(string keySuffix, string newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyRename(Key, newKey, (sr.When)when, (sr.CommandFlags)flags);
        }
        public void Restore(string keySuffix, byte[] value, TimeSpan? expiry = default(TimeSpan?), CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            command.KeyRestore(Key, value, expiry, (sr.CommandFlags)flags);
        }
        public TimeSpan? TimeToLive(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyTimeToLive(Key, (sr.CommandFlags)commandFlags);
        }
        public string Type(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.KeyType(Key, (sr.CommandFlags)commandFlags).ToString();
        }

        public int ScriptEvaluateAsInt(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            return (int)ScriptEvaluate(script, keys, values, commandFlags);
        }

        public string[] ScriptEvaluateAsList(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            return (string[])ScriptEvaluate(script, keys, values, commandFlags);
        }

        public string ScriptEvaluateString(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            return (string)ScriptEvaluate(script, keys, values, commandFlags);
        }

        public int DeleteKeys(string keySuffix, CommandFlags commandFlags)
        {
            InitRedisKey(keySuffix);
            return ScriptEvaluateAsInt(Script.RemoveKeysByPatternScript, null, new RedisValue[] { Key.ToString() }, commandFlags);
        }

        private RedisResult ScriptEvaluate(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None)
        {
            var command = RedisGroup.GetCommand(true, string.Empty);
            var redisResult = command.ScriptEvaluate(script, keys, values, (sr.CommandFlags)commandFlags);
            return redisResult;
        }

        public long[] GetRedisTime()
        {
            var result =  (long[])ScriptEvaluate(Script.RedisTime);
            return result;
        }

        public long GetRedisTimeResult()
        {
            var result = GetRedisTime();
            return result[0] * 1000000 + result[1];
        }
    }
}
