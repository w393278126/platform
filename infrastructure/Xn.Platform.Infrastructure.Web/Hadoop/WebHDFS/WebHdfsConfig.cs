using Logging.Client;
using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.Hadoop.WebHDFS
{
    public static class WebHdfsConfig
    {
        private static readonly ILog _logger = Logging.Client.LogManager.GetLogger(typeof(WebHdfsConfig));

        private static readonly Uri WebHdfsUri = new Uri("http://192.168.10.160:50070/webhdfs/v1");

        public static void WriteHdfs(string jsoncontent,string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return;
            }
            try
            {
                IWebHdfsHttpClient hdfsClient = new WebHdfsHttpClient(WebHdfsConfig.WebHdfsUri);
                var create = hdfsClient.Create(filepath, jsoncontent, true).Result;
                _logger.Info("Write Json to HDFS,Path：", create);
            }
            catch (Exception ex)
            {
                _logger.Error("Write Json to HDFS,Error：", ex);
            }
        }
    }
}
