using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

namespace ModuleCommunicationPeopleDelegateCommand
{
    public class ModuleCommunicationPeopleDelegateCommandModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            containerRegistry.Register<IPersonViewModel, PersonViewModel>();
            containerRegistry.Register<IPerson, Person>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            // DelegateCommand Example
            // This is for single content not tab view

            var vm = containerProvider.Resolve<IPersonViewModel>();
            regionManager.Regions[RegionNames.ContentRegionC_DC].Add(vm.View);

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}