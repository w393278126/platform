using  Xn.Platform.Abstractions.Redis.Configuration;

namespace Xn.Platform.Data.Redis
{
    public partial class RedisKeyDefinition
    {
        /// <summary>
        /// 回源鉴权流Id列表
        /// member：streamId
        /// </summary>
        public static readonly string[] LiveAuthStreamIdSet = { RedisServer.LiveCluster, "r:live:auth:streamId" };

        /// <summary>
        /// 回源鉴权房间列表
        /// member：roomId
        /// </summary>
        public static readonly string[] LiveAuthRoomidSet = { RedisServer.LiveCluster, "r:live:auth:roomId" };

    }
}
