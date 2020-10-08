using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend17.Views
{
    public partial class Navigation17 : UserControl, INavigation17
    {
        private static int _instanceCountV = 0;

        public Navigation17()
        {
            _instanceCountV++;
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public int InstanceCountV
        {
            get { return _instanceCountV; }
            set { _instanceCountV = value; }
        }
    }
}
