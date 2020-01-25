namespace WpfTetrisLib.Extensions
{
    /// <summary>
    /// Indexed element model
    /// </summary>
    /// <typeparam name="T">Indexed element type</typeparam>
    public struct IndexedItem<T>
    {
        /// <summary>
        /// Index of an element
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Indexed element type
        /// </summary>
        public T Element { get; }

        internal IndexedItem(int index, T element)
        {
            Index = index;
            Element = element;
        }
    }

    /// <summary>
    /// Indexed element model
    /// </summary>
    /// <typeparam name="T">Indexed element type</typeparam>
    public struct IndexedItem2<T>
    {
        /// <summary>
        /// X-axis value
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y-axis value
        /// </summary>
        public int Y { get; }
        /// <summary>
        /// Indexed element type
        /// </summary>
        public T Element { get; }


        internal IndexedItem2(int x, int y, T element)
        {
            X = x;
            Y = y;
            Element = element;
        }
    }
}