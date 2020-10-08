using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend18.ViewModels;

namespace FriendOrganizer.Presentation.Friend18.Views
{
    public partial class Friend18Main : UserControl
    {
        private readonly Friend18MainViewModel _viewModel;

        public Friend18Main(Friend18MainViewModel viewModel)
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
