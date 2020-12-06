using System.Reflection;
using System.Runtime.Versioning;
using System.Windows;

using WiredBrainCoffee.CustomerApp.UI.ViewModel;

namespace WiredBrainCoffee.CustomerApp.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            MainViewModel = mainViewModel;
            DataContext = MainViewModel;
            Loaded += MainWindow_Loaded;

            applicationHeader.Version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<TargetFrameworkAttribute>()
                .FrameworkName;
        }

        public MainViewModel MainViewModel { get; }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await MainViewModel.InitializeAsync();
        }
    }
}