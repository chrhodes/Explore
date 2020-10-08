using System.Windows.Controls;
using VNC.Core.Mvvm;
using ModuleInterfaces;

namespace ModuleMVVM_VM1_ViewInjection
{
    public partial class ContentA_VM1 : UserControl, IContentA
    {
        public ContentA_VM1()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return (IContentAViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
