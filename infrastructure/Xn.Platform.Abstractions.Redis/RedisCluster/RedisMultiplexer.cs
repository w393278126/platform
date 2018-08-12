using System.Collections.Generic;
using  Xn.Platform.Abstractions.Redis.Configuration;
using ServiceStack.Net30.Collections.Concurrent;
using StackExchange.Redis;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    internal class RedisMultiplexer
    {
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _cmList = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        private readonly object _syncObj = new object();

        private RedisMultiplexer()
        {
        }
        
        public static RedisMultiplexer Instance { get; } = new RedisMultiplexer();

        public IDatabase GetDatabase(string serverName)
        {
            lock (_syncObj)
            {
                ConnectionMultiplexer cm = null;
                _cmList.TryGetValue(serverName, out cm);

                if (cm != null && cm.IsConnected)
                {
                    return cm.GetDatabase(0);
                }

                if (cm != null)
                {
                    cm.Close(false);
                    cm.Dispose();
                    _cmList.TryRemove(serverName, out cm);
                }

                var masterServers = RedisServer.ConfigDict[serverName].MasterSettings;
                var salveServers = RedisServer.ConfigDict[serverName].SlaveSettings;
                var options = new ConfigurationOptions();
                var allServers = masterServers.Concat(salveServers).Distinct();
                foreach (var server in allServers)
                {
                    options.EndPoints.Add(server.Host, server.Port);
                }
                options.AbortOnConnectFail = false;
                options.AllowAdmin = true;
                options.SyncTimeout = 10000; //同步超时 3S

                cm = ConnectionMultiplexer.Connect(options);
                _cmList.TryAdd(serverName, cm);

                return cm.GetDatabase(0);
            }
        }
    }
}
