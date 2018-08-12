using System;
using System.Configuration;

namespace Xn.Home.Utils.Configuration
{
    /// <summary>
    /// 配置写死命名空间
    /// Xn.Home.Utils.Configuration.MailSection,Xn.Home.Utils
    /// </summary>
    public class MailSection : ConfigurationSection
    {
        [ConfigurationProperty("items", IsRequired = true)]
        public ItemCollectoin ItemGenerations
        {
            get
            {
                return (ItemCollectoin)this["items"];
            }
        }
    }

    public class ItemCollectoin : ConfigurationElementCollection
    {
        public ItemCollectoin()
        {
            this.AddElementName = "item";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Item();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Item)element).Key;
        }
    }

    public class Item : ConfigurationElement
    {
        /// <summary>
        /// 道具名
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("from", DefaultValue = "t3s@outlook.com")]
        public string From
        {
            get { return (string)this["from"]; }
        }

        [ConfigurationProperty("password", DefaultValue = "")]
        public string Password
        {
            get { return (string)this["password"]; }
        }

        [ConfigurationProperty("host", DefaultValue = "smtp.Xn.com")]
        public string Host
        {
            get { return (string)this["host"]; }
        }

        [ConfigurationProperty("port", DefaultValue = 587)]
        public int Port
        {
            get { return (int)this["port"]; }
        }
    }

    public sealed class MailManager
    {
        private static Lazy<MailSection> _configureSection
            = new Lazy<MailSection>(() =>
            {
                MailSection config = (MailSection)ConfigurationManager.GetSection("EMail");
                if (config == null)
                {
                    throw new SettingsPropertyNotFoundException("没有在配置文件中配置用户在线信息处理");
                }
                return config;
            });

        private static MailSection Configurations
        {
            get
            {
                return _configureSection.Value;
            }
        }

        public static Item Get(string key)
        {
            foreach (Item item in Configurations.ItemGenerations) if (item.Key.ToUpper() == key.ToUpper()) return item;
            return null;
        }
    }

}
