using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

namespace ModuleRibbon
{
    public class ModuleRibbonModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);


            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            //regionManager.RegisterTypeForNavigation<ViewA>();
            //regionManager.RegisterTypeForNavigation<ViewB>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
