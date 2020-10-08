using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend13.ViewModels;

namespace FriendOrganizer.Presentation.Friend13.Views
{
    public partial class Friend13Main : UserControl
    {
        private readonly Friend13MainViewModel _viewModel;

        public Friend13Main(Friend13MainViewModel viewModel)
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
