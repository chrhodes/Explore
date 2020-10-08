using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend07.Views
{
    public partial class Friend07Detail : UserControl, IFriend07Detail
    {
        public Friend07Detail(ViewModels.IFriend07DetailViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
