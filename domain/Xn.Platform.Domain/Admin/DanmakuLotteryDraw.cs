using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Admin
{
    /// <summary>
    /// 抽奖弹幕信息表
    /// </summary>
    public class DanmakuLotteryDraw : Entity
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        public int OperatorId { set; get; }

        /// <summary>
        /// 房间域名
        /// </summary>
        public string RoomDomain { set; get; }

        /// <summary>
        /// 房间id
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 机器人数量
        /// </summary>
        public int RobotCount { set; get; }

        /// <summary>
        /// 发言间隔最小时间
        /// </summary>
        public int SpeakGapMin { set; get; }

        /// <summary>
        /// 发言间隔最大时间
        /// </summary>
        public int SpeakGapMax { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 策略状态
        /// </summary>
        public StrategyStatus Status { set; get; }

        /// <summary>
        /// 操作人Uid集合
        /// </summary>
        [Computed]
        public string[] UserIds { get; set; }
    }

 
}
