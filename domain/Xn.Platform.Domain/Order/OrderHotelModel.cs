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

   
}
