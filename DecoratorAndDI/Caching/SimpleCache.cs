using System.Collections.Generic;

namespace DecoratorAndDI.Caching
{
    internal class SimpleCache : ICache
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>();

        public void Add<T>(string key, T value)
        {
            if (cache.ContainsKey(key))
                cache[key] = value;
            else
                cache.Add(key, value);
        }

        public bool Contains(string key) => cache.ContainsKey(key);
        public T Get<T>(string key) => (T)cache[key];
    }
}
