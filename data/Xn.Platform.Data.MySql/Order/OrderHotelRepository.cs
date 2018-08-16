using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain.Order;
using Dapper;

namespace Xn.Platform.Data.MySql.Order
{
    public class OrderHotelRepository : AbstractRepository<OrderHotelModel>
    {
        public OrderHotelRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        public List<OrderHotelResponse.OrderHotel> GetList(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT u.username,u.mobile,o.* ");
            strSql.Append(" FROM t_order_orderhotel o ");
            strSql.Append(" LEFT JOIN t_tour_user u on o.userID=u.id ");
            strSql.Append(" WHERE o.orderID=@orderId ");
            var list = new List<OrderHotelResponse.OrderHotel>();
            OpenSlaveConnection(conn =>
            {
                list = conn.Query<OrderHotelResponse.OrderHotel>(strSql.ToString(), new { orderId }).ToList();
            });
            return list;

        }

    }
}
