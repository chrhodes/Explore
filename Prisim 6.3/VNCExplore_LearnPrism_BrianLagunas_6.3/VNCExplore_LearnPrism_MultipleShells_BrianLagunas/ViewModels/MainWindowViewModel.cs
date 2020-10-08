using Prism.Commands;
using Prism.Regions;
using System;
using VNC.Core.Mvvm;
using VNC.Core.Mvvm.Prism;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace VNCExplore_LearnPrism_MultipleShells_BrianLagunas.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel, IRegionManagerAware
    {
        private string _title = "VNCExplore LearnPrism Multiple Shells - BrianLagunas";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
            //set { SetProperty(ref _title, value); }
            //set { _title = value;  }
        }

        private readonly IShellService _shellService;

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenShellCommand { get; set; }

        public IRegionManager RegionManager
        {
            get;
            set;
        }

        // This asks for the Global Region Manager which is for the first shell.

        public MainWindowViewModel()
        {
            Title = "MWVM()" + DateTime.Now.ToString();
        }

        public MainWindowViewModel(IShellService shellService)
        {
            Title = "MWVM(ss)" + DateTime.Now.ToString();

            _shellService = shellService;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            OpenShellCommand = new DelegateCommand<string>(OpenShell);

            // Register Delegate command with Composite command
            VNCExplore_LearnPrism_BrianLagunas.Infrastructure.ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
            VNCExplore_LearnPrism_BrianLagunas.Infrastructure.ApplicationCommands.OpenShellCommand.RegisterCommand(OpenShellCommand);
        }

        private void OpenShell(string viewName)
        {
            _shellService.ShowShell(viewName);
        }

        private void Navigate(string navigatePath)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegionS_MS, navigatePath, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            //MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
