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
    public class OrderTicketRepository : AbstractRepository<OrderTicketModel>
    {
        public OrderTicketRepository()
        {

            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 通过订单ID获取当前订单下的门票订单列表
        /// </summary>
        /// <param name="orderId">主订单ID</param>
        /// <returns></returns>
        public List<OrderTicketDTO> GetList(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT u.username,u.mobile,o.* ");
            strSql.Append(" FROM t_order_orderticket o ");
            strSql.Append(" LEFT JOIN t_tour_user u on o.userID=u.id ");
            strSql.Append(" WHERE o.orderID=@orderId ");
            var list = new List<OrderTicketDTO>();
            OpenSlaveConnection(conn =>
            {
                list = conn.Query<OrderTicketDTO>(strSql.ToString(), new { orderId }).ToList();
            });
            return list;
        }
        /// <summary>
        /// 通过主键ID获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderTicketModel GetEntityById(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            return GetList<OrderTicketModel>(parameter).FirstOrDefault();
        }
    }
}
