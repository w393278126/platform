using System;
using System.Diagnostics;
using System.Web;
using  Xn.Platform.Infrastructure.Auth;

namespace Xn.Home.Auth
{
    /// <summary>
    /// 配置写死命名空间
    /// <add name="XnAuthModule" type="Xn.Home.Auth.PluAuthModule" preCondition="managedHandler" />
    /// </summary>
    public class XnAuthModule : IHttpModule
    {
        private AuthenticateRequest AuthenticateRequest { get; set; }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            AuthenticateRequest = context_AuthenticateRequest;

            context.AddOnAuthenticateRequestAsync(AuthenticateRequest.BeginInvoke, AuthenticateRequest.EndInvoke);
            context.BeginRequest += context_BeginRequest;
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            var ctx = (sender as HttpApplication)?.Context;
            if (ctx != null)
            {
                ctx.User = new XnUserPrincipal();
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var ctx = (sender as HttpApplication)?.Context;
            if (ctx != null)
            {
                var activityIdHeader = ctx.Request.Headers.Get("X-CorrelationId");
                if (activityIdHeader != null)
                {
                    Guid activityId;
                    if (Guid.TryParseExact(activityIdHeader, "N", out activityId))
                    {
                        ctx.Items["X-CorrelationId"] = Trace.CorrelationManager.ActivityId = activityId;
                        return;
                    }
                }

                ctx.Items["X-CorrelationId"] = Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            }
        }
    }

    internal delegate void AuthenticateRequest(object sender, EventArgs e);
}