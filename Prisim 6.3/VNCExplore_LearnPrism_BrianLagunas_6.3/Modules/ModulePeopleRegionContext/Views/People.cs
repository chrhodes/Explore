using System.Windows.Controls;

using ModuleInterfaces;
using VNC.Core.Mvvm;

namespace ModulePeopleRegionContext
{
    /// <summary>
    /// Interaction logic for PeopleView.xaml
    /// </summary>
    public partial class People : UserControl, IPeople
    {
        public People()
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
