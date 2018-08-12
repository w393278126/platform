using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web
{
    public static class DropDownListExtensions
    {
        public static string DropDownList<TData>(this HtmlHelper helper, IEnumerable<TData> datas, Func<string> valueFunc, Func<string> textFunc)
        {
            const string template = "<option value={0}>{1}</option>";
            //action();
            valueFunc();
            StringBuilder sb = new StringBuilder();
            foreach (var item in datas)
            {
                sb.Append(string.Format(template, "", ""));
                item.GetType().GetProperty("").GetValue(item).ToString();
                //sb.Append()
            }

            return sb.ToString();
            //return String.Format(@"<span id=""{0}"">{1}</span>", id, text);
        }
    }
}
