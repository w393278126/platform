using Plu.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;

namespace Xn.Platform.Data.MySql.Admin
{
    public class AdminRoleResourceRepository : AbstractRepository<AdminRoleResourceModel>
    {
        public AdminRoleResourceRepository()
        {
            ConnectionString = ConfigSetting.ConnectionLongzhuSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionLongzhuSportsEntitiesReadOnly;
        }
    }
}
