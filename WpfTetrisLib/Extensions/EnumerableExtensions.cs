using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfTetrisLib.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Adds indexes to IEnumerable elements
        /// </summary>
        /// <typeparam name="T">IEnumerable element type</typeparam>
        /// <param name="self">IEnumerable</param>
        /// <returns>Indexed IEnumerable</returns>
        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> self)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));
            return self.Select((x, i) => new IndexedItem<T>(i, x));
        }

        /// <summary>
        /// Adds indexes to IEnumerable elements with index incrementation condition
        /// </summary>
        /// <typeparam name="T">IEnumerable element type</typeparam>
        /// <param name="self">IEnumerable</param>
        /// <param name="predicate">Index incrementation condition</param>
        /// <returns>Indexed IEnumerable</returns>
        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var i = 0;
            foreach (var x in self)
            {
                if (predicate(x))
                    i++;
                yield return new IndexedItem<T>(i, x);
            }
        }

        /// <summary>
        /// Adds indexes to IEnumerable elements
        /// </summary>
        /// <typeparam name="T">IEnumerable element type</typeparam>
        /// <param name="self">IEnumerable</param>
        /// <returns>Indexed IEnumerable</returns>
        public static IEnumerable<IndexedItem2<T>> WithIndex<T>(this T[,] self)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));

            for(var x = 0; x < self.GetLength(0); x++)
                for(var y = 0; y < self.GetLength(1); y++)
                    yield return new IndexedItem2<T>(x, y, self[x,y]);
        }

        /// <summary>
        /// Creates a two-dimensional dictionary
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TKeyX">X-axis type</typeparam>
        /// <typeparam name="TKeyY">Y-axis type</typeparam>
        /// <param name="self">IEnumerable</param>
        /// <param name="xSelector">X-axis key selector</param>
        /// <param name="ySelector">Y-axis key selector</param>
        /// <returns>Two-dimensional dictionary</returns>
        public static Dictionary<TKeyX, Dictionary<TKeyY, TSource>> ToDictionary2<TSource, TKeyX, TKeyY>(
            this IEnumerable<TSource> self, Func<TSource, TKeyX> xSelector, Func<TSource, TKeyY> ySelector)
        {
            if(self == null) throw new ArgumentNullException(nameof(self));
            if(xSelector == null) throw new ArgumentNullException(nameof(xSelector));
            if(ySelector == null) throw new ArgumentNullException(nameof(ySelector));

            return self.GroupBy(xSelector).ToDictionary(x => x.Key, xs => xs.ToDictionary(ySelector));
        }

        /// <summary>
        /// Creates a two-dimensional dictionary
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TKeyX">X-axis type</typeparam>
        /// <typeparam name="TKeyY">Y-axis type</typeparam>
        /// <typeparam name="TElement">IEnumerable element type</typeparam>
        /// <param name="self">IEnumerable</param>
        /// <param name="xSelector">X-axis key selector</param>
        /// <param name="ySelector">Y-axis key selector</param>
        /// <param name="elementSelector">Element selector</param>
        /// <returns>Two-dimensional dictionary</returns>
        public static Dictionary<TKeyX, Dictionary<TKeyY, TElement>> ToDictionary2<TSource, TKeyX, TKeyY, TElement>(
            this IEnumerable<TSource> self, Func<TSource, TKeyX> xSelector, Func<TSource, TKeyY> ySelector,
            Func<TSource, TElement> elementSelector)
        {
            if(self == null) throw new ArgumentNullException(nameof(self));
            if(xSelector == null) throw new ArgumentNullException(nameof(xSelector));
            if(ySelector == null) throw new ArgumentNullException(nameof(ySelector));
            if(elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return self.GroupBy(xSelector).ToDictionary(x => x.Key, xs => xs.ToDictionary(ySelector, elementSelector));
        }
    }
}