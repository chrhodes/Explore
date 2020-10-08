using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend14.ViewModels;

namespace FriendOrganizer.Presentation.Friend14.Views
{
    public partial class Friend14Main : UserControl
    {
        private readonly Friend14MainViewModel _viewModel;

        public Friend14Main(Friend14MainViewModel viewModel)
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
