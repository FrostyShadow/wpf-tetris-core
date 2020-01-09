using System;
using System.Collections.Generic;

namespace WpfTetrisLib.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key,
            TValue defaultValue = default)
        {
            if(self == null) throw new ArgumentNullException(nameof(self));
            return self.TryGetValue(key, out var result) ? result : defaultValue;
        }   
    }
}