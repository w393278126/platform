using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Admin
{
    /// <summary>
    /// 触发弹幕关键字表
    /// </summary>
    public class DanmakuKeyWord : Entity
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
        /// 房间Id
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 关键字，以逗号分隔
        /// </summary>
        public string KeyWords { set; get; }


        /// <summary>
        /// 匹配到的关键词对应的弹幕池的ID，以逗号分隔
        /// </summary>
        public string KeyWordIds { set; get; }


        /// <summary>
        /// 发言类型1：顺序2：随机
        /// </summary>
        public int SpeakType { set; get; }

        /// <summary>
        /// 弹幕数量
        /// </summary>
        public int BarrageCount { set; get; }

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
        /// 状态
        /// </summary>
        public StrategyStatus Status { set; get; }

        /// <summary>
        /// 操作人Uid集合
        /// </summary>
        [Computed]
        public string[] UserIds { get; set; }
    }


    public enum StrategyStatus
    {
        Delete = -1,
        Normal = 1
    }

    /// <summary>
    /// 用来给管理员匹配关键字
    /// </summary>
    public class KeyWordWithId
    {
        /// <summary>
        /// 弹幕池的弹幕关键字
        /// </summary>
        public string KeyWord;

        /// <summary>
        /// 该弹幕关键字所属的Id
        /// </summary>
        public int Id;
    }
}
