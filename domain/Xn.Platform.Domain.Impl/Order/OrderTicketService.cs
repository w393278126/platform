using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using AutoMapper;

namespace Xn.Platform.Domain.Impl.Order
{
    public class OrderTicketService
    {
        public OrderTicketRepository orderTicketRepository = new OrderTicketRepository();
        /// <summary>
        /// 退款
        /// </summary>
        /// <returns></returns>
        public ResultWithCodeEntity RefundTicekt(string Id)
        {
            try
            {
                var detail = orderTicketRepository.GetEntityById(Id);
                if (detail == null)
                    return Result.Error(ResultCode.ParameterError);
                detail.States = 3;  ///暂定退款状态为3
                var result = orderTicketRepository.Update(detail);
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
        /// <summary>
        /// 出票
        /// </summary>
        /// <returns></returns>
        public ResultWithCodeEntity OutOfTicket(string Id)
        {

            try
            {
                var detail = orderTicketRepository.GetEntityById(Id);
                if (detail == null)
                    return Result.Error(ResultCode.ParameterError);
                detail.States = 2;  ///暂定退款状态为3
                var result = orderTicketRepository.Update(detail);
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
