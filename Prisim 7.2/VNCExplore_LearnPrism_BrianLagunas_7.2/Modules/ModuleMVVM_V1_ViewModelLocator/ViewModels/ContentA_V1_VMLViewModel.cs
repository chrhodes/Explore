using ModuleInterfaces;
using VNC;
using VNC.Core.Mvvm;

namespace ModuleMVVM_V1_VML
{
    public class ContentA_V1_VMLViewModel : IContentAViewModel
    {
        // View 1st approach.  
        // ViewModel is not passed a View in constructor
        // ViewModel "Located" by View based on naming convention.

        public ContentA_V1_VMLViewModel()
        {
            long startTicks = Log.Trace("Enter/Exit", Common.LOG_APPNAME, 0);
        }

        private string _Message = "Message ContentA View 1st";

        public string Message
        {
            get { return _Message; }
            set { _Message = value; } 
            // Should implement INotifyPropertyChanged and set value
        }

        // This needs to be here just for the IContentAViewModel.  Not used.
        public IView View
        {
            get;
            set;
        }
    }
}
