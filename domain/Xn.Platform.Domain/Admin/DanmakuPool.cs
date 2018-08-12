using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Admin
{
    public class DanmakuPool : Entity
    {
        public string Name { get; set; }
        public DateTime ModifyTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int Operator { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }
        public string Keyword { get; set; }

        [Computed]
        public string OperatorName { get; set; }
        [Computed]
        public long DanmakuCount { get; set; }
    }

    public class DanmakuCheater : Entity
    {
        public string Domain { get; set; }
        public int Duration { get; set; }
        public int DurationMax { get; set; }
        public int StickDuration { get; set; }
        public int StickDurationMax { get; set; }
        public int EggDuration { get; set; }
        public int EggDurationMax { get; set; }
        public int GiftCount { get; set; }
        public int GiftCountMax { get; set; }
        public int GiftEggCount { get; set; }
        public int GiftEggCountMax { get; set; }
        public int Operator { get; set; }
        public int Status { get; set; }
        public int RobotCount { get; set; }
        public int Type { get; set; }
        public DateTime Time { get; set; }
        public DateTime EndTime { get; set; }
        public int PoolId { get; set; }
        public CheaterStatus CheaterStatus { get; set; }
        [Computed]
        public DanmakuPool DanmakuPool { get; set; }
        [Computed]
        public string[] UserIds { get; set; }
    }

    public enum CheaterStatus
    {
        NotStart = 1,
        Doing = 2,
        Finish = 3,
        Pause = 4
    }

    public class DanmakuCheaterModel
    {
        public string Domain { get; set; }

        public List<DanmakuCheater> CheaterList { get; set; }
    }

    public class DanmakuAmend
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 房间域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 弹幕池ID
        /// </summary>
        public string PoolIds { get; set; }

        /// <summary>
        /// 1为顺序2为随机
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 1为有效
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 机器人数量
        /// </summary>
        public int RobotCount { get; set; }

        public CheaterStatus AmendStatus { get; set; }

        [Computed]
        public string DanmakuPoolName { get; set; }
        [Computed]
        public string DanmakuPoolKeyword { get; set; }
        [Computed]
        public List<DanmakuPool> DanmakuList { get; set; }
        [Computed]
        [JsonIgnore]
        public string[] UserIds { get; set; }

    }


    public class DanmakuAmendJob
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int AmendId { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 需要的弹幕数
        /// </summary>
        public long RequiredDanmu { get; set; }
        /// <summary>
        /// 时间撮
        /// </summary>
        public long TimeStamp { get; set; }

    }
}
