using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.WebApi.MediaTypeFormatters
{
    public class PlainTextMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly Encoding _encoding;

        public PlainTextMediaTypeFormatter(Encoding encoding = null)
        {
            this._encoding = encoding ?? Encoding.Default;
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(string);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var reader = new StreamReader(stream, _encoding);
            string value = reader.ReadToEnd();

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(value);
            return tcs.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            var writer = new StreamWriter(stream, _encoding);
            writer.Write((string)value);
            writer.Flush();

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
    }
}
