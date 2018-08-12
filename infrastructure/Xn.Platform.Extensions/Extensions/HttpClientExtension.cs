using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Xn.Platform.Core.Extensions
{
    public class HttpClientExtension
    {
        public static Tuple<Exception, string> UploadData(string uploadUrl, HttpPostDataBuilder postData)
        {
            string contentType;
            var bytes = postData.BuildFormData(out contentType);
            var webClient = new WebClient();
            webClient.Headers.Add("Content-Type", contentType);
            webClient.Headers.Add("User-Agent", "plu");
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml,*/*");
            webClient.Headers.Add("Accept-Charset", "GBK,utf-8");
            try
            {
                var responseBytes = webClient.UploadData(uploadUrl, bytes);
                return Tuple.Create<Exception, string>(null, Encoding.UTF8.GetString(responseBytes));
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex, string.Empty);
            }
        }
    }

    public class HttpPostDataBuilder
    {
        const string Boundary = "----PW8f82wtCruG3Ae";

        class PostItem
        {
            public string Name { get; set; }
            public string Data { get; set; }
            public bool IsFile { get; set; }
            public Stream Stream { get; set; }
        }

        private readonly IList<PostItem> _postItems = new List<PostItem>();
        /// <summary>
        /// 添加上传的数据
        /// </summary>
        /// <param name="name">form name</param>
        /// <param name="value">数据</param>
        public void AddData(string name, string value)
        {
            _postItems.Add(new PostItem { Name = name, Data = value, IsFile = false });
        }

        /// <summary>
        /// 添加上传的文件
        /// </summary>
        /// <param name="name">form name</param>
        /// <param name="filepath">上传的文件地址</param>
        /// <exception cref="FileNotFoundException">上传文件的地址不存在</exception>
        public void AddFile(string name, string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("上传的文件地址不存在");
            }
            _postItems.Add(new PostItem { Name = name, Data = filepath, IsFile = true });
        }

        /// <summary>
        /// 添加上传的文件
        /// </summary>
        /// <param name="name">form name</param>
        /// <param name="filepath">上传的文件地址</param>
        /// <exception cref="FileNotFoundException">上传文件的地址不存在</exception>
        public void AddStream(string name, Stream filepath)
        {
            _postItems.Add(new PostItem { Name = name, Data = string.Empty, IsFile = true, Stream = filepath });
        }

        /// <summary>
        /// 构建form提交内容数据
        /// </summary>
        /// <param name="contentType">获取http请求contenttype</param>
        /// <returns>表单数据</returns>
        public byte[] BuildFormData(out string contentType)
        {
            contentType = _postItems.Count(i => i.IsFile) > 0 ? "multipart/form-data; boundary=" + Boundary : "application/x-www-form-urlencoded";
            var datas = new List<byte>();
            foreach (var item in _postItems)
            {
                datas.AddRange(BuildItem(item));
            }
            datas.AddRange(Encoding.UTF8.GetBytes("--" + Boundary + "--"));
            return datas.ToArray();
        }

        private static IEnumerable<byte> BuildItem(PostItem item)
        {
            return item.IsFile ? BuildFileItem(item) : BuildDataItem(item);
        }

        private static byte[] BuildDataItem(PostItem item)
        {
            var itemHeader = string.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n", "--" + Boundary,
                                              item.Name);
            var datas = new List<byte>();
            datas.AddRange(Encoding.UTF8.GetBytes(itemHeader));
            datas.AddRange(Encoding.UTF8.GetBytes(item.Data));
            datas.AddRange(Encoding.UTF8.GetBytes("\r\n"));
            return datas.ToArray();
        }

        private static byte[] BuildFileItem(PostItem item)
        {
            var itemHeader =
                string.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream\r\n\r\n", "--" + Boundary, item.Name, Path.GetFileName(item.Data));
            var datas = new List<byte>();
            datas.AddRange(Encoding.UTF8.GetBytes(itemHeader));
            Stream sr;
            if (item.Stream != null)
            {
                sr = item.Stream;
            }
            else
            {
                sr = new FileStream(item.Data, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            int length;
            var bytes = new byte[4096];
            while ((length = sr.Read(bytes, 0, bytes.Length)) > 0)
            {
                datas.AddRange(bytes.Take(length));
            }
            sr.Close();
            datas.AddRange(Encoding.UTF8.GetBytes("\r\n"));
            return datas.ToArray();
        }

    
    }


    public static class HttpRequestExtension
    {
        public static string GetData(this HttpRequest request)
        {
            var sm = request.InputStream;
            int len = (int)sm.Length; //post数据长度
            byte[] inputByts = new byte[len]; //字节数据,用于存储post数据
            sm.Read(inputByts, 0, len); //将post数据写入byte数组中
            sm.Close(); //关闭IO流
            string data = Encoding.UTF8.GetString(inputByts); //转为String
            return data;
        }
    }
}
