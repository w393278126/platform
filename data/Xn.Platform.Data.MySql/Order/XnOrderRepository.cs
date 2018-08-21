using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Data.MySql.Order
{
    public class XnOrderRepository : AbstractRepository<XnOrderModel>
    {
        public XnOrderRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }

    }
}
