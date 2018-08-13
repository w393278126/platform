using System;
using System.Linq;
using System.Web.Mvc;
using Xn.Platform.Infrastructure.Auth;
using Xn.Platform.Presentation.Admin.Models.Auth;
using Xn.Home.Auth;
using Xn.Platform.Core.Extensions;

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
                throw new ArgumentException("HttpContext.User is not PluUserPrincipal.");
            }

            if (!user.Identity.IsAuthenticated)
            {
                UnauthorizeHandler(ctx);
                return;
            }

            //var roleGroup = System.Web.HttpContext.Current.Request.Params["roleGroup"];

            var controller = ctx.RouteData.Values["controller"] as string;
            var action = ctx.RouteData.Values["action"] as string;
            var config = new SettingConfigManager(user.Identity.Name.AsInt()).AuthorizationConfig
                .Where(c => c.Controller.ToLower() == controller.ToLower() && c.Action.ToLower() == action.ToLower()).FirstOrDefault();

            var isAuthorized = false;

            isAuthorized = true;
            if (!isAuthorized)
                UnauthorizeHandler(ctx);

        }

        private static void UnauthorizeHandler(AuthorizationContext ctx)
        {
            ctx.Result = new HttpUnauthorizedResult();
        }


    }




}