using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    public static class HashSetValueDictionaryExtensions
    {
        public static void Add<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue value)
        {
            if (!dictionary.TryGetValue(key, out var list))
            {
                dictionary[key] = list = new HashSet<TValue>();
            }

            list.Add(value);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dictionary, TKey key, IEnumerable<TValue> values)
        {
            foreach (var value in values)
            {
                dictionary.Add(key, value);
            }
        }

        public static void Remove<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary.TryGetValue(key, out var hash))
            {
                hash.Remove(value);
                if (hash.Count == 0)
                {
                    dictionary.Remove(key);
                }
            }
        }

        public static IEnumerable<TValue> GetValues<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var hash))
            {
                return hash;
            }

            return Enumerable.Empty<TValue>();
        }
    }
}
