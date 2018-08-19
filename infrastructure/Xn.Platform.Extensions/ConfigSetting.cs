using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xn.Platform.Core
{
    public static class ConfigSetting
    {
        private static readonly ConnectionStringSettingsCollection ConnSettingItems = ConfigurationManager.ConnectionStrings;
        private static readonly NameValueCollection AppSettingItems = ConfigurationManager.AppSettings;

        /// <summary>
        /// 配置一览
        /// </summary>
        /// <returns></returns>
        public static string Glance()
        {
            var properties = typeof(ConfigSetting).GetProperties(BindingFlags.Public | BindingFlags.Static);
            var sb = new StringBuilder();
            object value;
            foreach (var property in properties)
            {
                try
                {
                    value = property.GetValue(null, null);
                }
                catch (Exception ex)
                {
                    value = $"[exception]{ex.Message}";
                }

                sb.AppendLine(string.Format("{0}: {1}", property.Name, value == null ? "[is null]" : value.ToString()));
            }

            return sb.ToString();
        }
        private static string _protectGuestIdString;
        private static bool _protectGuestId;
        public static bool ProtectGuestId
        {
            get
            {
                if (!string.IsNullOrEmpty(_protectGuestIdString))
                    return _protectGuestId;
                _protectGuestId = true;
                if (AppSettingItems.AllKeys.Contains("protect.guest.id"))
                {
                    var content = AppSettingItems["protect.guest.id"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _protectGuestIdString = content;
                        _protectGuestId = content.AsBool();
                    }
                }
                return _protectGuestId;
            }
        }


        private static string _authCookieName;
        public static string AuthCookieName
        {
            get
            {
                if (!string.IsNullOrEmpty(_authCookieName))
                    return _authCookieName;
                _authCookieName = "p1u_id";
                if (AppSettingItems.AllKeys.Contains("AuthCookieName"))
                {
                    var content = AppSettingItems["AuthCookieName"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _authCookieName = content;
                    }
                }
                return _authCookieName;
            }
        }

        private static string _validationKey;
        public static string ValidationKey
        {
            get
            {
                if (!string.IsNullOrEmpty(_validationKey))
                    return _validationKey;
                _validationKey = "C985085862F161091EEEFE30F7DC9D62";
                if (AppSettingItems.AllKeys.Contains("ValidationKey"))
                {
                    var content = AppSettingItems["ValidationKey"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _validationKey = content;
                    }
                }
                return _validationKey;
            }
        }

        private static string _encryptionKey;
        public static string EncryptionKey
        {
            get
            {
                if (!string.IsNullOrEmpty(_encryptionKey))
                    return _encryptionKey;
                _encryptionKey = "0F10F6CB2F5369C14D14FA07BAD302267901240CC8C845DD2C645FBD149A11C9";
                if (AppSettingItems.AllKeys.Contains("EncryptionKey"))
                {
                    var content = AppSettingItems["EncryptionKey"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _encryptionKey = content;
                    }
                }
                return _encryptionKey;
            }
        }
        #region 本地MySQL测试
        private static string _connectionMySqlSportsEntitiesReadOnly;
        public static string ConnectionMySqlSportsEntitiesReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMySqlSportsEntitiesReadOnly))
                {
                    _connectionMySqlSportsEntitiesReadOnly = ConnSettingItems["MySqlSportsEntitiesReadOnly"].ConnectionString;
                }
                return _connectionMySqlSportsEntitiesReadOnly;
            }

        }
        private static string _connectionMySqlSportsEntities;
        public static string ConnectionMySqlSportsEntities
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMySqlSportsEntities))
                    _connectionMySqlSportsEntities = ConnSettingItems["MySqlSportsEntities"].ConnectionString;
                return _connectionMySqlSportsEntities;
            }
        }

        private static string _connectionLongzhuSportsEntities;
        public static string ConnectionLongzhuSportsEntities
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuSportsEntities))
                {
                    _connectionLongzhuSportsEntities = ConnSettingItems["LongzhuSportsEntities"].ConnectionString;
                }
                return _connectionLongzhuSportsEntities;
            }
        }

        private static string _connectionLongzhuSportsEntitiesReadOnly;
        public static string ConnectionLongzhuSportsEntitiesReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuSportsEntitiesReadOnly))
                {
                    _connectionLongzhuSportsEntitiesReadOnly = ConnSettingItems["LongzhuSportsEntitiesReadOnly"].ConnectionString;
                }
                return _connectionLongzhuSportsEntitiesReadOnly;
            }
        }

        #endregion
    }
}
