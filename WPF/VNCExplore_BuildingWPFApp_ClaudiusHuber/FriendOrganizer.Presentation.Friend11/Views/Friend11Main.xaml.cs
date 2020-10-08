using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend11.ViewModels;

namespace FriendOrganizer.Presentation.Friend11.Views
{
    public partial class Friend11Main : UserControl
    {
        private readonly Friend11MainViewModel _viewModel;

        public Friend11Main(Friend11MainViewModel viewModel)
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
