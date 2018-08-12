using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育比赛基类
    /// </summary>
    public class MatchBasic
    {

    }

    /// <summary>
    /// 体育比赛基础列表
    /// </summary>
    public class SportMatchList : MatchBasic
    {
        public SportMatchList()
        {
            this.ReadyMatchList = new List<ReadyMatch>();
            this.LivingMatchList = new List<LivingMatch>();
            this.OverMatchList = new List<OverMatch>();
        }
        /// <summary>
        /// 即将开始的比赛
        /// </summary>
        public List<ReadyMatch> ReadyMatchList { get; set; }
        /// <summary>
        /// 正在直播的比赛
        /// </summary>
        public List<LivingMatch> LivingMatchList { get; set; }
        /// <summary>
        /// 观看回放的比赛
        /// </summary>
        public List<OverMatch> OverMatchList { get; set; }
    }

    /// <summary>
    /// 比赛基础信息
    /// </summary>
    public class SportMatchBasic
    {
        public SportMatchBasic()
        {
            this.TeamA = new MatchTeamInfo();
            this.TeamB = new MatchTeamInfo();
        }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 赛程名称
        /// </summary>
        public string ScheduleName { get; set; }
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// 赛事名称
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// 比赛开始时间
        /// </summary>
        public DateTime StarTime { get; set; }
        /// <summary>
        /// 比赛结束时间（如果有）
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 比赛类型
        /// </summary>
        public FightTypeEnum FightType { get; set; }
        /// <summary>
        /// A队信息    
        /// </summary>
        public MatchTeamInfo TeamA { get; set; }
        /// <summary>
        /// B队信息
        /// </summary>
        public MatchTeamInfo TeamB { get; set; }

        /// <summary>
        /// 是否显示比分
        /// </summary>
        public ShowScoreEnum ShowScore { get; set; }

        /// <summary>
        /// 当前比赛的状态
        /// </summary>
        public MatchStateEnum State { get; set; }
    }

    /// <summary>
    /// 参赛队伍明细
    /// </summary>
    public class MatchTeamInfo
    {
        /// <summary>
        /// 队伍ID
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
        /// 队伍名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 队伍logo
        /// </summary>
        public string TeamLogo { get; set; }
        /// <summary>
        /// 队伍得分
        /// </summary>
        public int TeamScore { get; set; }
    }

    /// <summary>
    /// 比赛预告信息
    /// </summary>
    public class ReadyMatch : SportMatchBasic
    {
        public ReadyMatch()
        {
        }
        /// <summary>
        /// 推荐房间列表
        /// </summary>
        public List<RecommendRoom> RecommendRooms { get; set; }

        /// <summary>
        /// 预约信息
        /// </summary>
        public List<SubscribeInfo> Subscribe { get; set; }
    }

    /// <summary>
    /// 比赛回放信息
    /// </summary>
    public class OverMatch : SportMatchBasic
    {
        public OverMatch()
        {
            this.RecommendRooms = new List<RecommendRoom>();
        }
        /// <summary>
        /// 推荐房间列表
        /// </summary>
        public List<RecommendRoom> RecommendRooms { get; set; }
    }

    /// <summary>
    /// 比赛直播中信息
    /// </summary>
    public class LivingMatch : SportMatchBasic
    {
        public LivingMatch()
        {
            this.RecommendRooms = new List<RecommendRoom>();
        }
        /// <summary>
        /// 推荐房间列表
        /// </summary>
        public List<RecommendRoom> RecommendRooms { get; set; }
    }

    /// <summary>
    /// 体育比赛查询条件
    /// </summary>
    public class SportMatchCondition : BaseQueryModel
    {
        public SportMatchCondition()
        {
            this.PlayState = 3;
            this.ClubId = 0;
            this.GameId = 0;
            this.MatchType = MatchTypeEnum.All;
            this.MatchExtend = 3;
            this.Category = RouteCategory.None;
            this.OrderBy = "start";
            this.ToSort = false;
        }

        /// <summary>
        /// 俱乐部ID
        /// </summary>
        public int ClubId { get; set; }
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StarTime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 来源枚举
        /// </summary>
        public PackageidEnum Packageid { get; set; }
        /// <summary>
        /// 渠道
        /// </summary> 
        public ChannelEnum Channel { get; set; }
        /// <summary>
        /// 播放状态  MatchStateEnum的位或结果
        /// </summary>
        public int PlayState { get; set; }
        /// <summary>
        /// 比赛扩展  MatchExtendEnum的位或结果
        /// </summary>
        public int MatchExtend { get; set; }
        /// <summary>
        /// 比赛分类
        /// </summary>
        public MatchTypeEnum MatchType { get; set; }
        /// <summary>
        /// 服务分类
        /// </summary>
        public RouteCategory Category { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// GameId集合
        /// </summary>
        public string GameIds { get; set; }
        /// <summary>
        /// App版本
        /// </summary>
        public string Version { get; set; }
        public string BundleId { get; set; }
    }

    /// <summary>
    /// 比赛当前状态
    /// </summary>
    public enum MatchStateEnum
    {
        Foreshow = 1,
        Living = 2,
        Playback = 4,
        Over = 8,
        IsSubscribe = 16
    }

    /// <summary>
    /// 渠道枚举
    /// </summary>
    public enum ChannelEnum
    {
        LongZhu = 0,
        PPTV = 1,
        QQBrowser = 2,
        NarrateMeeting = 3
    }

    /// <summary>
    /// 比赛分类
    /// </summary>
    public enum MatchTypeEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = -1,
        /// <summary>
        /// 对抗型
        /// </summary>
        Fight = 0,
        /// <summary>
        /// 非对抗型
        /// </summary>
        UnFight = 1,
    }

    /// <summary>
    /// 比赛扩展信息
    /// </summary>
    public enum MatchExtendEnum
    {
        TopRecommend = 1,
        DynamicRecommend = 2,
        Subscribe = 4,
    }

    /// <summary>
    /// 路由分类
    /// </summary>
    public enum RouteCategory
    {
        None = 0,
        Recommend = 1,
        HiPlay = 2
    }
}