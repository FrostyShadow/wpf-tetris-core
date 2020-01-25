using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.IO;
using System.Media;
using System.Windows;
using Reactive.Bindings;
using Unity;
using Unity.Microsoft.DependencyInjection;
using WpfTetrisApp.Views;
using WpfTetrisLib.Models;

namespace WpfTetrisApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static SoundPlayer BgmPlayer { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private IUnityContainer _container;
        protected override Window CreateShell()
        { 
            _container = Container.GetContainer();

            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Tetris WPF\\config\\");
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(Path.Combine(appDataPath, "appsettings.json"), true, true)
                .Build();
            BgmPlayer = new SoundPlayer("Resources\\Tetris.wav");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _container.BuildServiceProvider(serviceCollection);
            return _container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }
    }
}
