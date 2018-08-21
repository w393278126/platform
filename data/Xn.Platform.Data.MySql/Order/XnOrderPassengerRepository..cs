using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.Order;
using Xn.Platform.Core;
using Dapper;

namespace Xn.Platform.Data.MySql.Order
{
    public class XnOrderPassengerRepository : AbstractRepository<XnOrderPassengerModel>
    {
        public XnOrderPassengerRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 删除关联数据
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="PassengerId">游客ID</param>
        /// <returns></returns>
        public int DeleteOrderPassenger(int OrderId, int PassengerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM xnorderpassenger WHERE OrderId=@OrderId AND PassengerId=@PassengerId");
            int count = 0;
            OpenSlaveConnection(conn => count = conn.Execute(strSql.ToString(), new { OrderId, PassengerId }));
            return count;
        }
    }
}
