using System;

namespace Xn.Platform.Core.Extensions
{
    public static class UriExtensions
    {
        public static string GetLiveUri(this Uri originalUri)
        {
            var originalHost = originalUri.Host.ToLower();
            string newLive = null;
            switch (originalHost)
            {
                case "star.tga.Xn.cn":
                case "star.Xn.cn":
                case "star.longzhu.tv":
                    newLive = "star.longzhu.com";
                    break;
                case "test.star.tga.Xn.cn":
                case "test.star.Xn.cn":
                case "test.star.longzhu.tv":
                    newLive = "test.star.longzhu.com";
                    break;
                case "beta.star.tga.Xn.cn":
                case "beta.star.Xn.cn":
                    newLive = "beta.star.longzhu.com";
                    break;
                case "star.tga.Xn.dev":
                case "star.Xn.dev":
                    newLive = "star.longzhu.dev";
                    break;
            }
            if (string.IsNullOrEmpty(newLive))
                return null;
            var newUriBilder = new UriBuilder(originalUri);
            newUriBilder.Host = newLive;
            var newUri = newUriBilder.Uri;
            return newUri.ToString();
        }

        public static string GetVUri(this Uri originalUri)
        {
            var originalHost = originalUri.Host.ToLower();
            string newLive = null;
            switch (originalHost)
            {
                case "v.Xn.cn":
                    newLive = "v.longzhu.com";
                    break;
                case "test.v.Xn.cn":
                    newLive = "test.v.longzhu.com";
                    break;
                case "beta.v.Xn.cn":
                    newLive = "beta.v.longzhu.com";
                    break;
                case "v.Xn.dev":
                    newLive = "v.longzhu.dev";
                    break;
            }
            if (string.IsNullOrEmpty(newLive))
                return null;
            var newUriBilder = new UriBuilder(originalUri);
            newUriBilder.Host = newLive;
            var newUri = newUriBilder.Uri;
            return newUri.ToString();
        }

        public static string GetVideoUri(this Uri originalUri)
        {
            var originalHost = originalUri.Host.ToLower();
            string newHost = "v.longzhu.com";
            switch (originalHost)
            {
                case "star.tga.Xn.cn":
                case "star.Xn.cn":
                case "star.longzhu.tv":
                case "v.Xn.cn":
                case "star.longzhu.com":
                    newHost = "v.longzhu.com";
                    break;
                case "test.star.tga.Xn.cn":
                case "test.star.Xn.cn":
                case "test.star.longzhu.tv":
                case "test.star.longzhu.com":
                case "test.v.Xn.cn":
                    newHost = "test.v.longzhu.com";
                    break;
                case "beta.star.tga.Xn.cn":
                case "beta.star.Xn.cn":
                case "beta.star.longzhu.com":
                case "beta.v.Xn.cn":
                    newHost = "beta.v.longzhu.com";
                    break;
                case "star.tga.Xn.dev":
                case "star.Xn.dev":
                case "star.longzhu.dev":
                case "v.Xn.dev":
                    newHost = "v.longzhu.dev";
                    break;
            }
            var newUriBilder = new UriBuilder(originalUri);
            newUriBilder.Host = newHost;
            var newUri = newUriBilder.Uri;
            return newUri.ToString();
        }

        public static string GetApiUri(this Uri originalUri)
        {
            var newUriBilder = new UriBuilder(originalUri);
            newUriBilder.Host = "star.api.Xn.cn";
            var newUri = newUriBilder.Uri;
            return newUri.ToString();
        }
    }
}
