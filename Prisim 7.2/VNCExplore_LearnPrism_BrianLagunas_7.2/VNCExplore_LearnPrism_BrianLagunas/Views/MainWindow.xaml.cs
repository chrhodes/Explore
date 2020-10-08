using System.Windows;

namespace VNCExplore_LearnPrism_BrianLagunas.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            InitializeComponent();

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
