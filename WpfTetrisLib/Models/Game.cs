using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;

namespace WpfTetrisLib.Models
{
    public class Game
    {
        public GameResult GameResult { get; } = new GameResult();
        public Field Field { get; } = new Field();
        public IReadOnlyReactiveProperty<bool> IsPlaying => Field.IsActivated.ToReadOnlyReactiveProperty();
        public IReadOnlyReactiveProperty<bool> IsOver => Field.IsUpperLimitReached.ToReadOnlyReactiveProperty();
        public IReadOnlyReactiveProperty<TetriminoKind> NextTetrimino => _nextTetrimino;
        private readonly ReactiveProperty<TetriminoKind> _nextTetrimino = new ReactiveProperty<TetriminoKind>();
        private int PreviousCount { get; set; }

        public Game()
        {
            Field.PlacedBlocks.Subscribe(_ =>
            {
                var count = GameResult.TotalRowCount.Value / 10;
                if (count > PreviousCount)
                {
                    PreviousCount = count;
                    Field.SpeedUp();
                }

                var tetriminoKind = _nextTetrimino.Value;
                _nextTetrimino.Value = Tetrimino.RandomKind();
                Field.Tetrimino.Value = Tetrimino.Create(tetriminoKind);
            });
            Field.LastRemovedRowCount.Subscribe(GameResult.AddRowCount);
        }

        public void Play()
        {
            if(IsPlaying.Value)
                return;
            PreviousCount = 0;
            _nextTetrimino.Value = Tetrimino.RandomKind();
            Field.Activate(Tetrimino.RandomKind());
            GameResult.Clear();
        }
    }
}
