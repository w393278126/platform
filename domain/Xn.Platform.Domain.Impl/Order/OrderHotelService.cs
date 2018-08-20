using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.Order
{
    public class OrderHotelService
    {
        private OrderHotelRepository orderHotelRepository = new OrderHotelRepository();
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<OrderHotelDTO> GetInfo(string Id)
        {
            try
            {
                var data = orderHotelRepository.GetInfo(Id);
                var res = Mapper.Map<OrderHotelDTO>(data);
                return Result.Success(res);
            }
            catch (Exception ex)
            {
                return Result.Error<OrderHotelDTO>(ResultCode.ExceptionError);
            }
        }
        /// <summary>
        /// 预定
        /// </summary>
        /// <returns></returns>
        public ResultWithCodeEntity Reserve(string Id)
        {
            try
            {
                var detail = orderHotelRepository.GetInfo(Id);
                if (detail == null)
                {
                    return Result.Error(ResultCode.ParameterError);
                }
                detail.states = 2;
                var editResult = orderHotelRepository.Update(detail);
                if (editResult)
                {
                    return Result.Success();
                }
                return Result.Error(ResultCode.DefaultError);

            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity Refund(OrderHotelRequest.RefundRequest request)
        {
            var result = new ResultWithCodeEntity();
            try
            {
                //1.获取退款订单的信息（request.Id）
                var detail = orderHotelRepository.GetInfo(request.Id);
                //2.更新退款订单信息（包括状态）
                if (detail != null)
                {

                    ////验证确认码（点击退款的时候发送一个短信验证码给客户，客户报出短信验证码才允许退款）
                    //if (detail.confirmCode != request.confirmCode)
                    //{
                    //    result.Code = ResultCode.ParameterError;
                    //    return result;
                    //}

                    var res = (OrderHotelModel)Mapper.Map(request, detail, request.GetType(), detail.GetType());
                    res.states = 3; //暂定退票状态为3
                    var editResult = orderHotelRepository.Update(res);
                    if (editResult)
                    {
                        return Result.Success(ResultCode.Success);
                    }
                }
                return Result.Error(ResultCode.ParameterError);
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
    }
}
