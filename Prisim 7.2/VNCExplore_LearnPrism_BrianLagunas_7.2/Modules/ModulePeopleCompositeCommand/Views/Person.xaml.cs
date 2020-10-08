using System.Windows.Controls;

using ModuleInterfaces;

using VNC.Core.Mvvm;

namespace ModuleCommunicationPeopleCompositeCommand
{
    public partial class Person : UserControl, IPerson
    {
        public Person()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
