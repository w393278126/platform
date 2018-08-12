using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xn.Home.Utils.Configuration;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Admin
{
    public class ItemCheater : Entity
    {
        public string Domain { get; set; }
        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public int PerGift { get; set; }
        public int PerGiftMax { get; set; }
        public int Duration { get; set; }
        public int DurationMax { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
        public int JobStatus { get; set; }
        public int Operator { get; set; }
        public DateTime ModifyTime { get; set; }
        public int RobotCount { get; set ; }

        [Computed]
        public Item Item { get; set; }
        [Computed]
        public string[] UserIds { get; set; }
        [Computed]
        public string OperatorName { get; set; }
    }

    public class ItemCheaterLog : Entity
    {
        public int ItemCheaterId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Domain { get; set; }
        public int ItemCount { get; set; }
        public int Operator { get; set; }

        [Computed]
        public Item Item { get; set; }
        [Computed]
        public string UserName { get; set; }
        [Computed]
        public string OperatorName { get; set; }
    }
}
