using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Reactive.Bindings;
using WpfTetrisLib.Models;

namespace WpfTetrisApp.ViewModels
{
    public class GameResultViewModel : BindableBase
    {
        private GameResult GameResult { get; }

        private IReadOnlyReactiveProperty<int> _totalRowCount;

        public IReadOnlyReactiveProperty<int> TotalRowCount
        {
            get => _totalRowCount;
            set => SetProperty(ref _totalRowCount, value);
        }

        private IReadOnlyReactiveProperty<int> _rowCount1;

        public IReadOnlyReactiveProperty<int> RowCount1
        {
            get => _rowCount1;
            set => SetProperty(ref _rowCount1, value);
        }

        private IReadOnlyReactiveProperty<int> _rowCount2;

        public IReadOnlyReactiveProperty<int> RowCount2
        {
            get => _rowCount2;
            set => SetProperty(ref _rowCount2, value);
        }

        private IReadOnlyReactiveProperty<int> _rowCount3;

        public IReadOnlyReactiveProperty<int> RowCount3
        {
            get => _rowCount3;
            set => SetProperty(ref _rowCount3, value);
        }

        private IReadOnlyReactiveProperty<int> _rowCount4;

        public IReadOnlyReactiveProperty<int> RowCount4
        {
            get => _rowCount4;
            set => SetProperty(ref _rowCount4, value);
        }

        public GameResultViewModel(GameResult gameResult)
        {
            GameResult = gameResult;
            _totalRowCount = GameResult.TotalRowCount;
            _rowCount1 = GameResult.RowCount1;
            _rowCount2 = GameResult.RowCount2;
            _rowCount3 = GameResult.RowCount3;
            _rowCount4 = GameResult.RowCount4;
        }
    }
}
