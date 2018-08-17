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
        public ResultWithCodeEntity<OrderHotelModel> GetInfo(string Id)
        {
            var result = new ResultWithCodeEntity<OrderHotelModel>();
            try
            {
                result.Code = ResultCode.Success;
                result.Data = orderHotelRepository.GetInfo(Id);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;
            }
            return result;
        }
        /// <summary>
        /// 预定
        /// </summary>
        /// <returns></returns>
        public ResultWithCodeEntity Reserve(string Id)
        {
            var result = new ResultWithCodeEntity();
            try
            {
                var detail = orderHotelRepository.GetInfo(Id);
                if (detail == null)
                {
                    result.Code = ResultCode.ParameterError;
                }
                else
                {
                    detail.states = 2;
                    var editResult = orderHotelRepository.Update(detail);
                    if (editResult)
                    {
                        result.Code = ResultCode.Success;
                    }
                    else
                    {
                        result.Code = ResultCode.DefaultError;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;
            }
            return result;
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
                    ///赋值
                    PropertyInfo[] propertys = request.GetType().GetProperties();
                    foreach (var item in propertys)
                    {
                        var val = item.GetValue(request);
                        //不为空的都赋值
                        if (val != null)
                        {
                            foreach (var item1 in detail.GetType().GetProperties())
                            {
                                if (item1.Name == item.Name)
                                {
                                    item1.SetValue(detail, val);
                                    break;
                                }
                            }
                        }
                    }
                    detail.states = 3; //暂定退票状态为3
                    var editResult = orderHotelRepository.Update(detail);
                    if (editResult)
                    {
                        result.Code = ResultCode.Success;
                    }
                    else
                        result.Code = ResultCode.DefaultError;
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
