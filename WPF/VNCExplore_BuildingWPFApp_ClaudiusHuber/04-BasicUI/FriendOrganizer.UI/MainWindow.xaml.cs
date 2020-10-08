using System.Windows;

using FriendOrganizer.UI04.ViewModel;

namespace FriendOrganizer.UI04
{
    public partial class MainWindow : Window
  {
    private MainViewModel _viewModel;

    public MainWindow(MainViewModel viewModel)
    {
      InitializeComponent();
      _viewModel = viewModel;
      DataContext = _viewModel;
      Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      _viewModel.Load();
    }
  }
}
