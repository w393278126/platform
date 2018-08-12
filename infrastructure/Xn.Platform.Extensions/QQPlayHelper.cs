using Xn.Platform.Core.Extensions;
using System;
using System.Text.RegularExpressions;

namespace Xn.Platform.Core
{
    public class QQPlayHelper
    {
        public static Regex qqRegex1 = new Regex(@"TPout.swf\?vid=(?<qqvid>[^\/#\?]*)&attstart", RegexOptions.IgnoreCase);
        public static Regex qqRegex2 = new Regex(@"TPout.swf\?vid=(?<qqvid>[^\/#\?]*)&exid", RegexOptions.IgnoreCase);
        public static Regex liveIframeHtmlRegex = new Regex(@"cnlid=(?<cnlid>\d+)", RegexOptions.IgnoreCase);

        /// <summary>
        /// 根据腾讯FlashUrl获取Vid
        /// </summary>
        /// <param name="flashUrl"></param>
        /// <returns></returns>
        public static string GetVidByQQFlashUrl(string flashUrl)
        {
            if (flashUrl.IsNullOrEmpty() || !flashUrl.Contains("http"))
            {
                return "";
            }
            Uri uri = new Uri(flashUrl);
            if (uri != null)
            {
                string queryString = uri.Query;
                if (string.IsNullOrEmpty(queryString))
                    return "";
                var col = UrlHelper.GetQueryString(queryString);
                if (col == null)
                    return "";
                string searchKey = col["vid"];
                if (!searchKey.IsNullOrEmpty())
                    return searchKey;
            }
            return "";
        }

        /// <summary>
        /// 获取直播的vid
        /// </summary>
        /// <param name="liveIframe"></param>
        /// <returns></returns>
        public static string GetCnlidByLiveIframe(string liveIframe)
        {
            if (liveIframe.IsNullOrEmpty())
            {
                return "";
            }
            var match_qq = liveIframeHtmlRegex.Match(liveIframe);
            if (match_qq.Success)
            {
                var vid = match_qq.Groups["cnlid"].Value;
                if (!vid.IsNullOrEmpty())
                {
                    return vid;
                }
            }
            return "";
        }
    }
}
