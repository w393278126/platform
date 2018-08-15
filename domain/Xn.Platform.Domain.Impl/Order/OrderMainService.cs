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
        private OrderMainRepository tourUserRepository = new OrderMainRepository();
        public ResultWithCodeEntity<PagedEntity<OrderMainResponse.PageResponse>> PageList(OrderMainRequest.PageRequest request)
        {
            try
            {
                return new ResultWithCodeEntity<PagedEntity<OrderMainResponse.PageResponse>>
                {
                    Code = ResultCode.Success,
                    Data = tourUserRepository.PageInfo(request)
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

    }
}
