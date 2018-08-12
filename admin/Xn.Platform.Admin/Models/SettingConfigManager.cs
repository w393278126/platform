using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Xn.Platform.Presentation.Admin.Models.Auth
{
    public class SettingConfigManager
    {
        private string setting_config_cache_key = "setting_config_cache_key";
        List<SettingConfig> _authSet = null;
        object _lockItem = new object();
        private int UserId { get; set; }

        public SettingConfigManager(int userid)
        {
            this.UserId = userid;
        }


        public List<SettingConfig> AuthorizationConfig
        {
            get
            {
                if (_authSet == null)
                {
                    lock (_lockItem)
                    {
                        if (HttpContext.Current.Cache[setting_config_cache_key] == null)
                        {
                            _LoadAuthSet();
                            HttpContext.Current.Cache.Insert(setting_config_cache_key, _authSet, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration);

                        }
                        _authSet = HttpContext.Current.Cache[setting_config_cache_key] as List<SettingConfig>;
                    }
                }
                return _authSet;
            }
        }

        /// <summary>
        /// get all authorized action of role's collection
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<SettingConfig> GetAuthorizedAction()
        {
            return null;
            //return AuthorizationConfig.Where(c => c.IsAuthorized(roles));
        }

        /// <summary>
        /// check authorization of given role's collection
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsRoleAuthorized(string controller, string action)
        {
            return true;
            //var config = AuthorizationConfig
            //       .Where(c => c.Controller.ToLower() == controller.ToLower() && c.Action.ToLower() == action.ToLower()).FirstOrDefault();
            //return (config != null && config.IsAuthorized(roles));
        }

        void _LoadAuthSet()
        {
            List<SettingConfig> configList = new List<SettingConfig>();
            /*
            var controllerList = _permissionResourceRepository.GetIdentifie1(1);
            foreach (var item_control in controllerList)
            {
                var actionList = _permissionResourceRepository.GetPermissionResource(item_control).Distinct().ToList();
                foreach (var item_action in actionList)
                {
                    var category = _permissionResourceCategoryRepository.GetPermissionResourceCategory(item_action.CategoryId);
                    var group = string.Empty;
                    var groupIndex = 0;
                    if (category != null)
                    {
                        group = category.CategoryName;
                        groupIndex = category.Id;
                    }
                    SettingConfig config = new SettingConfig();
                    config.Controller = item_control;
                    config.Action = item_action.Identifie2;

                    config.Group = group;
                    config.GroupIndex = groupIndex;
                    config.Index = item_action.Sort;
                    config.Title = item_action.Title;

                    var parameters = new List<Tuple<string, string, object>>();
                    parameters.Add(new Tuple<string, string, object>("ResourceId", "=", item_action.Id));
                    config.AuthorizedRoles = _permissionResourceOfRoleRepository.GetList<PermissionResourceOfRole>(parameters).Select(p => new AdminRoleKey() { RoleId = p.RoleId, RoleGroup = p.RoleGroup }).ToList();
                    
                    configList.Add(config);
                }
            }
            */
            _authSet = configList.OrderBy(c => c.Index).ThenBy(c => c.GroupIndex).ToList();
        }
    }

}