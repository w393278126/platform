using StackExchange.Redis;
using System;
using System.Net;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public abstract class RedisStructure: IRedisStructure
    {
        protected string KeyPrefixFormat { get; set; }

        public string ServerName { get; set; }

        protected IDatabase Client => RedisMultiplexer.Instance.GetDatabase(ServerName);

        protected RedisStructure(string[] serverKeyPrefixFormat)
        {
            ServerName = serverKeyPrefixFormat[0];
            KeyPrefixFormat = serverKeyPrefixFormat[1];
        }

        public virtual RedisKey InitRedisKey(string keySuffix)
        {
            return string.IsNullOrEmpty(keySuffix) ? KeyPrefixFormat : string.Format(KeyPrefixFormat, keySuffix);
        }

        public string[] Keys(int database = 0, string pattern = null, int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyDelete(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public byte[] Dump(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyExists(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public bool Expire(string keySuffix, DateTime expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyExpire(InitRedisKey(keySuffix), expiry, (sr.CommandFlags)commandFlags);
        }

        public bool Expire(string keySuffix, TimeSpan expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyExpire(InitRedisKey(keySuffix), expiry, (sr.CommandFlags)commandFlags);
        }

        public void Migrate(string keySuffix, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Move(string keySuffix, int database, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Persist(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string Random(CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Rename(string keySuffix, string newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return Client.KeyRename(InitRedisKey(keySuffix), newKey, (sr.When)when, (sr.CommandFlags)flags);
        }
        public bool RenameByNewKeySuffix(string keySuffix, string newKeySuffix, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return Client.KeyRename(InitRedisKey(keySuffix), InitRedisKey(newKeySuffix), (sr.When)when, (sr.CommandFlags)flags);
        }

        public void Restore(string keySuffix, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public TimeSpan? TimeToLive(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyTimeToLive(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags);
        }

        public string Type(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.KeyType(InitRedisKey(keySuffix), (sr.CommandFlags)commandFlags).ToString();
        }

        public int ScriptEvaluateAsInt(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string[] ScriptEvaluateAsList(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string ScriptEvaluateString(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public int DeleteKeys(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        private RedisResult ScriptEvaluate(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisResult = Client.ScriptEvaluate(script, keys, values, (sr.CommandFlags)commandFlags);
            return redisResult;
        }

        public long[] GetRedisTime()
        {
            var result = (long[])ScriptEvaluate(Script.RedisTime);
            return result;
        }

        public long GetRedisTimeResult()
        {
            var result = GetRedisTime();
            return result[0] * 1000000 + result[1];
        }
    }
}
