using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfTetrisLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> self)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));
            return self.Select((x, i) => new IndexedItem<T>(i, x));
        }

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

        public static IEnumerable<IndexedItem2<T>> WithIndex<T>(this T[,] self)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));

            for(var x = 0; x < self.GetLength(0); x++)
                for(var y = 0; y < self.GetLength(1); y++)
                    yield return new IndexedItem2<T>(x, y, self[x,y]);
        }
    }
}