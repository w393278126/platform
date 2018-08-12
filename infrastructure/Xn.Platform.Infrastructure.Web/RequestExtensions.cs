using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Xn.Platform.Infrastructure.Web
{
    public static class RequestExtensions
    {
        public static string GetContentToString(this string url, ref string message)
        {
            string defaultEncoding = "utf-8";
            return GetContentToString(url, defaultEncoding, ref message);
        }

        public static string GetContentToString(this string url, string encoding, ref string message)
        {
            string text = null;
            Stream stream = null;
            StreamReader streamReader = null;
            HttpWebRequest httpWebRequest = null;

            if (string.IsNullOrEmpty(url))
            {
                return text;
            }

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Accept = "*/*";
                httpWebRequest.Referer = "";
                httpWebRequest.Headers["Accept-Language"] = "zh-cn";
                httpWebRequest.Headers["Accept-Encoding"] = "gzip,deflate";
                httpWebRequest.ServicePoint.Expect100Continue = false;
                httpWebRequest.ServicePoint.UseNagleAlgorithm = false;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.517.41 Safari/534.7";
                WebResponse webResponse = httpWebRequest.GetResponse();
                stream = webResponse.GetResponseStream();
                streamReader = new StreamReader(stream, Encoding.GetEncoding(encoding));
                text = streamReader.ReadToEnd();
                message = "请求视频信息成功!";
            }
            catch (Exception ex)
            {
                message = "请求视频信息失败!" + ex.ToString();
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
            return text;
        }
    }
}