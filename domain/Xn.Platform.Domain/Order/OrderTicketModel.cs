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
        public string Id { get; set; }
        /// <summary>
        /// userId
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// orderId
        /// </summary>		

        public string OrderId { get; set; }
        /// <summary>
        /// thirdSerialId
        /// </summary>		

        public string ThirdSerialId { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int States { get; set; }
        /// <summary>
        /// sceneryName
        /// </summary>		

        public string SceneryName { get; set; }
        /// <summary>
        /// ticketName
        /// </summary>		

        public string TicketName { get; set; }
        /// <summary>
        /// ticketId
        /// </summary>		

        public string TicketId { get; set; }
        /// <summary>
        /// ticketsNum
        /// </summary>		

        public int TicketsNum { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime TravelDate { get; set; }
        /// <summary>
        /// consumersType
        /// </summary>		

        public string ConsumersType { get; set; }
        /// <summary>
        /// screeningId
        /// </summary>		

        public string ScreeningId { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime ScreeningBeginTime { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime ScreeningEndTime { get; set; }
        /// <summary>
        /// tcAmount
        /// </summary>		

        public decimal TcAmount { get; set; }
        /// <summary>
        /// totalPrice
        /// </summary>		

        public decimal TotalPrice { get; set; }
        /// <summary>
        /// isRealName
        /// </summary>		

        public long IsRealName { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime CreateTime { get; set; }
    }

    public class OrderTicketDTO
    {
        public string Id { get; set; }
        /// <summary>
        /// userId
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// orderId
        /// </summary>		

        public string OrderId { get; set; }
        /// <summary>
        /// thirdSerialId
        /// </summary>		

        public string ThirdSerialId { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int States { get; set; }
        /// <summary>
        /// sceneryName
        /// </summary>		

        public string SceneryName { get; set; }
        /// <summary>
        /// ticketName
        /// </summary>		

        public string TicketName { get; set; }
        /// <summary>
        /// ticketId
        /// </summary>		

        public string TicketId { get; set; }
        /// <summary>
        /// ticketsNum
        /// </summary>		

        public int TicketsNum { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime TravelDate { get; set; }
        /// <summary>
        /// consumersType
        /// </summary>		

        public string ConsumersType { get; set; }
        /// <summary>
        /// screeningId
        /// </summary>		

        public string ScreeningId { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime ScreeningBeginTime { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime ScreeningEndTime { get; set; }
        /// <summary>
        /// tcAmount
        /// </summary>		

        public decimal TcAmount { get; set; }
        /// <summary>
        /// totalPrice
        /// </summary>		

        public decimal TotalPrice { get; set; }
        /// <summary>
        /// isRealName
        /// </summary>		

        public long IsRealName { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { get; set; }
        public string StatesVal
        {
            get
            {
                string value = "";
                switch (States)
                {
                    case 1:
                        value = "已付款";
                        break;
                    case 2:
                        value = "已出票";
                        break;
                    case 3:
                        value = "已退票";
                        break;
                    default:
                        value = "未付款";
                        break;
                }
                return value;
            }
        }
    }

}
