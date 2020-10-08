using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Input;
using VNC.Core.Mvvm;
using VNC.Core.Mvvm.Prism;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace VNCExplore_LearnPrism_BrianLagunas.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel, IRegionManagerAware
    {
        private string _title = "VNCExplore LearnPrism - BrianLagunas";
        public string Title
        {
            get { return _title; }
            set { _title = value;
                OnPropertyChanged(); }
            //set { SetProperty(ref _title, value); }
            //set { _title = value;  }
        }

        string _status = "Default Status";
        public string Status
        {
            get { return _status; }
            set { _status = value;
                OnPropertyChanged();  }
            //set { SetProperty<string>(ref _status, value); }
            //set { _status = value;  }
        }

        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public ICommand NotificationCommand { get; set; }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        public ICommand ConfirmationCommand { get; set; }

        public InteractionRequest<INotification> CustomPopupRequest { get; set; }
        public ICommand CustomPopupCommand { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IShellService _shellService;

        public DelegateCommand<object> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenShellCommand { get; set; }
        public IRegionManager RegionManager
        { get;
            set; }

        // This asks for the Global Region Manager which is for the first shell.

        public MainWindowViewModel()
        {
            Title = "MWVM()" + DateTime.Now.ToString();
        }

        public MainWindowViewModel(IRegionManager regionManager, IShellService shellService)
        {
            Title = "MWVM(rm, ss)" + DateTime.Now.ToString();

            _regionManager = regionManager;
            _shellService = shellService;

            NavigateCommand = new DelegateCommand<object>(Navigate);
            OpenShellCommand = new DelegateCommand<string>(OpenShell);

            // Register Delegate command with Composite command
            Infrastructure.ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
            Infrastructure.ApplicationCommands.OpenShellCommand.RegisterCommand(OpenShellCommand);

            NotificationRequest = new InteractionRequest<INotification>();
            NotificationCommand = new DelegateCommand(RaiseNotification);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            ConfirmationCommand = new DelegateCommand(RaiseConfirmation);

            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);
        }

        void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" }, r => Status = "Notified");
        }

        void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(new Confirmation { Title = "Confirmation", Content = "Confirmation Message" }, r => Status = r.Confirmed ? "Confirmed" : "Not Confirmed");
        }

        void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = "Custom Popup", Content = "Custom Popup Message " }, r => Status = "Good to go");
        }

        private void OpenShell(string viewName)
        {
            _shellService.ShowShell(viewName);
            //_shellService.ShowShell();
        }

        private void Navigate(object navigatePath)
        {
            // This is a hack.  Maybe different Commands.
            if (navigatePath != null)
            {
                //var v1 = (string)navigatePath;
                //var v2 = (Uri)navigatePath;
                var v3 = navigatePath;
                var vString = navigatePath.ToString();
                var vtype = v3.GetType();

                // TODO(crhodes)
                // Should do a null check on RegionManager

                if (vString.Contains("BasicRegionNavigation"))
                {
                    // Change to use the property instead of the passed in through the constructor RegionManager.
                    //RegionManager.RequestNavigate(RegionNames.ContentRegionN_VB_BRN, navigatePath.ToString(), NavigationComplete);
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_BRN, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("NavigationParticipation"))
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_NP, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("PassingParameters"))
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_PP, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("ExistingViews"))
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_EV, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("ConfirmCancel"))
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_CC, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("NavigationJournal"))
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegionN_VB_NJ, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("DialogService"))
                {
                    RegionManager.RequestNavigate(RegionNames.ContentRegionS_DS, navigatePath.ToString(), NavigationComplete);
                }
                else if (vString.Contains("ViewComposition"))
                {
                    RegionManager.RequestNavigate(RegionNames.ContentRegionS_VC, navigatePath.ToString(), NavigationComplete);
                }
                else
                {
                    MessageBox.Show("Navigation Region Unhandled");
                }
            }          
        }

        private void NavigationComplete(NavigationResult result)
        {
            //MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
