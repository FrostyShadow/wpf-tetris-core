using Prism.Mvvm;
using Reactive.Bindings;
using WpfTetrisLib.Models;

namespace WpfTetrisApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        
        private Game Game { get; } = new Game();

        private GameResultViewModel _gameResultViewModel;

        public GameResultViewModel GameResultViewModel
        {
            get => _gameResultViewModel;
            set => SetProperty(ref _gameResultViewModel, value);
        }

        private FieldViewModel _fieldViewModel;

        public FieldViewModel FieldViewModel
        {
            get => _fieldViewModel;
            set => SetProperty(ref _fieldViewModel, value);
        }

        private NextFieldViewModel _nextFieldViewModel;

        public NextFieldViewModel NextFieldViewModel
        {
            get => _nextFieldViewModel;
            set => SetProperty(ref _nextFieldViewModel, value);
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

        public MainWindowViewModel()
        {
            _gameResultViewModel = new GameResultViewModel(Game.GameResult);
            _fieldViewModel = new FieldViewModel(Game.Field);
            _nextFieldViewModel = new NextFieldViewModel(Game.NextTetrimino);
            _isPlaying = Game.IsPlaying;
            _isGameOver = Game.IsOver;
        }

        public void Play() => Game.Play();
    }
}
