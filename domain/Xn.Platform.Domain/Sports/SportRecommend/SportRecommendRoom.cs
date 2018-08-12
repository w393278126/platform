using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 推荐查询实体
    /// </summary>
    public class RecommendCodition : BaseQueryModel
    {
        public int RoomId { get; set; }

        public int MatchId { get; set; }
    }

    /// <summary>
    /// 推荐类型枚举
    /// </summary>
    //public enum RecommendEnum
    //{
    //    赛事预告推荐 = 0,
    //    赛事直播推荐 = 1,
    //    赛事回放推荐 = 2,
    //    同场推荐 = 3,
    //    精彩推荐 = 4,
    //}

    /// <summary>
    /// 体育房间基本信息
    /// </summary>
    public class SportRoomBasic
    {
        /// <summary>
        /// 房间域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// 房主ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 房间logo
        /// </summary>
        public string RoomLogo { get; set; }
        /// <summary>
        /// 房间标题
        /// </summary>
        public string PlayTitle { get; set; }
        /// <summary>
        /// 直播口号
        /// </summary>
        public string Slogan { get; set; }
        /// <summary>
        /// 房间是否直播,直播获取直播游戏id，未直播则取签约游戏id
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// 关联的体育赛事ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 是否再直播
        /// </summary>
        public bool IsLive { get; set; }
        /// <summary>
        /// 房间封面
        /// </summary>
        public string Preview { get; set; }
        /// <summary>
        /// 私密房类型
        /// </summary>
        public PrivateRoom PrivateRoom { get; set; }
    }

    /// <summary>
    ///推荐房间
    /// </summary>
    public class RecommendRoom : SportRoomBasic
    {
        /// <summary>
        /// 当前在线数
        /// </summary>
        public int OnlineCount { get; set; }

        /// <summary>
        /// 本场直播贡献值
        /// </summary>
        public int LiveContribution { get; set; }

        /// <summary>
        /// 是否置顶推荐
        /// </summary>
        public bool IsTopRecommendRooms { get; set; }

        /// <summary>
        /// 是否官方房间
        /// </summary>
        public bool IsOfficialRoom { get; set; }

        /// <summary>
        /// 最多播放数--回放
        /// </summary>
        public int OnlineMaxCount { get; set; }

        /// <summary>
        /// 回放视频Id
        /// </summary>
        public int ReplayVideoId { get; set; }
    }
}