using System.Collections.Generic;

namespace Xn.Platform.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = new TValue();
                dictionary[key] = value;
            }
            return value;
        }

        public static void IncrementValue<TKey>(this IDictionary<TKey, int> dictionary, TKey key, int value = 1)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary[key] += value;
        }

        public static void IncrementValue<TKey>(this IDictionary<TKey, long> dictionary, TKey key, long value = 1)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary[key] += value;
        }
    }
}
