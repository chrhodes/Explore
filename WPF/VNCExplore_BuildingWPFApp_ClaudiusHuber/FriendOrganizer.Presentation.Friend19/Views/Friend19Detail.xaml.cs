using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.Views
{
    public partial class Friend19Detail : UserControl, IFriend19Detail
    {
        public static int _instanceCountDV = 0;

        public Friend19Detail()
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
