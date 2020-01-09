using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Media;
using Prism.Mvvm;
using Reactive.Bindings;
using WpfTetrisLib.Extensions;
using WpfTetrisLib.Models;

namespace WpfTetrisApp.ViewModels
{
    public class FieldViewModel : BindableBase
    {
        private Field Field { get; }

        private CellViewModel[,] _cells;

        public CellViewModel[,] Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        private IReadOnlyReactiveProperty<bool> _isActivated;

        public IReadOnlyReactiveProperty<bool> IsActivated
        {
            get => _isActivated;
            set => SetProperty(ref _isActivated, value);
        }

        private static Color BackgroundColor => Colors.WhiteSmoke;

        public FieldViewModel(Field field)
        {
            Field = field;
            _isActivated = Field.IsActivated;

            Cells = new CellViewModel[Field.RowCount,Field.ColumnCount];
            foreach (var item2 in Cells.WithIndex())
            {
                Cells[item2.X,item2.Y] = new CellViewModel();
            }

            Field.Tetrimino.CombineLatest(Field.PlacedBlocks,
                (t, p) => (t == null ? p : p.Concat(t.Blocks)).ToDictionary2(x => x.Position.Row,
                    x => x.Position.Column)).Subscribe(
                x =>
                {
                    foreach (var item2 in Cells.WithIndex())
                    {
                        var color = x.GetValueOrDefault(item2.X)?.GetValueOrDefault(item2.Y)?.Color ?? BackgroundColor;
                        item2.Element.Color.Value = color;
                    }
                });
        }

        public void MoveTetrimino(MoveDirection moveDirection) => Field.MoveTetrimino(moveDirection);
        public void RotateTetrimino(RotationDirection rotationDirection) => Field.RotateTetrimino(rotationDirection);
        public void ForceFixTetrimino() => Field.ForceFixTetrimino();
    }
}
