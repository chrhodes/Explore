using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend09.Views
{
    public partial class Friend09Detail : UserControl, IFriend09Detail
    {
        private static int instanceCountDV = 0;

        public Friend09Detail(ViewModels.IFriend09DetailViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            instanceCountDV++;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
