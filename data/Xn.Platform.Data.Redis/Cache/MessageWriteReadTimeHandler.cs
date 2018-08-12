using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache
    /// </summary>
    public class MessageWriteTimeHandler : RedisHash
    {
        /// <summary>
        /// 系统和个人消息写入时间
        /// </summary>
        public MessageWriteTimeHandler() : base(RedisKeyDefinition.CacheHashMessageWriteTime)
        {
        }
    }

    /// <summary>
    /// Cache
    /// </summary>
    public class MessageReadTimeHandler : RedisHash
    {
        /// <summary>
        /// 个人消息读取时间
        /// </summary>
        public MessageReadTimeHandler() : base(RedisKeyDefinition.CacheHashMessageReadTime)
        {
        }
    }


    public class SysEmailResetTimeHandler : RedisHash
    {
        /// <summary>
        /// 系统和个人消息写入时间
        /// </summary>
        public SysEmailResetTimeHandler() : base(RedisKeyDefinition.CacheHashSysEmailResetTime)
        {
        }
    }


}
