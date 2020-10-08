using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend10A.ViewModels;

namespace FriendOrganizer.Presentation.Friend10A.Views
{
    public partial class Friend10AMain : UserControl
    {
        readonly Friend10AMainViewModel _viewModel;

        public Friend10AMain(Friend10AMainViewModel viewModel)
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
