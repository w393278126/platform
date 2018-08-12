using  Xn.Platform.Abstractions.Redis.Configuration;

namespace Xn.Platform.Data.Redis
{
    public partial class RedisKeyDefinition
    {

        #region 排行榜相关
        /// <summary>
        /// 排行榜，rk:{ranklistruleId}[:{roomId}:{gameId}:{yyMMdd}]:anchor|audience
        /// </summary>
        public static readonly string[] RankListSortedSetData = { RedisServer.CommonCluster, "rk:{0}" };
        /// <summary>
        /// 排行榜用户黑名单，rk:blackuserlist:{ruleId},value:{userId}
        /// </summary>
        public static readonly string[] RankListSetBlackUserList = { RedisServer.CommonCluster, "rk:blackuserlist:{0}" };
        /// <summary>
        /// 排行榜房间黑名单，rk:blackroomlist:{ruleId},value:{roomId}
        /// </summary>
        public static readonly string[] RankListSetBlackRoomList = { RedisServer.CommonCluster, "rk:blackroomlist:{0}" };
        /// <summary>
        /// 总榜
        /// 主播榜 event:rank:{target}:{eventid}:{phaseid}
        /// 道具榜 event:rank:{target}:{eventid}:{itemid}
        /// 其他 event:rank:{target}:{eventid}
        /// field:{roomid}|{userid}|{roomid-userid}|{familyid},
        /// value:score
        /// target = anchor|audience|couple|family|item
        /// </summary>
        public static readonly string[] EventRankListRank = { RedisServer.CommonCluster, "event:rank:{0}" };
        /// <summary>
        /// 主播总榜分组快照，event:rank:snap:{target}:{eventid}:{phaseid}:{groupid},
        /// field:{roomid}
        /// value:score
        /// 数据来源于总榜
        /// </summary>
        public static readonly string[] EventRankListGroupRank = { RedisServer.CommonCluster, "{{event:rank:snap}}:{0}" };
        /// <summary>
        /// 被淘汰的主播集合
        /// key:event:rank:out:{eventid}:{group.Description}
        /// field:{phaseEndTime.ToString("yyyyMMddHHmmss")}
        /// value:[roomId:score,roomId:score]
        /// </summary>
        public static readonly string[] EventRankListRoomOut = { RedisServer.CommonCluster, "event:rank:out:{0}" };
        public static readonly string[] EventRankListCurrentRoom = { RedisServer.CommonCluster, "event:rank:currentroom" };

        public static readonly string[] BoomPointRedisSortedSet = { RedisServer.ActivityCluster, "boom:point:{0}" };
        public static readonly string[] BoomTimesRedisSet = { RedisServer.ActivityCluster, "boom:times:{0}" };
        #endregion

        #region 身份标识相关

        /// <summary>
        /// 用户道具库存 {UserId}
        /// feild：itemName value：count
        /// </summary>
        public static readonly string[] PropsHashUserItemAccountCluster = { RedisServer.PropsCluster, "u:item:{0}" };

        /// <summary>
        /// 用户房间隐身信息
        /// key：u:room:stealthy:{uid} 
        /// value：{"IsHide": true,"Nickname": "","Avatar": ""}
        /// </summary>
        public static readonly string[] UserRoomStealthyInfoCluster = { RedisServer.CommonCluster, "u:room:stealthy:{0}" };

        /// <summary>
        /// 高级VIP用户已使用昵称池
        /// </summary>
        public static readonly string[] VipNicknamePoolHasBeenUsedCluster = { RedisServer.CommonCluster, "vip:usedname:set" };

        /// <summary>
        /// 用户VIP信息 （单用户多角色，）
        /// key: vip:infos:{uid}
        /// value: "{1:{"expire":144444},2:{"expire":33333}}"
        /// </summary>
        public static readonly string[] IdentityStringVipInfoCluster = { RedisServer.CommonCluster, "vip:infos:{0}" };

        /// <summary>
        /// 房间守护信息
        /// </summary>
        public static readonly string[] GuardInfoCluster = { RedisServer.CommonCluster, "guard:{0}" };

        /// <summary>
        /// 房间守护榜
        /// {0}:roomId
        /// field: userId
        /// </summary>
        public static readonly string[] RoomGuardRankCluster = { RedisServer.CommonCluster, "guard:room:{0}:rank" };

        /// <summary>
        /// 房间守护榜锁
        /// {0}:roomId
        /// </summary>
        public static readonly string[] RoomGuardRankLockerCluster = { RedisServer.CommonCluster, "guard:room:{0}:rank:locker" };

        /// <summary>
        /// 守护榜在线数
        /// key:guard:room:rank:online:{roomid},value:{count}
        /// </summary>
        public static readonly string[] GuardRankOnlineCluster = { RedisServer.CommonCluster, "guard:room:rank:online:{0}" };

        /// <summary>
        /// 座驾
        /// </summary>
        public static readonly string[] VehicleInfoCluster = { RedisServer.CommonCluster, "vehicle:{0}" };
        /// <summary>
        /// 房间座驾列表{roomId}
        /// member:userId
        /// </summary>
        public static readonly string[] RoomVehicleCluster = { RedisServer.CommonCluster, "vehicle:join:{0}" };
        /// <summary>
        /// 房间座驾列表锁
        /// {0}:roomId
        /// </summary>
        public static readonly string[] RoomVehicleLockerCluster = { RedisServer.CommonCluster, "vehicle:room:{0}:locker" };

        /// <summary>
        /// 用户已获勋章
        /// mt:user:{uid}
        /// </summary>

        public static readonly string[] MissionHashUserMedalTaskMedal = { RedisServer.CommonCluster, "mt:user:{0}" };

        /// <summary>
        /// 主播已获勋章
        /// mt:anchor:{hostId}
        /// </summary>
        public static readonly string[] MissionHashAnchorMedalTaskMedal = { RedisServer.CommonCluster, "mt:anchor:{0}" };

        /// <summary>
        /// 用户体育VIP信息 （单用户多角色）
        /// key: sportvip:infos:{uid}
        /// value: "[{"type":1,"expire":1597483671,"sort":1},{"type":2,"expire":1597483671,"sort":2}]"
        /// </summary>
        public static readonly string[] IdentityStringSportVipInfoCluster = { RedisServer.CommonCluster, "sportvip:infos:{0}" };
        #endregion

        #region 回放相关
        /// <summary>
        /// 房间回放推荐列表
        /// </summary>
        public static readonly string[] LiveListRoomReplayRecommendation = { RedisServer.CommonCluster, "r:replay:recommendation" };
        /// <summary>
        /// 房间回放播放次数
        /// </summary>
        public static readonly string[] RoomReplayViewCountHash = { RedisServer.CommonCluster, "r:replay:views:{0}" };
        #endregion

        #region Pvuv相关

        /// <summary>
        /// 用户当天在当前房间在线累积 {string.Empty/video:/app:}accumurate:{yyyyMMdd}:{roomId}
        /// </summary>
        public static readonly string[] PvUvHashOnlineDurationCluster = { RedisServer.PvuvCluster, "r:online:{0}" };
        /// <summary>
        /// 用户最后在线时间
        /// </summary>
        public static readonly string[] PvUvStringLastOnlineCluster = { RedisServer.PvuvCluster, "u:last:online:{0}" };

        public static readonly string[] PvUvSetOnlineDurationKeysCluster = { RedisServer.PvuvCluster, "r:online:keys:{0}" };

        public static readonly string[] PvUvStringItemInventoryCluster = { RedisServer.PvuvCluster, "item:inventory:{0}" };

        public static readonly string[] PvUvStringItemCluster = { RedisServer.PvuvCluster, "item:cd:{0}" };
        public static readonly string[] PvUvSetKeysCluster = { RedisServer.PvuvCluster, "item:cd:keys:{0}" };
        public static readonly string[] PvUvSetItemInventoryKeysCluster = { RedisServer.PvuvCluster, "item:inventory:keys:{0}" };

        #endregion

        #region 加密房间


        /// <summary>
        /// 收费房间 
        /// key：liveId, value:userId
        /// </summary>
        public static readonly string[] LivePrivateSetUserId = { RedisServer.CommonCluster, "r:live:private:{0}" };


        /// <summary>
        /// 收费房间类型
        /// field：roomId, value:type|value|isself
        /// </summary>
        public static readonly string[] LivePrivateHashRoomType = { RedisServer.CommonCluster, "r:live:private:type" };

        #endregion

        #region 龙珠一姐

        public static readonly string[] BigSisterCheerRedisHash = { RedisServer.CommonCluster, "bigsister:cheer:{0}" };

        public static readonly string[] BigSisterCheerRedisString = { RedisServer.CommonCluster, "bigsister:robotcheer:{0}" };

        public static readonly string[] BigSisterPointString = { RedisServer.CommonCluster, "bigsister:point:{0}" };

        public static readonly string[] BigSisterUserRedisHash = { RedisServer.CommonCluster, "bigsister:user:{0}" };

        public static readonly string[] BigSisterBuffString = { RedisServer.CommonCluster, "bigsister:buff:{0}" };

        #endregion

        #region 一战成名
        /// <summary>
        /// 一战成名前5
        /// </summary>
        public static readonly string[] YzcmFinalTop5 = { RedisServer.CommonCluster, "event:yzcmfinal:top5:{0}" };
        /// <summary>
        /// 一战成名前3
        /// </summary>
        public static readonly string[] YzcmFinalTop3 = { RedisServer.CommonCluster, "event:yzcmfinal:top3:{0}" };
        #endregion

        #region vector相关
        public static readonly string[] VectorUserIdRoomIdCluster = { RedisServer.VectorCluster, "user:room:{0}" };
        public static readonly string[] VectorRoomIdUserIdCluster = { RedisServer.VectorCluster, "room:user:{0}" };

        public static readonly string[] VectorDomainRoomIdCluster = { RedisServer.VectorCluster, "r:domain:id:{0}" };
        public static readonly string[] VectorRoomIdDomainCluster = { RedisServer.VectorCluster, "r:id:domain:{0}" };
        #endregion

        #region 抽奖
        /// <summary>
        /// 抽奖名单
        /// </summary>
        public static readonly string[] DrawUserListSet = { RedisServer.CommonCluster, "draw:user:{0}" };
        /// <summary>
        /// 符合条件用户累计送出道具
        /// </summary>
        public static readonly string[] DrawItemHash = { RedisServer.CommonCluster, "draw:item:{0}" };
        /// <summary>
        /// 此次抽奖累计送出的道具
        /// </summary>
        public static readonly string[] DrawItemSumString = { RedisServer.CommonCluster, "draw:itemSum:{0}" };
        /// <summary>
        /// 开始抽奖活动池
        /// field：roomId
        /// value：json
        /// </summary>
        public static readonly string[] DrawActivityHash = { RedisServer.CommonCluster, "draw:activity" };
        /// <summary>
        /// 抽奖活动状态记录
        /// key：activityId
        /// value：DrawStatus
        /// </summary>
        public static readonly string[] DrawStatusString = { RedisServer.CommonCluster, "draw:winlog:{0}" };
        /// <summary>
        /// 活动前关注数记录
        /// key：activityId
        /// value：关注数
        /// </summary>
        public static readonly string[] DrawBeforeFollowString = { RedisServer.CommonCluster, "draw:beforefollow:{0}" };


        #endregion

        #region 签约主播相关

        /// <summary>
        /// 是否是签约主播
        /// key：roomid, value:1
        /// </summary>
        public static readonly string[] IsWageRoomRedisString = { RedisServer.CommonCluster, "r:wage:room:{0}" };

        /// <summary>
        /// 保护名单
        /// key：roomid, value:1
        /// </summary>
        public static readonly string[] WageProtectSet = { RedisServer.Cache, "anchor:account:protected" };

        #endregion

        #region 粉丝勋章相关

        /// <summary>
        /// 自动切换粉丝勋章 
        /// key：userId, value:1
        /// </summary>
        public static readonly string[] MedalAutoChangeString = { RedisServer.CommonCluster, "medal:auto:{0}" };

        #endregion

        #region 主播屏蔽房间弹幕关键词
        /// <summary>
        /// 主播屏蔽关键字
        /// </summary>
        public static readonly string[] StarShieldKeyWordSet = { RedisServer.CommonCluster, "shieldkeyword:roomid:{0}" };
        #endregion

        #region 直播预告相关

        /// <summary>
        /// 直播预告 
        /// key：roomId, value:json
        /// </summary>
        public static readonly string[] NextLiveNoticeString = { RedisServer.CommonCluster, "nextlive:notice:{0}" };

        #endregion

        #region 弹幕机器人

        public static readonly string[] UserRobotSet = { RedisServer.ActivityCluster, "user:robot" };
        public static readonly string[] DanmakuRobotStrategyHash = { RedisServer.ActivityCluster, "danmaku:cheater:{0}" };
        public static readonly string[] FeeItemStrategyHash = { RedisServer.ActivityCluster, "freeitem:cheater" };
        public static readonly string[] DanmakuDomainSet = { RedisServer.ActivityCluster, "danmaku:domain" };
        public static readonly string[] DanmakuAmendHash = { RedisServer.ActivityCluster, "danmaku:amend" };
        public static readonly string[] DanmakuAmendJobList = { RedisServer.ActivityCluster, "danmaku:amendjob" };

        /// <summary>
        /// 后台配置的抽奖策略 Hash
        /// </summary>
        public static readonly string[] DanmakuLotteryDrawHash = { RedisServer.ActivityCluster, "danmaku:lottery" };

        /// <summary>
        /// 配置了抽奖策略的房间 Set
        /// </summary>
        public static readonly string[] DanmakuLotteryDrawSet = { RedisServer.ActivityCluster, "danmaku:lotteryroom" };

        /// <summary>
        /// 主播设置的抽奖弹幕 Hash
        /// </summary>
        public static readonly string[] DanmakuLotteryDrawKeywordHash = { RedisServer.ActivityCluster, "danmaku:lotterykeyword" };

        /// <summary>
        /// 后台配置的关键词弹幕的策略信息
        /// </summary>
        public static readonly string[] DanmakuKeyWordHash = { RedisServer.ActivityCluster, "danmaku:keyword" };

        /// <summary>
        /// 用于存储用户发的匹配的指定房间的弹幕
        /// </summary>
        public static readonly string[] DanmakuKeyBSWordHash = { RedisServer.ActivityCluster, "danmaku:keywordroomBS:{0}" };

        #endregion

        #region live相关
        public static readonly string[] LiveRoomLiveCluster = { RedisServer.LiveCluster, "r:live:{0}" };
        public static readonly string[] LiveUserLiveCluster = { RedisServer.LiveCluster, "u:live:{0}" };
        public static readonly string[] LiveRoomLiveKeysCluster = { RedisServer.LiveCluster, "r:live:keys" };

        /// <summary>
        /// 房间上次开播时间
        /// type:string
        /// key:r:live:lasttime:{roomid}  
        /// value：{time}//上次直播时间
        /// </summary>
        public static readonly string[] LiveLastStartTimeStringCluster = { RedisServer.LiveCluster, "r:live:lasttime:{0}" };
        #endregion

        #region 流相关

        /// <summary>
        /// 房间直播码率 
        /// key：roomId, value:码率
        /// </summary>
        public static readonly string[] StreamRateForRoomString = { RedisServer.CommonCluster, "stream:rate:room:{0}" };
        /// <summary>
        /// 房间拉流地址，关播去掉 
        /// key：roomId, value:拉流地址
        /// </summary>
        public static readonly string[] StreamLivePlayerUrlsString = { RedisServer.LiveCluster, "stream:live:playurls:{0}" };


        /// <summary>
        /// 房间秒开拉流地址
        /// key：roomId, value:拉流地址
        /// </summary>
        public static readonly string[] StreamLiveSecondsOpenUrlsString = { RedisServer.LiveCluster, "stream:live:sourls:{0}" };

        #endregion

        #region 百万

        #region 答题

        /// <summary>
        /// 当前题目答案分别对答用户
        /// type:set
        /// key:million:answertype:{mid}：{qid}：{opid}  //赛事Id:问题Id: 答案选项
        /// value：{uid}     //用户ID
        /// </summary>
        public static readonly string[] MillionAnswerTypeSet = { RedisServer.FenqianCluster, "million:answertype:{0}" };

        /// <summary>
        /// 当前题目答对用户
        /// type:set
        /// key:million:answerright:{matchId}:{questionId}   //赛事Id:问题Id
        /// value:{userId} //用户ID
        /// </summary>
        public static readonly string[] MillionAnswerRightSet = { RedisServer.FenqianCluster, "million:answerright:{0}" };

        /// <summary>
        /// 当前题目答错用户
        /// type:set
        /// key:million:answererror:{matchId}:{questionId}  //赛事Id:问题Id
        /// value:{userId} //用户ID
        /// </summary>
        public static readonly string[] MillionAnswerErrorSet = { RedisServer.FenqianCluster, "million:answererror:{0}" };

        /// <summary>
        /// 记录当前阶段过关的用户
        /// type:set
        /// key:million:answerstagepass:{matchId}:{stage}
        /// value:{uid} 真实用户
        /// </summary>
        public static readonly string[] MillionAnswerStagePassSet = { RedisServer.FenqianCluster, "million:answerstagepass:{0}" };

        /// <summary>
        /// 赛事最终分钱的用户（答对+复活）
        /// type:set
        /// key:million:answerfenqian:{matchId}
        /// value:{userId} 用户Id
        /// </summary>
        public static readonly string[] MillionAnswerFenqianSet = { RedisServer.FenqianCluster, "million:fenqianusers:{0}" };

        #endregion

        #region 发题

        /// <summary>
        /// 当前赛事已开启的题目
        /// type:set
        /// key:million:questionsend:{mid}  //赛事ID
        /// value:{qid}  //问题Id
        /// </summary>
        public static readonly string[] MillionQuestionSendSet = { RedisServer.FenqianCluster, "million:questionsend:{0}" };

        /// <summary>
        /// 正在进行的赛事答题
        /// type:string
        /// key:million:questionplay
        /// value:{"mid": 62088,"qid": 100,"stage":1,"stime": 400020202}
        /// </summary>
        public static readonly string[] MillionQuestionPlayString = { RedisServer.FenqianCluster, "million:questionplay" };

        #endregion

        #region 公布答案

        /// <summary>
        /// 公布答案中的答题
        /// type:string
        /// key:million:questionanswer
        /// </summary>
        public static readonly string[] MillionQuestionAnswerString = { RedisServer.FenqianCluster, "million:questionanswer" };

        /// <summary>
        /// 机器人计算参数
        /// type:string
        /// key:million:robotcalculation:{matchid}
        /// value:{json} 总参与人数+总淘汰人数
        /// </summary>
        public static readonly string[] MillionRobotCalculationInputString = { RedisServer.FenqianCluster, "million:robotcalculation:{0}" };

        /// <summary>
        /// 赛事每题答题真实人数
        /// type:hash
        /// key:million:publish:usercount:{matchid}
        /// field: {stage} 题目顺序
        /// value:{count}  真实人数
        /// </summary>
        public static readonly string[] MillionPublishParticipantsHash = { RedisServer.FenqianCluster, "million:publish:usercount:{0}" };

        /// <summary>
        /// 赛事每题答题人数外显
        /// type:hash
        /// key:million:publish:usercountdisplay:{matchid}
        /// field: {stage} 题目顺序
        /// value:{count}  外显人数
        /// </summary>
        public static readonly string[] MillionPublishParticipantsDisplayHash = { RedisServer.FenqianCluster, "million:publish:usercountdisplay:{0}" };

        /// <summary>
        /// 赛事答题信息
        /// type:hash
        /// key:million:statistics:{matchid}
        /// field: {questionId} 题目Id
        /// value:{json}  答题信息
        /// </summary>
        public static readonly string[] MillionStatisticsHash = { RedisServer.FenqianCluster, "million:statistics:{0}" };

        /// <summary>
        /// 赛事答题信息外显
        /// type:hash
        /// key:million:statistics:display:{matchid}
        /// field: {questionId} 题目Id
        /// value:{json}  答题信息
        /// </summary>
        public static readonly string[] MillionStatisticsDisplayHash = { RedisServer.FenqianCluster, "million:statistics:display:{0}" };

        /// <summary>
        /// 赛事最终分钱信息
        /// type:hash
        /// key:million:fenqian
        /// field:{matchId}
        /// value:{json} 
        /// </summary>
        public static readonly string[] MillionFenqianHash = { RedisServer.FenqianCluster, "million:fenqian" };

        /// <summary>
        /// 机器人算法记录
        /// type:string
        /// key:million:robotlog:{matchId}:{stage}
        /// value:{json} 
        /// </summary>
        public static readonly string[] MillionRobotLogString = { RedisServer.FenqianCluster, "million:robotlog:{0}" };

        #endregion

        /// <summary>
        /// 已使用复活次数
        /// type:	hash
        /// key:	million:relive:{uid} //uid
        /// field:	{mid}               //赛事id
        /// value:	{count}			    //复活次数
        /// </summary>
        public static readonly string[] MillionReliveString = { RedisServer.FenqianCluster, "million:relive:{0}" };

        /// <summary>
        /// 阶段复活用户
        /// type:	hash
        /// key:	million:relive:user:{uid}
        /// field:	{mid}:{stage}
        /// value:	{qid}|{cretetime}
        /// </summary>
        public static readonly string[] MillionReliveUserString = { RedisServer.FenqianCluster, "million:relive:user:{0}" };

        /// <summary>
        /// 阶段复活人数
        /// type:	hash
        /// key:	million:relive:stage:{mid}
        /// field:	{stage}
        /// value:	{count}
        /// </summary>
        public static readonly string[] MillionReliveStageString = { RedisServer.FenqianCluster, "million:relive:stage:{0}" };

        /// <summary>
        /// 邀请送币活动,赠送5元
        /// type:	set
        /// key:	million:cash5:{type}    //invite(邀请者): beinvited(被邀请者)
        /// value:	{uid}...
        /// </summary>
        public static readonly string[] MillionInviteCash5String = { RedisServer.FenqianCluster, "million:cash5:{0}" };

        /// <summary>
        /// 邀请人获赠复活币(uid为发出邀请人的uid)
        /// type:	string
        /// key:	million:invite:{uid}
        /// value:	{count}
        /// </summary>
        public static readonly string[] MillionInviteChanceString = { RedisServer.FenqianCluster, "million:invite:{0}" };

        /// <summary>
        /// 首次邀请码(只能输入别人的邀请码一次)
        /// type:	set
        /// key:	million:invite:first
        /// value:	{uid}...
        /// </summary>
        public static readonly string[] MillionInviteFirstString = { RedisServer.FenqianCluster, "million:invite:first" };

        /// <summary>
        /// 奖金排行榜
        /// type:	sorted set
        /// key:	million:ranking:{0}     //周:week 月:month 总:total
        /// value:	uid
        /// score:	ranking
        /// </summary>
        public static readonly string[] MillionRankingTotalString = { RedisServer.FenqianCluster, "million:ranking:{0}" };

        #endregion

        #region 主播的推流ratelevel
        /// <summary>
        /// 主播的推流ratelevel
        /// key：roomid, value:Dictionary<int, string>
        /// </summary>
        public static readonly string[] StreamRateLevelRedisString = { RedisServer.LiveCluster, "r:stream:ratelevel:dic:{0}" };
        #endregion

        #region 中超荣耀2018

        /// <summary>
        /// type   :zcry:type:{target}  排行榜类型
        /// member :roomid|userid|clubid
        /// value  :score
        /// stage: None|SpringPreliminary|SpringFinal|AutumnPreliminary|AutumnPreliminary|AutumnFinal|Final
        /// rank: GrassrootsRank|SpecialtyRank|FootballBabyRank|SportGameRank|UserRank|ClubRank
        /// target: stage：rank
        /// </summary>
        public static readonly string[] ZcryRankSortSet = { RedisServer.ActivityCluster, "zcry:type:{0}" };

        /// <summary>
        /// key   :zcry:{type}:data  参加中超荣耀的主播身份信息、球队信息
        /// field :roomid|clubid
        /// value :rank|json（球队数据）
        /// type :Star|Club
        /// rank: GrassrootsRank|SpecialtyRank|FootballBabyRank|SportGameRank|UserRank|ClubRank
        /// </summary>
        public static readonly string[] ZcryObjectTypeRedisHash = { RedisServer.ActivityCluster, "zcry:{0}:data" };

        /// <summary>
        /// key   :zcry:group:{target}  小组赛赛事
        /// field :赛事ID
        /// value  :json  赛事明细
        /// stage: None|SpringPreliminary|SpringFinal|AutumnPreliminary|AutumnPreliminary|AutumnFinal|Final
        /// target: stage：groupname
        /// </summary>
        public static readonly string[] ZcrySprinFinalRedisHash = { RedisServer.ActivityCluster, "zcry:group:{0}" };

        #endregion

        #region 热力值

        /// <summary>
        /// 房间实时热力值
        /// type:	string
        /// key:	r:{roomId}:heat
        /// value:	热力值
        /// </summary>
        public static readonly string[] RvsStringRoomHeat = { RedisServer.Rvs, "r:{0}:heat" };

        /// <summary>
        /// 房间热力值的基础值
        /// type:   hash
        /// key:    r:heat:dynamic:config
        /// filed:  roomid
        /// value:  基数值
        /// </summary>
        public static readonly string[] RvsStringBaseValue = { RedisServer.Rvs, "r:heat:dynamic:config" };

        #endregion

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


        #region 世界杯活动

        public static readonly string[] ActivityFiFaUserSortSet = { RedisServer.ActivityCluster, "actfifa:user" };

        public static readonly string[] ActivityFiFaCountryRoomSortSet = { RedisServer.ActivityCluster, "actfifa:countryroom:{0}" };

        public static readonly string[] ActivityFiFaRoomCountrySortSet = { RedisServer.ActivityCluster, "actfifa:roomcountry:{0}" };

        #endregion

        #region 商城活动

        public static readonly string[] ActShoppingConsumeHash = { RedisServer.ActivityCluster, "act:shopping:consume" };

        public static readonly string[] ActShoppingRewardHash = { RedisServer.ActivityCluster, "act:shopping:reward:{0}" };

        public static readonly string[] ActivityShoppingFirstSet = { RedisServer.Cache, "shopping:first" };

        public static readonly string[] ActivityShoppingVipInventoryHash = { RedisServer.ActivityCluster, "act:shopping:vip:{0}" };

        #endregion

        #region 女神活动

        public static readonly string[] ActivityGoddessDefenseHash = { RedisServer.ActivityCluster, "act:goddess:defense" };
        public static readonly string[] ActivityGoddessPersonDefaultRoomIdSet = { RedisServer.ActivityCluster, "act:goddess:person" };
        public static readonly string[] ActivityGoddessDefenseRecordList = { RedisServer.ActivityCluster, "act:goddess:defrecord" };

        #endregion
    }
}
