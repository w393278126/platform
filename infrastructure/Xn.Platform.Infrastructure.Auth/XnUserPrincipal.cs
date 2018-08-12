using Xn.Platform.Core;

using Xn.Platform.Domain.Admin;
using Sso;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;

namespace Xn.Platform.Infrastructure.Auth
{
    public class XnUserPrincipal : IPrincipal
    {

        public XnUserPrincipal()
        {
            Identity = new PluUserIdentity();
        }

        public XnUserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            if (Identity.IsAuthenticated)
            {
                //RoomRole r;
                int r;
                if (Enum.TryParse(role, out r))
                {
                    //  return (Roles & r) == r;
                    return false;
                }
            }
            return false;
        }



        #region 后台用户
        public List<AdminRoleKey> AdminRoles
        {
            get
            {
                LoadCurrentAdminUserRoles();
                return _adminRoles;
            }
        }
        List<AdminRoleKey> _adminRoles = new List<AdminRoleKey>();
        bool _adminRoleLoaded = false;

        private void LoadCurrentAdminUserRoles()
        {
            if (!_adminRoleLoaded)
            {
                int userId = 0;
                List<AdminRoleKey> rs = new List<AdminRoleKey>();
                if (int.TryParse(Identity.Name, out userId))
                {
                   // rs = UserRoleJudge.Instance.CheckAdminRole(userId);
                }
                _adminRoles = rs;
            }

            _adminRoleLoaded = true;
        }
        #endregion
    }

    public class PluUserIdentity : IIdentity
    {
        const string AuthType = "PluAuthentication";
        bool _isAuthenticated = false;
        string _name = null;
        bool _initialized = false;

        public PluUserIdentity()
        {
            _initialized = false;
        }
        public PluUserIdentity(string name)
        {
            _name = name;
            _isAuthenticated = true;
            _initialized = true;
        }
        public string AuthenticationType => AuthType;

        public bool IsAuthenticated
        {
            get
            {
                AuthUser();
                return _isAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                AuthUser();
                return _name;
            }
        }

        void AuthUser()
        {
            if (!_initialized)
            {
                //如果没有登录Cookie，未登录
                if (HttpContext.Current == null)
                {
                    _name = null;
                    _isAuthenticated = false;
                    return;
                }
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(ConfigSetting.AuthCookieName);
                if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                {
                    _name = null;
                    _isAuthenticated = false;
                    return;
                }
                string authFromCookie = XnAuthentication.GetAuthCookie(cookie.Value);
                if (!string.IsNullOrEmpty(authFromCookie))
                {
                    _name = authFromCookie;
                    _isAuthenticated = true;
                }
                else
                {
                    _name = null;
                    _isAuthenticated = false;
                }
                _initialized = true;
            }
        }
    }
}