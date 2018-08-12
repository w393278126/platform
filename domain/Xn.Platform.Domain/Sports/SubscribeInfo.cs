using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;
using System.ComponentModel;

namespace Xn.Platform.Domain.Sports
{

    public class SubscribeInfoRedis
    {
        public int gameId { get; set; }
        public long sortId { get; set; }
        public int uid { get; set; }
        public int roomId { get; set; }
        public DateTime subscribeDate { get; set; }
    }

    /// <summary>
    /// 体育，预约信息
    /// </summary>
    public class SubscribeInfo : SubscribeInfoRedis
    {
        public string avatar { get; set; }
        public string domain { get; set; }
        public bool isLiving { get; set; }
        public string nickname { get; set; }
        public string slogan { get; set; }
        public string playTitle { get; set; }

        /// <summary>
        /// 主播直播过多少分钟
        /// </summary>
        public long liveMinutes { get; set; }
        /// <summary>
        /// 封面或者截图
        /// </summary>
        public string Preview { get; set; }
        /// <summary>
        /// 开播类型
        /// </summary>
        public int GameType { get; set; }
    }

    /// <summary>
    /// 导出用
    /// </summary>
    public class ExSubscribeInfo
    {
        [Description("预约的日期")]
        public string subscribeDate { get; set; }
        [Description("预约的时间")]
        public string subscribeTime { get; set; }
        [Description("选择直播的日期")]
        public string liveData { get; set; }
        [Description("选择直播的时间")]
        public string liveTime { get; set; }
        [Description("选择直播的场次ID")]
        public int matchiId { get; set; }

        //[Description("对阵信息")]
        //public string TeamVS { get; set; }

        [Description("房间域名")]
        public string roomDomain { get; set; }
        [Description("房间名称（用户昵称）")]
        public string roomName { get; set; }
        [Description("房间ID")]
        public int roomId { get; set; }
        [Description("用户ID（UID）")]
        public int uid { get; set; }
        [Description("绑定游戏（绑定分类）")]
        public string bindGame { get; set; }
        [Description("直播时长（分钟）")]
        public long liveMinutes { get; set; }
    }

}
