using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Xn.Platform.Data.MongoDb.Configuration
{
    public class MongoConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("instance")]
        [ConfigurationCollection(typeof(GroupElementCollection), AddItemName = "group")]
        public GroupElementCollection Groups
        {
            get
            {
                return (GroupElementCollection)base["instance"];
            }
        }

        public static MongoConfigurationSection GetSection()
        {
            return ConfigurationManager.GetSection("mongoSettings") as MongoConfigurationSection;
        }

        public MongoGroup[] ToMongoGroups()
        {
            return Groups.Select(x => x.ToMongoGroup()).ToArray();
        }
    }

    [ConfigurationCollection(typeof(GroupElement))]
    public class GroupElementCollection : ConfigurationElementCollection, IEnumerable<GroupElement>
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as GroupElement).DatabaseName;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElement();
        }

        public new IEnumerator<GroupElement> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return (GroupElement)e.Current;
            }
        }
    }

    [ConfigurationCollection(typeof(SettingsElement))]
    public class GroupElement : ConfigurationElementCollection, IEnumerable<SettingsElement>
    {
        [ConfigurationProperty("databaseName")]
        public string DatabaseName
        {
            get { return base["databaseName"] as string; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SettingsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var elem = (element as SettingsElement);
            return Tuple.Create(elem.ConnectionString);
        }

        public new IEnumerator<SettingsElement> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return (SettingsElement)e.Current;
            }
        }

        public MongoGroup ToMongoGroup()
        {
            var settings = this.Select(x => new MongoSettings{ConnectionString = x.ConnectionString}).ToArray();
            return new MongoGroup(DatabaseName, settings);
        }
    }

    public class SettingsElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base["connectionString"]; }
        }
    }
}