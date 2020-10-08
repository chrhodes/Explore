using System.Windows.Controls;

using ModuleInterfaces;
using VNC;
using VNC.Core.Mvvm;

namespace ModuleCommunicationPeopleSharedService
{
    public partial class Person : UserControl, IPerson
    {
        public Person()
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            InitializeComponent();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
