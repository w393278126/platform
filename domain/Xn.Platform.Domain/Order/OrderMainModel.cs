using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    [Table("t_order_ordermain")]
    public class OrderMainModel
    {
        /// <summary>
        /// id
        /// </summary>		

        public string id { get; set; }
        /// <summary>
        /// orderID
        /// </summary>		

        public string orderID { get; set; }
        /// <summary>
        /// channelID
        /// </summary>		

        public string channelID { get; set; }
        /// <summary>
        /// orderType
        /// </summary>		

        public int orderType { get; set; }
        /// <summary>
        /// num
        /// </summary>		

        public int num { get; set; }
        /// <summary>
        /// userID
        /// </summary>		

        public string userID { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime addDate { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime sendDate { get; set; }
        /// <summary>
        /// states
        /// </summary>		

        public int states { get; set; }
        /// <summary>
        /// receiverName
        /// </summary>		

        public string receiverName { get; set; }
        /// <summary>
        /// receiverTelphone
        /// </summary>		

        public string receiverTelphone { get; set; }
        /// <summary>
        /// payName
        /// </summary>		

        public string payName { get; set; }
        /// <summary>
        /// payTelephone
        /// </summary>		

        public string payTelephone { get; set; }
        /// <summary>
        /// payType
        /// </summary>		

        public int payType { get; set; }
        /// <summary>
        /// payTSN
        /// </summary>		

        public string payTSN { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime payDate { get; set; }
        /// <summary>
        /// orderAmount
        /// </summary>		

        public decimal orderAmount { get; set; }
        /// <summary>
        /// payAmount
        /// </summary>		

        public decimal payAmount { get; set; }
        /// <summary>
        /// refundID
        /// </summary>		

        public string refundID { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime refundDate { get; set; }
        /// <summary>
        /// refundAmout
        /// </summary>		

        public decimal refundAmout { get; set; }
        /// <summary>
        /// expressAmount
        /// </summary>		

        public decimal expressAmount { get; set; }
        /// <summary>
        /// postType
        /// </summary>		

        public int postType { get; set; }
        /// <summary>
        /// expressCode
        /// </summary>		

        public string expressCode { get; set; }
        /// <summary>
        /// expressName
        /// </summary>		

        public string expressName { get; set; }
        /// <summary>
        /// expressOdd
        /// </summary>		

        public string expressOdd { get; set; }
        /// <summary>
        /// provinceID
        /// </summary>		

        public string provinceID { get; set; }
        /// <summary>
        /// provinceName
        /// </summary>		

        public string provinceName { get; set; }
        /// <summary>
        /// cityID
        /// </summary>		

        public string cityID { get; set; }
        /// <summary>
        /// cityName
        /// </summary>		

        public string cityName { get; set; }
        /// <summary>
        /// areaID
        /// </summary>		

        public string areaID { get; set; }
        /// <summary>
        /// address
        /// </summary>		

        public string address { get; set; }
        /// <summary>
        /// areaName
        /// </summary>		

        public string areaName { get; set; }
        /// <summary>
        /// platform
        /// </summary>		

        public string platform { get; set; }
        /// <summary>
        /// deviceID
        /// </summary>		

        public string deviceID { get; set; }
        /// <summary>
        /// longitude
        /// </summary>		

        public string longitude { get; set; }
        /// <summary>
        /// latitude
        /// </summary>		

        public string latitude { get; set; }
        /// <summary>
        /// mark1
        /// </summary>		

        public string mark1 { get; set; }
        /// <summary>
        /// mark2
        /// </summary>		

        public string mark2 { get; set; }
        /// <summary>
        /// mark3
        /// </summary>		

        public string mark3 { get; set; }
        /// <summary>
        /// mark4
        /// </summary>		

        public string mark4 { get; set; }
        /// <summary>
        /// mark5
        /// </summary>		

        public string mark5 { get; set; }
        /// <summary>
        /// mark6
        /// </summary>		

        public string mark6 { get; set; }
        /// <summary>
        /// mark7
        /// </summary>		

        public string mark7 { get; set; }
        /// <summary>
        /// mark8
        /// </summary>		

        public string mark8 { get; set; }
        /// <summary>
        /// mark9
        /// </summary>		

        public string mark9 { get; set; }
        /// <summary>
        /// mark10
        /// </summary>		

        public string mark10 { get; set; }
        /// <summary>
        /// mark11
        /// </summary>		

        public string mark11 { get; set; }
        /// <summary>
        /// mark12
        /// </summary>		

        public string mark12 { get; set; }
        /// <summary>
        /// mark13
        /// </summary>		

        public string mark13 { get; set; }
        /// <summary>
        /// mark14
        /// </summary>		

        public string mark14 { get; set; }
        /// <summary>
        /// mark15
        /// </summary>		

        public string mark15 { get; set; }
        /// <summary>
        /// mark16
        /// </summary>		

        public string mark16 { get; set; }
        /// <summary>
        /// mark17
        /// </summary>		

        public string mark17 { get; set; }
        /// <summary>
        /// mark18
        /// </summary>		

        public string mark18 { get; set; }
        /// <summary>
        /// mark19
        /// </summary>		

        public string mark19 { get; set; }
        /// <summary>
        /// mark20
        /// </summary>		

        public string mark20 { get; set; }
    }

    public class OrderMainResponse
    {
        public class PageResponse : OrderMainModel
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string userName { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string mobile { get; set; }
            public string orderTypeName
            {
                get
                {
                    string value = "未知";
                    switch (orderType)
                    {
                        case 1:
                            value = "机票";
                            break;
                        case 2:
                            value = "酒店";
                            break;
                        case 3:
                            value = "签证";
                            break;
                        case 4:
                            value = "门票";
                            break;
                        default:
                            break;
                    }
                    return value;
                }
            }
        }
        public class PageCount
        {
            public int TotalCount { get; set; }
        }
    }

    /// <summary>
    /// 主订单查询条件
    /// </summary>
    public class OrderMainRequest
    {
        /// <summary>
        /// 分页查询条件
        /// </summary>
        public class PageRequest : BaseQueryModel
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string userName { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 订单ID
            /// </summary>
            public string orderId { get; set; }
            /// <summary>
            /// 订单类型
            /// 1、机票 2、酒店 3、签证4、门票
            /// </summary>
            public int orderType { get; set; }
            /// <summary>
            /// 支付时间
            /// </summary>
            public string SpayDate { get; set; }
            public string EpayDate { get; set; }
            /// <summary>
            /// 下单时间
            /// </summary>
            public string SaddDate { get; set; }
            public string EaddDate { get; set; }
        }
    }
}
