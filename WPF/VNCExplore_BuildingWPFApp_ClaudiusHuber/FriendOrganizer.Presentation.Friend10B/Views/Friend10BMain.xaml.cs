using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend10B.ViewModels;

namespace FriendOrganizer.Presentation.Friend10B.Views
{
    public partial class Friend10BMain : UserControl
    {
        readonly Friend10BMainViewModel _viewModel;

        public Friend10BMain(Friend10BMainViewModel viewModel)
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
