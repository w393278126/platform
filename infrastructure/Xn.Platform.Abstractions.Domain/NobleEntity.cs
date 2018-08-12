using Xn.Platform.Core.Extensions;
using System;
using System.Runtime.Serialization;

namespace Xn.Platform.Abstractions.Domain
{
    /// <summary>
    /// 贵族实体
    /// </summary>
    [DataContract]
    public class NobleEntity
    {

        /// <summary>
        /// 贵族等级
        /// </summary>
        [DataMember(Name = "level")]
        public int Level { get; set; }

        /// <summary>
        /// 过期时间戳
        /// </summary>
        [DataMember(Name = "expireTime")]
        public int ExpireTime { get; set; }

        /// <summary>
        /// 过保时间戳
        /// </summary>
        [DataMember(Name = "protectTime")]
        public int ProtectTime { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        [DataMember(Name = "isExpire")]
        public bool IsExpire => ExpireTime <= DateTime.Now.ToUniversalTime().GetUnixTimeStamp();
    }
}
