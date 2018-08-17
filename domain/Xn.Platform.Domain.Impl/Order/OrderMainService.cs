using Xn.Platform.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;

namespace Xn.Platform.Domain.Impl.Order
{

    public class OrderMainService
    {
        private OrderMainRepository orderMainRepository = new OrderMainRepository();
        private OrderHotelRepository orderHotelRepository = new OrderHotelRepository();
        private OrderPlaneRepository orderPlaneRepository = new OrderPlaneRepository();
        private OrderTicketRepository orderTicketRepository = new OrderTicketRepository();
        /// <summary>
        /// 主订单分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<PagedEntity<OrderMainResponse.PageResponse>> PageList(OrderMainRequest.PageRequest request)
        {
            try
            {
                return new ResultWithCodeEntity<PagedEntity<OrderMainResponse.PageResponse>>
                {
                    Code = ResultCode.Success,
                    Data = orderMainRepository.PageInfo(request)
                };
            }
            catch (Exception ex)
            {

                return new ResultWithCodeEntity<PagedEntity<OrderMainResponse.PageResponse>>
                {
                    Code = ResultCode.ExceptionError,
                    Data = new PagedEntity<OrderMainResponse.PageResponse>()
                };
            }

        }

        /// <summary>
        /// 查询各个分表单的详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<object> GetInfo(string orderId, int type)
        {

            var result = new ResultWithCodeEntity<object> { Code = ResultCode.Success };
            try
            {
                //机票
                switch (type)
                {
                    case 1:
                        var plist = orderPlaneRepository.GetList(orderId);
                        result.Data = plist;
                        break;
                    case 2:
                        var hlist = orderHotelRepository.GetList(orderId);
                        result.Data = hlist;
                        break;
                    case 4:
                        var tlist = orderTicketRepository.GetList(orderId);
                        result.Data = tlist;
                        break;
                    case 3:
                    default:
                        var model = orderMainRepository.GetDetail(orderId);
                        result.Data = model;
                        break;

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;
                return result;
            }

        }
    }
}
