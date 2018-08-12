using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class ListVideoUploadCallBackHandler : RedisList
    {
        public ListVideoUploadCallBackHandler() : base(RedisKeyDefinition.CacheListVideoUploadCallBack)
        {
        }
    }

    public class ListAppJPushHandler : RedisList
    {
        public ListAppJPushHandler() : base(RedisKeyDefinition.CacheListAppJPush)
        {
        }
    }

    public class ListGuangmingForceoutHandler : RedisList
    {
        public ListGuangmingForceoutHandler() : base(RedisKeyDefinition.CacheListGuangmingForceout)
        {
        }
    }

    public class ListGiftBackupHandler : RedisList
    {
        public ListGiftBackupHandler() : base(RedisKeyDefinition.VectorListGiftBackup)
        {
        }
    }

    public class ListKafkaEventDataMissedHandler : RedisList
    {
        public ListKafkaEventDataMissedHandler() : base(RedisKeyDefinition.LogListKafkaEventDataMissed)
        {
        }
    }
}
