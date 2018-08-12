using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Abstractions.Domain
{
    /// <summary>
    /// 用户打卡
    /// </summary>
    public class UserMedalCheckinAddInfo
    {
        /// <summary>
        /// 这次打卡增加的亲密值(已废弃)
        /// </summary>
        public int AddIntimacy { get; set; }

        /// <summary>
        /// 该空间今天有多少个粉丝打卡	
        /// </summary>
        public int CheckinCount { get; set; }

        /// <summary>
        /// 某用户在该空间连续打卡天数	
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 本次打卡增加的粉丝值
        /// </summary>
        public int Fans { get; set; }

        /// <summary>
        /// 0 未关注、1-已关注、2-互相关注	
        /// </summary>
        public int FollowStatus { get; set; }

        /// <summary>
        /// 访问人和空间主人之间的亲密度	(废弃)
        /// </summary>
        public int Intimacy { get; set; }
    }

    /// <summary>
    /// 打卡信息
    /// </summary>
    public class UserMedalCheckinInfo
    {
        /// <summary>
        /// 该空间今天有多少个粉丝打卡	
        /// </summary>
        public int CheckinCount { get; set; }
        /// <summary>
        /// 某用户在该空间连续打卡天数	
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 当前用户的粉丝值	
        /// </summary>
        public int Fans { get; set; }

        /// <summary>
        /// 访问人和空间主人之间的亲密度	(废弃)
        /// </summary>
        public int Intimacy { get; set; }

        /// <summary>
        /// 当前时间，时间戳，单位是秒	
        /// </summary>
        public long Today { get; set; }

        /// <summary>
        /// 今日是否已打卡
        /// </summary>
        public bool IsCheckin { get; set; }
    }
}
