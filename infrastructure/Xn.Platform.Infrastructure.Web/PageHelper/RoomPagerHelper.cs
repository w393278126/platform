using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xn.Platform.Core.Extensions;

namespace System.Web.Mvc
{
    public static class RoomPagerHelper
    {
        public static Page Pager(int PageIndex, int PageSize, int TotalCount)
        {
            Page page = new Page();
            page.PageIndex = PageIndex;
            page.PageSize = PageSize;
            page.TotalCount = TotalCount;
            return page;
        }
    }
    public class Page
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int MaxPage { get { return (int)Math.Ceiling((this.TotalCount / this.PageSize.AsDecimal())); } }
    }
}