using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WpfTetrisLib.Models
{
    public class Block
    {
        public Color Color { get; }
        public Position Position { get; }

        public Block(Color color, Position position)
        {
            Color = color;
            Position = position;
        }
    }
}
