using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Reactive.Bindings;

namespace WpfTetrisLib.Models
{
    public class GameResult
    {
        /// <summary>
        /// Total score based on all removed rows
        /// </summary>
        public IReadOnlyReactiveProperty<int> TotalRowCount { get; }
        /// <summary>
        /// Number of single rows removed
        /// </summary>
        public IReadOnlyReactiveProperty<int> RowCount1 => _rowCount1;
        private readonly ReactiveProperty<int> _rowCount1 = new ReactiveProperty<int>();
        /// <summary>
        /// Number of double rows removed
        /// </summary>
        public IReadOnlyReactiveProperty<int> RowCount2 => _rowCount2;
        private readonly ReactiveProperty<int> _rowCount2 = new ReactiveProperty<int>();
        /// <summary>
        /// Number of triple rows removed
        /// </summary>
        public IReadOnlyReactiveProperty<int> RowCount3 => _rowCount3;
        private readonly ReactiveProperty<int> _rowCount3 = new ReactiveProperty<int>();
        /// <summary>
        /// Number of quadruple rows removed
        /// </summary>
        public IReadOnlyReactiveProperty<int> RowCount4 => _rowCount4;
        private readonly ReactiveProperty<int> _rowCount4 = new ReactiveProperty<int>();

        public GameResult()
        {
            TotalRowCount = RowCount1
                .CombineLatest(RowCount2, RowCount3, RowCount4, (x1, x2, x3, x4) => x1 * 1 + x2 * 2 + x3 * 3 + x4 * 4)
                .ToReadOnlyReactiveProperty();
        }

        /// <summary>
        /// Adds rows to scoreboard
        /// </summary>
        /// <param name="count">Number of rows</param>
        public void AddRowCount(int count)
        {
            switch (count)
            {
                case 1: _rowCount1.Value++;
                    break;
                case 2: _rowCount2.Value++;
                    break;
                case 3: _rowCount3.Value++;
                    break;
                case 4: _rowCount4.Value++;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(count));
            }
        }

        /// <summary>
        /// Clears the scoreboard
        /// </summary>
        public void Clear()
        {
            
            _rowCount1.Value = 0;
            _rowCount2.Value = 0;
            _rowCount3.Value = 0;
            _rowCount4.Value = 0;
        }
    }
}
