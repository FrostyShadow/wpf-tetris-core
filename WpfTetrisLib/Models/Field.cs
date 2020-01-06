using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using WpfTetrisLib.Extensions;
using Timer = System.Timers.Timer;

namespace WpfTetrisLib.Models
{
    public class Field
    {
        public const byte RowCount = 24;
        public const byte ColumnCount = 10;

        public IReadOnlyReactiveProperty<IReadOnlyList<Block>> PlacedBlocks => _placedBlocks;

        private readonly ReactiveProperty<IReadOnlyList<Block>> _placedBlocks =
            new ReactiveProperty<IReadOnlyList<Block>>(Array.Empty<Block>(),
                ReactivePropertyMode.RaiseLatestValueOnSubscribe);

        public ReactiveProperty<Tetrimino> Tetrimino { get; } = new ReactiveProperty<Tetrimino>();

        public IReadOnlyReactiveProperty<bool> IsActivated => _isActivated;
        private readonly ReactiveProperty<bool> _isActivated = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);

        public IReadOnlyReactiveProperty<bool> IsUpperLimitReached => _isUpperLimitReached;
        private readonly ReactiveProperty<bool> _isUpperLimitReached = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);

        public IReadOnlyReactiveProperty<int> LastRemovedRowCount => _lastRemovedRowCount;
        private readonly ReactiveProperty<int> _lastRemovedRowCount = new ReactiveProperty<int>(mode: ReactivePropertyMode.None);

        private Timer Timer { get; } = new Timer();

        public Field()
        {
            Timer.ElapsedAsObservable()
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x => MoveTetrimino(MoveDirection.Down));
        }

        public void Activate(TetriminoKind tetriminoKind)
        {
            _isActivated.Value = true;
            _isUpperLimitReached.Value = false;
            Tetrimino.Value = Models.Tetrimino.Create(tetriminoKind);
            _placedBlocks.Value = Array.Empty<Block>();
            Timer.Interval = 1000;
            Timer.Start();
        }

        private bool CheckCollision(Block block)
        {
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            if (block.Position.Column < 0)
                return true;

            if (ColumnCount <= block.Position.Column)
                return true;

            return RowCount <= block.Position.Row || _placedBlocks.Value.Any(x => x.Position == block.Position);
        }

        private Tuple<int, Block[]> RemoveAndFixBlock()
        {
            var rows = _placedBlocks.Value
                .Concat(Tetrimino.Value.Blocks)
                .GroupBy(x => x.Position.Row)
                .Select(x => new
                {
                    Row = x.Key,
                    IsFilled = ColumnCount <= x.Count(),
                    Blocks = x
                })
                .ToArray();

            var blocks = rows
                .OrderByDescending(x => x.Row)
                .WithIndex(x => x.IsFilled)
                .Where(x => !x.Element.IsFilled)
                .SelectMany(x =>
                {
                    if (x.Index == 0)
                        return x.Element.Blocks;

                    return x.Element.Blocks.Select(y =>
                    {
                        var position = new Position(y.Position.Row + x.Index, y.Position.Column);
                        return new Block(y.Color, position);
                    });
                })
                .ToArray();

            var removedRowCount = rows.Count(x => x.IsFilled);
            return Tuple.Create(removedRowCount, blocks);
        }

        private void FixTetrimino()
        {
            var result = RemoveAndFixBlock();

            var removedRowCount = result.Item1;
            if (removedRowCount > 0)
                _lastRemovedRowCount.Value = removedRowCount;

            if (result.Item2.Any(x => x.Position.Row < 0))
            {
                _isActivated.Value = false;
                _isUpperLimitReached.Value = true;
                return;
            }

            Tetrimino.Value = null;
            _placedBlocks.Value = result.Item2;
        }

        public void MoveTetrimino(MoveDirection moveDirection)
        {
            if(!_isActivated.Value) return;

            if (moveDirection == MoveDirection.Down)
            {
                Timer.Stop();
                if (Tetrimino.Value.Move(moveDirection, CheckCollision)) Tetrimino.ForceNotify();
                else FixTetrimino();
                Timer.Start();
                return;
            }

            if(Tetrimino.Value.Move(moveDirection, CheckCollision)) Tetrimino.ForceNotify();
        }
    }
}
