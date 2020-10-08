using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

namespace ModuleA1
{
    public class ModuleA1Module : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);


            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        //02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegionA1", typeof(ModuleA1View));

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
