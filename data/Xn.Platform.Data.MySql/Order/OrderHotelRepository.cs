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
        public List<OrderHotelDTO> GetList(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT u.username,u.mobile,o.* ");
            strSql.Append(" FROM t_order_orderhotel o ");
            strSql.Append(" LEFT JOIN t_tour_user u on o.userID=u.id ");
            strSql.Append(" WHERE o.orderID=@orderId ");
            var list = new List<OrderHotelDTO>();
            OpenSlaveConnection(conn =>
            {
                list = conn.Query<OrderHotelDTO>(strSql.ToString(), new { orderId }).ToList();
            });
            return list;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderHotelModel GetInfo(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            return GetList<OrderHotelModel>(parameter).FirstOrDefault();
           
        }

    }
}
