using System.Collections.Generic;
using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web
{
    public static class SelectListExtentions
    {
        public static IList<SelectListItem> GetSelectListItems(this IDictionary<string, string> entities , string selected = null)
        {
            var results = new List<SelectListItem>();
            foreach (var entity in entities)
            {
                var item = new SelectListItem {Value = entity.Key, Text = entity.Value};
                if (!string.IsNullOrEmpty(selected) && selected == entity.Key)
                    item.Selected = true;
                results.Add(item);
            }
            return results;
        }

        public static string GetSelectItem(this IDictionary<string, string> entities, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "";
            }
            if (entities.ContainsKey(key))
            {
                return entities[key];
            }
            else
            {
                return "";
            }
        }

    }
}
