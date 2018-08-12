using System;
using System.Collections.Generic;

namespace Xn.Platform.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 解决重复键问题，重复时新值覆盖旧值
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionaryEx<TElement, TKey, TValue>(
                this IEnumerable<TElement> items,
                Func<TElement, TKey> keyGetter,
                Func<TElement, TValue> valueGetter)
        {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var item in items)
            {
                dict[keyGetter(item)] = valueGetter(item);
            }
            return dict;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }


        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }
    }
}
