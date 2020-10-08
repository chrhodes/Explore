using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend06Detail.Views
{
    public partial class Friend06Detail : UserControl, IFriend06Detail
    {
        public Friend06Detail(ViewModels.IFriend06DetailViewModel viewModel)
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
