using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend12.ViewModels;

namespace FriendOrganizer.Presentation.Friend12.Views
{
    public partial class Friend12Main : UserControl
    {
        private readonly Friend12MainViewModel _viewModel;

        public Friend12Main(Friend12MainViewModel viewModel)
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
