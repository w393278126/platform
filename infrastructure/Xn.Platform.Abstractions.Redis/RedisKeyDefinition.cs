using  Xn.Platform.Abstractions.Redis.Configuration;

namespace Xn.Platform.Data.Redis
{
    /// <summary>
    /// RedisKey所有定义
    /// 不在这个类定义的Key将全部清理
    ///  
    /// 放置规范：放在适当的region中
    /// 命名规范：redis实例名+Key类型+Key作用，例如 用户余额是hash存储在cache中，命名为CacheHashUserBalance
    /// </summary>
    public partial class RedisKeyDefinition
    {

        #region Login相关

        /// <summary>
        /// 登录加密Key
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] LoginSortedSetXnKey = { RedisServer.Login, "xnkeys" };
        /// <summary>
        /// 登录验证码
        /// </summary>
        public static readonly string[] LoginValidateCodeKey = { RedisServer.Login, "xnvalidatecode" };



        #endregion Login相关

        #region Log待处理队列相关

        /// <summary>
        /// 待发邮件队列
        /// 房间后台竞猜，房间后台订阅招募、mb首冲T币、发礼包
        /// </summary>
        public static readonly string[] LogListEmail = { RedisServer.Log, "u:email" };//vector

        public static readonly string[] LogHashNotAuthorRoomLog = { RedisServer.Log, "r:notauthor:room:log" };

        /// <summary>
        /// 异常Kafka事件数据
        /// </summary>
        public static readonly string[] LogListKafkaEventDataMissed = { RedisServer.Log, "kafka:event:data:missed" };

        #endregion Log待处理队列相关
       
        #region Config系统配置相关

        public static readonly string ConfigHashList = "{0}:list";
        public static readonly string ConfigStringTimestamp = "{0}:timestamp";
        public static readonly string ConfigString = "{0}:string";

        #endregion Config系统配置相关

    }
}