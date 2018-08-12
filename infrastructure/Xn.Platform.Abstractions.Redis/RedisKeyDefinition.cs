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
        public static readonly string[] RedisStringUserCoinRatioAndBalance = { "settlement", "userratio:{0}" };
        public static readonly string[] RedisHashDailyUserData = { "settlement", "data:{0}" };
        public static readonly string[] RedisSetDailyUserIds = { "settlement", "user:{0}" };

        #region Cache用户

        public static readonly string[] CacheStringLastId = { RedisServer.Cache, "notifiy:last:id" };
        
        /// <summary>
        /// 不开启总榜房间集合
        /// </summary>
        public static readonly string[] CacheSetNotShowTotal = { RedisServer.Cache, "room:notshow:total" };

        /// <summary>
        /// 消息入场通知时间 {userid +"|"+ messageType}
        /// field: {roomid} value:时间戳
        /// </summary>
        public static readonly string[] Cache2HashMessageNotifyTime = { RedisServer.Cache2, "msg:{0}:msgnotice" };

        /// <summary>
        /// 微信公众号支付成功提醒,
        /// Expire:24h
        /// </summary>
        public static readonly string[] CacheStringWxOfficialPayNotice = { RedisServer.Cache, "wxoffical:notice:{0}" };

        /// <summary>
        /// 兑换修改密码错误次数统计
        /// </summary>
        public static readonly string[] Cache2StringXcoinExchangePasswordError = { RedisServer.Cache2, "u:{0}:passworderror" };
        /// <summary>
        /// 修改密码token
        /// </summary>
        public static readonly string[] Cache2StringXcoinExchangeToken = { RedisServer.Cache2, "u:{0}:token" };


        /// <summary>
        /// 用户礼券兑换次数计算
        /// </summary>
        public static readonly string[] CacheStringCouponExchangeTimes = { RedisServer.Cache2, "u:{0}:couponexchange" };

        /// <summary>
        /// 用户每日限时礼物计数
        /// </summary>
        public static readonly string[] CacheHashUserLimitTimes = { RedisServer.Cache2, "limitgift:user:times" };

        #endregion Cache用户
        
        #region Cache待处理队列相关

        public static readonly string[] CacheListVideoHlsTransfer = { RedisServer.Cache, "video:hls:transfer" };
        public static readonly string[] VectorHashWsCloudMediaRetryKey = { RedisServer.Vector, "video:wscloudmedia:retry" };
        public static readonly string[] VectorHashHlsTransferReTryKey = { RedisServer.Vector, "video:wscloudmedia:hlsretry" };

        public static readonly string[] CacheListVideoUploadCallBack = { RedisServer.Vector, "video:upload:callback" };
        public static readonly string[] CacheListAppJPush = { RedisServer.Cache, "app:push:list" };
        public static readonly string[] CacheListGuangmingForceout = { RedisServer.Cache, "kf:guangming:forceout" };
        public static readonly string[] CacheStringCaptcha = { RedisServer.Cache, "captcha:{0}" };

        /// <summary>
        /// 当前在转码的房间列表
        /// </summary>
        public static readonly string[] CacheSetTranscodingRooms = { RedisServer.Cache, "trans:rooms:set" };

        /// <summary>
        /// 只记录付费的交易订单
        /// </summary>
        public static readonly string[] CacheListGiftPayTradeInfos = { RedisServer.Cache, "pay:tradeinfos" };

        #endregion Cache待处理队列相关

        #region Cache后台统计结果相关

        public static readonly string[] CacheHashRoomStatisticsChat = { RedisServer.Cache, "room:statistics:{0}" };

        /// <summary>
        /// 房间 最近7天视频播放量
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] CacheSortedSetRoomMediaRecentView = { RedisServer.Vector, "r:recentView" };

        /// <summary>
        /// batch batch监控
        /// key：roomId value：timestamp
        /// </summary>
        public static readonly string[] CacheHashBatchTimeStamp = { RedisServer.Cache, "batch:timestamp" };

        #endregion Cache后台统计结果相关

        #region Cache用户相关定时清理

        public static readonly string[] CacheSetCertificationSet = { RedisServer.Cache, "userid:certification:set" };

        public static readonly string[] CacheSetCertificationHash = { RedisServer.Cache, "userid:certification:hash" };

        /// <summary>
        /// 全民直播JoinRoom观看人数{roomId}
        /// member:userId
        /// </summary>
        public static readonly string[] CacheSetLiveAppJoin = { RedisServer.Cache, "app:join:{0}" };

        /// <summary>
        /// 视频标签名称
        /// feild：mediaId value：join
        /// </summary>
        public static readonly string[] CacheHashMediaTag = { RedisServer.Cache, "media:tag" };

        /// <summary>
        /// 系统和个人消息写入时间
        /// feild：userId value：时间截
        /// </summary>
        public static readonly string[] CacheHashMessageWriteTime = { RedisServer.Cache, "m:write:time" };

        /// <summary>
        /// 系统和个人消息清空时间
        /// feild：userId value：时间截
        /// </summary>
        public static readonly string[] CacheHashSysEmailResetTime = { RedisServer.Cache, "sysemail:reset:time" };

        /// <summary>
        /// 个人消息读取时间
        /// feild：userId value：时间截
        /// </summary>
        public static readonly string[] CacheHashMessageReadTime = { RedisServer.Cache, "m:read:time" };

        /// <summary>
        /// 评论屏蔽IP
        /// feild：ip value：次数
        /// </summary>
        public static readonly string[] CacheHashCommentSpamIp = { RedisServer.Cache, "spam:ip" };

        /// <summary>
        /// 评论屏蔽IP
        /// feild：ip value：次数
        /// </summary>
        public static readonly string[] CacheHashCommentSpamIpLog = { RedisServer.Cache, "spam:log:now" };

        /// <summary>
        /// 道具策略更新时间
        /// </summary>
        public static readonly string[] CacheStringStrategyTimestamp = { RedisServer.Cache, "strategy:timestamp" };

        /// <summary>
        /// 题目更新列表时间
        /// </summary>
        public static readonly string[] CacheStringQuestionTimestamp = { RedisServer.Cache, "Question:timestamp" };

        /// <summary>
        /// 房间座驾列表{roomId}
        /// member:userId
        /// </summary>
        public static readonly string[] CacheHashVehicleJoin = { RedisServer.Cache, "vehicle:join:{0}" };

        #endregion Cache用户相关定时清理

        #region Cache合作相关

        /// <summary>
        /// 通知TGP房间直播状态缓存
        /// </summary>
        public static readonly string[] CacheSetNoticeTgpRoomStatus = { RedisServer.Cache, "notice:tpg:room:status" };
        /// <summary>
        /// 存放了用户的芝麻认证的key
        /// </summary>
        public static readonly string[] CacheStringZhimaNumber = { RedisServer.Cache, "zhima:certifyuser:{0}" };

        #endregion Cache合作相关

        #region Cache微信公众号票据

        /// <summary>
        /// 微信公众号唯一票据
        /// </summary>
        public static readonly string[] ConfigStringWxOfficialAk = { RedisServer.Config, "wxoffical:ak:{0}" };

        /// <summary>
        /// 微信jssdk调用票据
        /// </summary>
        public static readonly string[] ConfigStringWxOfficialJsApiTiket = { RedisServer.Config, "wxoffical:jsapitiket:{0}" };

        #endregion

        #region BlackList禁停/举报计数

        /// <summary>
        /// 房间举报计数
        /// </summary>
        public static readonly string[] BlackListHashRoomSpamCount = { RedisServer.BlackList, "room:spam:count" };

        /// <summary>
        /// 房间禁停计数
        /// </summary>
        public static readonly string[] BlackListHashRoomSpamBlackCount = { RedisServer.BlackList, "room:spamblack:count" };

        #endregion BlackList禁停/举报计数

        #region BlackList房间黑名单

        /// <summary>
        /// 房间黑名单{roomId}
        /// feild：userId value：解禁时间
        /// </summary>
        public static readonly string[] VectorHashBlackUserList = { RedisServer.BlackList, "r:{0}:blacklist" };

        /// <summary>
        /// ios充值黑名单idfa
        /// value：idfa
        /// </summary>
        public static readonly string[] BlackIosSetIdfas = { RedisServer.BlackList, "ios:black:idfa" };
        /// <summary>
        /// ios的transaction_id
        /// </summary>
        public static readonly string[] TransactionIdIosSet = { RedisServer.BlackList, "ios:receipt:transaction_id" };
        #endregion BlackList房间黑名单

        #region BlackList 踢出房间名单
        /// <summary>
        ///  踢出房间名单{roomId}
        /// field：userId value：可以进入时间
        /// field：userId:operator value：踢人的人的uid
        /// field：userId:role value：踢人的人的房间角色
        /// </summary>
        public static readonly string[] BlackListHashKickOutRoomUserList = { RedisServer.BlackList, "r:{0}:kickout" };
        #endregion

        #region Config系统配置相关

        public static readonly string ConfigHashList = "{0}:list";
        public static readonly string ConfigStringTimestamp = "{0}:timestamp";
        public static readonly string ConfigString = "{0}:string";

        /// <summary>
        /// 加密LUA版本号
        /// </summary>
        public static readonly string[] ConfigStringEncryptVersion = { RedisServer.Config, "goblin:lua:version" };

        /// <summary>
        /// 敏感词
        /// </summary>
        public static readonly string[] ConfigKeyAdKeyWordChinese = { RedisServer.Config, "ad:keyword:chinese" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixAdPattern = { RedisServer.Config, "ad:pattern" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixCommentHot = { RedisServer.Config, "commenthot" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixConfig = { RedisServer.Config, "config" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixItem = { RedisServer.Config, "item" };

        /// <summary>
        /// 道具Id生成器
        /// </summary>
        public static readonly string[] ConfigStringItemId = { RedisServer.Config, "item:maxid" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixMbInterval = { RedisServer.Config, "mbinterval" };

        /// <summary>
        /// 在线的区间系数
        /// </summary>
        public static readonly string[] ConfigKeySuffixExpectCoefficients = { RedisServer.Config, "onlinecount:expectcoefficients" };

        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string[] ConfigKeySuffixNotification = { RedisServer.Config, "notification" };

        /// <summary>
        /// 分类信息
        /// field：categoryId value：name
        /// </summary>
        public static readonly string[] ConfigKeySuffixCategory = { RedisServer.Vector, "category:name" };

        /// <summary>
        /// 分类信息
        /// field：categoryId value：domain
        /// </summary>
        public static readonly string[] ConfigKeySuffixCategoryDomain = { RedisServer.Vector, "category:domain" };

        public static readonly string[] ConfigHashKeyApp = { RedisServer.Config, "config:app:{0}" };

        /// <summary>
        /// 消息模板配置
        /// </summary>
        public static readonly string[] ConfigHashMsgTemplate = { RedisServer.Config, "config:msg:tmpl" };

        /// <summary>
        /// 游戏头条配置
        /// </summary>
        public static readonly string[] ConfigHashGameRollingSetting = { RedisServer.Config, "config:rolling:game" };

        /// <summary>
        /// html 资源版本控制
        /// field: htmlkey+version+resourcekey value: resource
        /// </summary>
        public static readonly string[] ConfigHashHtmlVersion = { RedisServer.Config, "config:html:resources" };

        /// <summary>
        /// 大数据算出来top200的房间
        /// </summary>
        public static readonly string[] ConfigSetTopRoom = { RedisServer.Config, "pc_top_flow" };

        /// <summary>
        /// 后台手动设置的转码列表,roomids
        /// </summary>
        public static readonly string[] ConfigSetAdminNoTranscodingRooms = { RedisServer.Config, "adminnotrans:rooms:set" };


        /// <summary>
        /// 每日更新最近使用道具数量的排行
        /// </summary>
        public static readonly string[] ConfigSortedSetItemUseFrequency = { RedisServer.Config, "config:sorted:itemcount" };
        /// <summary>
        /// 热词相关roomids
        /// </summary>
        public static readonly string[] ConfigHashSearchRoomIds = { RedisServer.Config, "config:search:roomids" };
        /// <summary>
        /// 关闭弹幕的房间id，如果key是0则全平台
        /// </summary>
        public static readonly string[] ConfigHashShieldBarrageRoomIds = { RedisServer.Config, "config:shield:barrageroomids" };
        /// <summary>
        /// 关闭聊天的房间id，如果key是0则全平台
        /// </summary>
        public static readonly string[] ConfigHashShieldChatRoomIds = { RedisServer.Config, "config:shield:chatroomids" };
        /// <summary>
        /// 枚举如下：  1 先审后发  2 先发后审  3 关闭
        /// </summary>
        public static readonly string[] ConfigKeyAvatarReview = { RedisServer.Config, "avatarReviewType" };

        /// <summary>
        /// 任务宝箱单次概率配置
        /// 数据格式: {单次限制}|{单次下降概率}
        /// </summary>
        public static readonly string[] ConfigMissionBoxOnceChance = { RedisServer.Config, "config:missionbox:oncechance" };


        /// <summary>
        /// 礼物自定义图标上传历史
        /// </summary>
        public static readonly string[] ItemTagCustomHistory = { RedisServer.Config, "itemtagcustomhistory" };

        #endregion Config系统配置相关

        #region Event互动：任务、红包、竞猜、小游戏

        /// <summary>
        /// 防止并发的redis
        /// </summary>
        public static readonly string[] EventSetSend = { RedisServer.Event, "send:set" };

        /// <summary>
        /// 分享任务短链接{0}为share
        /// field：自增id value：sharejson
        /// </summary>
        public static readonly string[] EventHashShortShare = { RedisServer.Event, "short:{0}:url" };

        /// <summary>
        /// 分享任务短链接自增Id
        /// </summary>
        public static readonly string[] EventStringShortShareCount = { RedisServer.Event, "short:share:count" };

        /// <summary>
        /// 红包道具池{redEnvelopeId}
        /// member：道具价值
        /// </summary>
        public static readonly string[] EventListRedEnvelopeDraw = { RedisServer.Event, "red:draw:{0}" };

        /// <summary>
        /// 红包领取用户集合{redEnvelopeId}
        /// member：userId
        /// </summary>
        public static readonly string[] EventSetRedEnvelopeUser = { RedisServer.Event, "red:user:{0}" };

        /// <summary>
        /// 红包验证码时间{redEnvelopeId}
        /// field：userId value：time
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeCheckTime = { RedisServer.Event, "red:checktiem:{0}" };

        /// <summary>
        /// 红包领取验证{redEnvelopeId}
        /// field：userId value：value
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeRequestCount = { RedisServer.Event, "red:request:count:{0}" };
        /// <summary>
        /// 红包领取总数
        /// field：userId value：value
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeTotalCount = { RedisServer.Cache2, "red:total:count:{0}" };
        /// <summary>
        /// 红包领取验证{redEnvelopeId}
        /// field：userId value：value
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeRequestTime = { RedisServer.Event, "red:request:time:{0}" };
        /// <summary>
        /// 红包剩余次数
        /// field：redEnvelopeId value：count
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeInventory = { RedisServer.Event, "room:red:inventory" };
        /// <summary>
        /// 红包信息：用户
        /// field：redEnvelopeId value：userjson
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeInfo = { RedisServer.Event, "red:info" };

        /// <summary>
        /// 红包信息：房间域名
        /// field：redEnvelopeId value：domain
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeDomain = { RedisServer.Event, "red:domain" };

        /// <summary>
        /// 红包信息：时间
        /// field：redEnvelopeId value：timejson
        /// </summary>
        public static readonly string[] EventHashRedEnvelopeTime = { RedisServer.Event, "red:time" };

        /// <summary>
        /// 房间竞猜状态
        /// field：roomId value：1开启，0关闭
        /// </summary>
        public static readonly string[] EventHashRoomBet = { RedisServer.Event, "r:bet" };

        /// <summary>
        /// 捕鱼活动
        /// field：guid value：userId
        /// </summary>
        public static readonly string[] EventHashSmallFishGame = { RedisServer.Event, "g:game:fish" };

        /// <summary>
        /// 捕鱼活动消费
        /// field：gameCode|orderId value：count
        /// </summary>
        public static readonly string[] EventHashSmallFishGameConsume = { RedisServer.Event, "g:game:fish:consume" };

        /// <summary>
        /// 捕鱼活动游戏币兑换为龙币
        /// field：gameCode|orderId value：count
        /// </summary>
        public static readonly string[] EventHashSmallFishGameExchange = { RedisServer.Event, "g:game:fish:exchange" };
        /// <summary>
        /// 激活码{setKey}
        /// member：激活码
        /// </summary>
        public static readonly string[] EventSetCdKey = { RedisServer.Event, "cdkey:user:{0}" };

        /// <summary>
        /// 任务更新时间
        /// </summary>
        public static readonly string[] MissionStringMissionTimestamp = { RedisServer.Mission, "mission:timestamp" };

        ///// <summary>
        ///// 勋章任务更新时间
        ///// </summary>
        //public static readonly string[] MedalTaskStringMissionTimestamp = { RedisServer.Mission, "mt:timestamp" };


        /// <summary>
        /// 任务完成进度 
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// field：userId value：step
        /// </summary>
        public static readonly string[] EventHashMissionStep = { RedisServer.Mission, "mission:step:{0}" };

        /// <summary>
        /// 任务完成进度内容数据
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{userId}
        /// 
        /// member:context
        /// </summary>
        public static readonly string[] EventSetMissionContext = { RedisServer.Mission, "mission:context:{0}" };

        /// <summary>
        /// 任务完成进度内容数据
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{userId}
        /// 
        /// member:context
        /// </summary>
        public static readonly string[] EventListMissionContextList = { RedisServer.Mission, "mission:contextlist:{0}" };

        /// <summary>
        /// 任务统计数据
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{userId}
        /// 
        /// field：userId value：step
        /// </summary>
        public static readonly string[] EventHashMissionData = { RedisServer.Mission, "mission:data:{0}" };


        /// <summary>
        /// 签到任务类型类型
        /// field：20170629 value：1，2
        /// </summary>
        public static readonly string[] EventHashMissionSignType = { RedisServer.Mission, "mission:signtype" };

        /// <summary>
        /// 签到金额类型
        /// field：1周签，2月签 value：金额
        /// </summary>
        public static readonly string[] EventHashMissionPriceType = { RedisServer.Mission, "mission:signprice" };



        /// <summary>
        /// 当前任务是否上线过
        /// value：任务ID 
        /// </summary>
        public static readonly string[] EventSetMissionOffLine = { RedisServer.Mission, "mission:offline" };

        /// <summary>
        /// 任务阶段
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// 
        /// field：userId value：step
        /// </summary>
        public static readonly string[] EventHashMissionStage = { RedisServer.Mission, "mission:stage:{0}" };
        /// <summary>
        /// 任务统计数据
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{userId}
        /// 
        /// field：userId value：step
        /// </summary>
        public static readonly string[] EventHashMissionStageTime = { RedisServer.Mission, "mission:stagetime:{0}" };

        /// <summary>
        /// 任务元气值
        /// 每日任务：{missionId}:{yyMMdd}
        /// rank：userId score：step
        /// </summary>
        public static readonly string[] EventSortedSetMissionVigour = { RedisServer.Mission, "mission:vigour:{0}" };
        /// <summary>
        /// 任务关卡数
        /// 每日任务：{missionId}:{yyMMdd}
        /// rank：userId score：step
        /// </summary>
        public static readonly string[] EventSortedSetMissionLevel = { RedisServer.Mission, "mission:level:{0}" };
        /// <summary>
        /// 任务关卡最后时间
        /// 每日任务：{missionId}:{yyMMdd}
        /// field：userId value：utc
        /// </summary>
        public static readonly string[] EventHashMissionLevelLastTime = { RedisServer.Mission, "mission:leveltime:{0}" };

        /// <summary>
        /// 任务通知{roomId}
        /// 
        /// value：json
        /// </summary>
        public static readonly string[] EventStringMissionJoin = { RedisServer.Mission, "mission:join:{0}" };

        /// <summary>
        /// 任务通知
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{stageId}
        /// 
        /// member：userId
        /// </summary>
        public static readonly string[] EventSetMissionMessage = { RedisServer.Mission, "mission:message:{0}" };

        /// <summary>
        /// 任务领取奖励
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{stageId}
        /// 
        /// member：userId
        /// </summary>
        public static readonly string[] EventSetMissionReward = { RedisServer.Mission, "mission:reward:{0}" };

        /// <summary>
        /// 任务补签记录
        /// 每月任务：{missionId}:{yyMM}
        /// 每周任务：{missionId}:{weekofyear}
        /// 每日任务：{missionId}:{yyMMdd}
        /// 单次任务：{missionId}:{0}
        /// :{userId}
        /// 
        /// member:day
        /// </summary>
        public static readonly string[] EventSetMissionGap = { RedisServer.Mission, "mission:gap:{0}" };

        /// <summary>
        /// 每天直播{yyMMdd}
        /// member:roomId,userId
        /// </summary>
        public static readonly string[] EventSetHostMissionHost = { RedisServer.Mission, "mission:host:{0}" };

        /// <summary>
        /// 主播挑战任务对应表
        /// 0:{yyMMdd}
        /// field：userId value：taskId
        /// </summary>
        public static readonly string[] EventHashHostMissionMap = { RedisServer.Mission, "mission:host:map:{0}" };

        /// <summary>
        /// 主播挑战任务当前阶段对应表
        /// 0:{yyMMdd}
        /// field：userId value：stageId
        /// </summary>
        public static readonly string[] EventHashHostMissionStageMap = { RedisServer.Mission, "mission:host:stage:map:{0}" };

        /// <summary>
        /// 任务映射时间
        /// field：taskId value：date
        /// </summary>
        public static readonly string[] EventHashMissionIdMapDate = { RedisServer.Mission, "mission:missionid:date" };
        /// <summary>
        /// 任务映射时间
        /// field：taskId value：date
        /// </summary>
        public static readonly string[] EventHashMissionIdMapDateDay = { RedisServer.Mission, "mission:missionid:date:{0}" };

        /// <summary>
        /// 任务道具礼包
        /// </summary>
        public static readonly string[] EventListMissionGift = { RedisServer.Event, "dazhonggift:gift" };

        /// <summary>
        /// 消耗时长道具 uid,itemid,count
        /// </summary>
        public static readonly string[] EventListMissionTimeGift = { RedisServer.Event, "dazhonggift:timegift" };

        /// <summary>
        /// 消耗库存道具
        /// </summary>
        public static readonly string[] EventListCommonInventoryGift = { RedisServer.Event, "commongift:inventorygift" };
        /// <summary>
        /// 配额领取货币订单
        /// </summary>
        public static readonly string[] EventHashQuotaExchange = { RedisServer.Event, "coin:quota:exchange" };
        /// <summary>
        /// 增发货币订单
        /// </summary>
        public static readonly string[] EventHashIssuanceExchange = { RedisServer.Event, "coin:issuance:exchange" };
        
        /// <summary>
        /// 机构申请站内信
        /// </summary>
        public static readonly string[] EventListFamilyApplyInfo = { RedisServer.Event, "family:apply:mail" };
        /// <summary>
        /// 任务宝箱实例 yyyyddMM
        /// key:任务宝箱实例Id
        /// </summary>
        public static readonly string[] EventSetMissionGiftBox = { RedisServer.Mission, "mission:giftbox:day:{0}" };
        /// <summary>
        /// 任务宝箱实例
        /// key:任务宝箱实例Id
        /// list item: MissionRewardItemCore.ToJson()
        /// </summary>
        public static readonly string[] EventListMissionGiftBoxInstance = { RedisServer.Mission, "mission:giftbox:instance:{0}" };

        /// <summary>
        /// 开宝箱实例前的时间间隔
        /// key:任务宝箱实例Id
        /// value:"lock"
        /// </summary>
        public static readonly string[] EventStringMissionGiftBoxInstanceTimespan = { RedisServer.Mission, "mission:giftbox:instance:{0}:timespan" };

        #endregion

        #region Mission Identity 勋章任务相关

        /// <summary>
        /// 勋章任务完成进度 
        /// mt:step:{taskid}(:{itemName}-可选):({yyMMdd}|{weekofyear}|{yyMM})
        /// </summary>
        public static readonly string[] MissionHashMedalTaskStep = { RedisServer.Mission, "mt:step:{0}" };

        /// <summary>
        /// 达到发放勋章任务条件 
        /// mt:award:{yyMMdd}
        /// </summary>
        public static readonly string[] MissionHashMedalTaskAward = { RedisServer.Mission, "mt:award:{0}" };
        #endregion

        #region PerttyNumber Identity 靓号相关

        /// <summary>
        /// 靓号基础数据
        /// HASH pn:info {number} {info_json}
        /// </summary>
        public static readonly string[] PrettyNumberInfo = { RedisServer.CommonCluster, "pn:info" };

        /// <summary>
        /// 靓号商城
        /// SET pn:store:{prettyType}//1:永久,2:尊贵  {number}
        /// </summary>
        public static readonly string[] PrettyNumberStore = { RedisServer.CommonCluster, "pn:store:{0}" };

        /// <summary>
        /// 用户靓号
        /// HASH pn:user:number  {uid} {number,type}
        /// </summary>
        public static readonly string[] PrettyNumberUser = { RedisServer.CommonCluster, "pn:user:number" };

        /// <summary>
        /// 靓号用户
        /// HASH pn:number:user  {number} {uid}
        /// </summary>
        public static readonly string[] PrettyNumberNumber = { RedisServer.CommonCluster, "pn:number:user" };

        /// <summary>
        /// 靓号充值记录
        /// HASH pn:pay:{uid} {Number}:{PeriodDate} {Count}
        /// </summary>
        public static readonly string[] PrettyNumberPay = { RedisServer.CommonCluster, "pn:pay:{0}" };

        /// <summary>
        /// 靓号购买锁定(防止重复购买)
        /// pn:lock:{number}
        /// </summary>
        public static readonly string[] PrettyNumberLock = { RedisServer.Cache2, "pn:lock:{0}" };

        /// <summary>
        /// 单关宝箱概率控制
        /// box:once:{boxid}  field:{uid}
        /// </summary>
        public static readonly string[] OpenBoxOnceChance = { RedisServer.Cache2, "box:once:{0}" };

        #endregion

        #region 贵族相关

        /// <summary>
        /// 用户已获贵族
        /// key: noble:{uid}
        /// value: {id, level, title, expireTime, protectTime} json
        /// </summary>
        public static readonly string[] NobleIdentity = { RedisServer.CommonCluster, "noble:{0}" };

        /// <summary>
        /// 购买贵族锁定(防并发)
        /// </summary>
        public static readonly string[] NobleBuyLock = { RedisServer.Cache2, "noble:buylock:{0}" };

        /// <summary>
        /// 用户贵族身份保护期
        /// key: noble:protect:{uid}
        /// value: {id, level, title, expireTime, protectTime} json
        /// </summary>
        public static readonly string[] NobleProtectIdentity = { RedisServer.CommonCluster, "noble:protect:{0}" };

        #endregion

        #region 新粉丝勋章相关

        /// <summary>
        /// 已做过迁移的勋章
        /// Key:    u:medal:move:{type}   1:单粉丝勋章, 2:单用户 3:房间
        /// Type:   Set
        /// Member: {uid}:{roomid}
        /// </summary>
        public static readonly string[] UserNewFansMedalMove = { RedisServer.CommonCluster, "u:medal:move:{0}" };

        /// <summary>
        /// 粉丝值 关联粉丝值更新数据 操作 有粉丝值增加,就更新
        /// Key:    u:m:v:{roomId}
        /// Type:   SortSet
        /// Member: {userId}
        /// Score:  {fans}
        /// </summary>
        public static readonly string[] UserNewFansMedalFans = { RedisServer.CommonCluster, "u:m:v:{0}" };


        /// <summary>
        /// 增加粉丝值 弹幕 每日记录
        /// Key:    u:m:msg:{uid-yyyyMMdd}
        /// Type:   Hash
        /// Filed:  {roomId}
        /// Value:  {score}: 今日弹幕数量
        /// </summary>
        public static readonly string[] UserNewFansMedalMessage = { RedisServer.CommonCluster, "u:m:msg:{0}" };

        /// <summary>
        /// 粉丝值增加完成
        /// Key:    u:m:done:{yyyyMMdd}
        /// Type:   Set
        /// Member: {uid}:{roomId}:{type(1:打卡,2:弹幕:3:观看)}
        /// </summary>
        public static readonly string[] UserNewFansMedalDone = { RedisServer.CommonCluster, "u:m:done:{0}" };
        /// <summary>
        /// 粉丝观看时长
        /// Key:    u:m:wt:{uid-yyyyMMdd}
        /// Type:   Hash
        /// Filed:  {roomId}
        /// Value:  {second}
        /// </summary>
        public static readonly string[] UserNewFansMedalWatchTime = { RedisServer.CommonCluster, "u:m:wt:{0}" };

        /// <summary>
        /// 粉丝值最后更新数据
        /// Key:    u:m:ut:{roomId}
        /// Type:   SortSet
        /// Member: {userId}
        /// Score:  {timeSpan}  最后更新时间戳
        /// </summary>
        public static readonly string[] UserNewFansMedalUpdateTime = { RedisServer.CommonCluster, "u:m:ut:{0}" };

        /// <summary>
        /// 处理过衰减
        /// Key:    u:m:rd:{roomId-yyyyMMdd}
        /// Type:   set
        /// member: {uid}
        /// </summary>
        public static readonly string[] UserNewFansMedalReduceSet = { RedisServer.CommonCluster, "u:m:rd:{0}" };

        /// <summary>
        /// 用户打卡
        /// Key:    checkin:{hostId}:{date:yyyyMMdd}
        /// Type:   set
        /// Member: {userId}
        /// </summary>
        public static readonly string[] UserNewFansMedalCheckin = { RedisServer.RelationshipCluster, "checkin:{0}" };


        /// <summary>
        /// 某个用户连续打卡天数
        /// Key:    checkin:{hostId}:days
        /// Type:   Hash
        /// Field:  {userId}
        /// Value:  {days}
        /// </summary>
        public static readonly string[] UserNewFansMedalCheckinDays = { RedisServer.RelationshipCluster, "checkin:{0}:days" };

        #endregion

        #region Feed订阅相关
        
        /// <summary>
        /// 用户订阅待处理列表
        /// member：json
        /// api/RoomSubscription/SubscribeRoom?roomId=14168
        /// api/RoomSubscription/SubscribeByRoomIds?roomIds=14168
        /// api/RoomSubscription/UnsubscribeRoom?roomId=14168
        /// </summary>
        public static readonly string[] FeedListSubUpdate = { RedisServer.Feed, "u:needdeal" };

        public static readonly string[] FeedSetPushUserIgnore = { RedisServer.PushService, "push:user:ignore:{0}" };

        public static readonly string[] FeedSetPushDisableAll = { RedisServer.PushService, "push:disable:all" };

        /// <summary>
        /// 用户设备标识查询
        /// key=userid:渠道（龙珠=1）:设备标识
        /// </summary>
        public static readonly string[] PushDeviceByUserSet = { RedisServer.PushService, "push:user:device:{0}" };

        /// <summary>
        ///设备推送结果
        /// key:    pushenum:devicetype
        /// field : objectid
        /// value : 1 成功，0不成功
        /// </summary>
        public static readonly string[] PushDeviceResult = { RedisServer.PushService, "push:result:{0}" };

        #endregion Feed订阅相关

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

        #region Log相关

        /// <summary>
        /// 记录用户观看视频历史记录{userId}
        /// 写mon，读api/UserBehavior/SyncViewHistory
        /// </summary>
        public static readonly string[] LogListViewHistroy = { RedisServer.Log, "u:view:history:{0}" };

        /// <summary>
        /// 消费Kafka赠送VIP时，存放txVersion
        /// member: txVersion
        /// </summary>
        public static readonly string[] FreeVipInfosLogSet = { RedisServer.Log, "vip:free:txversion" };

        /// <summary>
        /// 用户注册时，消费kafka，赠送改名卡道具，存放txVersion
        /// </summary>
        public static readonly string[] AccountRegisterGiveItemLogSet = { RedisServer.Log, "u:register:item:changenickname" };



        /// <summary>
        /// 消费Kafka合并PPTV账户数据时，存放txVersion
        /// </summary>
        public static readonly string[] PPTVUserJoinLogSet = { RedisServer.Log, "pptv:userjoin:txversion" };


        #region PPTV 账户合并

        /// <summary>
        /// 用户VIP数据合并时，存放已合并掉的VipInfosV2表Id
        /// </summary>
        public static readonly string[] UserVipJoinLogSet = { RedisServer.Log, "pptvmergelog:vip:id" };

        /// <summary>
        /// 用户Guard数据合并时，存放已合并掉的GuardInfos表Id
        /// </summary>
        public static readonly string[] UserGuardJoinLogSet = { RedisServer.Log, "pptvmergelog:guard:id" };

        /// <summary>
        /// 用户Grade数据合并时，存放已合并掉的Grade信息Id
        /// </summary>
        public static readonly string[] NewGradeJoinLogSet = { RedisServer.Log, "pptvmergelog:grade:id" };

        /// <summary>
        /// 用户龙币数据合并时，存放已合并掉的龙币信息Id
        /// </summary>
        public static readonly string[] LongbiJoinLogSet = { RedisServer.Log, "pptvmergelog:longbi:longbiid" };

        /// <summary>
        /// 用户道具库存合并时，存放已合并过的道具库存信息id
        /// </summary>
        public static readonly string[] ItemInventoryJoinLogSet = { RedisServer.Log, "pptvmergelog:iteminventory:id" };

        /// <summary>
        /// 合并账户数据后，赠送改名卡
        /// </summary>
        public static readonly string[] GiveChangeNickTenyuanLogSet = { RedisServer.Log, "pptvmergelog:changenicktenyuan:uid" };

        #endregion


        #endregion Log相关

        #region Live机器人相关

        /// <summary>
        /// 根据机器人策略得到的在线数值
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashOnlineCountByRobot = { RedisServer.Live, "r:online:robot" };

        /// <summary>
        /// 每次处理的变化数，含正负
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashOnlineChangeCountByRobot = { RedisServer.Live, "r:online:change:robot" };

        /// <summary>
        /// 机器人策略列表
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashRobotStrategy = { RedisServer.Live, "r:robot:strategy" };

        /// <summary>
        /// 最高在线值
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashOnlineLimitCountList = { RedisServer.Live, "r:online:limit:count" };

        /// <summary>
        /// 单房间最高系数人工限制
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashLimitCoefficients = { RedisServer.Live, "r:online:coefficients:count" };

        /// <summary>
        /// 机器人策略 房间开始结束时间
        /// field: roomId value: "2016-12-13|2016-12-14"
        /// </summary>
        public static readonly string[] LiveHashRobotStrategyPeriod = { RedisServer.Live, "r:robot:strategy:period" };

        #endregion Live机器人相关

        #region Live直播列表

        /// <summary>
        /// 直播状态
        /// </summary>
        public static readonly string[] LiveKeySuffixRoomLive = { RedisServer.Live, "r:live" };
        public static readonly string[] LiveKeySuffixUserLive = { RedisServer.Live, "u:live:list" };

        #endregion Live直播列表

        #region Live直播数据

        /// <summary>
        /// 最高平均码率，开播清理
        /// field：streamname value：rate
        /// </summary>
        public static readonly string[] LiveStreamHashAvgRate = { RedisServer.LiveStream, "r:stream:avgrate" };
        /// <summary>
        /// 网宿流队列
        /// field：roomId value：streamId
        /// </summary>
        public static readonly string[] LiveStreamHashRoomWsLiveSource = { RedisServer.LiveStream, "r:ws:livesource" };

        /// <summary>
        /// 七牛流队列
        /// field：roomId value：streamId
        /// </summary>
        public static readonly string[] LiveStreamHashRoomQiniuLiveSource = { RedisServer.LiveStream, "r:qiniu:livesource" };

        /// <summary>
        /// 金山流队列
        /// field：roomId value：streamId
        /// </summary>
        public static readonly string[] LiveStreamHashRoomKsyunLiveSource = { RedisServer.LiveStream, "r:ksyun:livesource" };

        /// <summary>
        /// 全民直播存储信息，用于记录聊天消息为历史消息
        /// field：roomId value：playId|mediaId|start
        /// </summary>
        public static readonly string[] LiveHashAppInfo = { RedisServer.Live, "a:live:info" };

        /// <summary>
        /// 全民直播 点赞数/开始时间{admire/begin}
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] LiveSortedSetAppInfo = { RedisServer.Live, "a:live:{0}" };

        /// <summary>
        /// 直播流码率队列
        /// </summary>
        public static readonly string[] LiveListStreamRate = { RedisServer.Live, "stream:rate:{0}" };

        /// <summary>
        /// 房间直播重播列表
        /// </summary>
        public static readonly string[] LiveSetStreamReplay = { RedisServer.Live, "r:stream:replay" };

        /// <summary>
        /// 重播房间的直播Id
        /// </summary>
        public static readonly string[] LiveHashRoomReplayStreamId = { RedisServer.LiveStream, "r:replay:streamid" };

        /// <summary>
        /// 房间播放器拉流类型列表
        /// </summary>
        public static readonly string[] LiveListRoomPlayLiveStreamType = { RedisServer.Live, "r:{0}:play:livestreamtype" };

        /// <summary>
        /// 房间播放器拉流类型列表
        /// </summary>
        public static readonly string[] LiveListRoomPushLiveStreamType = { RedisServer.Live, "r:{0}:push:livestreamtype" };

        /// <summary>
        /// 七牛流直播网络状况检测
        /// </summary>
        public static readonly string[] LiveHashQiniuStreamStatus = { RedisServer.Live, "r:qiniu:streamstatus" };


        /// <summary>
        ///金山云流直播网络状况检测
        /// </summary>
        public static readonly string[] LiveHashKsyunStreamStatus = { RedisServer.Live, "r:ksyun:streamstatus" };

        public static readonly string[] LiveHashLiveStreamConnected = { RedisServer.Live, "r:{0}:connected" };
        /// <summary>
        /// 房间在线数
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashOnlineCountV2 = { RedisServer.Live, "all:online:countv2" };
        public static readonly string[] CacheHashOnlineAvg = { RedisServer.Cache, "r:online:avg" };
        /// <summary>
        /// 房间在线峰值
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] LiveHashOnlineMaxCount = { RedisServer.Live, "all:online:maxcount" };
        /// <summary>
        /// 房间分游戏直播列表{gameId}
        /// member：roomId
        /// </summary>
        public static readonly string[] LiveSetLiveGame = { RedisServer.Live, "r:live:game:{0}" };
        /// <summary>
        /// 映射参与的活动
        /// </summary>
        public static readonly string[] LiveSetMessageMappingActivity = { RedisServer.Live, "r:live:msg:activity" };
        /// <summary>
        /// 需要转发送礼消息的映射roomid
        /// </summary>
        public static readonly string[] LiveHashGiftMsgMapping = { RedisServer.Live, "r:live:msgmap:{0}" };
        /// <summary>
        /// 需要转发送礼消息的映射roomid
        /// </summary>
        public static readonly string[] LiveHashChartMsgMapping = { RedisServer.Live, "r:live:msg:activity:{0}" };

        /// <summary>
        /// 分类型真实用户在线数量{_live_logon/_live/_logon/string.Empty/_app_logon/_app}
        /// </summary>
        public static readonly string[] LiveHashSuffixOnlineCount = { RedisServer.Live, "r:online:count{0}" };

        /// <summary>
        /// 分类型真实用户在线数量{_live_logon/_live/_logon/string.Empty/_app_logon/_app}(临时)
        /// </summary>
        public static readonly string[] LiveHashSuffixOnlineCountTemp = { RedisServer.Live, "r:online:temp:count{0}" };

        /// <summary>
        /// 截图-长方形
        /// </summary>
        public static readonly string[] LiveHashScreenshotPic = { RedisServer.Live, "live:screen:pic" };

        /// <summary>
        /// 截图-正方形
        /// </summary>
        public static readonly string[] LiveHashScreenshotPic2 = { RedisServer.Live, "live:screen:squarepic" };

        /// <summary>
        /// 随拍截图
        /// key:userid, value:地址
        /// </summary>
        public static readonly string[] LiveHashUserSquare = { RedisServer.Live, "live:screen:user:square" };

        /// <summary>
        /// 游戏截图
        /// key:userid, value:地址
        /// </summary>
        public static readonly string[] LiveHashUserScreen = { RedisServer.Live, "live:screen:user" };


        /// <summary>
        /// 收费房间输错密码时间
        /// key：liveId:userId, value:now()
        /// </summary>
        public static readonly string[] LivePrivateListLimitUserId = { RedisServer.Cache2, "r:live:private:{0}" };

        /// <summary>
        /// 世界飞屏
        /// key：room:flashscreen:, value:now()
        /// </summary>
        public static readonly string[] RoomFlashScreen = { RedisServer.Live, "room:flashscreen:{0}" };

        #endregion Live直播数据

        #region Property用户资产数据

        public static readonly string[] CacheHashCoinLastId = { RedisServer.Cache, "u:sync:last:id" };
        /// <summary>
        /// 用户龙币:{userId}
        /// value：值
        /// </summary>
        public static readonly string[] CommonClusterStringCoin = { RedisServer.CommonCluster, "u:coin:{0}" };

        /// <summary>
        /// 用户贵族币:{userId}
        /// value：值
        /// </summary>
        public static readonly string[] CommonClusterStringNobleCoin = { RedisServer.CommonCluster, "u:noble:{0}" };

        /// <summary>
        /// 用户龙豆
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] PropertyHashUserBean = { RedisServer.Property, "u:bean" };

        /// <summary>
        /// 用户勋章
        /// feild：userId value：roomId
        /// </summary>
        public static readonly string[] PropertyHashUserMedal = { RedisServer.Property, "u:medal:roomId" };

        /// <summary>
        /// 用户免费经验值
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] PropertyHashFreeExperience = { RedisServer.Property, "u:free:exp" };

        /// <summary>
        /// 用户付费经验值
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] PropertyHashFeeExperience = { RedisServer.Property, "u:fee:exp" };

        #endregion Property用户资产数据

        #region Profile用户相关数据

        /// <summary>
        /// 用户等级
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] ProfileHashUserNewGrade = { RedisServer.Profile, "u:newgrade" };

        /// <summary>
        /// 有效用户集合
        /// value:userId
        /// </summary>
        public static readonly string[] ProfileSetUserValid = { RedisServer.Profile, "u:valid" };

        /// <summary>
        /// 用户安全等级
        /// feild：userId value：值
        /// </summary>
        public static readonly string[] ProfileHashUserSecurityLevel = { RedisServer.Security, "user:security:level" };


        #endregion

        #region CoolDown用户cd相关

        /// <summary>
        /// 用户免费道具库存累计{itemName}
        /// field:userId value:count
        /// </summary>
        public static readonly string[] CoolDownHashItemInventory = { RedisServer.CoolDown, "item:{0}:inventory" };

        public static readonly string[] CoolDownStringMessageInteval = { RedisServer.CoolDown, "u:msg:interval:{0}" };

        /// <summary>
        /// 连击序号
        /// </summary>
        public static readonly string[] CoolDownHashUserSendItemComboId = { RedisServer.CoolDown, "u:item:{0}:comboid" };
        /// <summary>
        /// 道具连击
        /// </summary>
        public static readonly string[] CoolDownHashUserSendItemCombo = { RedisServer.CoolDown, "u:item:{0}:combo" };

        public static readonly string[] CoolDownHashUserBoxLastPost = { RedisServer.CoolDown, "u:box:last:post" };
        public static readonly string[] CoolDownHashUserBoxTime = { RedisServer.CoolDown, "u:box:times" };
        public static readonly string[] CoolDownHashUserBoxLastPostV2 = { RedisServer.CoolDown, "u:box2:last:post" };
        public static readonly string[] CoolDownHashUserBoxTimeV2 = { RedisServer.CoolDown, "u:box2:times" };

        public static readonly string[] CoolDownHashUserNhTime = { RedisServer.CoolDown, "u:times:{0}" };

        public static readonly string[] CoolDownHashUsercfmTime = { RedisServer.CoolDown, "u:cfm:times" };
        public static readonly string[] CoolDownHashUserkplTime = { RedisServer.CoolDown, "u:kpl:times" };

        public static readonly string[] CoolDownHashUserCdTime = { RedisServer.CoolDown, "u:cd:times" };
        public static readonly string[] CoolDownHashUserDrawChanceTime = { RedisServer.CoolDown, "u:drawchance:times" };

        public static readonly string[] CoolDownHashSunnyDollTime = { RedisServer.CoolDown, "u:sunny:times" };

        public static readonly string[] CoolDownHashUserCslattackTime = { RedisServer.CoolDown, "u:cslattack:times" };

        #endregion CoolDown用户cd相关

        #region Vector系统相关

        public static readonly string[] VectorHashDomainAllocation = { RedisServer.Vector, "r:domain:alloc" };
        public static readonly string[] VectorSetGoodDomain = { RedisServer.Vector, "r:good:domain" };

        #endregion Vector系统相关

        #region Vector道具相关

        //UserContributionHandler文件

        /// <summary>
        /// 某个房间的用户粉丝总值{roomId}
        /// field:userId value:count
        /// </summary>
        public static readonly string[] VectorHashRoomUserTotalFan = { RedisServer.Vector, "r:fan:{0}" };

        /// <summary>
        /// 用户贡献总值
        /// field:userId value:count
        /// </summary>
        public static readonly string[] VectorHashUserTotalContribution = { RedisServer.Vector, "u:contribution" };

        /// <summary>
        /// 房间贡献总值
        /// field:roomId value:count
        /// </summary>
        public static readonly string[] VectorHashRoomTotalContribution = { RedisServer.Vector, "r:contribution" };

        //UserContributionHandler文件

        /// <summary>
        /// 1.房间分天用户贡献值产生的房间数据{yyyyMMdd}
        /// member:roomId
        /// </summary>
        public static readonly string[] ItemSetRoom = { RedisServer.Item, "zr:contribution:{0}" };
        /// <summary>
        /// 2.用户贡献值当日总榜{yyyyMMdd}
        /// member:roomId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomDayContribution = { RedisServer.Item, "zr:r:{0}:contribution" };
        /// <summary>
        /// 3.用户贡献值当日分房间榜{yyyyMMdd}:{roomId}
        /// member:userId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomUserDayContribution = { RedisServer.Item, "zr:u:contribution:{0}" };
        /// <summary>
        /// 4.用户经验值当日分房间榜{yyyyMMdd}:{roomId}
        /// member:userId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomUserDayExperience = { RedisServer.Item, "experience:{0}" };
        /// <summary>
        /// 5.用户经验值当日总榜{yyyyMMdd}
        /// member:roomId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomDayExperience = { RedisServer.Item, "experience:total:{0}" };
        /// <summary>
        /// 6.用户经验值分房间每场榜:{roomId}
        /// member:userId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomUserSessionExperience = { RedisServer.Item, "experience:session:{0}" };
        /// <summary>
        /// 7.用户经验值每场榜
        /// rank:roomId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomSessionExperience = { RedisServer.Item, "experience:session:total" };
        /// <summary>
        /// 8.房间贡献值每场榜
        /// </summary>
        public static readonly string[] ItemSortedSetRoomSessionContribution = { RedisServer.Item, "contribution:session:total" };

        //UsingLogHandler文件

        /// <summary>
        /// 当天指定房间指定道具每个用户的赠送数量 {yyyyMMdd}:{roomId}:{itemId}
        /// field:userId value:count
        /// </summary>
        public static readonly string[] ItemHashItemUsingLog = { RedisServer.Item, "r:item:using:{0}" };
        /// <summary>
        /// 当天哪些道具有赠送记录 {yyyyMMdd}
        /// member:itemId
        /// </summary>
        public static readonly string[] ItemSetItemIdLog = { RedisServer.Item, "r:using:itemid:{0}" };
        /// <summary>
        /// 当天指定道具哪些房间有赠送记录 {yyyyMMdd}:{itemId}
        /// member:roomId
        /// </summary>
        public static readonly string[] ItemSetItemIdRoomIdLog = { RedisServer.Item, "r:using:roomid:{0}" };
        /// <summary>
        /// 当天指定道具房间赠送记录 {yyyyMMdd}:{itemId}
        /// field:roomId value:count
        /// </summary>
        public static readonly string[] ItemHashItemIdRoomIdLog = { RedisServer.Item, "r:using:roomid:{0}:count" };
        /// <summary>
        /// 当周周星道具配置
        /// member:itemId
        /// </summary>
        public static readonly string[] ItemSetItemId = { RedisServer.Item, "weekstar:{0}" };
        /// <summary>
        /// 当周指定房间每个道具赠送数量榜 {week}:{roomId}
        /// member:itemId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetRoomIdItemId = { RedisServer.Item, "weekstar:roomid:{0}" };
        /// <summary>
        /// 当周指定道具每个房间赠送数量榜 {week}:{itemId}
        /// member:roomId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetItemIdRoomId = { RedisServer.Item, "weekstar:itemid:{0}" };
        /// <summary>
        /// 当周指定用户每个道具赠送数量榜 {week}:{userId}
        /// member:itemId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetUserIdItemId = { RedisServer.Item, "weekstar:userid:{0}" };
        /// <summary>
        /// 当周指定道具每个用户赠送数量榜 {week}:{itemId}
        /// member:userId score:count
        /// </summary>
        public static readonly string[] ItemSortedSetItemIdUserId = { RedisServer.Item, "weekstar:itemiduserid:{0}" };

        /// <summary>
        /// 房间赠送道具累计{itemName}
        /// field:roomId value:count
        /// todo: 迁出vector
        /// </summary>
        public static readonly string[] VectorHashItemCounter = { RedisServer.Vector, "item:{0}:count" };

        /// <summary>
        /// 用户付费道具库存累计{itemName}
        /// todo: 迁出vector
        /// field:userId value:count
        /// </summary>
        public static readonly string[] VectorHashItemInventory = { RedisServer.Vector, "item:{0}:inventory" };

        /// <summary>
        /// 用户道具累计{itemName}
        /// field:userId value:count
        /// todo: 迁出vector
        /// </summary>
        public static readonly string[] VectorHashItemTotal = { RedisServer.Vector, "item:{0}:total" };

        /// <summary>
        /// 生日弹幕存储{roomId}
        /// rank:json score:timestamp
        /// </summary>
        public static readonly string[] ItemSortedSetRoomBirthDayItem = { RedisServer.Item, "room:item:birthday:{0}" };

        #endregion Vector道具相关

        #region Vector房间数据

        public static readonly string[] VectorHashRoomGiftExperience = { RedisServer.Vector, "r:gift:exp" };
        public static readonly string[] VectorHashRoomLiveExperience = { RedisServer.Vector, "r:live:exp" };
        public static readonly string[] VectorHashRoomGrade = { RedisServer.Vector, "r:grade" };
        public static readonly string[] VectorHashRoomMedal = { RedisServer.Vector, "roomId:medal" };
        #endregion Vector房间数据

        #region Vector房间数据缓存

        /// <summary>
        /// 分组对应Tag{collectionId}
        /// member:tagName
        /// </summary>
        public static readonly string[] VectorSetTagCollectionMapping = { RedisServer.Vector, "tag:collection:{0}" };

        /// <summary>
        /// 分组对应Tag{collectionId}
        /// member:tagName
        /// </summary>
        public static readonly string[] VectorSetTagSortedCollectionMapping = { RedisServer.Vector, "tag:sortedCollection:{0}" };

        /// <summary>
        /// 分类对应Tag{categoryId}
        /// member:tagName
        /// </summary>
        public static readonly string[] VectorSetTagCategoryMapping = { RedisServer.Vector, "tag:category:{0}" };

        /// <summary>
        /// 对象类型Tag被打次数{itemtype}
        /// field：tagId value：count
        /// </summary>
        public static readonly string[] VectorHashTagCountByItemType = { RedisServer.Vector, "tag:{0}:count" };

        /// <summary>
        /// 对象类型对应Tag{itemtype}
        /// member:tagName
        /// </summary>
        public static readonly string[] VectorSetTagConfig = { RedisServer.Vector, "tag:{0}:member" };

        public static readonly string[] VectorHashRoomInfo = { RedisServer.Cache, "room:{0}" };
        public static readonly string[] VectorHashDomainRoomId = { RedisServer.Vector, "r:domain:id" };
        public static readonly string[] VectorHashRoomIdDomain = { RedisServer.Vector, "r:id:domain" };
        public static readonly string[] VectorHashDomainUserId = { RedisServer.Vector, "u:domain:id" };
        public static readonly string[] VectorHashUserIdDomain = { RedisServer.Vector, "u:id:domain" };
        public static readonly string[] VectorHashUserIdRoomId = { RedisServer.Vector, "user:room" };
        public static readonly string[] VectorHashRoomIdUserId = { RedisServer.Vector, "room:user" };
        public static readonly string[] VectorHashUserIdUserTitle = { RedisServer.Vector, "users:titles" };
        public static readonly string[] VectorHashStreamIdRoomId = { RedisServer.Vector, "streamid:roomId" };

        public static readonly string[] VectorHashRoomLiveTotalSeconds = { RedisServer.Vector, "playtime" };
        public static readonly string[] VectorHashRoomPercentageConcurrent = { RedisServer.Cache, "room:percentage:concurrent" };

        public static readonly string[] VectorHashRoomTag = { RedisServer.Vector, "room:roomTag" };

        /// <summary>
        /// 房间的推荐tab信息
        /// key:roomid
        /// field:recommendroomid
        /// value:recommendname
        /// </summary>
        public static readonly string[] VectorHashRoomTabRecommend = { RedisServer.Vector, "room:{0}:tab:recommend" };

        #endregion Vector房间数据缓存

        #region Vector视频相关缓存

        #endregion Vector视频相关缓存

        #region Vector用户VIP数据

        /// <summary>
        /// 用户VIP信息
        /// field: userId value:{viptype},{expireTicks}
        /// </summary>
        public static readonly string[] VectorHashVipInfo = { RedisServer.Vector, "vip:info" };

        #endregion

        #region Vector幸运礼物奖金池

        public static readonly string[] CacheStringLuckyGiftBonus = { RedisServer.Cache, "luckygift:bonus" };

        #endregion

        #region Vector 老版体育相关

        /// <summary>
        /// 房间对阵信息
        /// field：roomId value：json
        /// </summary>
        public static readonly string[] VectorHashSportRoomVsInfo = { RedisServer.Vector, "sport:room:vsinfo" };

        /// <summary>
        /// 用户阵营信息{roomId}
        /// field：userId value：roomId
        /// </summary>
        public static readonly string[] VectorHashSportRoomUser = { RedisServer.Vector, "sport:room:{0}:user" };

        /// <summary>
        /// 阵营(Team)积分
        /// field：roomId value：count
        /// </summary>
        public static readonly string[] VectorHashSportRoomPoint = { RedisServer.Vector, "sport:room:points" };

        /// <summary>
        /// 排行榜{roomId}
        /// rank：userId score：count
        /// </summary>
        public static readonly string[] VectorSortedSetSportRoomRank = { RedisServer.Vector, "sport:room:{0}:rank" };

        /// <summary>
        /// 体育房间回放房间标题
        /// field: roomId value: title
        /// </summary>
        public static readonly string[] VectorHashSportRoomPlayBackTitle = { RedisServer.Vector, "sport:room:playback:title" };

        /// <summary>
        /// 房间开播比赛
        /// field: roomId value: matchId
        /// </summary>
        public static readonly string[] VectorHashSportRoomMatch = { RedisServer.Vector, "sport:room:match" };

        /// <summary>
        /// 比赛队伍分数
        /// field: matchId value: score
        /// </summary>
        public static readonly string[] VectorHashSportMatchScore = { RedisServer.Vector, "sport:match:score" };

        /// <summary>
        /// 是否已经切换过体育对阵信息
        /// </summary>
        public static readonly string[] VectorSetSportSwitchTemplate = { RedisServer.Vector, "sport:switch:template" };

        /// <summary>
        /// 可以送道具的场次id
        /// </summary>
        public static readonly string[] VectorSetSportSendGiftMatchIds = { RedisServer.Vector, "sport:sendgift:matchids" };

        #endregion

        #region Sport 新版体育相关

        /// <summary>
        /// 房间对阵信息
        /// field：roomId value：json
        /// </summary>
        public static readonly string[] SportHashSportV2RoomVsInfo = { RedisServer.Sport, "sportv2:room:vsinfo" };

        /// <summary>
        /// 用户阵营信息{teamId}
        /// field：userId value：teamId
        /// </summary>
        public static readonly string[] SportHashSportV2RoomUser = { RedisServer.Sport, "sportv2:room:{0}:user" };

        /// <summary>
        /// 房间双方阵营总积分{roomId}
        /// field：teamId value：count
        /// </summary>
        public static readonly string[] SportHashSportV2TeamPoint = { RedisServer.Sport, "sportv2:team:points:{0}" };

        /// <summary>
        /// 房间阵营排行榜{roomId-teamId}
        /// rank：userId score：count
        /// </summary>
        public static readonly string[] SportSortedSetSportV2RoomRank = { RedisServer.Sport, "sportv2:room:{0}:rank" };

        /// <summary>
        /// 比赛队伍分数
        /// field: matchId value: score
        /// </summary>
        public static readonly string[] SportHashSportMatchV2Score = { RedisServer.Sport, "sportv2:match:score" };

        /// <summary>
        /// 顶部直播间导流
        /// field: matchId value: json
        /// </summary>
        public static readonly string[] SportHashSportV2TopRoomRecommend = { RedisServer.Sport, "sportv2:toproomrecommend:rank" };

        /// <summary>
        /// 体育房间slogan
        /// </summary>
        public static readonly string[] SportHashSportV2RoomSlogan = { RedisServer.Sport, "sportv2:roomslogan:rank" };

        /// <summary>
        /// 当前有效赛事id
        /// member: matchId
        /// </summary>
        public static readonly string[] SportSetSportV2ValidMatchIds = { RedisServer.Sport, "sportv2:sendgift:matchids" };

        /// <summary>
        /// 一个赛事id下直播的roomid列表 {matchId}
        /// member: roomId
        /// </summary>
        public static readonly string[] SportSetSportRoomIdsInMatchId = { RedisServer.Sport, "sport:roomids:matchid:{0}" };

        /// <summary>
        /// 以赛事Id为维度，存储赛事下有哪些主播预约
        /// field: roomId，value：{"gameId":"","sortId":"","uid":"","subscribeDate":""}
        /// </summary>
        public static readonly string[] SportHashMatchSubscribeHostId = { RedisServer.Sport, "sportv2:match:subscribe:{0}" };

        /// <summary>
        /// 以roomId为维度，存放该房间预约的赛事
        /// member: matchId
        /// </summary>
        public static readonly string[] SportSetRoomSubscribeGameId = { RedisServer.Sport, "sportv2:room:subscribe:{0}" };

        /// <summary>
        /// 一个matchid关联一个官方房间id
        /// field: matchId value: json
        /// </summary>
        public static readonly string[] SportHashSportV2OfficialRoomByMatchId = { RedisServer.Sport, "sportv2:official:matchid" };
        /// <summary>
        /// 一个官方房间id关联1个或者多个matchid
        /// </summary>
        public static readonly string[] SportHashSportV2MatchIdByOfficialRoom = { RedisServer.Sport, "sportv2:matchid:official:{0}" };


        /// <summary>
        /// 体育回放信息
        /// keySuffix：matchId
        /// field：roomId
        /// value：json
        /// </summary>
        public static readonly string[] SportHashSportV2Replay = { RedisServer.Sport, "sportv2:replay:match:{0}" };


        /// <summary>
        /// 域名重定向信息
        /// field：fromdomain
        /// value：json
        /// </summary>
        public static readonly string[] DomainRedirection = { RedisServer.Sport, "sportv2:domainredirection:fromdomain" };

        /// <summary>
        /// 体育主播推荐
        /// key:主播类型
        /// field：id
        /// value：json
        /// </summary>
        public static readonly string[] SportStarRecommend = { RedisServer.Sport, "sportv2:sportstarrecommend:fromid" };

        /// <summary>
        /// 体育直播权限
        /// keySuffix：matchId
        /// field：roomid
        /// value：channels
        /// </summary>
        public static readonly string[] SportLivePermissions = { RedisServer.Sport, "sportv2:sportlivepermissions:match:{0}" };

        /// <summary>
        /// 体育比赛直播分成比例
        /// field：matchid
        /// value：json
        /// </summary>
        public static readonly string[] SportMatchProportion = { RedisServer.Sport, "sportv2:sportmatchproportion:matchs" };

        /// <summary>
        /// 体育预约信息
        /// keySuffix：type:matchid  MatchSubscribe = 0, RoomSubscribe = 1
        /// field：userid
        /// value：
        /// </summary>
        public static readonly string[] SportSubscribe = { RedisServer.Sport, "sportv2:sportsubscribe:{0}" };

        #endregion

        #region Identity守护相关

        /// <summary>
        /// 用户VIP信息 （单用户多角色，）
        /// key: vip:infos
        /// field: uid
        /// value: "{1:{"expire":144444},2:{"expire":33333}}"
        /// </summary>
        public static readonly string[] IdentityHashVipInfo = { RedisServer.Identity, "vip:infos" };



        /// <summary>
        /// 房间守护信息
        /// field: userId
        /// </summary>
        public static readonly string[] IdentityHashUserGuardsInfo = { RedisServer.Identity, "guard:{0}" };

        /// <summary>
        /// 房间守护榜
        /// {0}:roomId
        /// field: userId
        /// </summary>
        public static readonly string[] IdentitySortedSetRoomGuardRank = { RedisServer.Identity, "guard:room:{0}:rank" };

        /// <summary>
        /// 房间守护榜锁
        /// {0}:roomId
        /// </summary>
        public static readonly string[] IdentityStringRoomGuardRankLocker = { RedisServer.Identity, "guard:room:{0}:rank:locker" };

        /// <summary>
        /// 守护补偿的进度
        /// </summary>
        public static readonly string[] IdentityStringGuardCompensatePostion = { RedisServer.Identity, "guard:cp" };

        /// <summary>
        /// 修正守护补偿的进度
        /// </summary>
        public static readonly string[] IdentityStringGuardFixCompensatePostion = { RedisServer.Identity, "guard:fixed" };


        /// <summary>
        /// field:roomId，value:{count:5}
        /// </summary>
        public static readonly string[] IdentityHashGuardRankOnline = { RedisServer.Identity, "guard:room:rank:online" };

        #endregion

        #region Identity座驾相关
        public static readonly string[] IdentityHashUserVehicleInfo = { RedisServer.Identity, "vehicle" };

        /// <summary>
        /// 房间座驾列表锁
        /// {0}:roomId
        /// </summary>
        public static readonly string[] IdentityStringRoomVehicleLocker = { RedisServer.Identity, "vehicle:room:{0}:locker" };
        #endregion

        #region MessageBus相关

        /// <summary>
        /// 聊天消息
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] MessageBusSortedSetQueueMessage = { RedisServer.MessageBus, "rz:{0}:mq" };

        /// <summary>
        /// 道具消息
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] MessageBusSortedSetPrimaryQueueMessage = { RedisServer.MessageBus, "rz:primary:{0}:mq" };

        /// <summary>
        /// 历史栏队列送道具
        /// </summary>
        public static readonly string[] MessageBusListGiftHistory = { RedisServer.MessageBus, "r:{0}:receive:gift:history" };
        /// <summary>
        /// 送道具时间
        /// </summary>
        public static readonly string[] MessageBusListGiftCostTime = { RedisServer.MessageBus, "r:receive:gift:costTime" };
        /// <summary>
        /// 送道具头条
        /// </summary>
        public static readonly string[] MessageBusStringHeadlineGift = { RedisServer.MessageBus, "r:receive:gift:headline" };
        /// <summary>
        /// 送道具轮询
        /// </summary>
        public static readonly string[] MessageBusListRollingGift = { RedisServer.MessageBus, "r:receive:gift:rolling" };
        /// <summary>
        /// 历史送红包
        /// rank：roomId score：count
        /// </summary>
        public static readonly string[] MessageBusSortedSetRedHistory = { RedisServer.MessageBus, "r:group:msg:historybag:{0}" };

        /// <summary>
        /// 聊天Id {yyyyMMddHH}
        /// field：roomId value：id
        /// </summary>
        public static readonly string[] ChatHashMessageId = { RedisServer.Chat, "mbid:{0}" };

        #endregion MessageBus相关

        #region PvUv相关

        /// <summary>
        /// pv累计统计{yyMMdd}
        /// 写操作mon，api/TGP/AddMediaCount,发礼物和消息，读操作点播页视频播放数和api/TGAHomeData/GetVideoInfo接口
        /// </summary>
        public static readonly string[] PvUvHashRoomPvCounter = { RedisServer.PvUv0, "room:pvcounter:{0}" };

        /// <summary>
        /// 分时段分房间分类型用户列表{_live_logon/_live/_logon/string.Empty/_app_logon/_app}:{roomId}:{TimeStamp}
        /// </summary>
        public static readonly string[] PvUvSetOnlineSet = { RedisServer.PvUv0, "r:online:users{0}" };

        #endregion PvUv相关

        #region 在线相关

        /// <summary>
        /// 整站在线用户统计{category}:{timeStamp}
        /// </summary>
        public static readonly string[] WholeSiteSetWholeSiteUser = { RedisServer.WholeSite, "site:online:users:{0}" };

        /// <summary>
        /// 整站在线个数统计{category}
        /// </summary>
        public static readonly string[] WholeSiteStringWholeSiteCount = { RedisServer.WholeSite, "site:online:count:{0}" };

        #endregion 在线相关

        #region 视频相关

        /// <summary>
        /// 视频-顶
        /// </summary>
        public static readonly string[] VectorHashMediaAdmire3 = { RedisServer.Vector, "admire:3:all" };

        /// <summary>
        /// 视频动态列表
        /// </summary>
        public static readonly string[] VectorHashMediaRtk = { RedisServer.Vector, "media:rtk" };

        /// <summary>
        /// 
        /// </summary>
        public static readonly string[] CacheHashHotCategory = { RedisServer.Cache, "media:hotcategory" };

        /// <summary>
        /// 
        /// </summary>
        public static readonly string[] VectorListGiftBackup = { RedisServer.Vector, "giftkey:increase" };

        #endregion 视频相关

        #region 充值活动

        /// <summary>
        /// 参与充值活动用户列表 rechargeActivity:join:{RechargeActivityId} value[用户列表]
        /// </summary>
        public static readonly string[] RechargeActivityJoin = { RedisServer.Mission, "rechargeActivity:join:{0}" };

        /// <summary>
        /// 首次充值用户
        /// </summary>
        public static readonly string[] RechargeFirst = { RedisServer.Cache, "recharge:first" };

        #endregion

        #region 商城道具

        /// <summary>
        /// 商城道具锁
        /// Key:    sp:lock:{type}:{uid}
        /// Value:  lock
        /// </summary>
        public static readonly string[] ShopPackageLock = { RedisServer.Cache2, "sp:lock:{0}" };


        #endregion

        /// <summary>
        /// 广告投放房间记录 -- roomid
        /// </summary>
        public static readonly string[] VectorHashAdvertiseTargetRooms = { RedisServer.Vector, "Advertise:target:room:{0}" };

        /// <summary>
        /// 房间广告管理 -- id
        /// </summary>
        public static readonly string[] VectorHashRoomAdvertise = { RedisServer.Vector, "RoomAdvertise:id:new" };

        /// <summary>
        /// App活动记录 -- id
        /// </summary>
        public static readonly string[] CacheHashAppActivity = { RedisServer.Cache, "AppActivity:id:{0}" };

        /// <summary>
        /// 用户信息修改通知（nickname,photo）
        /// </summary>
        public static readonly string[] CacheListUserInfoUpdate = { RedisServer.Cache, "userinfo:update" };

        /// <summary>
        /// 未开直播九宫格 -- 第一页插入信息 -- position
        /// </summary>
        public static readonly string[] VectorHashNineCustomCells = { RedisServer.Vector, "NineCustomCells:position:{0}" };


        /// <summary>
        /// 月度配额（次月） -- userid
        /// </summary>
        public static readonly string[] VectorHashMonthlyQuotas = { RedisServer.Vector, "monthlyquotas:userid" };

        public static readonly string[] SouHotHashLastUpdateUserInfo = { RedisServer.Cache, "souhot:lastupdate:user" };


        #region 机构信息
        /// <summary>
        /// 用户机构
        /// hash field:uid value: familyId
        /// </summary>
        public static readonly string[] FamilyHashUserFamily = { RedisServer.Family, "u:family" };

        /// <summary>
        /// 房间机构信息
        /// field:roomId value: familyId
        /// </summary>
        public static readonly string[] FamilyHashRoomFamily = { RedisServer.Family, "room:family" };

        #endregion

        #region 黑产OpenId

        /// <summary>
        /// 黑产OpenId{黑产OpenId}
        /// value：UId
        /// </summary>
        public static readonly string[] BlackAvatarSetOpenId2Uids = { RedisServer.BlackAvatar, "r:blackuser:{0}" };

        /// <summary>
        /// 黑产头像MD5
        /// value：Md5
        /// </summary>
        public static readonly string[] BlackAvatarSetMd5s = { RedisServer.BlackAvatar, "r:blackavatar" };

        /// <summary>
        /// 头像MD5与UID {Md5}
        /// value：uids
        /// </summary>
        public static readonly string[] BlackAvatarSetMd5ToUids = { RedisServer.BlackAvatar, "r:avatar:{0}" };

        #endregion BlackList房间黑名单

        #region 房间实时流切换

        /// <summary>
        /// field：original domain value：target domain
        /// </summary>
        public static readonly string[] RoomChangeStreamHashODomainTDomain = { RedisServer.LiveStream, "changestreams:originaldomain:targetdomain" };

        /// <summary>
        /// field：target domain  value：original domain 
        /// </summary>
        public static readonly string[] RoomChangeStreamHashTDomainODomain = { RedisServer.LiveStream, "changestreams:targetdomain:originaldomain" };

        #endregion

        #region 用户关系

        public static readonly string[] RelationshipSortedSetUserStar = { RedisServer.Relationship, "r:{0}:stars" };
        public static readonly string[] RelationshipHashUserFansCount = { RedisServer.Relationship, "r:fanscount" };

        #endregion

        #region 嗨播节目单

        /// <summary>
        /// PPTV节目源
        /// </summary>
        public static readonly string[] PPLiveProgram = { RedisServer.Cache, "r:ppliveprogram:type:{0}" };

        /// <summary>
        /// 房间转播列表
        /// </summary>
        public static readonly string[] BroadcastProgram = { RedisServer.Cache, "ppliveprogram:roomid:{0}" };

        #endregion 嗨播节目单

        #region 腾讯云回调

        /// <summary>
        /// 腾讯云转码完成
        /// </summary>
        public static readonly string[] QCloudTranscodeStringFileIdDefinition = { RedisServer.Live, "qcloud:transcode:{0}" };

        /// <summary>
        /// 腾讯云图片采集完成
        /// </summary>
        public static readonly string[] QCloudSampleSnapshotStringFileId = { RedisServer.Live, "qcloud:samplesnapshot:{0}" };


        #endregion

        /// <summary>
        /// 直播回放播放N时长后可保存
        /// </summary>
        public static readonly string[] ReplayVideoStringMiniSaveTime = { RedisServer.Live, "r:replay:time" };

        #region 系统公告

        /// <summary>
        /// 系统公告
        /// </summary>
        public static readonly string[] CacheHashAnnouncement = { RedisServer.Cache, "announcement" };

        /// <summary>
        /// 系统公告，需要终止的公告
        /// </summary>
        public static readonly string[] CacheSetAnnouncementCancel = { RedisServer.Cache, "announcement:cancel:set" };

        #endregion

        #region 微信提醒
        /// <summary>
        /// 通知微信客服（PHP（KK）那边负责消费） 
        /// 当前包含业务：1.体育房间检测出盗屏通知客服 
        /// </summary>
        public static readonly string[] WxNotifications = { RedisServer.Log, "wx:notifications" };

        #endregion

        #region PK

        /// <summary>
        /// 排行榜主播总分：记录排行榜pkid场次的总分，在pk期间每送一个龙币道具会加相应的总分。
        /// pk:score:{PkId}:{RoomId}
        /// </summary>
        public static readonly string[] PkScore = { RedisServer.ActivityCluster, "pk:score:{0}" };

        /// <summary>
        /// 排行榜主播榜： 记录pk期间，用户送礼的排行榜。
        /// pk:rank:{PkId}:{RoomId}	
        /// field:userid
        /// </summary>
        public static readonly string[] PkRank = { RedisServer.ActivityCluster, "pk:rank:{0}" };

        /// <summary>
        /// pk邀请 主播发出的邀请都会记录到池中。包含邀请时间。
        /// type:   string
        /// key:    pk:invite:{uid}:{frienduid}
        /// value:  {time}
        /// </summary>
        public static readonly string[] PkInvite = { RedisServer.ActivityCluster, "pk:invite:{0}" };

        /// <summary>
        /// pk对手信息 pk开始会加入
        /// pk:userroom:{roomId}
        /// userid：123123
        /// pkid：123
        /// </summary>
        public static readonly string[] PkRoomUser = { RedisServer.ActivityCluster, "pk:userroom:{0}" };

        /// <summary>
        /// 执行中的pk pk 整体结束之后删除
        /// </summary>
        public static readonly string[] PkExecuting = { RedisServer.ActivityCluster, "pk:executing" };

        /// <summary>
        /// pk 心跳信息 3分钟后过期
        /// </summary>
        public static readonly string[] PkHeartBeat = { RedisServer.ActivityCluster, "pk:heartbeat:{0}" };

        /// <summary>
        /// pk被邀请 主播接受到邀请都会记录到池中。包含被邀请时间。
        /// </summary>
        public static readonly string[] PkReceive = { RedisServer.ActivityCluster, "pk:receive:{0}" };

        /// <summary>
        /// 拒绝邀请列表
        /// </summary>
        public static readonly string[] PkRejectInvite = { RedisServer.ActivityCluster, "pk:rejectinvite" };

        /// <summary>
        /// 大乱斗
        /// key:	pk:melee
        /// type:	hash
        /// field:	{uid}
        /// value:	{userpkinfo}   -- uid,grade,time
        /// </summary>
        public static readonly string[] PkMelee = { RedisServer.ActivityCluster, "pk:melee" };

        /// <summary>
        /// PK状态机
        /// key:	pk:status:{uid}
        /// type:	string
        /// value:  {status},{CreteTime},{PkId}    none, melee, prepare, ready, start, review
        /// 
        /// 额外: 
        /// key:    pk:status:room:{roomid}
        /// type:   string
        /// value:  {status}    房间PK状态,0:未PK, 1:准备PK, 2:正在PK, 3:正在交流	
        /// </summary>
        public static readonly string[] PkStatus = { RedisServer.ActivityCluster, "pk:status:{0}" };

        /// <summary>
        /// 正在PK的比赛   --PK结束后删除
        /// key:	pk:matching
        /// type:	hash
        /// field:	{pkId}
        /// value:	{startTimeSpan}:{endTimeSpan}
        /// </summary>
        public static readonly string[] PkMatching = { RedisServer.ActivityCluster, "pk:matching" };

        /// <summary>
        /// PK信息(根据pkid 获取所有主播roomid等)
        /// key:	pk:room:{pkId}
        /// type:	hash
        /// field:	{roomId}
        /// value:	{userId}
        /// </summary>
        public static readonly string[] PkRoom = { RedisServer.ActivityCluster, "pk:room:{0}" };

        /// <summary>
        /// Pk记录
        /// key:    pk:record:{pkId}
        /// type:   string
        /// value:  {json} 字段同record表
        /// </summary>
        public static readonly string[] PkRecord = { RedisServer.ActivityCluster, "pk:record:{0}" };

        /// <summary>
        /// Pk计划任务
        /// key:	pk:plan:{time}    time:yyyyMMddHH   每小时一个Key
        /// type:	sortset
        /// value:	{json}
        /// score:	{time}计划时间戳
        /// {join}:	pkId,type(任务类型),userId
        /// </summary>
        public static readonly string[] PkPlan = { RedisServer.ActivityCluster, "pk:plan:{0}" };

        /// <summary>
        /// Pk锁
        /// type:   string
        /// </summary>
        public static readonly string[] PkLock = { RedisServer.Cache2, "pk:lock:{0}" };

        /// <summary>
        /// Pk锁
        /// type:   string
        /// </summary>
        public static readonly string[] PkGiftHistory = { RedisServer.ActivityCluster, "pk:history:{0}" };

        #endregion

        #region P2P

        /// <summary>
        /// PtoP 更新时间
        /// </summary>
        public static readonly string[] PtoPTimestamp = { RedisServer.Cache, "ptop:timestamp" };

        #endregion

        #region 免流量卡

        /// <summary>
        /// 免流红点显示状态
        /// </summary>
        public static readonly string[] FlowcardRedpointDisplayInfo = { RedisServer.Cache, "flowcard:redpoint:display：info" };

        /// <summary>
        /// 免流量卡列表刷新时间
        /// </summary>
        public static readonly string[] FlowcardListRefreshtime = { RedisServer.Cache, "flowcard:list:refreshtime" };

        #endregion

        #region 体育竞猜

        public static readonly string[] LotteryListHash = { RedisServer.SportLottery, "lotterylist:{0}" };

        public static readonly string[] BetClubMapHash = { RedisServer.SportLottery, "betclub" };

        public static readonly string[] BetQuizeString = { RedisServer.SportLottery, "bet:quize:{0}" };

        public static readonly string[] BetRankSortedSet = { RedisServer.ActivityCluster, "bet:rank:{0}" };

        #endregion

        /// <summary>
        /// 转码配置刷新时间
        /// </summary>
        public static readonly string[] TranscodeSettingRefreshtime = { RedisServer.Cache, "transcodeSet:refreshtime" };

        /// <summary>
        /// 流调度更新时间
        /// </summary>
        public static readonly string[] StreamDispatchTimestamp = { RedisServer.Cache, "streamdispatch:timestamp" };

        /// <summary>
        /// Gmw封停更新时间
        /// </summary>
        public static readonly string[] GmwForceOutTimestamp = { RedisServer.Cache, "gmw:timestamp" };

        #region 关播后推荐  LiveCluster

        /// <summary>
        /// 主播设置关播后推荐的房间 RELR:RoomEndLiveRecommend 的缩写；HS：hostSet
        /// </summary>
        public static readonly string[] EndLiveRecommendHostSet = { RedisServer.LiveCluster, "relr:hs:{0}" };

        /// <summary> 
        /// 用户关播之后，用来给没有设置推荐的房间存储系统推荐的房间 RELR:RoomEndLiveRecommend 的缩写；sys：system
        /// </summary>
        public static readonly string[] EndLiveRecommendSysRecommend = { RedisServer.LiveCluster, "relr:sys:{0}" };

        #endregion

        #region 结算相关
        /// <summary>
        /// 是否正在跑结算
        /// </summary>
        public static readonly string[] SettlementHashRun = { RedisServer.Cache, "settlement:run" };
        /// <summary>
        /// 这个结算周期需要增加的主播roomid
        /// </summary>
        public static readonly string[] CacheListNeedAddSettlementRoom = { RedisServer.Cache, "settlement:room:add" };

        #endregion

    }
}