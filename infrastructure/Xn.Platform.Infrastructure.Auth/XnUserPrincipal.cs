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
            Identity = new XnUserIdentity();
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
    }

    public class XnUserIdentity : IIdentity
    {
        const string AuthType = "XnAuthentication";
        bool _isAuthenticated = false;
        string _name = null;
        bool _initialized = false;

        public XnUserIdentity()
        {
            _initialized = false;
        }
        public XnUserIdentity(string name)
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