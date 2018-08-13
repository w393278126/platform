using Plu.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Data.MySql;
using Xn.Platform.Data.MySql.Admin;

namespace Plu.Platform.Domain.Impl.Admin
{
    public class RoleResourceService: BaseService
    {
        private AbstractRepository<AdminRoleResourceModel> adminResourceRspository;
        private AbstractRepository<AdminRoleResourceModel> adminRoleResourceRspository;

        public RoleResourceService()
        {
            adminResourceRspository = GetService<AbstractRepository<AdminRoleResourceModel>>();
            adminRoleResourceRspository = GetService<AbstractRepository<AdminRoleResourceModel>>();
        }
        public object GetAuthAll()
        {
            var resource = adminResourceRspository.GetList<AdminResourceModel>(null);
            if (resource == null)
            {

            }
            var resourceIds = resource.Select(o => o.Id).ToList<int>();
            if (resourceIds == null)
            {

            }
            var roleResource = adminRoleResourceRspository.GetList<AdminRoleResourceModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("id", "in", resourceIds) });
            if (roleResource == null)
            {

            }
            var result = new List<int>();
            foreach (var item in roleResource)
            {
                if (item != null && item.ModelId > 0 && item.RoleId > 0)
                {
                    result.Add(1);
                }
            }

            return result;
        }

    }
}
