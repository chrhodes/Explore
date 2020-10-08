using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleB
{
    // Can put attributes on Modules
    //[Module(ModuleName = "ModuleB", OnDemand = true)]
    //[ModuleDependency("ModuleA")]
    public class ModuleBModule : IModule
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

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionB, typeof(ToolBarView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegionB, typeof(ContentView));

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
