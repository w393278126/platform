using Xn.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;

namespace Xn.Platform.Data.MySql.Admin
{
    public class AdminRoleRepository : AbstractRepository<AdminRoleModel>
    {
        public AdminRoleRepository()
        {
            ConnectionString = ConfigSetting.ConnectionLongzhuSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionLongzhuSportsEntitiesReadOnly;
        }
    }
}
