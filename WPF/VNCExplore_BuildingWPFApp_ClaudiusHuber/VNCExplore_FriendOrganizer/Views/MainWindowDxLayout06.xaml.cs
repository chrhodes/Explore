using System.Windows;
using VNCExplore_FriendOrganizer.ViewModels;

namespace VNCExplore_FriendOrganizer.Views
{
    public partial class MainWindowDxLayout06 : Window
    {
        private MainWindowDxLayoutViewModel06 _viewModel;

        public MainWindowDxLayout06(MainWindowDxLayoutViewModel06 viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
