using System.Collections.Generic;

namespace EasySharpStandard.Collections.Dictionaries.Core.Models
{
    public class MutualDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> keyToValues = new Dictionary<TKey, TValue>();
        private readonly Dictionary<TValue, TKey> valueToKeys = new Dictionary<TValue, TKey>();

        public TValue this[TKey key]
        {
            get => this.keyToValues[key];

            set => this.Add(key, value);
        }

        public TKey GetKey(TValue value)
        {
            return this.valueToKeys[value];
        }

        public int Count => this.keyToValues.Count;

        public ICollection<TKey> Keys => this.keyToValues.Keys;

        public ICollection<TValue> Values => this.keyToValues.Values;

        public void Add(TKey key, TValue value)
        {
            this.keyToValues.Add(key, value);
            this.valueToKeys.Add(value, key);
        }

        public void Clear()
        {
            this.keyToValues.Clear();
            this.valueToKeys.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return this.keyToValues.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return this.valueToKeys.ContainsKey(value);
        }

        public bool Remove(TKey key)
        {
            if (!this.keyToValues.TryGetValue(key, out var value))
            {
                return false;
            }

            this.keyToValues.Remove(key);
            this.valueToKeys.Remove(value);

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.keyToValues.TryGetValue(key, out value);
        }

        public bool TryGetKey(TValue value, out TKey key)
        {
            return this.valueToKeys.TryGetValue(value, out key);
        }
    }
}
