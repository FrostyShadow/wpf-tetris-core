using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using WpfTetrisLib.Extensions;
using WpfTetrisLib.Models;

namespace WpfTetrisApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private Game Game { get; } = new Game();

        private GameResultViewModel _gameResult;

        public GameResultViewModel GameResult
        {
            get => _gameResult;
            set => SetProperty(ref _gameResult, value);
        }

        private FieldViewModel _field;

        public FieldViewModel Field
        {
            get => _field;
            set => SetProperty(ref _field, value);
        }

        private NextFieldViewModel _nextField;

        public NextFieldViewModel NextField
        {
            get => _nextField;
            set => SetProperty(ref _nextField, value);
        }

        private IReadOnlyReactiveProperty<bool> _isPlaying;

        public IReadOnlyReactiveProperty<bool> IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        private IReadOnlyReactiveProperty<bool> _isGameOver;

        public IReadOnlyReactiveProperty<bool> IsGameOver
        {
            get => _isGameOver;
            set => SetProperty(ref _isGameOver, value);
        }

        public DelegateCommand<object> MoveLeftCommand { get; }
        public DelegateCommand<object> MoveRightCommand { get; }
        public DelegateCommand<object> ForceDownCommand { get; }
        public DelegateCommand<object> RotateCommand { get; }
        public DelegateCommand<object> NewGameCommand { get; }

        public MainWindowViewModel()
        {
            MoveLeftCommand = new DelegateCommand<object>(MoveLeft);
            MoveRightCommand = new DelegateCommand<object>(MoveRight);
            ForceDownCommand = new DelegateCommand<object>(ForceDown);
            RotateCommand = new DelegateCommand<object>(Rotate);
            NewGameCommand = new DelegateCommand<object>(NewGame);
            _gameResult = new GameResultViewModel(Game.GameResult);
            _field = new FieldViewModel(Game.Field);
            _nextField = new NextFieldViewModel(Game.NextTetrimino);
            SetupField(Field.FieldGrid ,Field.Cells, 30);
            SetupField(NextField.FieldGrid ,NextField.Cells, 18);
            _isPlaying = Game.IsPlaying;
            _isGameOver = Game.IsOver;
            Game.Play();
            if(IsPlaying.Value) App.BgmPlayer.PlayLooping();
        }

        private void MoveLeft(object parameter) => Field.MoveTetrimino(MoveDirection.Left);
        private void MoveRight(object parameter) => Field.MoveTetrimino(MoveDirection.Right);
        private void ForceDown(object parameter) => Field.ForceFixTetrimino();
        private void Rotate(object parameter) => Field.RotateTetrimino(RotationDirection.Right);

        private void NewGame(object parameter)
        {
            if (IsPlaying.Value) return;
            App.BgmPlayer.Stop();
            Game.Play();
            App.BgmPlayer.PlayLooping();
        }

        private static void SetupField(Grid field, CellViewModel[,] cells, byte blockSize)
        {
            for(var i = 0;i < cells.GetLength(0); i++)
            {
                field.RowDefinitions.Add(new RowDefinition {Height = new GridLength(blockSize, GridUnitType.Pixel)});
            }

            for (var i = 0; i < cells.GetLength(1); i++)
            {
                field.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(blockSize, GridUnitType.Pixel)});
            }

            foreach (var indexedItem2 in cells.WithIndex())
            {
                var brush = new SolidColorBrush();
                var control = new TextBlock
                {
                    DataContext = indexedItem2.Element,
                    Background = brush,
                    Margin = new Thickness(1)
                };
                BindingOperations.SetBinding(brush, SolidColorBrush.ColorProperty, new Binding("Color.Value"));

                Grid.SetRow(control, indexedItem2.X);
                Grid.SetColumn(control, indexedItem2.Y);
                field.Children.Add(control);
            }
        }
    }
}
