using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend17.Views
{
    public partial class Friend17Detail : UserControl, IFriend17Detail
    {
        public static int _instanceCountDV = 0;

        public Friend17Detail()
        {
            _instanceCountDV++;
            InitializeComponent();
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
