using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;

namespace Xn.Platform.Domain.Impl.Order
{
    public class XnOrderPassengerService
    {
        private XnOrderPassengerRepository xnOrderPassengerRepository = new XnOrderPassengerRepository();

        /// <summary>
        /// 移除游客订单关联数据
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="PassengerId"></param>
        /// <returns></returns>
        public ResultWithCodeEntity DeletePassenger(int OrderId, int PassengerId)
        {
            try
            {
                var count = xnOrderPassengerRepository.DeleteOrderPassenger(OrderId, PassengerId);
                if (count > 0)
                {
                    return Result.Success();
                }
                else
                {
                    return Result.Error(ResultCode.DefaultError);
                }
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
    }
}
