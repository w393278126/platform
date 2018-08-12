using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 排行榜类型枚举
    /// </summary>
    public enum ZcryRankEnum
    {
        /// <summary>
        /// 草根精英
        /// </summary>
        GrassrootsRank = 0,
        /// <summary>
        /// 专业王者
        /// </summary>
        SpecialtyRank = 1,
        /// <summary>
        /// 足球宝贝
        /// </summary>
        FootballBabyRank = 2,
        /// <summary>
        /// 体育游戏
        /// </summary>
        SportGameRank = 3,
        /// <summary>
        /// 用户榜
        /// </summary>
        UserRank = 4,
        /// <summary>
        /// 俱乐部
        /// </summary>
        ClubRank = 5,
        /// <summary>
        /// 世界杯东半球
        /// </summary>
        WorldCupEast = 6,
        /// <summary>
        /// 世界杯西半球
        /// </summary>
        WorldCupWest = 7,
        /// <summary>
        /// 男子组
        /// </summary>
        ManRank = 8,
        /// <summary>
        /// 女子组
        /// </summary>
        WomenRank = 9,
    }

    /// <summary>
    /// 中超荣耀阶段分类
    /// </summary>
    public enum ZcryStageEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        None = 0,
        /// <summary>
        /// 春季初赛
        /// </summary>
        [Description("中超荣耀春季决赛-春季初赛")]
        SpringPreliminary = 1,
        /// <summary>
        /// 春季决赛-小组赛
        /// </summary>
        [Description("中超荣耀春季决赛-小组赛")]
        SpringFinalGroup = 2,
        /// <summary>
        /// 春季决赛-淘汰赛
        /// </summary>
        [Description("中超荣耀春季决赛-淘汰赛")]
        SpringFinal = 3,
        /// <summary>
        /// 秋季初赛-海选
        /// </summary>
        [Description("中超荣耀秋季海选")]
        AutumnAuditions = 4,
        /// <summary>
        /// 秋季初赛-初赛
        /// </summary>
        [Description("中超荣耀秋季初赛")]
        AutumnPreliminary = 5,
        /// <summary>
        /// 秋季决赛
        /// </summary>
        [Description("中超荣耀秋季决赛")]
        AutumnFinal =6,
        /// <summary>
        /// 总决赛
        /// </summary>
        [Description("中超荣耀总决赛")]
        Final = 7,
        /// <summary>
        /// 世界杯选拔赛
        /// </summary>
        [Description("世界杯选拔赛")]
        WorldCupGroup = 11,
        /// <summary>
        /// 世界杯淘汰赛
        /// </summary>
        [Description("世界杯淘汰赛")]
        WorldCupFinal = 12
    }

    /// <summary>
    /// 中超荣耀对象枚举
    /// </summary>
    public enum ZcryObjectEnum
    {
        /// <summary>
        /// 主播
        /// </summary>
        Star = 0,
        /// <summary>
        /// 用户
        /// </summary>
        User = 1,
        /// <summary>
        /// 俱乐部
        /// </summary>
        Club = 2,
    }

    /// <summary>
    /// 中超荣耀春季榜单实体
    /// </summary>
    public class RankEntity
    {
        public RankEntity()
        {
            this.IsLive = false;
        }
        /// <summary>
        /// 房间ID、球队ID、用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 主播名称、球队名称、用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主播头像、球队队徽、用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 是否直播
        /// </summary>
        public bool IsLive { get; set; }
        /// <summary>
        /// 排行榜分数
        /// </summary>
        public double Scores { get; set; }
        /// <summary>
        /// 房间域名
        /// </summary>
        public string Domain { get; set; }

    }

    /// <summary>
    /// 春季决赛榜单model
    /// </summary>
    public class SpringFinalRankEntity : RankEntity
    {
        /// <summary>
        /// 参加阶段集合
        /// </summary>
        public List<PromotedEntity> JoinSum { get; set; }
        /// <summary>
        /// 小组赛成绩
        /// </summary>
        public string GroupRank { get; set; }
    }

    /// <summary>
    /// 我当前的排名
    /// </summary>
    public class MyRankEntity : RankEntity
    {
        /// <summary>
        /// 当前我的排名
        /// </summary>
        public double RankIng { get; set; }
    }

    /// <summary>
    /// 晋级阶段明细
    /// </summary>
    public class PromotedEntity
    {
        /// <summary>
        /// 所在阶段
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 所得分数
        /// </summary>
        public double Scores { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 本场比赛结果
        /// </summary>
        public PkResultEnum PkResult { get; set; } 
    }


    /// <summary>
    /// 中超荣耀榜单
    /// </summary>
    public class ZcryRank<T>
    {
        public ZcryRank()
        {
            this.Ranks = new List<T>();
            this.ItemCount = 0;
        }
        public List<T> Ranks { get; set; }

        public int ItemCount { get; set; }
    }

    /// <summary>
    /// 中超荣耀春季赛决赛榜单
    /// </summary>
    public class ZcrySpringFinalRank
    {
        public ZcrySpringFinalRank()
        {
            this.AStar = new SpringFinalRankEntity();
            this.BStar = new SpringFinalRankEntity();
        }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CoreId { get; set; }

        public SpringFinalRankEntity AStar { get; set; }

        public SpringFinalRankEntity BStar { get; set; }

        public string ATeamName { get; set; }
        public string BTeamName { get; set; }
        public string ATeamAvatar { get; set; }
        public string BTeamAvatar { get; set; }

        /// <summary>
        /// 当前PK状态
        /// </summary>
        public PkStateEnum State { get; set; }
        public int Id { get; set; }
    }

    public enum PkStateEnum
    {
        None = 0,
        Ready = 1,
        PkIng = 2,
        Over = 3
    }


    public enum PkResultEnum
    {
        None = 0,
        Victory = 1,
        Lose = 2,
    }
}