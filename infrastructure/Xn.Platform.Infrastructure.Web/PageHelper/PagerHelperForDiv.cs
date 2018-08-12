using System.Collections.Specialized;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class PagerHelperForDiv
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="htmlAttributes">分页头标签属性</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static string Div(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount, string className, PageMode mode)
        {
            TagBuilder builder = new TagBuilder("div");
            builder.IdAttributeDotReplacement = "_";
            builder.GenerateId(id);
            builder.AddCssClass(className);
            //builder.MergeAttributes(new RouteValueDictionary(null));
            builder.InnerHtml = GetNormalPage(currentPageIndex, pageSize, recordCount, mode);
            return builder.ToString();
        }
        /// <summary>
        /// 获取普通分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        private static string GetNormalPage(int currentPageIndex, int pageSize, int recordCount, PageMode mode)
        {
            int pageCount = (recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1);

            if (pageCount <= 1)
            {
                return "";
            }

            StringBuilder url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.AbsolutePath + "?pageindex={0}");
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "pageindex")
                    url.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='fr'>");

            if (currentPageIndex > 1)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex - 1);
                sb.AppendFormat("<a href='{0}' class='paginator-prev paging_page'>&lt;</a>", url1);
            }
            else
                sb.Append("<a href='javascript:;' class='paginator-prev paging_page disabled'>&lt;</a>");

            if (mode == PageMode.Numeric)
                sb.Append(GetNumericPage(currentPageIndex, pageSize, recordCount, pageCount, url.ToString()));

            if (currentPageIndex < pageCount)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex + 1);
                sb.AppendFormat("<a href='{0}' class='paginator-next paging_page'>&gt;</a>", url1);
            }
            else
                sb.Append("<a href='javascript:;' class='paginator-prev paging_page disabled'>&gt;</a>");

            sb.Append("</div>");

            return sb.ToString();
        }
        /// <summary>
        /// 获取数字分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetNumericPage(int currentPageIndex, int pageSize, int recordCount, int pageCount, string url)
        {
            int k = currentPageIndex == 5 ? 0 : currentPageIndex / 5;
            int m = currentPageIndex % 5;
            StringBuilder sb = new StringBuilder();
            if (currentPageIndex / 5 == pageCount / 5)
            {
                if (m == 0)
                {
                    if (k > 0)
                    {
                        k--;
                    }
                    m = 5;
                }
                else
                {
                    m = pageCount % 5;
                }
            }
            else
            {
                if (k > 1 && m == 0)
                {
                    k--;
                }
                m = 5;
            }

            for (int i = k * 5 + 1; i <= k * 5 + m; i++)
            {
                if (i == currentPageIndex)
                    sb.AppendFormat("<a href='javascript:;' class='paging_page curr active'>{0}</a>", i);
                else
                {
                    string url1 = string.Format(url.ToString(), i);
                    sb.AppendFormat("<a href='{0}' class='paging_page'>{1}</a>", url1, i);
                }
            }

            return sb.ToString();
        }
    }
}
