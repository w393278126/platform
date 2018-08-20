using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using Xn.Platform.Domain.Order;
using AutoMapper;

namespace Xn.Platform.Domain.Impl
{
    public class OrderPlaneService
    {
        private OrderPlaneRepository orderPlaneRepository = new OrderPlaneRepository();
        public ResultWithCodeEntity<OrderPlaneDTO> GetInfo(string Id)
        {

            try
            {
                var result = orderPlaneRepository.GetInfoById(Id);
                var res = Mapper.Map<OrderPlaneDTO>(result);
                return Result.Success(res);
            }
            catch (Exception ex)
            {
                return Result.Error<OrderPlaneDTO>(ResultCode.ExceptionError);
            }
        }
        /// <summary>
        /// 退票方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity RefundPlane(OrderPlaneRequest.RefundRequest request)
        {
            try
            {
                //1.获取退款订单的信息（request.Id）
                var detail = orderPlaneRepository.GetInfoById(request.Id);
                //2.更新退款订单信息（包括状态）
                if (detail != null)
                {
                    var res = (OrderPlaneModel)Mapper.Map(request, detail, request.GetType(), detail.GetType());
                    res.states = 3; //暂定退票状态为3
                    var editResult = orderPlaneRepository.Update(res);
                    if (editResult)
                    {
                        return Result.Success();
                    }
                    return Result.Error(ResultCode.DefaultError);
                }
                return Result.Error(ResultCode.ParameterError);
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }

        /// <summary>
        /// 出票
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity OutOfPlane(string Id)
        {
            var result = new ResultWithCodeEntity();
            try
            {
                //1.获取退款订单的信息（request.Id）
                var detail = orderPlaneRepository.GetInfoById(Id);
                if (detail != null)
                {
                    detail.states = 2;
                    var editResult = orderPlaneRepository.Update(detail);
                    if (editResult)
                    {
                        result.Code = ResultCode.Success;
                    }
                    else
                    {
                        result.Code = ResultCode.DefaultError;
                    }
                }
                else
                {
                    result.Code = ResultCode.ParameterError;
                }

            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;

            }
            return result;
        }
    }
}
