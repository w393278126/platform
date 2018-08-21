using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.Order
{
    public class XnOrderService
    {
        private XnOrderRepository xnOrderRepository = new XnOrderRepository();
        private XnPassengerRepository xnPassengerRepository = new XnPassengerRepository();
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<XnOrderDTO> GetInfo(int Id)
        {
            try
            {
                //1.获取订单基础信息
                var orderModel = xnOrderRepository.Get(Id);
                var res = Mapper.Map<XnOrderDTO>(orderModel);
                //2.通过订单ID获取出行人信息
                var passengers = xnPassengerRepository.GetPassengerList(Id);
                res.orderPassengerDTOs = passengers;
                return Result.Success(res);
            }
            catch (Exception ex)
            {
                return Result.Error<XnOrderDTO>(ResultCode.ExceptionError);
            }
        }
        /// <summary>
        /// 改价操作
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public ResultWithCodeEntity EditPrice(int Id, decimal price)
        {
            try
            {
                var detail = xnOrderRepository.Get(Id);
                detail.OrderAmount = price;
                var result = xnOrderRepository.Update(detail);
                if (result)
                    return Result.Success();
                else
                    return Result.Error(ResultCode.DefaultError);
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
    }
}
