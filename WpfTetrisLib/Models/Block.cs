using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WpfTetrisLib.Models
{
    public class Block
    {
        /// <summary>
        /// Block color
        /// </summary>
        public Color Color { get; }
        /// <summary>
        /// Block position
        /// </summary>
        public Position Position { get; }

        public Block(Color color, Position position)
        {
            Color = color;
            Position = position;
        }
    }
}
