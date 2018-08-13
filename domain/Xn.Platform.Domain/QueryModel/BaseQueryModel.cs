using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain
{
    public class BaseQueryModel
    {
        /// <summary>
        /// 设置默认值
        /// </summary>
        public BaseQueryModel()
        {
            PageSize = 10;
            PageIndex = 1;
            OrderBy = "Id";
            ToSort = true;

        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }


        public string OrderBy { get; set; }

        /// <summary>
        /// true:降序 false:升序
        /// </summary>
        public bool ToSort { get; set; }
    }

    public class DataTablesPageRequest : BaseQueryModel
    {
        public int PageNumber
        {
            get
            {
                return this.iDisplayStart == 0 ? 0 : (this.iDisplayStart / this.iDisplayLength);
            }
        }
        /// <summary>
        /// Display start point in the current data set.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of records that the table can display in the current draw. It is expected that
        /// the number of recordsreturned will be equal to this number, unless the server has fewer records to return.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// Optional - this is a string of column names, comma separated (used in combination with sName) which will allow DataTables to reorder data on the client-side if required for display.
        /// Note that the number of column names returned must exactly match the number of columns in the table. For a more flexible JSON format, please consider using mDataProp.
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Number of columns being displayed (useful for getting individual column search info)
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Global search field
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// True if the global filter should be treated as a regular expression for advanced filtering, false if not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// (int) Indicator for if a column is flagged as searchable or not on the client-side
        /// </summary>
        public List<bool> bSearchable_ { get; set; }

        /// <summary>
        /// (int) Individual column filter
        /// </summary>
        public List<string> SearchColumns { get; set; }

        /// <summary>
        /// (int) True if the individual column filter should be treated as a regular expression for advanced filtering, false if not
        /// </summary>
        public List<bool> bRegex_ { get; set; }

        /// <summary>
        /// Indicator for if a column is flagged as sortable or not on the client-side
        /// </summary>
        public List<bool> bSortable_ { get; set; }

        /// <summary>
        /// Number of columns to sort on
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// (int) Column being sorted on (you will need to decode this number for your database)
        /// </summary>
        public List<int> iSortCol_0 { get; set; }

        /// <summary>
        /// (int) Direction to be sorted - "desc" or "asc".
        /// </summary>
        public List<string> sSortDir_0 { get; set; }

        /// <summary>
        /// (int) The value specified by mDataProp for each column. This can be useful for ensuring that
        /// the processing of data is independent from the order of the columns.
        /// </summary>
        public List<string> mDataProp_0 { get; set; }

        /// <summary>
        /// Information for DataTables to use for rendering.
        /// </summary>
        public int sEcho { get; set; }

        public static DataTablesPageRequest Default = new DataTablesPageRequest
        {
            iDisplayLength = 10,
            iDisplayStart = 0,
        };
        /// <summary>
        /// constructor
        /// </summary>
        public DataTablesPageRequest()
        {
            this.iDisplayLength = 10;
        }
    }
}
