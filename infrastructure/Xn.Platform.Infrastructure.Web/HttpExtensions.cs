using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Xn.Platform.Infrastructure.Web
{
    public static class HttpExtensions
    {
        private const string DefaultUserAgent =
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static string Get(this string url, int timeout = 0, string userAgent = null, CookieCollection cookies = null, IDictionary<string, string> heads = null)
        {
            string content = string.Empty;

            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;

                if (timeout != 0)
                {
                    request.Timeout = timeout;
                }
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                if (heads != null)
                {
                    foreach (var head in heads)
                    {
                        request.Headers.Add(head.Key, head.Value);
                    }
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        content = reader.ReadToEnd();
                        return content;
                    }
                }
            }
            catch (WebException web_ex)
            {
                if (web_ex.Response == null)
                {
                    return web_ex.ToString();
                }
                using (var stream = web_ex.Response.GetResponseStream())
                {
                    var reader = new StreamReader(stream, Encoding.UTF8);
                    content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 异步创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(this string url, int timeout = 0, string userAgent = null, CookieCollection cookies = null, IDictionary<string, string> heads = null, string type = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;

            if (!string.IsNullOrWhiteSpace(type))
            {
                request.ContentType = type;
            }
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            if (heads != null)
            {
                foreach (var head in heads)
                {
                    request.Headers.Add(head.Key, head.Value);
                }
            }
            try
            {
                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream, Encoding.UTF8);
                    var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                    return content;
                }
            }
            catch (WebException web_ex)
            {
                if (web_ex.Response == null)
                {
                    return web_ex.ToString();
                }
                using (var stream = web_ex.Response.GetResponseStream())
                {
                    var reader = new StreamReader(stream, Encoding.UTF8);
                    var content = reader.ReadToEnd();
                    return content + "/r/n" + url;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <param name="heads"></param>
        /// <param name="value"></param>
        /// <param name="isThrow"></param>
        /// <returns></returns>
        public static async Task<string> AsyncPost(this string url, IDictionary<string, string> parameters, int timeout = 0,
    string userAgent = null, CookieCollection cookies = null, IDictionary<string, string> heads = null, string type = null, string value = null, bool isThrow = false)
        {
            try
            {
                HttpWebRequest request;
                //如果是发送HTTPS请求
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = string.IsNullOrWhiteSpace(type) ? "application/x-www-form-urlencoded" : type;
                request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;
                if (timeout != 0)
                {
                    request.Timeout = timeout;
                }
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                if (heads != null)
                {
                    foreach (var head in heads)
                    {
                        request.Headers.Add(head.Key, head.Value);
                    }
                }
                //如果需要POST数据
                if (!(parameters == null || parameters.Count == 0))
                {
                    var buffer = new StringBuilder();
                    var i = 0;
                    foreach (var parameter in parameters)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", parameter.Key, parameter.Value);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
                        }
                        i++;
                    }
                    byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    byte[] data = Encoding.UTF8.GetBytes(value);
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
                if (isThrow)
                    throw;
                return string.Empty;
            }
        }

        public static string GetPostUrl(this string url, IDictionary<string, string> parameters)
        {
            var buffer = new StringBuilder();
            var i = 0;
            foreach (var parameter in parameters)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", parameter.Key, parameter.Value);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
                }
                i++;
            }
            var postUrl = url + buffer.ToString();
            return postUrl;
        }

        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <param name="heads"></param>
        /// <param name="value"></param>
        /// <param name="isThrow"></param>
        /// <returns></returns>
        public static string Post(this string url, IDictionary<string, string> parameters, int timeout = 0,
            string userAgent = null, CookieCollection cookies = null, IDictionary<string, string> heads = null, string type = null, string value = null, bool isThrow = false)
        {
            try
            {
                HttpWebRequest request;
                //如果是发送HTTPS请求
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = string.IsNullOrWhiteSpace(type) ? "application/x-www-form-urlencoded" : type;
                request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;
                if (timeout != 0)
                {
                    request.Timeout = timeout;
                }
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                if (heads != null)
                {
                    foreach (var head in heads)
                    {
                        request.Headers.Add(head.Key, head.Value);
                    }
                }
                //如果需要POST数据
                if (!(parameters == null || parameters.Count == 0))
                {
                    var buffer = new StringBuilder();
                    var i = 0;
                    foreach (var parameter in parameters)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", parameter.Key, parameter.Value);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
                        }
                        i++;
                    }
                    byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    byte[] data = Encoding.UTF8.GetBytes(value);
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var content = reader.ReadToEnd();
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
                if (isThrow)
                    throw;
                return string.Empty;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        public static string PostJson(this string url, string json, IDictionary<string, string> heads = null)
        {
            json = json.Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            //创建一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";


            if (heads != null)
            {
                foreach (var head in heads)
                {
                    request.Headers.Add(head.Key, head.Value);
                }
            }

            byte[] payload;
            //将Json字符串转化为字节
            payload = System.Text.Encoding.UTF8.GetBytes(json);
            //设置请求的ContentLength
            request.ContentLength = payload.Length;
            //发送请求，获得请求流
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();
            StreamReader Reader = new StreamReader(s);
            String strValue = Reader.ReadToEnd();
            Reader.Close();
            s.Close();

            return strValue;
        }



        public static string SendRequest(this string url, SortedDictionary<string, string> requestParams, string fileName = null)
        {

            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.Timeout = 10000;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)";

            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            var beginBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            var memStream = new MemoryStream();

            var strBuf = new StringBuilder();
            foreach (var key in requestParams.Keys)
            {
                strBuf.Append("\r\n--" + boundary + "\r\n");
                strBuf.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n\r\n");
                strBuf.Append(requestParams[key].ToString());
            }
            var paramsByte = Encoding.GetEncoding("utf-8").GetBytes(strBuf.ToString());
            memStream.Write(paramsByte, 0, paramsByte.Length);

            if (fileName != null)
            {
                memStream.Write(beginBoundary, 0, beginBoundary.Length);
                var fileInfo = new FileInfo(fileName);
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                const string filePartHeader =
                    "Content-Disposition: form-data; name=\"entityFile\"; filename=\"{0}\"\r\n" +
                    "Content-Type: application/octet-stream\r\n\r\n";
                var header = string.Format(filePartHeader, fileInfo.Name);
                var headerbytes = Encoding.UTF8.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
            }
            memStream.Write(endBoundary, 0, endBoundary.Length);
            request.ContentLength = memStream.Length;

            var requestStream = request.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();


            var response = request.GetResponse();
            using (var s = response.GetResponseStream())
            {
                var reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
    }
}