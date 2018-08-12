using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育直播时长日志表
    /// </summary>
    [Table("SportV2MatchStartLiveLog")]
    public class SportV2MatchStartLiveLog : Entity
    {
        public SportV2MatchStartLiveLog()
        {
            WriteTime = DateTime.Now;
            PlayId = 0;
            RoomId = 0;
            MatchId = 0;
            GameId = 0;
            HostUserId = 0;
            PriorToThisSecondCount = 0;
            UpdateTime = DateTime.Now;
        }

        public int PlayId { get; set; }
        public int RoomId { get; set; }
        public int MatchId { get; set; }
        public int GameId { get; set; }
        public int HostUserId { get; set; }
        public DateTime WriteTime { get; set; }
        public int PriorToThisSecondCount { get; set; }
        public DateTime UpdateTime { get; set; }
    }

}
