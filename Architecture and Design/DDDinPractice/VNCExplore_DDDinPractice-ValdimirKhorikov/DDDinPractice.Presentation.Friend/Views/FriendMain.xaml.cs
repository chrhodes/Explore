using System.Windows;
using System.Windows.Controls;

using FriendOrganizer.Presentation.Friend.ViewModels;

namespace FriendOrganizer.Presentation.Friend.Views
{
    public partial class FriendMain : UserControl
    {
        private readonly FriendMainViewModel _viewModel;

        public FriendMain(FriendMainViewModel viewModel)
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
