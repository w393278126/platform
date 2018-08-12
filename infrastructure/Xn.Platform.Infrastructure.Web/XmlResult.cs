using Xn.Platform.Core.Extensions;

namespace System.Web.Mvc
{

    /// <summary>
    /// JSONP封装类
    /// </summary>
    public class XmlResult: ActionResult
    {
        public object Data { get; set; }

        public XmlResult(object data)
        {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (Data == null)
            {
                throw new ArgumentNullException("Data");
            }

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";

            string content = Data.ToXml();//ServiceStack.Text.XmlSerializer.SerializeToString<T>(Data);
            response.Write(content);
        }
    }
}