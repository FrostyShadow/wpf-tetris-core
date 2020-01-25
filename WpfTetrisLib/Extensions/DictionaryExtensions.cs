using System;
using System.Collections.Generic;

namespace WpfTetrisLib.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="self">Target dictionary</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Value or default if no value</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key,
            TValue defaultValue = default)
        {
            if(self == null) throw new ArgumentNullException(nameof(self));
            return self.TryGetValue(key, out var result) ? result : defaultValue;
        }   
    }
}