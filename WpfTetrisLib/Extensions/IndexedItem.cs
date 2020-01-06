namespace WpfTetrisLib.Extensions
{
    public struct IndexedItem<T>
    {
        public int Index { get; }
        public T Element { get; }

        internal IndexedItem(int index, T element)
        {
            Index = index;
            Element = element;
        }
    }

    public struct IndexedItem2<T>
    {
        public int X { get; }
        public int Y { get; }
        public T Element { get; }


        internal IndexedItem2(int x, int y, T element)
        {
            X = x;
            Y = y;
            Element = element;
        }
    }
}