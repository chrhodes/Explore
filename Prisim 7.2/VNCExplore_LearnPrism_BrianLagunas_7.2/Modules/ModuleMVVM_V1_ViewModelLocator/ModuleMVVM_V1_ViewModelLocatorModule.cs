using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

namespace ModuleMVVM_V1_VML
{
    public class ModuleMVVM_V1_ViewModelLocatorModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            // 1. Register Views

            // View first approach.

            containerRegistry.Register<ToolBarA>();
            containerRegistry.Register<ContentA_V1_VML>();
            //containerRegistry.Register<IContentAViewModel, ContentA_V1_VML_ViewModel>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            // 2. Compose Application views using registered Views and ViewModels

            // Enable view discovery for ToolBar

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_V1_VML, typeof(ToolBarA));

            // Enable view discovery for content

            regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_V1_VML, typeof(ContentA_V1_VML));

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
