using  Xn.Platform.Abstractions.Redis.Configuration;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;

namespace Xn.Platform.Abstractions.Redis
{
    public abstract class RedisStructure : IRedisStructure
    {
        protected string KeyPrefixFormat { get; set; }
        public string ServerName { get; set; }

        protected RedisStructure(string[] serverKeyPrefixFormat)
        {
            ServerName = serverKeyPrefixFormat[0];
            KeyPrefixFormat = serverKeyPrefixFormat[1];
        }

        protected virtual RedisKey InitRedisKey(string keySuffix)
        {
            return string.IsNullOrEmpty(keySuffix) ? KeyPrefixFormat : string.Format(KeyPrefixFormat, keySuffix);
        }

        public string[] Keys(int database = 0, string pattern = null, int pageSize = 10, long cursor = 0,
            int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Keys
                return redis.SearchKeys(pattern).ToArray();
            }
        }

        /// <summary>
        /// 使用SCAN MATCH pattern获取所有Key
        /// </summary>
        public string[] KeysSafe(string pattern)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.KeysSafe(pattern);
            }
        }

        public string[] KeysWithPattern()
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Keys
                return redis.SearchKeys(InitRedisKey("*")).ToArray();
            }
        }

        public bool Delete(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Del
                return redis.Remove(InitRedisKey(keySuffix));
                //或者
                //return redis.RemoveEntry(Key);
            }
        }

        public bool DeleteSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.RemoveKeySafe(InitRedisKey(keySuffix));
            }
        }

        public void DeleteByRename(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                redis.RemoveKeyByRename(InitRedisKey(keySuffix));
            }
        }

        public byte[] Dump(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //Exists
                return redis.ContainsKey(InitRedisKey(keySuffix));
            }
        }

        public bool Expire(string keySuffix, DateTime expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ExpireAt
                return redis.ExpireEntryAt(InitRedisKey(keySuffix), expiry);
            }
        }

        public bool Expire(string keySuffix, TimeSpan expiry, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Expire
                return redis.ExpireEntryIn(InitRedisKey(keySuffix), expiry);
            }
        }

        public void Migrate(string keySuffix, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0,
            MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None)
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
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //RandomKey
                return redis.GetRandomKey();
            }
        }

        public bool Rename(string keySuffix, string newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Rename
                redis.RenameKey(InitRedisKey(keySuffix), newKey);
            }
            return true;
        }

        public bool RenameByNewKeySuffix(string keySuffix, string newKeySuffix, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Rename
                redis.RenameKey(InitRedisKey(keySuffix), InitRedisKey(newKeySuffix));
            }
            return true;
        }

        public bool RenameFast(string keySuffix, string newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                redis.RenameFast(InitRedisKey(keySuffix), newKey);
            }
            return true;
        }

        public void Restore(string keySuffix, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public TimeSpan? TimeToLive(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //Ttl
                return redis.GetTimeToLive(InitRedisKey(keySuffix));
            }
        }

        public string Type(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public int ScriptEvaluateAsInt(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            string[] key = null;
            string[] arg = new string[0];
            if (keys != null)
            {
                key = keys.Select(o => o.ToString()).ToArray();
            }
            if (values != null)
            {
                arg = values.Select(o => o.ToString()).ToArray();
            }
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (key == null)
                    return (int)redis.ExecLuaAsInt(script, arg);
                return (int)redis.ExecLuaAsInt(script, key, arg);
            }
        }

        public string[] ScriptEvaluateAsList(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            string[] key = null;
            string[] arg = new string[0];
            if (keys != null)
            {
                key = keys.Select(o => o.ToString()).ToArray();
            }
            if (values != null)
            {
                arg = values.Select(o => o.ToString()).ToArray();
            }
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (key == null)
                    return redis.ExecLuaAsList(script, arg).ToArray();
                return redis.ExecLuaAsList(script, key, arg).ToArray();
            }
        }

        public string ScriptEvaluateString(string script, RedisKey[] keys = null, RedisValue[] values = null,
            CommandFlags commandFlags = CommandFlags.None)
        {
            string[] key = null;
            string[] arg = new string[0];
            if (keys != null)
            {
                key = keys.Select(o => o.ToString()).ToArray();
            }
            if (values != null)
            {
                arg = values.Select(o => o.ToString()).ToArray();
            }
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (key == null)
                    return redis.ExecLuaAsString(script, arg);
                return redis.ExecLuaAsString(script, key, arg);
            }
        }

        public int DeleteKeys(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.RemoveKeysByPattern(InitRedisKey(keySuffix));
            }
        }

        public int DeleteKeysSafe(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                return redis.RemoveKeysByPatternSafe(InitRedisKey(keySuffix));
            }
        }

        public long[] GetRedisTime()
        {
            using (var redis = Server.GetRedisClient(RedisServer.Feed, true))
            {
                var multiDataList = ((RedisNativeClient)redis).Time();
                var results = new long[2];
                for (int i = 0; i < 2; i++)
                {
                    results[i] = multiDataList[i].FromUtf8Bytes().AsLong();
                }
                return results;
            }
        }

        public long GetRedisTimeResult()
        {
            var time = GetRedisTime();
            return time[0] * 1000000 + time[1];
        }
    }
}
