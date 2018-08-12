using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace Xn.Platform.Infrastructure.Web.WebApi
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var error = string.Format("Url:{0}\r\nMachineName:{1}\r\nSource:{2}\r\nDetail:{3}\r\nStatusCode:{4}\r\nHTTPMethod:{5}\r\nIPAddress:{6}\r\nRequestHeaders {7}",
                context.Request.RequestUri, Environment.MachineName, context.Exception.Source, context.Exception, "0", context.Request.Method, 0, "");

            Trace.TraceError(error);
        }
    }
}