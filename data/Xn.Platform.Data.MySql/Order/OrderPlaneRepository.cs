using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Data.MySql.Order
{
    public class OrderPlaneRepository : AbstractRepository<OrderPlaneModel>
    {
        public OrderPlaneRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 通过订单ID查询机票列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderPlaneResponse.OrderPlan> GetList(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT u.username,u.mobile,o.* ");
            strSql.Append(" FROM t_order_orderplane o ");
            strSql.Append(" LEFT JOIN t_tour_user u on o.userID=u.id ");
            strSql.Append(" WHERE o.orderID=@orderId ");
            var list = new List<OrderPlaneResponse.OrderPlan>();
            OpenSlaveConnection(conn =>
            {
                list = conn.Query<OrderPlaneResponse.OrderPlan>(strSql.ToString(), new { orderId }).ToList();
            });
            return list;
        }
        /// <summary>
        /// 通过ID获取实体列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderPlaneModel GetInfoById(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            return GetList<OrderPlaneModel>(parameter).FirstOrDefault();
        }
    }
}
