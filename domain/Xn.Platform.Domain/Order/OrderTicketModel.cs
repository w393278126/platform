using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    [Table("t_order_orderticket")]
    public class OrderTicketModel
    {
        /// <summary>
        /// id
        /// </summary>		

        public string id { get; set; }
        /// <summary>
        /// userId
        /// </summary>		

        public string userId { get; set; }
        /// <summary>
        /// orderId
        /// </summary>		

        public string orderId { get; set; }
        /// <summary>
        /// thirdSerialId
        /// </summary>		

        public string thirdSerialId { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int states { get; set; }
        /// <summary>
        /// sceneryName
        /// </summary>		

        public string sceneryName { get; set; }
        /// <summary>
        /// ticketName
        /// </summary>		

        public string ticketName { get; set; }
        /// <summary>
        /// ticketId
        /// </summary>		

        public string ticketId { get; set; }
        /// <summary>
        /// ticketsNum
        /// </summary>		

        public int ticketsNum { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime travelDate { get; set; }
        /// <summary>
        /// consumersType
        /// </summary>		

        public string consumersType { get; set; }
        /// <summary>
        /// screeningId
        /// </summary>		

        public string screeningId { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime screeningBeginTime { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime screeningEndTime { get; set; }
        /// <summary>
        /// tcAmount
        /// </summary>		

        public decimal tcAmount { get; set; }
        /// <summary>
        /// totalPrice
        /// </summary>		

        public decimal totalPrice { get; set; }
        /// <summary>
        /// isRealName
        /// </summary>		

        public long isRealName { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime createTime { get; set; }
    }

    public class OrderTicketResponse
    {
        public class OrderTicket:OrderTicketModel
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string userName { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string mobile { get; set; }
        }
    }
}
