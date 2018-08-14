using System;
using System.Linq;
using System.Web.Mvc;
using Xn.Platform.Infrastructure.Auth;
using Xn.Home.Auth;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain.Impl.Admin;
using System.Web.Routing;

namespace Xn.Platform.Presentation.Admin.Models
{

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class AdministratorActionFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ctx = filterContext;
            if (ctx == null)
                throw new ArgumentNullException("filterContext");

            XnUserPrincipal user = ctx.HttpContext.User as XnUserPrincipal;
            if (user == null)
            {
                throw new ArgumentException("HttpContext.User is not XnUserPrincipal.");
            }

            if (!user.Identity.IsAuthenticated)
            {
                ctx.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "client", filterContext.RouteData.Values[ "client" ] },
                    { "controller", "admin" },
                    { "action", "index" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
                return;
            }
            var isAuthorized = false;

            var controller = ctx.RouteData.Values["controller"] as string;
            var action = ctx.RouteData.Values["action"] as string;
            isAuthorized = new RoleResourceService().IsRoleAuthorized(user.Identity.Name.AsInt(), controller, action);

            if (!isAuthorized)
                UnauthorizeHandler(ctx);

        }

        private static void UnauthorizeHandler(AuthorizationContext ctx)
        {
            ctx.Result = new HttpUnauthorizedResult();
        }


    }




}