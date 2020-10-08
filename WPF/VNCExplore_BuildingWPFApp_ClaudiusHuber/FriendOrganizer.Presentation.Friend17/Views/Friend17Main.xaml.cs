using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend17.ViewModels;

namespace FriendOrganizer.Presentation.Friend17.Views
{
    public partial class Friend17Main : UserControl
    {
        private readonly Friend17MainViewModel _viewModel;

        public Friend17Main(Friend17MainViewModel viewModel)
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
