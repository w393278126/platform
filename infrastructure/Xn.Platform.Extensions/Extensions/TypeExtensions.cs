using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xn.Platform.Core.Extensions
{
    public static class TypeExtensions
    {
        public static T AsEnum<T>(this string value)
        {
            try
            {

                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return default(T);
            }

        }
        public static short AsShort(this string value, short defaultValue = 0)
        {
            short result;
            return short.TryParse(value, out result) ? result : defaultValue;
        }

        public static int AsInt(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }
        public static byte AsByte(this string value, byte defaultValue = 0)
        {
            byte result;
            return byte.TryParse(value, out result) ? result : defaultValue;
        }

        public static int AsInt(this object value, int defaultValue = 0)
        {
            if (value == null) { return 0; }
            return value.ToString().AsInt(defaultValue);
        }
        public static bool IsInt(this string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        public static decimal AsDecimal(this string value, decimal defaultValue = default(decimal))
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            //注意 这是Decimal类型一个bug
            //Decimal.TryParse does not work consistently for some locales. For instance for lt-LT, it accepts but ignores decimal values so "12.12" is parsed as 1212.
            return As(value, defaultValue);
        }

        public static decimal AsDecimal(this object value, decimal defaultValue = default(decimal))
        {
            return value.ToString().AsDecimal(defaultValue);
        }

        public static bool IsDecimal(this string value)
        {
            // 注意 这是Decimal类型一个bug For some reason, Decimal.TryParse incorrectly parses floating point values as decimal value for some cultures.
            // For example, 12.5 is parsed as 125 in lt-LT.
            return Is<decimal>(value);
        }

        public static float AsFloat(this string value, float defaultValue = default(float))
        {
            float result;
            return float.TryParse(value, out result) ? result : defaultValue;
        }

        public static float AsFloat(this object value, float defaultValue = default(float))
        {
            return value.ToString().AsFloat(defaultValue);
        }

        public static bool IsFloat(this string value)
        {
            float result;
            return float.TryParse(value, out result);
        }

        public static DateTime AsDateTime(this string value, DateTime defaultValue = default(DateTime))
        {
            DateTime result;
            return DateTime.TryParse(value, out result) ? result : defaultValue;
        }

        public static DateTime AsDateTime(this object value, DateTime defaultValue = default(DateTime))
        {
            return value.ToString().AsDateTime(defaultValue);
        }

        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public static bool AsBool(this string value, bool defaultValue = default(bool))
        {
            bool result;
            return bool.TryParse(value, out result) ? result : defaultValue;
        }

        public static bool AsBool(this object value, bool defaultValue = default(bool))
        {
            return value.ToString().AsBool(defaultValue);
        }

        public static bool IsBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result);
        }

        public static long AsLong(this string value, long defaultValue = default(long))
        {
            long result;
            return long.TryParse(value, out result) ? result : defaultValue;
        }

        public static long AsLong(this object value, long defaultValue = default(long))
        {
            return value.ToString().AsLong(defaultValue);
        }

        public static double AsDouble(this string value, double defaultValue = default(double))
        {
            double result;
            return double.TryParse(value, out result) ? result : defaultValue;
        }

        public static double AsDouble(this object value, double defaultValue = default(double))
        {
            return value.ToString().AsDouble(defaultValue);
        }

        public static TValue As<TValue>(this string value, TValue defaultValue = default(TValue))
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(value);
                }
                // try the other direction
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch
            {
                // eat all exceptions and return the defaultValue, assumption is that its always a parse/format exception
            }
            return defaultValue;
        }

        public static bool Is<TValue>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TValue));
            try
            {
                if ((value == null) || converter.CanConvertFrom(null, value.GetType()))
                {
                    // TypeConverter.IsValid essentially does this - a try catch - but uses InvariantCulture to convert. 
                    converter.ConvertFrom(null, CultureInfo.CurrentCulture, value);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Public | BindingFlags.Instance);
        }

        public static MemberInfo DecodeMemberAccessExpressionOf<TEntity>(Expression<Func<TEntity, object>> expression)
        {
            MemberInfo memberOfDeclaringType;
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                if ((expression.Body.NodeType == ExpressionType.Convert) && (expression.Body.Type == typeof(object)))
                {
                    memberOfDeclaringType = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
                }
                else
                {
                    throw new Exception(string.Format("Invalid expression type: Expected ExpressionType.MemberAccess, Found {0}",
                                                      expression.Body.NodeType));
                }
            }
            else
            {
                memberOfDeclaringType = ((MemberExpression)expression.Body).Member;
            }
            PropertyInfo memberOfReflectType;
            if (typeof(TEntity).IsInterface)
            {
                // Type.GetProperty(string name,Type returnType) does not work properly with interfaces
                return memberOfDeclaringType;
            }
            else
            {
                memberOfReflectType = typeof(TEntity).GetProperty(memberOfDeclaringType.Name, memberOfDeclaringType.GetPropertyOrFieldType());
            }
            return memberOfReflectType;
        }

        public static MemberInfo DecodeMemberAccessExpressionOf<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            MemberInfo memberOfDeclaringType;
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                if ((expression.Body.NodeType == ExpressionType.Convert) && (expression.Body.Type == typeof(object)))
                {
                    memberOfDeclaringType = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
                }
                else
                {
                    throw new Exception(string.Format("Invalid expression type: Expected ExpressionType.MemberAccess, Found {0}",
                                                                                        expression.Body.NodeType));
                }
            }
            else
            {
                memberOfDeclaringType = ((MemberExpression)expression.Body).Member;
            }
            PropertyInfo memberOfReflectType;
            if (typeof(TEntity).IsInterface)
            {
                // Type.GetProperty(string name,Type returnType) does not work properly with interfaces
                return memberOfDeclaringType;
            }
            else
            {
                memberOfReflectType = typeof(TEntity).GetProperty(memberOfDeclaringType.Name, memberOfDeclaringType.GetPropertyOrFieldType());
            }
            return memberOfReflectType;
        }

        public static Type GetPropertyOrFieldType(this MemberInfo propertyOrField)
        {
            if (propertyOrField.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)propertyOrField).PropertyType;
            }

            if (propertyOrField.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)propertyOrField).FieldType;
            }
            throw new ArgumentOutOfRangeException("propertyOrField","Expected PropertyInfo or FieldInfo; found :" + propertyOrField.MemberType);
        }

        public static void AddCountValue<T>(this IList<T> list,T value,int count)
        {
           for(var i= 0; i < count; i++)
           {
               list.Add(value);
           }
        }
    }
}
