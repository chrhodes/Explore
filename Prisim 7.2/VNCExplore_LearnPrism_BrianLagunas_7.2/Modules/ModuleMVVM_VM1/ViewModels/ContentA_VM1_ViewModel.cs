using VNC.Core.Mvvm;
using ModuleInterfaces;

namespace ModuleMVVM_VM1
{
    public class ContentA_VM1_ViewModel : IContentAViewModel_VM1
    {
        private string _Message = "Message ContentA ViewModel 1st";

        // ViewModel first approach.  
        // View is passed in constructor

        public ContentA_VM1_ViewModel(IContentA view)
        {
            View = view;
            // Point the view to this ViewModel
            View.ViewModel = this;
        }

        public IView View
        {
            get;
            set;
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; } 
        }
    }
}
