using System.Windows.Controls;
using VNC.Core.Mvvm;
using ModuleInterfaces;

namespace ModulePeopleCompositeCommand
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
