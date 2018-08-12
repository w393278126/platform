using System;
using System.Collections.Concurrent;
using System.Linq;
using  Xn.Platform.Abstractions.Redis.Configuration;
using ServiceStack.Redis;

namespace Xn.Platform.Data.Redis
{
    public static class Server
    {
        public static IRedisClient GetRedisClient(string server, bool isMaster)
        {
            IRedisClient client = isMaster
                        ? GetPooledRedisClientManager(server).GetClient()
                        : GetPooledRedisClientManager(server).GetReadOnlyClient();
            return client;
        }

        public static string GetOnlineRedisClientName(int roomId)
        {
            var id = roomId % 6;
            var redis = string.Format("online{0}", id);
            return redis;
        }

        private static readonly DateTime StartTime = new DateTime(2016, 3, 31);
        private static readonly int PvUvRedisCount = 3;
        public static string GetPvUvRedisClientName(DateTime date, string server)
        {
            var totalDay = (date - StartTime).TotalDays;
            if (totalDay < 0)
                return server;
            var id = totalDay%PvUvRedisCount;
            var redis = string.Format("pvuv{0}", id);
            return redis;
        }
        
        private static readonly ConcurrentDictionary<string, PooledRedisClientManager> RedisClientManager = new ConcurrentDictionary<string, PooledRedisClientManager>();
        public static PooledRedisClientManager GetPooledRedisClientManager(string redisServer)
        {
            return RedisClientManager.GetOrAdd(redisServer, key =>
            {
                var readwriteServers = RedisServer.ConfigDict[key].MasterSettings.Select(o => o.Server).ToList();
                var readonlyServers = RedisServer.ConfigDict[key].SlaveSettings.Select(o => o.Server).ToList();
                return new PooledRedisClientManager(readwriteServers, readonlyServers, null, 0, RedisServer.ConfigDict[key].PoolSize, null);
            });
        }

        public static IDisposable GetRedisClient(object mission, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
