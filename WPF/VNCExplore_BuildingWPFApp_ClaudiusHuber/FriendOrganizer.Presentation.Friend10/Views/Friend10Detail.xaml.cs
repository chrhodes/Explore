using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10.Views
{
    public partial class Friend10Detail : UserControl, IFriend10Detail
    {
        public static int _instanceCountDV = 0;

        public Friend10Detail(ViewModels.IFriend10DetailViewModel viewModel)
        {
            _instanceCountDV++;
            InitializeComponent();
            ViewModel = viewModel;

            Friend10Module.friend10Detail = this;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public int InstanceCountDV
        {
            get { return _instanceCountDV; }
            set { _instanceCountDV = value; }
        }
    }
}
