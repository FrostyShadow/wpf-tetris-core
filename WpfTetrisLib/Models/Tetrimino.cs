using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Media;
using WpfTetrisLib.Providers;

namespace WpfTetrisLib.Models
{
    public class Tetrimino
    {
        /// <summary>
        /// Kind of tetrimino
        /// </summary>
        public TetriminoKind TetriminoKind { get; }
        /// <summary>
        /// Color of tetrimino
        /// </summary>
        public Color Color => TetriminoKind.BlockColor();
        /// <summary>
        /// Position of tetrimino
        /// </summary>
        public Position Position { get; private set; }
        /// <summary>
        /// Direction of tetrimino
        /// </summary>
        public Direction Direction { get; private set; }
        /// <summary>
        /// List of blocks building tetrimino
        /// </summary>
        public IReadOnlyList<Block> Blocks { get; private set; }

        private Tetrimino(TetriminoKind tetriminoKind)
        {
            TetriminoKind = tetriminoKind;
            Position = tetriminoKind.InitialPosition();
            Blocks = tetriminoKind.CreateBlocks(Position);
        }

        /// <summary>
        /// Randomizes tetrimino kind
        /// </summary>
        /// <returns>Random tetrimino kind</returns>
        public static TetriminoKind RandomKind()
        {
            var length = Enum.GetValues(typeof(TetriminoKind)).Length;
            return (TetriminoKind) RandomProvider.ThreadRandom.Next(length);
        }

        /// <summary>
        /// Creates new tetrimino
        /// </summary>
        /// <param name="tetriminoKind">Tetrimino kind</param>
        /// <returns>Newly created tetrimino</returns>
        public static Tetrimino Create(TetriminoKind? tetriminoKind = null)
        {
            tetriminoKind ??= RandomKind();
            return new Tetrimino(tetriminoKind.Value);
        }

        /// <summary>
        /// Moves tetrimino
        /// </summary>
        /// <param name="moveDirection">Direction of movement</param>
        /// <param name="checkCollision">Collision checker</param>
        /// <returns>True if tetrimino has moved</returns>
        public bool Move(MoveDirection moveDirection, Func<Block, bool> checkCollision)
        {
            var position = Position;
            if (moveDirection == MoveDirection.Down)
            {
                var row = position.Row + 1;
                position = new Position(row, position.Column);
            }
            else
            {
                var delta = (moveDirection == MoveDirection.Right) ? 1 : -1;
                var column = position.Column + delta;
                position = new Position(position.Row, column);
            }

            var blocks = TetriminoKind.CreateBlocks(position, Direction);
            if (blocks.Any(checkCollision)) return false;

            Position = position;
            Blocks = blocks;
            return true;
        }

        /// <summary>
        /// Rotates tetrimino
        /// </summary>
        /// <param name="rotationDirection">Direction of rotation</param>
        /// <param name="checkCollision">Collision checker</param>
        /// <returns>True if tetrimino has rotated</returns>
        public bool Rotation(RotationDirection rotationDirection, Func<Block, bool> checkCollision)
        {
            var count = Enum.GetValues(typeof(Direction)).Length;
            var delta = (rotationDirection == RotationDirection.Right) ? 1 : -1;
            var direction = (int) Direction + delta;
            if (direction < 0) direction += count;
            if (direction >= count) direction %= count;

            var adjustPattern = TetriminoKind == TetriminoKind.I ? new[] {0, 1, -1, 2, -2} : new[] {0, 1, -1};

            foreach (var adjust in adjustPattern)
            {
                var position = new Position(Position.Row, Position.Column + adjust);
                var blocks = TetriminoKind.CreateBlocks(position, (Direction) direction);

                if (blocks.Any(checkCollision)) continue;
                Direction = (Direction) direction;
                Position = position;
                Blocks = blocks;
                return true;
            }

            return false;
        }
    }
}
