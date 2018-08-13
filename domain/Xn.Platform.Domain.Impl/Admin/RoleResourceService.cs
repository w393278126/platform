using Plu.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Data.MySql;
using Xn.Platform.Data.MySql.Admin;
using Xn.Platform.Extensions;

namespace Plu.Platform.Domain.Impl.Admin
{
    public class RoleResourceService : BaseService
    {
        private AbstractRepository<AdminRoleResourceModel> adminResourceRspository;
        private AbstractRepository<AdminRoleResourceModel> adminRoleResourceRspository;
        private AbstractRepository<AdminRoleModel> adminRoleRspository;
        private AbstractRepository<AdminUserModel> adminUserRspository;

        public RoleResourceService()
        {
            adminResourceRspository = GetService<AbstractRepository<AdminRoleResourceModel>>();
            adminRoleResourceRspository = GetService<AbstractRepository<AdminRoleResourceModel>>();
            adminRoleRspository = GetService<AbstractRepository<AdminRoleModel>>();
            adminUserRspository = GetService<AbstractRepository<AdminUserModel>>();
        }
        /// <summary>
        /// 获取所有的后台访问权限
        /// </summary>
        /// <returns></returns>
        public List<AdminRoleAuth> GetAuthAll()
        {
            //获取所有的管理员信息（含角色）
            var result = new List<AdminRoleAuth>();
            var users = adminResourceRspository.GetList<AdminUserModel>(null);
            if (users == null)
            {
                return result;
            }
            //获取角色的管理的所有模块 以及角色的明细
            var roleIds = users.Select(o => o.Role).Distinct().ToList<int>();
            var roleResourceAll = adminRoleResourceRspository.GetList<AdminRoleResourceModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("RoleId", "in", roleIds) });
            if (roleResourceAll == null)
            {
                return result;
            }

            var roles = adminRoleRspository.GetList<AdminRoleModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("id", "in", roleIds) });
            if (roles == null)
            {
                return result;
            }

            var resourceIds = roleResourceAll.Select(o => o.ModelId).Distinct().ToList<int>();

            //获取管理模块的所有明细信息
            var resourceAll = adminResourceRspository.GetList<AdminResourceModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("id", "in", resourceIds) });
            if (resourceAll == null)
            {
                return result;
            }

            foreach (var uitem in users)
            {
                if (uitem == null && uitem.Role <= 0)
                {
                    continue;
                }
                var item = new AdminRoleAuth();
                item.UserId = uitem.Id;
                item.RoleId = uitem.Role;
                item.Type = roles.Where(o => o.Id == item.RoleId).FirstOrDefault().Type;
                var modelIds = roleResourceAll.Where(o => o.RoleId == item.RoleId).Select(o => o.ModelId);
                if (modelIds == null || modelIds.Count() <= 0)
                {
                    continue;
                }
                item.Resource = resourceAll.Where(o => modelIds.Contains(o.Id)).ToList();
                result.Add(item);
            }

            return result;
        }

        public List<AdminRoleAuth> GetAuthAllCache()
        {
            return LocalCache.Current.GetOrSet("Admin_GetAuthAllCache", () => GetAuthAll(), 5.Minutes());
        }
        /// <summary>
        /// 模块访问权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsRoleAuthorized(int userId, string controller, string action)
        {
            var userAuth = GetAuthAllCache().First(o => o.UserId == userId);
            if (userAuth == null)
            {
                return false;
            }
            if (userAuth.Type == AdminType.SuperAdmin)
            {
                return true;
            }
            else if (userAuth.Type == AdminType.FunctionAdmin)
            {
                return userAuth.Resource.Exists(o => o.Controller.ToLower() == controller.ToLower() && o.Action.ToLower() == action.ToLower());
            }
            return false;
        }
    }

}