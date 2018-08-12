using Autofac.Integration.Mvc;
using Logging.Client;
using Xn.Platform.Domain.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xn.Platform.Infrastructure.Web
{
    public class XnBaseController : Controller
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(XnBaseController));

        protected HttpNotFoundResult RoomNotExist()
        {
            return HttpNotFound("您访问的房间未开通！");
        }

        protected override void Initialize(RequestContext requestContext)
        {
            requestContext.HttpContext.UpdateCorrelationId();
            base.Initialize(requestContext);
        }

#if !DEBUG
        protected override void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["Controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();

            var tag = new Dictionary<string, string>();
            tag.Add("controller", controller);
            tag.Add("action", action);

            if (filterContext.Exception is ArgumentException || filterContext.Exception is HttpRequestValidationException || filterContext.Exception is HttpException || filterContext.Exception is InvalidOperationException)
            {
                _logger.Warm("mvc.exception", new Error(filterContext.Exception, filterContext.HttpContext).ToString(), tag);
            }
            else
            {
                _logger.Error("mvc.exception", new Error(filterContext.Exception, filterContext.HttpContext).ToString(), tag);
            }

            filterContext.ExceptionHandled = true;
            var errResult = new { code = 500, msg = "内部程序异常" };
            filterContext.Result = JsonpResult.AsNewtonsoftJson(errResult);

            base.OnException(filterContext);
        }
#endif
        protected override void HandleUnknownAction(string actionName)
        {
            Response.StatusCode = 404;
            Response.StatusDescription = string.Format(" Not Find The Action :{0}", actionName);
            Response.Write(" Illegal Request. " + Response.StatusDescription);
            Response.End();
        }

        protected T GetService<T>()
        {
            return AutofacDependencyResolver.Current.GetService<T>();
        }

        protected IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>)AutofacDependencyResolver.Current.GetServices<T>();
        }


        /// <summary>   
        /// 取得网站的根目录的URL   
        /// </summary>   
        /// <returns></returns>   
        public string GetRootURI()
        {
            string AppPath = "";

            var Req = Request;
            if (Req != null)
            {
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                {
                    //直接安装在   Web   站点      
                    AppPath = UrlAuthority;
                }
                else
                {
                    //安装在虚拟子目录下
                    AppPath = UrlAuthority + Req.ApplicationPath;
                }
            }
            return AppPath;
        }
    }
}