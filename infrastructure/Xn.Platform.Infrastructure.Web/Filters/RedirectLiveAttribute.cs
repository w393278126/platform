using Xn.Platform.Core.Extensions;

namespace System.Web.Mvc
{
    public class RedirectLiveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Uri originalUri = filterContext.HttpContext.Request.Url;
            var newUri = originalUri.GetLiveUri();
            if(newUri == null)
                return;
            filterContext.Result = new RedirectResult(newUri, true);
        }
    }
}
