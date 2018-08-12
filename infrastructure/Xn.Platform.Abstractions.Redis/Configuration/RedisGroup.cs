using System.Collections.Generic;
using System.Net;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.Configuration
{
    public class RedisGroup
    {
        public RedisGroup()
        {
            MasterSettings = new List<RedisSettings>();
            SlaveSettings = new List<RedisSettings>();
        }
        public string GroupName { get; private set; }

        public int PoolSize { get; private set; }
        public IList<RedisSettings> MasterSettings { get; set; }

        public IList<RedisSettings> SlaveSettings { get; set; }

        readonly IServerSelector _serverSelector;

        public RedisGroup(string groupName, int poolSize, RedisSettings[] settings, IServerSelector selector = null): this()
        {
            GroupName = groupName;
            PoolSize = poolSize;
            foreach (var setting in settings)
            {
                if (setting.Master)
                {
                    MasterSettings.Add(setting);
                }
                else
                {
                    SlaveSettings.Add(setting);
                }
            }
            if (SlaveSettings.Count == 0)
            {
                SlaveSettings = MasterSettings;
            }
            _serverSelector = selector ?? new SimpleHashingSelector();
        }

        public IDatabase GetCommand(bool master, RedisKey key)
        {
            var settings = _serverSelector.Select(master ? MasterSettings : SlaveSettings, key);
            var connection = settings.GetConnection();
            return connection.GetDatabase(settings.Db);
        }

        public IServer GetServer(bool master, RedisKey key)
        {
            var settings = _serverSelector.Select(master ? MasterSettings : SlaveSettings, key);
            var connection = settings.GetConnection();
            EndPoint[] endpoints = connection.GetEndPoints();
            return connection.GetServer(endpoints[0]);
        }
    }
}
