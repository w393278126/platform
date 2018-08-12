using System;
using System.Collections.Generic;
using System.Web;
using Xn.Platform.Core.Extensions;

namespace Xn.Home.Web.Utils
{
    /// <summary>
    /// 配置移除未使用的头
    /// 配置写死命名空间
    /// <add name="RemoveUselessHeaderModule" type="Xn.Home.Web.Utils.RemoveUselessHeaderModule" />
    /// </summary>
    public class RemoveUselessHeaderModule : IHttpModule
    {
        private static readonly string[] Headers = { "Server", "X-AspNet-Version", "X-Powered-By", "X-AspNetMvc-Version" };



        private static readonly Lazy<string> LocalIp = new Lazy<string>(() =>
        {
            var ip = HostUtility.GetLocalIP();
            return ip.Substring(ip.LastIndexOf('.') + 1);
        });

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += context_PreSendRequestHeaders;
        }

        void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            var context = sender as HttpApplication;
            if (null != context)
            {
                var value = "*";
                //if (context.Request.UrlReferrer != null)
                //{
                //    var absolutePath = context.Request.UrlReferrer.Host;
                //    if (urls.Contains(absolutePath))
                //    {
                //        value = absolutePath;
                //    }
                //}
                context.Response.Headers.Add("Access-Control-Allow-Origin", value);
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Access-Control-Max-Age", "86400");
                context.Response.Headers.Add("XAge", LocalIp.Value);
                foreach (var header in Headers)
                {
                    context.Response.Headers.Remove(header);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}