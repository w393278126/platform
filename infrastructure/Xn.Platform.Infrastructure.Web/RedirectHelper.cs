using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web
{
    public class HttpResponseResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            
        }
    }

    public class PermanentRedirectResult : HttpResponseResult
    {
        private string _url { get; set; }

        public PermanentRedirectResult(string url)
        {
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Status = "301 Moved Permanently";
            context.HttpContext.Response.StatusCode = 301;
            context.HttpContext.Response.AddHeader("Location", _url);
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.End();
        }
    }
}
