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

        private static string _connectionHome;
        public static string ConnectionHome
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionHome))
                {
                    _connectionHome = ConnSettingItems["PLUHomeEntities"].ConnectionString;
                }
                return _connectionHome;
            }
        }

        private static string _connectionHomeReadOnly;
        public static string ConnectionHomeReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionHomeReadOnly))
                {
                    _connectionHomeReadOnly = ConnSettingItems["PLUHomeEntitiesReadOnly"].ConnectionString;
                }
                return _connectionHomeReadOnly;
            }
        }

        private static string _connectionPLULog;
        public static string ConnectionPLULog
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPLULog))
                {
                    _connectionPLULog = ConnSettingItems["PLULog"].ConnectionString;
                }
                return _connectionPLULog;
            }
        }

        private static string _connectionLongzhuLive;
        public static string ConnectionLongzhuLive
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuLive))
                {

                    _connectionLongzhuLive = ConnSettingItems["LongzhuLive"].ConnectionString;
                }
                return _connectionLongzhuLive;
            }
        }

        private static string _connectionLongzhuLiveReadonly;
        public static string ConnectionLongzhuLiveReadonly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuLiveReadonly))
                {
                    _connectionLongzhuLiveReadonly = ConnSettingItems["LongzhuLiveReadonly"].ConnectionString;
                }
                return _connectionLongzhuLiveReadonly;
            }
        }


        private static string _connectionUc;
        public static string ConnectionUc
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionUc))
                {
                    _connectionUc = ConnSettingItems["PLUUC"].ConnectionString;
                }
                return _connectionUc;
            }
        }

        private static string _connectionUcReadOnly;
        public static string ConnectionUcReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionUcReadOnly))
                {
                    _connectionUcReadOnly = ConnSettingItems["PLUUCReadOnly"].ConnectionString;
                }
                return _connectionUcReadOnly;
            }
        }

        private static string _connectionReport;
        public static string ConnectionReport
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionReport))
                {
                    _connectionReport = ConnSettingItems["PLUReport"].ConnectionString;
                }
                return _connectionReport;
            }
        }

        private static string _connectionReportDDL;
        public static string ConnectionReportDDL
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionReportDDL))
                {
                    _connectionReportDDL = ConnSettingItems["PLUReportDDL"].ConnectionString;
                }
                return _connectionReportDDL;
            }
        }

        private static string _connectionReportReadOnly;
        public static string ConnectionReportReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionReportReadOnly))
                {
                    _connectionReportReadOnly = ConnSettingItems["PLUReportReadOnly"].ConnectionString;
                }
                return _connectionReportReadOnly;
            }
        }

        private static string _connectionOlyra;
        public static string ConnectionOlyra
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionOlyra))
                {
                    _connectionOlyra = ConnSettingItems["olyra"].ConnectionString;
                }
                return _connectionOlyra;
            }
        }

        private static string _connectionOlyraReadOnly;
        public static string ConnectionOlyraReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionOlyraReadOnly))
                {
                    _connectionOlyraReadOnly = ConnSettingItems["olyra"].ConnectionString;
                }
                return _connectionOlyraReadOnly;
            }
        }

        private static string _connectionStatistics;
        public static string ConnectionStatistics
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionStatistics))
                {
                    _connectionStatistics = ConnSettingItems["PLUstatistics"].ConnectionString;
                }
                return _connectionStatistics;
            }
        }

        private static string _connectionStatisticsReadOnly;
        public static string ConnectionStatisticsReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionStatisticsReadOnly))
                {
                    _connectionStatisticsReadOnly = ConnSettingItems["PLUstatistics"].ConnectionString;
                }
                return _connectionStatisticsReadOnly;
            }
        }

        private static string _connectionPayment;
        public static string ConnectionPayment
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPayment))
                {
                    _connectionPayment = ConnSettingItems["PLUPayment"].ConnectionString;
                }
                return _connectionPayment;
            }
        }

        private static string _connectionPaymentReadOnly;
        public static string ConnectionPaymentReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPaymentReadOnly))
                {
                    _connectionPaymentReadOnly = ConnSettingItems["PLUPaymentReadOnly"].ConnectionString;
                }
                return _connectionPaymentReadOnly;
            }
        }

        private static string _connectionBGReport;
        public static string ConnectionBGReport
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionBGReport))
                {
                    _connectionBGReport = ConnSettingItems["BGreport"].ConnectionString;
                }
                return _connectionBGReport;
            }
        }

        private static string _connectionBGReportReadOnly;
        public static string ConnectionBGReportReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionBGReportReadOnly))
                {
                    _connectionBGReportReadOnly = ConnSettingItems["BGReportReadOnly"].ConnectionString;
                }
                return _connectionBGReportReadOnly;
            }
        }

        private static string _connectionPluGrim;
        public static string ConnectionPluGrim
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPluGrim))
                {
                    _connectionPluGrim = ConnSettingItems["PLUPluGrim"].ConnectionString;
                }
                return _connectionPluGrim;
            }
        }

        private static string _connectionPluGrimReadOnly;
        public static string ConnectionPluGrimReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPluGrimReadOnly))
                {
                    _connectionPluGrimReadOnly = ConnSettingItems["PLUPluGrimReadOnly"].ConnectionString;
                }
                return _connectionPluGrimReadOnly;
            }
        }

        private static string _connectionRelationship;
        public static string ConnectionRelationship
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionRelationship))
                {
                    _connectionRelationship = ConnSettingItems["PLURelationship"].ConnectionString;
                }
                return _connectionRelationship;
            }
        }

        private static string _connectionRelationshipReadOnly;
        public static string ConnectionRelationshipReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionRelationshipReadOnly))
                {
                    _connectionRelationshipReadOnly = ConnSettingItems["PLURelationshipReadOnly"].ConnectionString;
                }
                return _connectionRelationshipReadOnly;
            }
        }

        private static string _connectionProperty;
        public static string ConnectionProperty
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionProperty))
                {
                    _connectionProperty = ConnSettingItems["PLUProperty"].ConnectionString;
                }
                return _connectionProperty;
            }
        }

        private static string _connectionPropertyReadOnly;
        public static string ConnectionPropertyReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPropertyReadOnly))
                {
                    _connectionPropertyReadOnly = ConnSettingItems["PLUPropertyReadOnly"].ConnectionString;
                }
                return _connectionPropertyReadOnly;
            }
        }


        private static string _connectionComment;
        public static string ConnectionComment
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionComment))
                {
                    _connectionComment = ConnSettingItems["PLUCommentContext"].ConnectionString;
                }
                return _connectionComment;
            }
        }

        private static string _connectionCommentReadOnly;
        public static string ConnectionCommentReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionCommentReadOnly))
                {
                    _connectionCommentReadOnly = ConnSettingItems["PLUCommentReadOnly"].ConnectionString;
                }
                return _connectionCommentReadOnly;
            }
        }

        private static string _connectionIpKu;
        public static string ConnectionIpKu
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionIpKu))
                {
                    _connectionIpKu = ConnSettingItems["PLUIPKu"].ConnectionString;
                }
                return _connectionIpKu;
            }
        }

        private static string _connectionIpKuReadOnly;
        public static string ConnectionIpKuReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionIpKuReadOnly))
                {
                    _connectionIpKuReadOnly = ConnSettingItems["PLUIPKu"].ConnectionString;
                }
                return _connectionIpKuReadOnly;
            }
        }


        private static string _connectionPvCounter;
        public static string ConnectionPvCounter
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPvCounter))
                {
                    _connectionPvCounter = ConnSettingItems["mysql.pvcounter"].ConnectionString;
                }
                return _connectionPvCounter;
            }
        }

        private static string _connectionPvCounterReadOnly;
        public static string ConnectionPvCounterReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPvCounterReadOnly))
                {
                    _connectionPvCounterReadOnly = ConnSettingItems["mysql.pvcounter"].ConnectionString;
                }
                return _connectionPvCounterReadOnly;
            }
        }

        private static string _connectionSubscription;
        public static string ConnectionSubscription
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionSubscription))
                {
                    _connectionSubscription = ConnSettingItems["PLUSubscription"].ConnectionString;
                }
                return _connectionSubscription;
            }
        }

        private static string _connectionSubscriptionReadOnly;
        public static string ConnectionSubscriptionReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionSubscriptionReadOnly))
                {
                    _connectionSubscriptionReadOnly = ConnSettingItems["PLUSubscriptionReadOnly"].ConnectionString;
                }
                return _connectionSubscriptionReadOnly;
            }
        }

        private static string _connectionProps;
        public static string ConnectionProps
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionProps))
                {
                    _connectionProps = ConnSettingItems["PLUProps"].ConnectionString;
                }
                return _connectionProps;
            }
        }

        private static string _connectionPropsReadOnly;
        public static string ConnectionPropsReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPropsReadOnly))
                {
                    _connectionPropsReadOnly = ConnSettingItems["PLUPropsReadOnly"].ConnectionString;
                }
                return _connectionPropsReadOnly;
            }
        }

        private static string _connectionMission;
        public static string ConnectionMission
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMission))
                {
                    _connectionMission = ConnSettingItems["PLUHomeEntities"].ConnectionString;
                }
                return _connectionMission;
            }
        }

        private static string _connectionMissionReadOnly;
        public static string ConnectionMissionReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMissionReadOnly))
                {
                    _connectionMissionReadOnly = ConnSettingItems["PLUHomeEntitiesReadOnly"].ConnectionString;
                }
                return _connectionMissionReadOnly;
            }
        }

        private static string _connectionAdvertising;
        public static string ConnectionAdvertising
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionAdvertising))
                {
                    _connectionAdvertising = ConnSettingItems["advertising"].ConnectionString;
                }
                return _connectionAdvertising;
            }
        }

        private static string _connectionAdvertisingReadOnly;
        public static string ConnectionAdvertisingReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionAdvertisingReadOnly))
                {
                    _connectionAdvertisingReadOnly = ConnSettingItems["advertisingReadOnly"].ConnectionString;
                }
                return _connectionAdvertisingReadOnly;
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

        private static string _connectionLongzhuFenQian;
        public static string ConnectionLongzhuFenQian
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuFenQian))
                {
                    _connectionLongzhuFenQian = ConnSettingItems["LongzhuFenQian"].ConnectionString;
                }
                return _connectionLongzhuFenQian;
            }
        }

        private static string _connectionLongzhuFenQianReadOnly;
        public static string ConnectionLongzhuFenQianReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuFenQianReadOnly))
                {
                    _connectionLongzhuFenQianReadOnly = ConnSettingItems["LongzhuFenQianReadOnly"].ConnectionString;
                }
                return _connectionLongzhuFenQianReadOnly;
            }
        }

        private static string _connectionEvent;
        public static string ConnectionEvent
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionEvent))
                {
                    _connectionEvent = ConnSettingItems["LongzhuEvent"].ConnectionString;
                }
                return _connectionEvent;
            }
        }

        private static string _connectionEventReadOnly;
        public static string ConnectionEventReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionEventReadOnly))
                {
                    _connectionEventReadOnly = ConnSettingItems["LongzhuEventReadOnly"].ConnectionString;
                }
                return _connectionEventReadOnly;
            }
        }

        private static string _connectionTgaEntities;
        public static string ConnectionTgaEntities
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionTgaEntities))
                {
                    _connectionTgaEntities = ConnSettingItems["tga"].ConnectionString;
                }
                return _connectionTgaEntities;
            }
        }

        private static string _connectionTgaEntitiesReadOnly;
        public static string ConnectionTgaEntitiesReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionTgaEntitiesReadOnly))
                {
                    _connectionTgaEntitiesReadOnly = ConnSettingItems["tga"].ConnectionString;
                }
                return _connectionTgaEntitiesReadOnly;
            }
        }

        private static string _connectionMedia;
        public static string ConnectionMedia
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMedia))
                {
                    _connectionMedia = ConnSettingItems["LongzhuMedia"].ConnectionString;
                }
                return _connectionMedia;
            }
        }

        private static string _connectionMediaReadOnly;
        public static string ConnectionMediaReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionMediaReadOnly))
                {
                    _connectionMediaReadOnly = ConnSettingItems["LongzhuMediaReadOnly"].ConnectionString;
                }
                return _connectionMediaReadOnly;
            }
        }

        private static string _connectionLongzhuAuth;
        public static string ConnectionLongzhuAuth
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuAuth))
                {
                    _connectionLongzhuAuth = ConnSettingItems["LongzhuAuth"].ConnectionString;
                }
                return _connectionLongzhuAuth;
            }
        }

        private static string _connectionLongzhuAuthReadOnly;
        public static string ConnectionLongzhuAuthReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionLongzhuAuthReadOnly))
                {
                    _connectionLongzhuAuthReadOnly = ConnSettingItems["LongzhuAuthReadOnly"].ConnectionString;
                }
                return _connectionLongzhuAuthReadOnly;
            }
        }

        private static string _connectionSNBaseLiveStream;
        public static string ConnectionSNBaseLiveStream
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionSNBaseLiveStream))
                {
                    _connectionSNBaseLiveStream = ConnSettingItems["SNBaseLiveStream"].ConnectionString;
                }
                return _connectionSNBaseLiveStream;
            }
        }

        private static string _connectionSNBaseLiveStreamReadOnly;
        public static string ConnectionSNBaseLiveStreamReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionSNBaseLiveStreamReadOnly))
                {
                    _connectionSNBaseLiveStreamReadOnly = ConnSettingItems["SNBaseLiveStreamReadOnly"].ConnectionString;
                }
                return _connectionSNBaseLiveStreamReadOnly;
            }
        }


        private static string _longZhuHome;
        public static string LongZhuHome
        {
            get
            {
                if (_longZhuHome != null)
                    return _longZhuHome;
                _longZhuHome = "www.longzhu.com";
                if (AppSettingItems.AllKeys.Contains("LongZhuHome"))
                {
                    var content = AppSettingItems["LongZhuHome"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _longZhuHome = content;
                    }
                }
                return _longZhuHome;
            }
        }


        private static string _longZhuRoom;
        public static string LongZhuRoom
        {
            get
            {
                if (_longZhuRoom != null)
                    return _longZhuRoom;
                _longZhuRoom = "star.longzhu.com";
                if (AppSettingItems.AllKeys.Contains("LongZhuRoom"))
                {
                    var content = AppSettingItems["LongZhuRoom"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _longZhuRoom = content;
                    }
                }
                return _longZhuRoom;
            }
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

        static int _shardingCount = 0;
        /// <summary>
        /// 读取配置文件中设定的Sharding份数，默认为5
        /// </summary>
        public static int ShardingCount
        {
            get
            {
                if (_shardingCount != 0)
                    return _shardingCount;
                _shardingCount = 5;
                if (AppSettingItems.AllKeys.Contains("chatroomShardings"))
                {
                    var content = AppSettingItems["chatroomShardings"].AsInt();
                    if (content > 0)
                    {
                        _shardingCount = content;
                    }
                }
                return _shardingCount;
            }
        }

        private static string _authCookieName;
        public static string AuthCookieName
        {
            get
            {
                if (!string.IsNullOrEmpty(_authCookieName))
                    return _authCookieName;
                _authCookieName = "xn_id";
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

        private static int _machineId;
        public static int MachineId
        {
            get
            {
                if (_machineId != 0)
                    return _machineId;
                _machineId = 1;
                if (AppSettingItems.AllKeys.Contains("MachineId"))
                {
                    var content = AppSettingItems["MachineId"].AsInt();
                    if (content > 0)
                    {
                        _machineId = content;
                    }
                }
                return _machineId;
            }
        }

        private static string _videoUrl;
        public static string VideoUrl
        {
            get
            {
                if (_videoUrl != null)
                    return _videoUrl;
                _videoUrl = "v.longzhu.com";
                if (AppSettingItems.AllKeys.Contains("VideoUrl"))
                {
                    var content = AppSettingItems["VideoUrl"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _videoUrl = content;
                    }
                }
                return _videoUrl;
            }
        }

        private static string _screenShotPath;
        public static string ScreenShotPath
        {
            get
            {
                if (_screenShotPath != null)
                    return _screenShotPath;
                _screenShotPath = @"\\192.168.8.152\RoomImg";
                if (AppSettingItems.AllKeys.Contains("ScreenShotPath"))
                {
                    var content = AppSettingItems["ScreenShotPath"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _screenShotPath = content;
                    }
                }
                return _screenShotPath;
            }
        }
        private static int _onlineexpired;
        public static int OnlineExpired
        {
            get
            {
                if (_onlineexpired != 0)
                    return _onlineexpired;
                _onlineexpired = 600;
                if (AppSettingItems.AllKeys.Contains("online.expire.seconds"))
                {
                    var content = AppSettingItems["online.expire.seconds"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _onlineexpired = content.AsInt(600);
                    }
                }
                return _onlineexpired;
            }
        }

        private static string _commentAuthKey;
        public static string CommentAuthKey
        {
            get
            {
                if (_commentAuthKey != null)
                    return _commentAuthKey;
                _commentAuthKey = "4059F31A05E642AEA14A8482BB070336";
                if (AppSettingItems.AllKeys.Contains("CommentAuthKey"))
                {
                    var content = AppSettingItems["CommentAuthKey"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _commentAuthKey = content;
                    }
                }
                return _commentAuthKey;
            }
        }

        private static string _roomUploadImageKey;
        public static string RoomUploadImageKey
        {
            get
            {
                if (_roomUploadImageKey != null)
                    return _roomUploadImageKey;
                _roomUploadImageKey = "pluroomikuji3k_3ikk";
                if (AppSettingItems.AllKeys.Contains("RoomUploadImageKey"))
                {
                    var content = AppSettingItems["RoomUploadImageKey"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _roomUploadImageKey = content;
                    }
                }
                return _roomUploadImageKey;
            }
        }


        private static string _qiniuSnapshotNotifyUrl;
        public static string QiniuSnapshotNotifyUrl
        {
            get
            {
                if (_qiniuSnapshotNotifyUrl != null)
                    return _qiniuSnapshotNotifyUrl;
                _qiniuSnapshotNotifyUrl = "http://liveapi.Xn.cn/qiniu/SnapshotNotify";
                if (AppSettingItems.AllKeys.Contains("QiniuSnapshotNotifyUrl"))
                {
                    var content = AppSettingItems["QiniuSnapshotNotifyUrl"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _qiniuSnapshotNotifyUrl = content;
                    }
                }
                return _qiniuSnapshotNotifyUrl;
            }
        }

        private static string _qiniuRtmpLiveHost;
        public static string QiniuRtmpLiveHost
        {
            get
            {
                if (_qiniuRtmpLiveHost != null)
                    return _qiniuRtmpLiveHost;
                _qiniuRtmpLiveHost = "rtmp2.Xn.cn";
                if (AppSettingItems.AllKeys.Contains("QiniuRtmpLiveHost"))
                {
                    var content = AppSettingItems["QiniuRtmpLiveHost"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _qiniuRtmpLiveHost = content;
                    }
                }
                return _qiniuRtmpLiveHost;
            }
        }


        private static string _videoCommentCacheMinutes;
        public static string VideoCommentCacheMinutes
        {
            get
            {
                if (_videoCommentCacheMinutes != null)
                    return _videoCommentCacheMinutes;
                _videoCommentCacheMinutes = "5";
                if (AppSettingItems.AllKeys.Contains("VideoCommentCacheMinutes"))
                {
                    var content = AppSettingItems["VideoCommentCacheMinutes"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _videoCommentCacheMinutes = content;
                    }
                }
                return _videoCommentCacheMinutes;
            }
        }

        private static string _roomVersion;
        public static string RoomVersion
        {
            get
            {
                if (_roomVersion != null)
                    return _roomVersion;
                _roomVersion = "0";
                if (AppSettingItems.AllKeys.Contains("RoomVersion"))
                {
                    var content = AppSettingItems["RoomVersion"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _roomVersion = content;
                    }
                }
                return _roomVersion;
            }
        }

        private static string _cpsSpreaderKey;
        public static string CpsSpreaderKey
        {
            get
            {
                if (_cpsSpreaderKey != null)
                    return _cpsSpreaderKey;
                _cpsSpreaderKey = "C9ikj4kkk42F161091EEEFE30F7DC9D62";
                if (AppSettingItems.AllKeys.Contains("CpsSpreaderKey"))
                {
                    var content = AppSettingItems["CpsSpreaderKey"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _cpsSpreaderKey = content;
                    }
                }
                return _cpsSpreaderKey;
            }
        }

        private static IDictionary<string, bool> _liveDomains;
        public static IDictionary<string, bool> LiveDomains
        {
            get
            {
                if (_liveDomains != null)
                    return _liveDomains;
                _liveDomains = new Dictionary<string, bool>(0);
                if (AppSettingItems.AllKeys.Contains("liveDomains"))
                {
                    var content = AppSettingItems["liveDomains"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _liveDomains = content.Split(',').ToDictionary(o => o, v => true);
                    }
                }
                return _liveDomains;
            }
        }

        /// <summary>
        /// 调用网宿接口加密串
        /// </summary>
        private static string _wcsCloudCode;
        public static string WCSCloudCode
        {
            get
            {
                if (_wcsCloudCode != null)
                    return _wcsCloudCode;
#if DEBUG
                _wcsCloudCode = "jjjjsjs@@#jdjjd1122233";
#else
                _wcsCloudCode = "longzhu@#";
#endif
                if (AppSettingItems.AllKeys.Contains("WCSCloudCode"))
                {
                    var content = AppSettingItems["WCSCloudCode"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _wcsCloudCode = content;
                    }
                }
                return _wcsCloudCode;
            }
        }

        private static string _videoUploadTokenUrl;
        /// <summary>
        /// 视频上传的TokenUrl
        /// </summary>
        public static string VideoUploadTokenUrl
        {
            get
            {
                if (_videoUploadTokenUrl != null)
                    return _videoUploadTokenUrl;
#if DEBUG
                _videoUploadTokenUrl = "http://test.api.v.longzhu.com";
#else
                _videoUploadTokenUrl = "http://api.v.longzhu.com";
#endif
                if (AppSettingItems.AllKeys.Contains("VideoUploadTokenUrl"))
                {
                    var content = AppSettingItems["VideoUploadTokenUrl"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _videoUploadTokenUrl = content;
                    }
                }
                return _videoUploadTokenUrl;
            }
        }

        private static string _txTokenUrl;
        public static string TxTokenUrl
        {
            get
            {
                if (_txTokenUrl != null)
                    return _txTokenUrl;
#if DEBUG
                _txTokenUrl = "http://test.api.v.longzhu.com/tx/token?";
#else
                _txTokenUrl = "http://api.v.longzhu.com/tx/token?";
#endif
                if (AppSettingItems.AllKeys.Contains("TxTokenUrl"))
                {
                    var content = AppSettingItems["TxTokenUrl"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _txTokenUrl = content;
                    }
                }
                return _txTokenUrl;
            }
        }

        private static string _Not_Transfer_BD_RoomId;
        public static string Not_Transfer_BD_RoomId
        {
            get
            {
                if (_Not_Transfer_BD_RoomId != null)
                    return _Not_Transfer_BD_RoomId;
                _Not_Transfer_BD_RoomId = "";
                if (AppSettingItems.AllKeys.Contains("Not_Transfer_BD_RoomId"))
                {
                    var content = AppSettingItems["Not_Transfer_BD_RoomId"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _Not_Transfer_BD_RoomId = content;
                    }
                }
                return _Not_Transfer_BD_RoomId;
            }
        }

        private static string _Not_Transfer_SD_RoomId;
        public static string Not_Transfer_SD_RoomId
        {
            get
            {
                if (_Not_Transfer_SD_RoomId != null)
                    return _Not_Transfer_SD_RoomId;
                _Not_Transfer_SD_RoomId = "";
                if (AppSettingItems.AllKeys.Contains("Not_Transfer_SD_RoomId"))
                {
                    var content = AppSettingItems["Not_Transfer_SD_RoomId"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _Not_Transfer_SD_RoomId = content;
                    }
                }
                return _Not_Transfer_SD_RoomId;
            }
        }

        private static string _zookeeperConnectionString;
        public static string ZookeeperConnectionString
        {
            get
            {
                if (_zookeeperConnectionString != null)
                    return _zookeeperConnectionString;
                _zookeeperConnectionString = "10.200.150.3:2181";
                if (AppSettingItems.AllKeys.Contains("ZookeeperConnectionString"))
                {
                    var content = AppSettingItems["ZookeeperConnectionString"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _zookeeperConnectionString = content;
                    }
                }
                return _zookeeperConnectionString;
            }
        }


        private static string _zookeeperFenqianConnectionString;
        public static string ZookeeperFenqianConnectionString
        {
            get
            {
                if (_zookeeperFenqianConnectionString != null)
                    return _zookeeperFenqianConnectionString;
                _zookeeperFenqianConnectionString = "10.200.150.3:2181";
                if (AppSettingItems.AllKeys.Contains("ZookeeperConnectionString_XiaSha"))
                {
                    var content = AppSettingItems["ZookeeperConnectionString_XiaSha"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _zookeeperFenqianConnectionString = content;
                    }
                }
                return _zookeeperFenqianConnectionString;
            }
        }


        private static string _zookeeperConnectionString2;
        public static string ZookeeperConnectionString2
        {
            get
            {
                if (_zookeeperConnectionString2 != null)
                    return _zookeeperConnectionString2;
                _zookeeperConnectionString2 = "10.200.150.202:2181";
                if (AppSettingItems.AllKeys.Contains("ZookeeperConnectionString2"))
                {
                    var content = AppSettingItems["ZookeeperConnectionString2"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _zookeeperConnectionString2 = content;
                    }
                }
                return _zookeeperConnectionString2;
            }
        }

        private static int _roomStatusCacheMinutes;
        public static int RoomStatusCacheMinutes
        {
            get
            {
                if (_roomStatusCacheMinutes != 0)
                    return _roomStatusCacheMinutes;
                _roomStatusCacheMinutes = 1;
                if (AppSettingItems.AllKeys.Contains("RoomStatusCacheMinutes"))
                {
                    var content = AppSettingItems["RoomStatusCacheMinutes"];
                    if (!string.IsNullOrEmpty(content))
                    {
                        _roomStatusCacheMinutes = content.AsInt();
                    }
                }
                return _roomStatusCacheMinutes;
            }
        }


        private static string _htmlVersionEnv = null;

        public static string HtmlVersionEnv
        {
            get
            {
                if (_htmlVersionEnv != null)
                {
                    return _htmlVersionEnv;
                }

                _htmlVersionEnv = string.Empty;

                if (AppSettingItems.AllKeys.Contains("html:env"))
                {
                    _htmlVersionEnv = AppSettingItems["html:env"];
                }

                return _htmlVersionEnv;
            }
        }

        private static string _yoyoKafkaTopic = null;

        public static string YoyoKafkaTopic
        {
            get
            {
                if (_yoyoKafkaTopic == null)
                {
                    _yoyoKafkaTopic = string.Empty;
                    if (AppSettingItems.AllKeys.Contains("yoyo:topic"))
                    {
                        _yoyoKafkaTopic = AppSettingItems["yoyo:topic"];
                    }
                }

                return _yoyoKafkaTopic;
            }
        }

        private static string _yoyoMysqlConnectionString = null;
        public static string YoyoMysqlConnectionString
        {
            get
            {
                if (_yoyoMysqlConnectionString == null)
                {
                    _yoyoMysqlConnectionString = string.Empty;
                    if (AppSettingItems.AllKeys.Contains("yoyo:mysql:conn"))
                    {
                        _yoyoMysqlConnectionString = AppSettingItems["yoyo:mysql:conn"];
                    }
                }

                return _yoyoMysqlConnectionString;
            }
        }

        private static string _yoyoMigrateMysqlConnectionString = null;
        public static string YoyoMigrateMysqlConnectionString
        {
            get
            {
                if (_yoyoMigrateMysqlConnectionString == null)
                {
                    _yoyoMigrateMysqlConnectionString = string.Empty;
                    if (AppSettingItems.AllKeys.Contains("yoyo:migrate:mysql:conn"))
                    {
                        _yoyoMigrateMysqlConnectionString = AppSettingItems["yoyo:migrate:mysql:conn"];
                    }
                }

                return _yoyoMigrateMysqlConnectionString;
            }
        }

        private static string _connectionPLUStarkEntitiesReadOnly;
        public static string ConnectionPLUStarkEntitiesReadOnly
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionPLUStarkEntitiesReadOnly))
                {
                    _connectionPLUStarkEntitiesReadOnly = ConnSettingItems["PLUStark"].ConnectionString;
                }
                return _connectionPLUStarkEntitiesReadOnly;
            }
        }

        #region 热力值

        private static string _consulAddr;

        /// <summary>
        /// consul地址
        /// </summary>
        public static string ConsulAddr
        {
            get
            {
                if (string.IsNullOrEmpty(_consulAddr))
                {
                    _consulAddr = AppSettingItems["ConsulAddr"] ?? string.Empty;
                }
                return _consulAddr;
            }
        }


        #endregion
    }
}
