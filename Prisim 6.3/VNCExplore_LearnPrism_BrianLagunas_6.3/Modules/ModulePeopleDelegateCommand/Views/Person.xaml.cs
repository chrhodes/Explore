using ModuleInterfaces;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModulePeopleDelegateCommand
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

        //IViewModel IView.ViewModel { get; set; }
    }
}
