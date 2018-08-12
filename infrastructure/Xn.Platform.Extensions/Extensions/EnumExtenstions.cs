using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Xn.Platform.Core.Extensions
{
    public static class EnumExtenstions
    {
        public static string GetDescription(this Type enumType, string item)
        {
            var fi = enumType.GetField(item);
            var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            var description = attribute == null ? item : ((DescriptionAttribute)attribute).Description;
            return description;
        }

        public static string GetDescription(this Enum enumType)
        {
            var field = enumType.GetType().GetField(enumType.ToString());
            if (field == null)
                return "";
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
            var description = attribute == null ? enumType.ToString() : attribute.Description;
            return description;
        }
        public static string GetEnumDescription(Enum enumValue)
        {
            var str = enumValue.ToString();
            var field = enumValue.GetType().GetField(str);
            if (field == null)
                return string.Empty;
            var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs.Length == 0) return str;
            var da = (DescriptionAttribute)objs[0];
            return da.Description;
        }

        public static List<Tuple<int, string, int>> GetAllEnum(Type enumType)
        {
            var results = new List<Tuple<int, string, int>>();
            var enums = Enum.GetNames(enumType);
            foreach (string item in enums)
            {
                var fi = enumType.GetField(item);
                var value = (int)fi.GetValue(item);
                var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                var description = attribute == null ? item : ((DescriptionAttribute)attribute).Description;

                var dvalue = fi.GetCustomAttributes(typeof(DefaultValueAttribute), true).FirstOrDefault();
                var per = dvalue == null ? 0 : ((DefaultValueAttribute)dvalue).Value.AsInt();

                var entity = Tuple.Create(value, description, per);
                results.Add(entity);
            }
            return results;
        }

        public static string[] ToStringArray(this Enum o)
        {
            return o.ToString().ToLower()
                .Split(new[] {", "}, StringSplitOptions.None);

        }
    }
}
