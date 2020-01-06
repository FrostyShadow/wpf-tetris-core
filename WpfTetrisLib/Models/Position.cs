using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTetrisLib.Models
{
    public struct Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

        public override string ToString() => $"({Row}, {Column})";

        public static bool operator ==(Position position1, Position position2) =>
            position1.Row == position2.Row && position1.Column == position2.Column;

        public static bool operator !=(Position position1, Position position2) => !(position1 == position2);

        public override bool Equals(object obj)
        {
            return obj != null && this == (Position) obj;
        }
    }
}
