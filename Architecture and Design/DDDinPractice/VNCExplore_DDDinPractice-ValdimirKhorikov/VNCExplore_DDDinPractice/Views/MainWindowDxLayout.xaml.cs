using System.Windows;
using MahApps.Metro.Controls;
using VNCExplore_FriendOrganizer.ViewModels;

namespace VNCExplore_FriendOrganizer.Views
{
    public partial class MainWindowDxLayout : MetroWindow
    {
        private MainWindowDxLayoutViewModel _viewModel;

        public MainWindowDxLayout(MainWindowDxLayoutViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
