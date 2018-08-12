using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis.Configuration
{
    public class CloudStructuresConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("instance")]
        [ConfigurationCollection(typeof(RedisGroupElementCollection), AddItemName = "group")]
        public RedisGroupElementCollection Groups
        {
            get
            {
                return (RedisGroupElementCollection)base["instance"];
            }
        }

        public static CloudStructuresConfigurationSection GetSection()
        {
            return ConfigurationManager.GetSection("redisSettings") as CloudStructuresConfigurationSection;
        }

        public RedisGroup[] ToRedisGroups()
        {
            return Groups.Select(x => x.ToRedisGroup()).ToArray();
        }
    }

    [ConfigurationCollection(typeof(RedisGroupElement))]
    public class RedisGroupElementCollection : ConfigurationElementCollection, IEnumerable<RedisGroupElement>
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as RedisGroupElement).Name;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisGroupElement();
        }

        public new IEnumerator<RedisGroupElement> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return (RedisGroupElement)e.Current;
            }
        }
    }

    [ConfigurationCollection(typeof(RedisSettingsElement))]
    public class RedisGroupElement : ConfigurationElementCollection, IEnumerable<RedisSettingsElement>
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return base["name"] as string; }
        }

        [ConfigurationProperty("poolSize", DefaultValue = 10)]
        public int PoolSize
        {
            get
            {
                var poolSize = (int) base["poolSize"];
                return poolSize > 0 ? poolSize : 10;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisSettingsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var elem = (element as RedisSettingsElement);
            return Tuple.Create(elem.ConnectionString, elem.Db);
        }

        public new IEnumerator<RedisSettingsElement> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return (RedisSettingsElement)e.Current;
            }
        }

        public RedisGroup ToRedisGroup()
        {
            var settings = this.Select(x => new RedisSettings(x.ConnectionString, x.Db, x.Master)).ToArray();
            return new RedisGroup(Name, PoolSize, settings);
        }
    }

    public class RedisSettingsElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base["connectionString"]; }
        }

        [ConfigurationProperty("db", DefaultValue = 0)]
        public int Db
        {
            get { return (int)base["db"]; }
        }

        [ConfigurationProperty("master")]
        public bool Master
        {
            get { return (bool)base["master"]; }
        }
    }
}