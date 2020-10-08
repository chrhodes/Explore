using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend15.ViewModels;

namespace FriendOrganizer.Presentation.Friend15.Views
{
    public partial class Friend15Main : UserControl
    {
        private readonly Friend15MainViewModel _viewModel;

        public Friend15Main(Friend15MainViewModel viewModel)
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
