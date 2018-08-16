using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.OrderPlane
{
    public class OrderPlaneService
    {
        private OrderPlaneRepository orderPlaneRepository = new OrderPlaneRepository();
        public ResultWithCodeEntity<OrderPlaneModel> GetInfo(string Id)
        {
            var result = new ResultWithCodeEntity<OrderPlaneModel>();
            try
            {
                result.Code = ResultCode.Success;
                result.Data = orderPlaneRepository.GetInfoById(Id);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;
                result.Data = new OrderPlaneModel();
            }
            return result;
        }
        /// <summary>
        /// 退票方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity RefundPlane(OrderPlaneRequest.RefundRequest request)
        {
            var result = new ResultWithCodeEntity();
            try
            {
                //1.获取退款订单的信息（request.Id）
                var detail = orderPlaneRepository.GetInfoById(request.Id);
                //2.更新退款订单信息（包括状态）
                if (detail != null)
                {
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
                    detail.states = 3; //暂定退票状态为2
                    var editResult = orderPlaneRepository.Update(detail);
                    if (editResult)
                    {
                        result.Code = ResultCode.Success;
                    }
                    else
                        result.Code = ResultCode.DefaultError;
                }
                else
                    result.Code = ResultCode.ParameterError;

                
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.ExceptionError;
            }
            return result;
        }
    }
}
