using  Xn.Platform.Infrastructure.Auth;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
    Inherited = true, AllowMultiple = false)]
    public class AnonymousForbiddenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAnonymousUser = false;
            var ctx = filterContext;
            if (ctx == null)
            {
                isAnonymousUser = true;
            }
            else
            {
                XnUserPrincipal user = ctx.HttpContext.User as XnUserPrincipal;
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    isAnonymousUser = true;
                }
            }
            if (isAnonymousUser)
            {
#if DEBUG
                ctx.Result = new RedirectResult("http://login.Xn.dev/member/login?returnurl=" + HttpUtility.UrlEncode(ctx.RequestContext.HttpContext.Request.Url.AbsoluteUri));
#else
                ctx.Result = new RedirectResult("https://login.Xn.cn/member/login?returnurl=" + HttpUtility.UrlEncode(ctx.RequestContext.HttpContext.Request.Url.AbsoluteUri));
#endif
            }
        }

    }
}