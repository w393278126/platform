using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis.Configuration
{
    /// <summary>
    /// Redis使用规范，如果需要使用Redis，请在RedisKeyDefinition类定义中定义Key，然后新建类继承对应的RedisXXX
    /// 所以这个类仅在当前程序集中公开
    /// </summary>
    internal class RedisServer
    {
        public const string Cache = "cache";
        public const string Config = "config";
        public const string Event = "event";
        public const string Feed = "feed";
        public const string Login = "login";
        public const string Log = "log";
        public const string Live = "live";
        public const string Profile = "profile";
        public const string Property = "property";
        public const string Vector = "vector";
        public const string MessageBus = "messageBus";
        public const string Chat = "chat";
        public const string CoolDown = "itemCoolDown";
        public const string BlackList = "blacklist";
        public const string LiveStream = "livestream";
        public const string PvUv0 = "pvuv0";
        public const string WholeSite = "wholesite";
        public const string Room = "room";
        public const string PushService = "pushservice";
        public const string Security = "security";
        public const string Family = "family";
        public const string Item = "item";
        public const string Identity = "identity";
        public const string BlackAvatar = "blackavatar";
        public const string Sport = "sport";
        public const string Relationship = "relationship";
        public const string Mission = "mission";
        public const string Idfa = "idfa";
        public const string Time = "time";
        public const string SportLottery = "sportlottery";
        public const string Rvs = "rvs";


        // 注意！本实例每日凌晨6点flushall
        public const string Cache2 = "cache2";

        //redis Cluster 集群
        public const string PvuvCluster = "pvuv_cluster";
        public const string CommonCluster = "common_cluster";
        public const string FenqianCluster = "fenqian_cluster";
        public const string VectorCluster = "vector_cluster";
        public const string LiveCluster = "live_cluster";
        public const string ActivityCluster = "activity_cluster";
        public const string PropsCluster = "props_cluster";
        public const string RvsCluster = "rvs_cluster";
        public const string RelationshipCluster = "relationship_cluster";

        public static readonly Dictionary<string, RedisGroup> ConfigDict = CloudStructuresConfigurationSection
            .GetSection()
            .ToRedisGroups()
            .ToDictionary(x => x.GroupName);
    }
}
