using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleStatusBar
{
    public class ModuleStatusBarModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            containerRegistry.Register<IStatusBarViewModel, StatusBarViewModel>();
            containerRegistry.Register<IStatusBar, StatusBar>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            var vmDC = containerProvider.Resolve<IStatusBarViewModel>();
            var vmCC = containerProvider.Resolve<IStatusBarViewModel>();
            var vmEA = containerProvider.Resolve<IStatusBarViewModel>();
            var vmSS = containerProvider.Resolve<IStatusBarViewModel>();
            var vmRC = containerProvider.Resolve<IStatusBarViewModel>();

            regionManager.Regions[RegionNames.StatusBarRegionC_DC].Add(vmDC.View);
            regionManager.Regions[RegionNames.StatusBarRegionC_CC].Add(vmCC.View);
            regionManager.Regions[RegionNames.StatusBarRegionC_EA].Add(vmEA.View);
            regionManager.Regions[RegionNames.StatusBarRegionC_SS].Add(vmSS.View);
            regionManager.Regions[RegionNames.StatusBarRegionC_RC].Add(vmRC.View);

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
