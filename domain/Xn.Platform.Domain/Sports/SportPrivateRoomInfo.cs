using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{

    public class SportPrivateRoomInfo
    {
        [Description("ID（数据标识）")]
        public string Id { get; set; }
        [Description("房间（RoomID）")]
        public string RoomId { get; set; }
        [Description("房间（Domain）")]
        public string domain { get; set; }
        [Description("比赛ID")]
        public string MatchId { get; set; }
        [Description("时间")]
        public string Date { get; set; }
        [Description("比赛信息")]
        public string MatchMsg { get; set; }
        [Description("付费人（Uid）")]
        public string Uid { get; set; }
        [Description("金额（元）")]
        public string Price { get; set; }
        [Description("分成：(平台：主播)")]
        public string Proportion { get; set; }
        [Description("平台收入（元）")]
        public string OfficialIncome { get; set; }
        [Description("主播收入（元）")]
        public string StarIncome { get; set; }
    }

}
