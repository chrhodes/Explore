using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleToolBar
{
    public class ModuleToolBarModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            containerRegistry.Register<IToolBarViewModel, ToolBarViewModel>();
            containerRegistry.Register<IToolBar, ToolBar>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            var vm = containerProvider.Resolve<IToolBarViewModel>();

            var vmCC = containerProvider.Resolve<IToolBarViewModel>();
            var vmEA = containerProvider.Resolve<IToolBarViewModel>();
            var vmSS = containerProvider.Resolve<IToolBarViewModel>();

            regionManager.Regions[RegionNames.ToolBarRegionC_CC].Add(vmCC.View);
            regionManager.Regions[RegionNames.ToolBarRegionC_EA].Add(vmEA.View);
            regionManager.Regions[RegionNames.ToolBarRegionC_SS].Add(vmSS.View);

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
