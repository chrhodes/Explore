using System.Windows.Controls;
using VNC;

namespace ModuleMVVM_V1_VML
{
    public partial class ToolBarA : UserControl
    {
        public ToolBarA()
        {
            long startTicks = Log.Trace("Enter/Exit", Common.LOG_APPNAME, 0);
            InitializeComponent();
        }
    }
}
