using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    [Table("t_order_orderplane")]
    public class OrderPlaneModel
    {
        /// <summary>
        /// id
        /// </summary>		

        public string id { get; set; }
        /// <summary>
        /// userID
        /// </summary>		

        public string userID { get; set; }
        /// <summary>
        /// orderID
        /// </summary>		

        public string orderID { get; set; }
        /// <summary>
        /// states 0：未付款 1：已付款 2：已出票 3:已退票 
        /// </summary>		

        public int states { get; set; }
        /// <summary>
        /// startAddr
        /// </summary>		

        public string startAddr { get; set; }
        /// <summary>
        /// startAirport
        /// </summary>		

        public string startAirport { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime startDate { get; set; }
        /// <summary>
        /// destAddr
        /// </summary>		

        public string destAddr { get; set; }
        /// <summary>
        /// destAirport
        /// </summary>		

        public string destAirport { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime destDate { get; set; }
        /// <summary>
        /// custName
        /// </summary>		

        public string custName { get; set; }
        /// <summary>
        /// idCardNo
        /// </summary>		

        public string idCardNo { get; set; }
        /// <summary>
        /// personType
        /// </summary>		

        public int personType { get; set; }
        /// <summary>
        /// airways
        /// </summary>		

        public string airways { get; set; }
        /// <summary>
        /// airwaysIcon
        /// </summary>		

        public string airwaysIcon { get; set; }
        /// <summary>
        /// flightNumber
        /// </summary>		

        public string flightNumber { get; set; }
        /// <summary>
        /// roomType
        /// </summary>		

        public int roomType { get; set; }
        /// <summary>
        /// price
        /// </summary>		

        public decimal price { get; set; }
        /// <summary>
        /// salePrice
        /// </summary>		

        public decimal salePrice { get; set; }
        /// <summary>
        /// cancelReason
        /// </summary>		

        public string cancelReason { get; set; }
        /// <summary>
        /// returnFee
        /// </summary>		

        public decimal returnFee { get; set; }
        /// <summary>
        /// returnTotal
        /// </summary>		

        public decimal returnTotal { get; set; }
        /// <summary>
        /// channelID
        /// </summary>		

        public string channelID { get; set; }
        /// <summary>
        /// noticeId
        /// </summary>		

        public string noticeId { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime addDate { get; set; }
        /// <summary>
        /// costPrice
        /// </summary>		

        public decimal costPrice { get; set; }
        /// <summary>
        /// desAirCode
        /// </summary>		

        public string desAirCode { get; set; }
        /// <summary>
        /// tripNo
        /// </summary>		

        public string tripNo { get; set; }
        /// <summary>
        /// depAirCode
        /// </summary>		

        public string depAirCode { get; set; }
        /// <summary>
        /// psgInfo
        /// </summary>		

        public string psgInfo { get; set; }
        /// <summary>
        /// sellsPrice
        /// </summary>		

        public decimal sellsPrice { get; set; }
    }
    public class OrderPlaneDTO
    {
        /// <summary>
        /// id
        /// </summary>		

        public string Id { get; set; }
        /// <summary>
        /// orderID
        /// </summary>		

        public string OrderId { get; set; }
        /// <summary>
        /// states 0：未付款 1：已付款 2：已出票 3:已退票 
        /// </summary>		

        public int States { get; set; }
        /// <summary>
        /// startAddr
        /// </summary>		

        public string StartAddr { get; set; }
        /// <summary>
        /// startAirport
        /// </summary>		

        public string StartAirport { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime StartDate { get; set; }
        /// <summary>
        /// destAddr
        /// </summary>		

        public string DestAddr { get; set; }
        /// <summary>
        /// destAirport
        /// </summary>		

        public string DestAirport { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime DestDate { get; set; }
        /// <summary>
        /// custName
        /// </summary>		

        public string CustName { get; set; }
        /// <summary>
        /// idCardNo
        /// </summary>		

        public string IdCardNo { get; set; }
        /// <summary>
        /// personType
        /// </summary>		

        public int PersonType { get; set; }
        /// <summary>
        /// airways
        /// </summary>		

        public string Airways { get; set; }
        /// <summary>
        /// airwaysIcon
        /// </summary>		

        public string AirwaysIcon { get; set; }
        /// <summary>
        /// flightNumber
        /// </summary>		

        public string FlightNumber { get; set; }
        /// <summary>
        /// roomType
        /// </summary>		

        public int RoomType { get; set; }
        /// <summary>
        /// price
        /// </summary>		

        public decimal Price { get; set; }
        /// <summary>
        /// salePrice
        /// </summary>		

        public decimal SalePrice { get; set; }
        /// <summary>
        /// cancelReason
        /// </summary>		

        public string CancelReason { get; set; }
        /// <summary>
        /// returnFee
        /// </summary>		

        public decimal ReturnFee { get; set; }
        /// <summary>
        /// returnTotal
        /// </summary>		

        public decimal ReturnTotal { get; set; }
        /// <summary>
        /// channelID
        /// </summary>		

        public string ChannelID { get; set; }
        /// <summary>
        /// noticeId
        /// </summary>		

        public string NoticeId { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime AddDate { get; set; }
        /// <summary>
        /// costPrice
        /// </summary>		

        public decimal CostPrice { get; set; }
        /// <summary>
        /// desAirCode
        /// </summary>		

        public string DesAirCode { get; set; }
        /// <summary>
        /// tripNo
        /// </summary>		

        public string TripNo { get; set; }
        /// <summary>
        /// depAirCode
        /// </summary>		

        public string DepAirCode { get; set; }
        /// <summary>
        /// psgInfo
        /// </summary>		

        public string PsgInfo { get; set; }
        /// <summary>
        /// sellsPrice
        /// </summary>		

        public decimal SellsPrice { get; set; }
        public string UserName { get; set; }
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

    public class OrderPlaneRequest
    {
        public class RefundRequest
        {
            public string Id { get; set; }
            /// <summary>
            /// 退款金额
            /// </summary>
            public decimal ReturnTotal { get; set; }
            /// <summary>
            /// 手续费
            /// </summary>
            public decimal ReturnFee { get; set; }
            /// <summary>
            /// 退款原因
            /// </summary>
            public string CancelReason { get; set; }
        }
    }
}
