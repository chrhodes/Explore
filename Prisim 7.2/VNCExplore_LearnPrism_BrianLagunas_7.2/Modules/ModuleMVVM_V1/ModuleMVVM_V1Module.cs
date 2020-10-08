using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

namespace ModuleMVVM_V1
{
    public class ModuleMVVM_V1Module : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            // 1. Register Views and ViewModels

            // View first approach.

            // View is responsible for instantiating the ViewModel
            // When container creates View is sees a ViewModel is required in constructor.
            // Container resolves the ViewModel and passes it into the View's constructor.

            // Register a View as type IContentA View.  Container will return ContentA_V1
            // because ContentA_V1 implements IContentA
            // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            containerRegistry.Register<ToolBarA>();
            containerRegistry.Register<IContentA, ContentA_V1>();
            containerRegistry.Register<IContentAViewModel, ContentA_V1_ViewModel>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            // 2. Compose Application using registered Views

            // Enable view discovery for ToolBar

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_V1, typeof(ToolBarA));

            // Enable view discovery for Content

            regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_V1, typeof(ContentA_V1));

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
