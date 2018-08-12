using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    public class BigSisterBuff
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 道具ID
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// 道具数量
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public int Duration { get; set; }

    }

    public class ExlTradeLog
    {
        [Description("流水号")]
        public int TradeId { get; set; }
        [Description("消费时间")]
        public string ConsumeTime { get; set; }
        [Description("送礼人Id")]
        public int UserId { get; set; }
        [Description("送礼人名称")]
        public string UserName { get; set; }
        [Description("道具Id")]
        public int ItemId { get; set; }
        [Description("道具标题")]
        public string ItemTitle { get; set; }
        [Description("道具数量")]
        public int Count { get; set; }
        [Description("花费")]
        public decimal Cost { get; set; }
    }
}
