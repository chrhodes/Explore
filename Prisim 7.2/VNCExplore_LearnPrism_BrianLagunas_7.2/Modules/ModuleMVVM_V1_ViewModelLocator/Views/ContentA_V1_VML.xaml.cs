using System.Windows.Controls;
using VNC;

namespace ModuleMVVM_V1_VML
{
    public partial class ContentA_V1_VML : UserControl
    {
        // View 1st approach.  
        // ViewModel "Located" by View based on naming convention.

        public ContentA_V1_VML()
        {
            long startTicks = Log.Trace("Enter/Exit", Common.LOG_APPNAME, 0);
            InitializeComponent();
        }
    }
}
