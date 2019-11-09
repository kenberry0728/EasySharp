using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    public static class ListValueDictionaryExtensions
    {
        public static void Add<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
        {
            if (!dictionary.TryGetValue(key, out var list))
            {
                dictionary[key] = list = new List<TValue>();
            }

            list.Add(value);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, IEnumerable<TValue> values)
        {
            foreach (var value in values)
            {
                dictionary.Add(key, value);
            }
        }

        public static void Remove<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                list.Remove(value);
                if (list.Count == 0)
                {
                    dictionary.Remove(key);
                }
            }
        }

        public static IEnumerable<TValue> GetValues<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                return list;
            }

            return Enumerable.Empty<TValue>();
        }
    }
}
