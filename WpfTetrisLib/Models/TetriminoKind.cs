using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WpfTetrisLib.Models
{
    public enum TetriminoKind
    {
        I,
        O,
        S,
        Z,
        J,
        L,
        T
    }

    public static class TetriminoExtensions
    {
        public static Color BlockColor(this TetriminoKind self)
        {
            return self switch
            {
                TetriminoKind.I => Colors.LightBlue,
                TetriminoKind.O => Colors.Yellow,
                TetriminoKind.S => Colors.YellowGreen,
                TetriminoKind.Z => Colors.Red,
                TetriminoKind.J => Colors.Blue,
                TetriminoKind.L => Colors.Orange,
                TetriminoKind.T => Colors.Purple,
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null),
            };
        }

        public static Position InitialPosition(this TetriminoKind self)
        {
            var length = 0;
            switch (self)
            {
                case TetriminoKind.I: length = 4;
                    break;
                case TetriminoKind.O: length = 2;
                    break;
                case TetriminoKind.S:
                case TetriminoKind.Z:
                case TetriminoKind.J:
                case TetriminoKind.L:
                case TetriminoKind.T: length = 3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(self), self, null);
            }

            var row = -length;
            var column = (Field.ColumnCount - length) / 2;
            return new Position(row, column);
        }

        public static IReadOnlyList<Block> CreateBlocks(this TetriminoKind self, Position offset,
            Direction direction = Direction.Up)
        {
            int[,] pattern = null;
            switch (self)
            {
                case TetriminoKind.I:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {0, 1, 0, 0 },
                                {0, 1, 0, 0 },
                                {0, 1, 0, 0 },
                                {0, 1, 0, 0 }
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {0, 0, 0, 0},
                                {1, 1, 1, 1},
                                {0, 0, 0, 0},
                                {0, 0, 0, 0}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {0, 0, 1, 0},
                                {0, 0, 1, 0},
                                {0, 0, 1, 0},
                                {0, 0, 1, 0}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {0, 0, 0, 0},
                                {0, 0, 0, 0},
                                {1, 1, 1, 1},
                                {0, 0, 0, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.O:
                    pattern = new[,]
                    {
                        {1, 1},
                        {1, 1}
                    };
                    break;
                case TetriminoKind.S:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {0, 1, 1},
                                {1, 1, 0},
                                {0, 0, 0}
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 1},
                                {0, 0, 1}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {0, 0, 0},
                                {0, 1, 1},
                                {1, 1, 0}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {1, 0, 0},
                                {1, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                    }
                    break;
                case TetriminoKind.Z:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {1, 1, 0},
                                {0, 1, 1},
                                {0, 0, 0}
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {0, 0, 1},
                                {0, 1, 1},
                                {0, 1, 0}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 0},
                                {0, 1, 1}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 0},
                                {1, 0, 0}
                            };
                            break;
                    }
                    break;
                case TetriminoKind.J:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 0},
                                {1, 1, 0}
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {1, 0, 0},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {0, 1, 1},
                                {0, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {0, 0, 1}
                            };
                            break;
                    }
                    break;
                case TetriminoKind.L:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 0},
                                {0, 1, 1}
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {1, 0, 0}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {1, 1, 0},
                                {0, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {0, 0, 1},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.T:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;
                        case Direction.Right:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 1},
                                {0, 1, 0}
                            };
                            break;
                        case Direction.Down:
                            pattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {0, 1, 0}
                            };
                            break;
                        case Direction.Left:
                            pattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(self), self, null);
            }

            var color = self.BlockColor();
            return Enumerable.Range(0, pattern.GetLength(0))
                .SelectMany(r => Enumerable.Range(0, pattern.GetLength(1)).Select(c => new Position(r, c)))
                .Where(x => pattern[x.Row, x.Column] != 0)
                .Select(x => new Position(x.Row + offset.Row, x.Column + offset.Column))
                .Select(x => new Block(color, x))
                .ToArray();
        }
    }
}