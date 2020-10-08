using ModuleInterfaces;
using VNC.Core.Mvvm;

namespace ModuleMVVM_V1_ViewInjection
{
    public class ContentA_V1_ViewModel : IContentAViewModel
    {
        // View 1st approach.  
        // ViewModel is not passed a View in constructor
        public ContentA_V1_ViewModel()
        {

        }

        private string _Message = "Message ContentA View 1st ViewInjection";

        public string Message
        {
            get { return _Message; }
            set { _Message = value; } // Should implement INotifyPropertyChanged and set value
        }

        public IView View
        {
            get;
            set;
        }
    }
}
