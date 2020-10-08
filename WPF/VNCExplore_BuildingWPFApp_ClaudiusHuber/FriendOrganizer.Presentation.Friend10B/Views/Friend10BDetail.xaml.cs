using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10B.Views
{
    public partial class Friend10BDetail : UserControl, IFriend10BDetail
    {
        public static int _instanceCountDV = 0;

        public Friend10BDetail()
        {
            _instanceCountDV++;
            InitializeComponent();
            //ViewModel = viewModel;

            //Friend10BModule.friend10Detail = this;
        }
        //public Friend10BDetail(ViewModels.IFriend10BDetailViewModel viewModel)
        //{
        //    _instanceCountDV++;
        //    InitializeComponent();
        //    ViewModel = viewModel;

        //    //Friend10BModule.friend10Detail = this;
        //}

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
