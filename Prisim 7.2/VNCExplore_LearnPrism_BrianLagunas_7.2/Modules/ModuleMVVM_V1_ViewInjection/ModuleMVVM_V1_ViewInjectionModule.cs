using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

namespace ModuleMVVM_V1_ViewInjection
{
    public class ModuleMVVM_V1_ViewInjectionModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

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

            // 2. Compose Application views using registered Views and View Models

            // Enable view discovery for ToolBar
            // Not clear if need to RegisterType with container, supra, if using region manager

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_V1_VI, typeof(ToolBarA));

            // Enable view discovery for content

            regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_V1_VI, typeof(ContentA_V1));

            // If you need more control of region,

            //try
            //{
            //    IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            //    // Can get list of Views, ActiveViews, Activate, Deactivate, Add, Remove, Activate, Deactivate, etc.
            //    region.Add(vm2.View);
            //}
            //catch (Exception ex)
            //{
            //    // This gets thrown because we left the first view in place. (5)
            //    MessageBox.Show(ex.ToString());
            //}

            // TODO(crhodes)
            // Show deactivating or removing view

            // TODO(crhodes)
            // Play with switch views and the model associated with view
            //try
            //{
            //    //IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            //    //// Can get list of Views, ActiveViews, Activate, Deactivate, Add, Remove, Activate, Deactivate, etc.
            //    ////region.Deactivate(vm.View); // Doing this still through exception
            //    ////region.Deactivate(vm2.View);
            //    //region.Activate(vm2.View);
            //}
            //catch (Exception ex)
            //{
            //    // This gets thrown because we left the first view in place. (5)
            //    MessageBox.Show(ex.ToString());
            //}

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
