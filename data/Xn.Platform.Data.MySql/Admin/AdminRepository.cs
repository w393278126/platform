using Plu.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.MySql.Admin
{
    public class AdminRepository : AbstractRepository<AdminUserModel>
    {
        public AdminRoleResourceRepository()
        {
            ConnectionString = ConfigSetting.ConnectionLongzhuSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionLongzhuSportsEntitiesReadOnly;
        }

        public AdminUserModel GetInfo(string UserName, string PassWord)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("UserName", "=", UserName));
            parameter.Add(new Tuple<string, string, object>("PassWord", "=", PassWord));
            var info = GetList<AdminUserModel>(0, 1, "id desc", parameter).FirstOrDefault();
            return info;
        }
    }
}
