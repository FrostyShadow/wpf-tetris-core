using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Prism.Mvvm;
using Reactive.Bindings;

namespace WpfTetrisApp.ViewModels
{
    public class CellViewModel : BindableBase
    {
        private ReactiveProperty<Color> _color = new ReactiveProperty<Color>();

        public ReactiveProperty<Color> Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }
}
