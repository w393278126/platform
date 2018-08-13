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
        public static readonly string[] LoginSortedSetPluKey = { RedisServer.Login, "plukeys" };

        /// <summary>
        /// 用户信息
        /// </summary>
        public static readonly string[] LoginStringUser = { RedisServer.Login, "u:{0}" };

        /// <summary>
        /// 用户附加信息
        /// </summary>
        public static readonly string[] LoginStringUserExpand = { RedisServer.Login, "u:x:{0}" };

        /// <summary>
        /// 通过手机验证的uid
        /// </summary>
        public static readonly string[] LoginSetUserMobile = { RedisServer.Login, "mobiles" };

        /// <summary>
        /// 是否登录过直播App
        /// </summary>
        public static readonly string[] LoginSetUserApp = { RedisServer.Login, "users:app:list" };

        /// <summary>
        /// 用户手机号
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] LoginHashUserMobiles = { RedisServer.Login, "u:mobiles" };

        /// <summary>
        /// 用户手机号
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] LoginSetSpreaderGameServer = { RedisServer.Login, "spreader:{0}" };

        /// <summary>
        /// 用户验证信息{userId}
        /// birthdays 出生日期至1900-1-1的天数 / name 姓名  / sex 性别 1 男， 0 女  /  cid 身份证 / birth 出生日期
        /// </summary>
        public static readonly string[] LoginStringUserAuthorize = { RedisServer.Login, "u:authorize:{0}" };

        /// <summary>
        /// 修改昵称
        /// </summary>
        public static readonly string[] LoginHashNickName = { RedisServer.Login, "custom:nickname:list" };


        /// <summary>
        /// qq注册的号
        /// </summary>
        public static readonly string[] LoginHashQqAccount = { RedisServer.Login, "uid2openid" };

        /// <summary>
        /// weixin注册的号
        /// </summary>
        public static readonly string[] LoginHashWeiXinAccount = { RedisServer.Login, "uid2openid:weixin" };

        /// <summary>
        /// 新浪微博注册的号
        /// </summary>
        public static readonly string[] LoginHashWeiboAccount = { RedisServer.Login, "uid2openid:weibo" };

        /// <summary>
        /// 是否已经升级为手机账号登录
        /// </summary>
        public static readonly string[] LoginSetUserUpgrade = { RedisServer.Login, "u:upgrade:sets" };

        /// <summary>
        /// 危险等级的用户名单
        /// </summary>
        public static readonly string[] LoginHashBlackList = { RedisServer.Login, "blacklist" };

        /// <summary>
        /// PPTV注册的号
        /// </summary>
        public static readonly string[] LoginHashPPTVAccount = { RedisServer.Login, "openids:pptv" };

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