using Xn.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xn.Platform.Data.MySql.Admin;
using Xn.Platform.Extensions;

namespace Xn.Platform.Domain.Impl.Admin
{
    public class RoleResourceService : BaseService
    {
        private static AdminRepository adminRepository = new AdminRepository();
        private static AdminResourceRepository adminResourceRepository = new AdminResourceRepository();
        private static AdminRoleRepository adminRoleRepository = new AdminRoleRepository();
        private static AdminRoleResourceRepository adminRoleResourceRepository = new AdminRoleResourceRepository();

        /// <summary>
        /// 获取所有的后台访问权限
        /// </summary>
        /// <returns></returns>
        public List<AdminRoleAuth> GetAuthAll()
        {
            //获取所有的管理员信息（含角色）
            var result = new List<AdminRoleAuth>();
            var users = adminRepository.GetList<AdminUserModel>(null);
            if (users == null)
            {
                return result;
            }
            //获取角色的管理的所有模块 以及角色的明细
            var roleIds = users.Select(o => o.Role).Distinct().ToList<int>();
            var roleResourceAll = adminRoleResourceRepository.GetList<AdminRoleResourceModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("RoleId", "in", roleIds) });
            if (roleResourceAll == null)
            {
                return result;
            }

            var roles = adminRoleRepository.GetList<AdminRoleModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("id", "in", roleIds) });
            if (roles == null)
            {
                return result;
            }

            var resourceIds = roleResourceAll.Select(o => o.ModelId).Distinct().ToList<int>();

            //获取管理模块的所有明细信息
            var resourceAll = adminResourceRepository.GetList<AdminResourceModel>(new List<Tuple<string, string, object>> { new Tuple<string, string, object>("id", "in", resourceIds) });
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
                if (modelIds != null && modelIds.Count() > 0)
                {
                    item.Resource = resourceAll.Where(o => modelIds.Contains(o.Id)).ToList();
                }
                result.Add(item);
            }

            return result;
        }

        public List<AdminRoleAuth> GetAuthAllCache()
        {
            return LocalCache.Current.GetOrSet("Admin_GetAuthAllCache", () => GetAuthAll(), 5.Minutes());
        }
        /// <summary>
        /// 是否具备功能访问权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsRoleAuthorized(int userId, string controller, string action)
        {
            var userAuth = GetAuthAllCache()?.First(o => o.UserId == userId);
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

        /// <summary>
        /// 获取指定用户的访问权限
        /// </summary>
        /// <returns></returns>
        public AdminRoleAuth GetAuthByUserId(int userId)
        {
            return GetAuthAllCache()?.First(o => o.UserId == userId);
        }

    }

}