using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育比赛推荐列表
    /// </summary>
    public class SportMatchRecommendList : MatchBasic
    {
        public SportMatchRecommendList()
        {
            this.Matchs = new List<RecommendMatch>();
        }
        public List<RecommendMatch> Matchs { get; set; }
    }
    public class RecommendMatch : SportMatchBasic
    {
        /// <summary>
        /// 推荐房间列表
        /// </summary>
        public RecommendRoom RecommendRooms { get; set; }
    }
}