using System.IO;

namespace System.Web.Mvc
{
    /// <summary>
    /// JSONP封装类
    /// </summary>
    public abstract class JsonpStreamResult : ActionResult
    {
        protected ContentType _contentType;
        private string _callback;

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HandleContentType(context);
            GenerateResponse(context);
        }

        private void HandleContentType(ControllerContext context)
        {
            var callback = (context.RouteData.Values["callback"] as string) ?? context.HttpContext.Request["callback"];
            if (!string.IsNullOrEmpty(callback))
            {
                _contentType = ContentType.Jsonp;
                _callback = callback;
                context.HttpContext.Response.ContentType = "application/x-javascript; charset=utf-8";
                return;
            }

            var format = (context.RouteData.Values["format"] as string) ?? context.HttpContext.Request["format"];
            if (format == "msgpack")
            {
                _contentType = ContentType.MsgPack;
                context.HttpContext.Response.ContentType = "application/x-msgpack";
                return;
            }

            _contentType = ContentType.Json;
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
        }

        private void GenerateResponse(ControllerContext context)
        {
            if (_contentType == ContentType.Jsonp)
            {
                using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream))
                {
                    writer.Write(_callback);
                    writer.Write('(');
                    WriteContent(context, writer);
                    writer.Write(')');
                }
            }
            else
            {
                using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream))
                {
                    WriteContent(context, writer);
                }
            }
        }

        protected abstract void WriteContent(ControllerContext context, StreamWriter writer);
    }

    public enum ContentType
    {
        Json = 0,
        Jsonp,
        MsgPack
    }
}