using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;

namespace WpfTetrisLib.Models
{
    public class Game
    {
        /// <summary>
        /// Game results
        /// </summary>
        public GameResult GameResult { get; } = new GameResult();
        /// <summary>
        /// Game field
        /// </summary>
        public Field Field { get; } = new Field();
        /// <summary>
        /// Is game running
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsPlaying => Field.IsActivated.ToReadOnlyReactiveProperty();
        /// <summary>
        /// Is game over
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsOver => Field.IsUpperLimitReached.ToReadOnlyReactiveProperty();
        /// <summary>
        /// Upcoming tetrimino
        /// </summary>
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

        /// <summary>
        /// Starts game
        /// </summary>
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
