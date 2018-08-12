using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Xn.Platform.Domain.Impl;

namespace Xn.Platform.Infrastructure.Web
{
    public class XnBaseApiController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (controllerContext.Request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = controllerContext.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (ctx != null)
                {
                    ctx.UpdateCorrelationId();
                }
            }
            base.Initialize(controllerContext);
        }
            
        protected T GetService<T>() where T : class 
        {
            return Request.GetDependencyScope().GetService(typeof(T)) as T;
        }
    }
}
