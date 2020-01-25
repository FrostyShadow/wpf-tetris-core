using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTetrisLib.Models
{
    public struct Position
    {
        /// <summary>
        /// Position in row
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// Position in column
        /// </summary>
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets position HashCode
        /// </summary>
        /// <returns>Position HashCode</returns>
        public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

        /// <summary>
        /// Gets position as a string
        /// </summary>
        /// <returns>Position as string</returns>
        public override string ToString() => $"({Row}, {Column})";

        /// <summary>
        /// Position equality operator
        /// </summary>
        /// <param name="position1">First operand</param>
        /// <param name="position2">Second operand</param>
        /// <returns>True if positions are equal</returns>
        public static bool operator ==(Position position1, Position position2) =>
            position1.Row == position2.Row && position1.Column == position2.Column;

        /// <summary>
        /// Position inequality operator
        /// </summary>
        /// <param name="position1">First operand</param>
        /// <param name="position2">Second operand</param>
        /// <returns>True if positions aren't equal</returns>
        public static bool operator !=(Position position1, Position position2) => !(position1 == position2);

        /// <summary>
        /// Equates two positions
        /// </summary>
        /// <param name="obj">Position to compare to</param>
        /// <returns>True if positions are equal</returns>
        public override bool Equals(object obj)
        {
            return obj != null && this == (Position) obj;
        }
    }
}
