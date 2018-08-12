using System;
using Xn.Platform.Core.Data;
using Xn.Platform.Domain.Report;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育档期赛事
    /// </summary>
    [Table("SportMatchs")]
    public class SportMatch : Entity
    {
        public SportMatch()
        {
            SceneSwitchTime = Convert.ToDateTime("2009-01-01");
        }
        public int GameId { set; get; }
        public int ScheduleId { set; get; }
        public string StreamAddress { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public int RoomAId { set; get; }
        [Computed]
        public int RoomAUserId { set; get; }
        [Computed]
        public int ClubAId { get; set; }
        [Computed]
        public string ClubAName { get; set; }
        public int RoomAScore { set; get; }
        public int RoomBId { set; get; }
        [Computed]
        public int RoomBUserId { set; get; }
        [Computed]
        public int ClubBId { get; set; }
        [Computed]
        public string ClubBName { get; set; }
        public int RoomBScore { set; get; }
        public string GameDesc { get; set; }
        public string GameAwardDesc { get; set; }
        public int TemplateType { get; set; }
        public DateTime SceneSwitchTime { get; set; }
        [ExcludeStatus]
        public int Status { get; set; }
    }


    [Table("SportMatchsV2")]
    public class SportMatchV2 : Entity
    {
        public SportMatchV2()
        {
            StreamAddress = string.Empty;
            TeamAName = string.Empty;
            TeamBName = string.Empty;
            GameDesc = string.Empty;
            GameAwardDesc = string.Empty;
            LimitEndHour = 4;
            CanByLottery = 0;
            BeforeTheStartAlert = 30;
            FrontShow = FrontShowEnum.Yes;
            PKMatchBar = 1;
            FightType = FightTypeEnum.Yes;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            ShowScore = ShowScoreEnum.No;
            CopyRight = "";
        }

        public int GameId { set; get; }
        public int ScheduleId { set; get; }
        public string StreamAddress { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public int TeamAId { set; get; }
        [Computed]
        public string TeamAName { get; set; }
        public int TeamAScore { set; get; }
        public int TeamBId { set; get; }
        [Computed]
        public string TeamBName { get; set; }
        public int TeamBScore { set; get; }
        [ExcludeStatus]
        public int Status { get; set; }
        public string GameDesc { get; set; }
        public string GameAwardDesc { get; set; }
        public int MatchSortId { get; set; }
        public int LimitEndHour { get; set; }
        /// <summary>
        /// App推荐 0-未推荐, 1-推荐
        /// </summary>
        public int AppRecommend { get; set; }
        /// <summary>
        /// PC推荐 0-未推荐, 1-推荐
        /// </summary>
        public int PCRecommend { get; set; }


        #region  Computed

        /// <summary>
        /// 赛事名称
        /// </summary>
        [Computed]
        public string MatchName { get; set; }

        #endregion

        /// <summary>
        /// 是否显示购彩入口（默认是）
        /// </summary>
        public int CanByLottery { get; set; }


        /// <summary>
        /// PK对阵条
        /// </summary>
        public int PKMatchBar { get; set; }

        /// <summary>
        /// PK对阵关系时效时间（默认是比赛开始时间起4小时，以运营手填为准）
        /// </summary>
        public DateTime PKMatchValid { get; set; }

        /// <summary>
        /// 开赛前提示进入房间（手写，默认30分钟）
        /// </summary>
        public int BeforeTheStartAlert { get; set; }

        /// <summary>
        /// 前端显示（是、否），默认为是
        /// </summary>
        public FrontShowEnum FrontShow { get; set; }

        /// <summary>
        /// 对阵类型（0=对抗型，1=非对抗型）
        /// </summary>
        public FightTypeEnum FightType { get; set; }

        /// <summary>
        /// 嗨播显示（0=显示，-1=不显示）
        /// </summary>
        public HiPlayShowEnum HiPlayShow { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否显示比分
        /// </summary>
        public ShowScoreEnum ShowScore { get; set; }
        /// <summary>
        /// 是否显示版权，0-不显示，1-显示
        /// </summary>
        public int ShowCopyRight { get; set; }
        /// <summary>
        /// 版权内容
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// 是否海外禁播
        /// </summary>
        public OverseasBanShowEnum OverseasBanShow { get; set; }

        /// <summary>
        /// 是否采用新H5
        /// </summary>
        public NewH5ShowEnum NewH5Show { get; set; }
        /// <summary>
        /// 赛事关键字
        /// </summary>
        public string KeyWord { get; set; }
        ///// <summary>
        ///// 赛事白名单（roomid）
        ///// </summary>
        //public string WhiteList { get; set; }
    }

    public enum ShowScoreEnum
    {
        No = -1,
        Yes = 0
    }

    public enum HiPlayShowEnum
    {
        No = -1,
        Yes = 0
    }

    public enum FightTypeEnum
    {
        /// <summary>
        /// 其他
        /// </summary>
        Other = -1,
        /// <summary>
        /// 对抗型
        /// </summary>
        Yes = 0,
        /// <summary>
        /// 非对抗型
        /// </summary>
        No = 1
    }

    /// <summary>
    /// 是否在前端展示
    /// </summary>
    public enum FrontShowEnum
    {
        Yes = 1,
        No = 0
    }

    public class SportRankList
    {
        public string GameDesc { get; set; }
        public string GameAwardDesc { get; set; }
        public long[] PkPoint { get; set; }
    }


    /// <summary>
    /// 来源枚举
    /// </summary>
    public enum PackageidEnum
    {
        None = 0,
        /// <summary>
        ///  针对app(ios, android)
        /// </summary>
        APP = 1,
        /// <summary>
        ///  针对嗨播
        /// </summary>
        HiPlay = 17,
        /// <summary>
        ///  针对体育PC
        /// </summary>
        SportPC = 88,
        /// <summary>
        /// PPTV体育SDK
        /// </summary>
        PPTVSDK = 11,
    }

    /// <summary>
    /// 设备枚举
    /// </summary>
    //public enum DeviceEnum
    //{
    //    /// <summary>
    //    /// PC
    //    /// </summary>
    //    PC = 1,
    //    /// <summary>
    //    /// IOS
    //    /// </summary>
    //    IOS = 2,
    //    /// <summary>
    //    /// Android
    //    /// </summary>
    //    Android = 4,
    //    /// <summary>
    //    /// H5
    //    /// </summary>
    //    H5 = 8,
    //    /// <summary>
    //    /// 嗨播
    //    /// </summary>
    //    HiPlay = 64,
    //}

    /// <summary>
    /// 是否海外禁播
    /// </summary>
    public enum OverseasBanShowEnum
    {
        No = 0,
        Yes = 1
    }

    /// <summary>
    /// 是否新H5播放
    /// </summary>
    public enum NewH5ShowEnum
    {
        No = 0,
        Yes = 1
    }
}