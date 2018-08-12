using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育比赛推荐列表
    /// </summary>
    public class SportMatchHiPlayList : MatchBasic
    {
        public SportMatchHiPlayList()
        {
            this.Matchs = new List<HiPlayMatch>();
        }
        public List<HiPlayMatch> Matchs { get; set; }
    }

    /// <summary>
    /// 嗨播赛事列表
    /// </summary>
    public class HiPlayMatch : SportMatchBasic
    {
        public PrivateMatch PrivateMatch { get; set; }

        /// <summary>
        /// 是否显示版权，0-不显示，1-显示
        /// </summary>
        public int ShowCopyRight { get; set; }
        /// <summary>
        /// 版权内容
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// 官方直播流房间ID
        /// </summary>
        public int LiveRoomId { get; set; }
    }

    /// <summary>
    /// 私密房
    /// </summary>
    public class PrivateMatch
    {
        public PrivateMatchEnum PrivateType { get; set; }

        /// <summary>
        /// 根据PrivateMatchEnum相对应 私密房明细（PrivateType=2->具体金额）
        /// </summary>
        public string PrivateInfo { get; set; }

        /// <summary>
        /// 根据PrivateMatchEnum相对应 私密房明细（PrivateType=2 平台：主播->分成比例）
        /// </summary>
        public string PrivateExtend { get; set; }
    }

    /// <summary>
    /// 私密赛事类型枚举
    /// </summary>
    public enum PrivateMatchEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        None = 0,
        /// <summary>
        /// 收费
        /// </summary>
        Charge = 2
    }
}