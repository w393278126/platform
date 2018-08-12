using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.User;

namespace Xn.Platform.Domain.Sports
{
    public class CheerModel
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 助威分数
        /// </summary>
        public long Point { get; set; }
        /// <summary>
        /// 是否已经支持过
        /// </summary>
        public bool Cheer { get; set; }

    }


    public class RoomPopularity
    {
        /// <summary>
        /// 金主排行
        /// </summary>
        public List<RankModel> Rank { get; set; }
        /// <summary>
        /// 人气值
        /// </summary>
        public long Point { get; set; }
        /// <summary>
        /// BUFF
        /// </summary>
        public BuffModel Buff { get; set; }

        public UserInfo UserInfo { get; set; }

        public string Url { get; set; }

    }

    public class BuffModel
    {
        public BuffType BuffType { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public TimeSpan LastTime { get; set; }
        /// <summary>
        /// 送礼用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }

    public enum BuffType
    {
        /// <summary>
        /// 系统开启
        /// </summary>
        System = 1,
        /// <summary>
        /// 用户送礼开启
        /// </summary>
        User = 2
    }


    public class RankModel
    {
        public UserInfo Userinfo { get; set; }

        public long Point { get; set; }
    }
}
