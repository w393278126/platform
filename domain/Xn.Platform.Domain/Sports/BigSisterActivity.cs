using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    public class BigSisterActivity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int MatchId { get; set; }

        /// <summary>
        /// 参赛房间A
        /// </summary>
        public int RoomIdA { get; set; }

        /// <summary>
        /// 参赛房间B
        /// </summary>
        public int RoomIdB { get; set; }

        /// <summary>
        /// 官方房间
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUser { get; set; }

        /// <summary>
        /// 1龙币等于人气价值
        /// </summary>
        public int CoinPrice { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        [Computed]
        public string ActivityStatus => StartTime > DateTime.Now ? "等待开始" : EndTime < DateTime.Now ? "已结束" : "进行中";

        [Computed]
        public List<BigSisterBuff> BuffList { get; set; }

        public int RoomAItemId { get; set; }

        public int RoomBItemId { get; set; }

        public string Url { get; set; }

        public bool DisableDanmu { get; set; }
    }

    public class BigSisterModel
    {
        /// <summary>
        /// buff日志
        /// </summary>
        public List<BigSisterLog> BuffLog { get; set; }

        public List<BigSisterRoomInfo> RoomInfo { get; set; }
    }

    public class BigSisterRoomInfo
    {
        /// <summary>
        /// 房间号
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 助威值
        /// </summary>
        public long CheerPoint { get; set; }
        /// <summary>
        /// 人气值
        /// </summary>
        public long Point { get; set; }


    }

    [Table("BigSister_Log")]
    public class BigSisterLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 操作，人气和助威为+1000， buff为持续时间
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string ModifyUser { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public BigSisterType Type { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId { get; set; }

        public DateTime CreateTime { get; set; }

    }

    public enum BigSisterType
    {
        Cheer = 1,
        Popularity = 2,
        Buff = 3
    }
}
