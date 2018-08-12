using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Presentation.Admin.Models.Auth
{
    public class SettingConfig
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public int GroupIndex { get; set; }
        public string Group { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }

        //public List<AdminRoleKey> AuthorizedRoles { get; set; }
        /*
        public bool IsAuthorized(AdminRoleKey current)
        {
            if (AuthorizedRoles != null &&
                AuthorizedRoles.Count() > 0 &&
                AuthorizedRoles.Exists(p => p.RoleGroup == current.RoleGroup))
            {
                var adminRoleList = AuthorizedRoles.FindAll(p => p.RoleGroup == current.RoleGroup);
                foreach (var r in adminRoleList)
                {
                    if ((r.RoleId & current.RoleId) == r.RoleId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        */
    }


}