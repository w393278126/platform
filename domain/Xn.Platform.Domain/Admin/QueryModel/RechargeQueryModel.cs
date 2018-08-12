using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain
{
    public class RechargeQueryModel : BaseQueryModel
    {
        public string Uid { get; set; }
        public string NickName { get; set; }
        public string QQ { get; set; }
        public int RechargeType { get; set; }
        public int RechargeStatus { get; set; }
        public string RoomId { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int TradeStatus { get; set; }
        public int ChannelId { get; set; }
        public int RechargeId { get; set; }
        public string Trxid { get; set; }
    }
}
