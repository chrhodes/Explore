using System.Windows;
using VNCExplore_FriendOrganizer.ViewModels;

namespace VNCExplore_FriendOrganizer.Views
{
    public partial class MainWindowDxLayout : Window
    {
        private MainWindowDxLayoutViewModel _viewModel;

        public MainWindowDxLayout(MainWindowDxLayoutViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //_viewModel.Load();
        //}

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
