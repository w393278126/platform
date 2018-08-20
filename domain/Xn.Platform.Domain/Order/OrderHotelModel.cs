using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    [Table("t_order_orderhotel")]
    public class OrderHotelModel
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
        /// bookingID
        /// </summary>		

        public string bookingID { get; set; }
        /// <summary>
        /// bookingStates
        /// </summary>		

        public string bookingStates { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int states { get; set; }
        /// <summary>
        /// cityID
        /// </summary>		

        public string cityID { get; set; }
        /// <summary>
        /// cityName
        /// </summary>		

        public string cityName { get; set; }
        /// <summary>
        /// hotelID
        /// </summary>		

        public string hotelID { get; set; }
        /// <summary>
        /// hotelName
        /// </summary>		

        public string hotelName { get; set; }
        /// <summary>
        /// hotelPhone
        /// </summary>		

        public string hotelPhone { get; set; }
        /// <summary>
        /// checkInDate
        /// </summary>		

        public string checkInDate { get; set; }
        /// <summary>
        /// checkOutDate
        /// </summary>		

        public string checkOutDate { get; set; }
        /// <summary>
        /// nights
        /// </summary>		

        public int nights { get; set; }
        /// <summary>
        /// roomName
        /// </summary>		

        public string roomName { get; set; }
        /// <summary>
        /// breakfast
        /// </summary>		

        public string breakfast { get; set; }
        /// <summary>
        /// nationality
        /// </summary>		

        public string nationality { get; set; }
        /// <summary>
        /// adult
        /// </summary>		

        public string adult { get; set; }
        /// <summary>
        /// children
        /// </summary>		

        public string children { get; set; }
        /// <summary>
        /// childrenAge
        /// </summary>		

        public string childrenAge { get; set; }
        /// <summary>
        /// rateCode
        /// </summary>		

        public string rateCode { get; set; }
        /// <summary>
        /// roomCount
        /// </summary>		

        public string roomCount { get; set; }
        /// <summary>
        /// currency
        /// </summary>		

        public string currency { get; set; }
        /// <summary>
        /// totalAmount
        /// </summary>		

        public decimal totalAmount { get; set; }
        /// <summary>
        /// name
        /// </summary>		

        public string name { get; set; }
        /// <summary>
        /// phone
        /// </summary>		

        public string phone { get; set; }
        /// <summary>
        /// email
        /// </summary>		

        public string email { get; set; }
        /// <summary>
        /// guestRemarks
        /// </summary>		

        public string guestRemarks { get; set; }
        /// <summary>
        /// rooms
        /// </summary>		

        public string rooms { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime addDate { get; set; }
        /// <summary>
        /// channel
        /// </summary>		

        public int channel { get; set; }
        /// <summary>
        /// confirmCode
        /// </summary>		

        public string confirmCode { get; set; }
        /// <summary>
        /// cancelReason
        /// </summary>		

        public string cancelReason { get; set; }
        /// <summary>
        /// cancelAmount
        /// </summary>		

        public decimal cancelAmount { get; set; }
        /// <summary>
        /// costPrice
        /// </summary>		

        public string costPrice { get; set; }
        /// <summary>
        /// credit
        /// </summary>		

        public string credit { get; set; }
    }

    public class OrderHotelDTO
    {
        /// <summary>
        /// id
        /// </summary>		

        public string Id { get; set; }
        /// <summary>
        /// userID
        /// </summary>		

        public string UserID { get; set; }
        /// <summary>
        /// orderID
        /// </summary>		

        public string OrderID { get; set; }
        /// <summary>
        /// bookingID
        /// </summary>		

        public string BookingID { get; set; }
        /// <summary>
        /// bookingStates
        /// </summary>		

        public string BookingStates { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int States { get; set; }
        /// <summary>
        /// cityID
        /// </summary>		

        public string CityID { get; set; }
        /// <summary>
        /// cityName
        /// </summary>		

        public string CityName { get; set; }
        /// <summary>
        /// hotelID
        /// </summary>		

        public string HotelID { get; set; }
        /// <summary>
        /// hotelName
        /// </summary>		

        public string HotelName { get; set; }
        /// <summary>
        /// hotelPhone
        /// </summary>		

        public string HotelPhone { get; set; }
        /// <summary>
        /// checkInDate
        /// </summary>		

        public string CheckInDate { get; set; }
        /// <summary>
        /// checkOutDate
        /// </summary>		

        public string CheckOutDate { get; set; }
        /// <summary>
        /// nights
        /// </summary>		

        public int Nights { get; set; }
        /// <summary>
        /// roomName
        /// </summary>		

        public string RoomName { get; set; }
        /// <summary>
        /// breakfast
        /// </summary>		

        public string Breakfast { get; set; }
        /// <summary>
        /// nationality
        /// </summary>		

        public string Nationality { get; set; }
        /// <summary>
        /// adult
        /// </summary>		

        public string Adult { get; set; }
        /// <summary>
        /// children
        /// </summary>		

        public string Children { get; set; }
        /// <summary>
        /// childrenAge
        /// </summary>		

        public string ChildrenAge { get; set; }
        /// <summary>
        /// rateCode
        /// </summary>		

        public string RateCode { get; set; }
        /// <summary>
        /// roomCount
        /// </summary>		

        public string RoomCount { get; set; }
        /// <summary>
        /// currency
        /// </summary>		

        public string Currency { get; set; }
        /// <summary>
        /// totalAmount
        /// </summary>		

        public decimal TotalAmount { get; set; }
        /// <summary>
        /// name
        /// </summary>		

        public string Name { get; set; }
        /// <summary>
        /// phone
        /// </summary>		

        public string Phone { get; set; }
        /// <summary>
        /// email
        /// </summary>		

        public string Email { get; set; }
        /// <summary>
        /// guestRemarks
        /// </summary>		

        public string GuestRemarks { get; set; }
        /// <summary>
        /// rooms
        /// </summary>		

        public string Rooms { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime AddDate { get; set; }
        /// <summary>
        /// channel
        /// </summary>		

        public int Channel { get; set; }
        /// <summary>
        /// confirmCode
        /// </summary>		

        public string ConfirmCode { get; set; }
        /// <summary>
        /// cancelReason
        /// </summary>		

        public string CancelReason { get; set; }
        /// <summary>
        /// cancelAmount
        /// </summary>		

        public decimal CancelAmount { get; set; }
        /// <summary>
        /// costPrice
        /// </summary>		

        public string CostPrice { get; set; }
        /// <summary>
        /// credit
        /// </summary>		

        public string Credit { get; set; }

        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string StatesVal
        {
            get
            {
                var value = "";
                switch (States)
                {
                    case 1:
                        value = "已付款";
                        break;
                    case 2:
                        value = "已预定";
                        break;
                    case 3:
                        value = "已退款";
                        break;
                    default:
                        value = "未付款";
                        break;
                }
                return value;
            }
        }
    }

    public class OrderHotelRequest
    {
        public class RefundRequest
        {
            public string Id { get; set; }
            /// <summary>
            /// 退款金额
            /// </summary>
            public decimal CancelAmount { get; set; }
            /// <summary>
            /// 退款原因
            /// </summary>
            public string CancelReason { get; set; }
            /// <summary>
            /// 取消确认码
            /// </summary>
            public string ConfirmCode { get; set; }
        }
    }


}
