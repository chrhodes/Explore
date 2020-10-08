using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend16.ViewModels;

namespace FriendOrganizer.Presentation.Friend16.Views
{
    public partial class Friend16Main : UserControl
    {
        private readonly Friend16MainViewModel _viewModel;

        public Friend16Main(Friend16MainViewModel viewModel)
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
