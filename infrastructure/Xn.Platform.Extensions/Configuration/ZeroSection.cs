using System;
using System.Configuration;
using System.Linq;

namespace Xn.Home.Utils.Configuration
{
    public class ZeroSection : ConfigurationSection
    {
        [ConfigurationProperty("connections", IsRequired = true)]
        public ConnectionCollectoin ItemGenerations => (ConnectionCollectoin)this["connections"];
    }

    public class ConnectionCollectoin : ConfigurationElementCollection
    {
        public ConnectionCollectoin()
        {
            this.AddElementName = "connection";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Connection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Connection)element).EventType;
        }
    }

    public class Connection : ConfigurationElement
    {
        [ConfigurationProperty("eventType", IsRequired = true)]
        public string EventType => (string)this["eventType"];

        [ConfigurationProperty("endpoint")]
        public string Host => (string)this["endpoint"];
    }

    public sealed class ZeroConnectionManager
    {
        private static readonly Lazy<ZeroSection> _configureSection
            = new Lazy<ZeroSection>(() =>
            {
                var config = (ZeroSection) ConfigurationManager.GetSection("zeroSettings");
                if (config == null)
                {
                    throw new SettingsPropertyNotFoundException("没有在配置文件中配置ZeroMQ信息");
                }
                return config;
            });

        private static ZeroSection Configurations => _configureSection.Value;

        public static Connection Get(string eventType)
        {
            return Configurations.ItemGenerations.Cast<Connection>()
                                                 .FirstOrDefault(connection => connection.EventType.ToUpper() == eventType.ToUpper());
        }
    }
}
