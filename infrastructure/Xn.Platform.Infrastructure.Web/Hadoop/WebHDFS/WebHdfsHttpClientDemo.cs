
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.Hadoop.WebHDFS
{
    internal class WebHdfsHttpClientDemo
    {
        private static readonly Uri webHdfsUri = new Uri("http://192.168.10.160:50070/webhdfs/v1");
        private static readonly string testContent = "This is test content";
        private void Test()
        {

            IWebHdfsHttpClient c = new WebHdfsHttpClient(webHdfsUri);
            string path = "/tmp/WebHdfsHttpClient/test.txt";
            var create = c.Create(path, testContent, true).Result;
            Console.WriteLine("create:" + create);

            var open = c.Open(path).Result;
            Console.WriteLine("open:" + open);

            c.Append(path, " this is append content").Wait();

            var append = c.Open(path).Result;
            Console.WriteLine("append:" + append);

            var fileStatus = c.GetFileStatus(path).Result;
            Console.WriteLine("GetFileStatus:" + fileStatus.ToString());

            Console.WriteLine("MkDirs:");
            string dir = "/tmp/WebHdfsHttpClient/mk";
            c.MkDirs(dir);

            c.Create(dir + "/test2.txt", testContent, true).Wait();
            c.Create(dir + "/test3.txt", testContent, true).Wait();
            var lst = c.ListStatus(dir);


            c.Delete(path, true);

            Console.ReadLine();
        }

    }
}
