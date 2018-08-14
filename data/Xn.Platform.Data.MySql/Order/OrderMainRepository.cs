using Plu.Platform.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain;

namespace Xn.Platform.Data.MySql.Order
{
    public class OrderMainRepository : AbstractRepository<OrderMainModel>
    {
        public OrderMainRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedEntity<OrderMainModel> PageInfo(OrderMainRequest.PageRequest request)
        {
            var parameter = new List<Tuple<string, string, object>>();
            //parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            if (request.ToSort)
                request.OrderBy += " desc ";
            var info = GetPagedEntity<OrderMainModel>(request.PageIndex, request.PageSize, request.OrderBy, parameter);
            return info;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderMainModel GetInfo(string Id)
        {
            return GetInfo(Id);
        }
        /// <summary>
        /// 增加订单
        /// </summary>
        /// <param name=""></param>
        public void Add(OrderMainModel entity)
        {
            Insert(entity);
        }
    }
}
