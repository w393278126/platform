using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xn.Platform.Admin.Models
{
    public class OrderPlaneModel
    {
        public class RefundRequest
        {
            public string Id { get; set; }
            /// <summary>
            /// 退款金额
            /// </summary>
            public string returnTotal { get; set; }
            /// <summary>
            /// 手续费
            /// </summary>
            public string returnFee { get; set; }
            /// <summary>
            /// 退款原因
            /// </summary>
            public string cancelReason { get; set; }
        }
    }
}