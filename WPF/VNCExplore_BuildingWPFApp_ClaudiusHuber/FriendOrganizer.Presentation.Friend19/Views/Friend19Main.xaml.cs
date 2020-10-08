using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend19.ViewModels;

namespace FriendOrganizer.Presentation.Friend19.Views
{
    public partial class Friend19Main : UserControl
    {
        private readonly Friend19MainViewModel _viewModel;

        public Friend19Main(Friend19MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            Loaded += UserControl_Loaded;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs args)
        {
            await _viewModel.LoadAsync();
        }
    }
}
