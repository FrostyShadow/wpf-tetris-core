using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Mvvm;
using Reactive.Bindings;
using WpfTetrisLib.Extensions;
using WpfTetrisLib.Models;

namespace WpfTetrisApp.ViewModels
{
    public class NextFieldViewModel : BindableBase
    {
        private const byte RowCount = 5;
        private const byte ColumnCount = 5;

        private CellViewModel[,] _cells;

        public CellViewModel[,] Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        private Grid _fieldGrid;

        public Grid FieldGrid
        {
            get => _fieldGrid;
            set => SetProperty(ref _fieldGrid, value);
        }

        private static Color BackgroundColor => Colors.WhiteSmoke;

        public NextFieldViewModel(IReadOnlyReactiveProperty<TetriminoKind> nextTetriminoKind)
        {
            FieldGrid = new Grid();
            _cells = new CellViewModel[RowCount, ColumnCount];
            foreach (var cell in Cells.WithIndex())
            {
                _cells[cell.X, cell.Y] = new CellViewModel();
            }

            nextTetriminoKind.Select(x =>
                    Tetrimino.Create(x).Blocks.ToDictionary2(y => y.Position.Row, y => y.Position.Column))
                .Subscribe(x =>
                {
                    var offset = new Position((-6 - x.Count) / 2, 2);

                    foreach (var item2 in Cells.WithIndex())
                    {
                        var color = x.GetValueOrDefault(item2.X + offset.Row)
                                        ?.GetValueOrDefault(item2.Y + offset.Column)?.Color ?? BackgroundColor;
                        item2.Element.Color.Value = color;
                    }
                });
        }
    }
}
