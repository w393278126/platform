using Dapper;
using  Xn.Platform.Data.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using Plu.Platform.Core;
using Plu.Platform.Domain.EventRankList;

namespace Xn.Platform.Data.MySql.EventRankList
{
    public class GroupRepository : AbstractRepository<Group>
    {
        public GroupRepository()
        {
            ConnectionString = ConfigSetting.ConnectionEvent;
            SlaveConnectionString = ConfigSetting.ConnectionEventReadOnly;
        }
    }
}
