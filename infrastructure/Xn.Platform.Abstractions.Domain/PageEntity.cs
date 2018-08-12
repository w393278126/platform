using System.Collections.Generic;
using System.ComponentModel;

namespace Xn.Platform.Domain
{
    /// <summary>
    /// 分页结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedEntity<T>
    {
        public PagedEntity()
        {
            Items = new List<T>();
        }
        public PagedEntity(int totalItems, ICollection<T> items)
        {
            TotalItems = totalItems;
            Items = items;
        }
        /// <summary>
        /// 当前集合中可用的总条目数(总计)
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 属性名items被保留用作表示一组条目。
        /// 这种结构的目的是给与当前结果相关的集合提供一个标准位置。
        /// 例如，知道页面上的items是数组，JSON输出便可能插入一个通用的分页系统。
        /// 如果items存在，它应该是data对象的最后一个属性。
        /// </summary>
        public ICollection<T> Items { get; set; }
    }

    /// <summary>
    /// 分页结构，具有某一列的统计信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedEntityWithStatistical<T> : PagedEntity<T>
    {
        public PagedEntityWithStatistical()
        {
            Items= new List<T>();
        }

        public PagedEntityWithStatistical(int totalItems, ICollection<T> items)
        {
            TotalItems = totalItems;
            Items = items;
        }

        public PagedEntityWithStatistical(int totalItems, object statistical, ICollection<T> items)
        {
            TotalItems = totalItems;
            Items = items;
            TotalStatistical = statistical;
        }


        public object TotalStatistical { get; set; }
    }


    public class PagedEntityExtForGuard<T> : PagedEntity<T>
    {
        public PagedEntityExtForGuard()
        {
            Items = new List<T>();
        }

        public PagedEntityExtForGuard(int totalItems, ICollection<T> items, long totalOnline)
        {
            TotalItems = totalItems;
            Items = items;
            TotalOnline = totalOnline;
        }


        public long TotalOnline { get; set; }
    }

    public enum OrderBy
    {
        [Description("LastChanged Desc")]
        LastChangedDesc,
        [Description("LastChanged")]
        LastChanged,
        [Description("Id Desc")]
        IdDesc,
        [Description("Id")]
        Id,
        [Description("views Desc")]
        Views
    }
}
