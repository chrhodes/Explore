using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleM
{
    public class ModuleMModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            // 1. Register Views and ViewModels

            // These are used when no MVVM pattern with Interfaces is used

            //_container.RegisterType<ToolBarA>();  // If this is commented out, still works!

            // These are MVVM approaches

            // View first approach.  This is more common

            // View is responsible for instantiating the ViewModel
            // When container creates View, it sees a ViewModel is required in constructor.
            // Container resolves the ViewModel and passes it into the View's constructor.

            // Register a View as type IContentA_V1_View.  Container will return ContentA_V1
            // because ContentA_V1 implements IContentA_V1_View
            // Do same for IContentA_V1_ViewViewModel, ContentA_V1_ViewViewModel

            //_container.RegisterType<ContentA_V1>();   // Don't have to use interface but recommended.
            //_container.RegisterType<IContentA_V1_ViewViewModel, ContentA_V1_ViewViewModel>();

            // ViewModel first approach.

            // ViewModel is responsible for instantiating the view
            // When container creates ViewModel is sees a View is required in constructor.
            // Container resolves the View and passes it into the ViewModel's constructor.

            // Register a View as type IContentA_VM1_View.  Container will return ContentA_VM1
            // because ContentA_VM1 implements IContentA_VM1_View
            // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            //_container.RegisterType<ContentA_VM1>();
            containerRegistry.Register<IContentA, ContentA_VM1>();

            containerRegistry.Register<IContentAViewModel, ContentA_VM1_ViewViewModel>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            // 2. Compose Application views using registered Views and View Models

            // Enable view discovery for ToolBar
            // Not clear if need to RegisterType with container, supra, if using region manager

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionM, typeof(ToolBarA));

            // Enable view discovery for content

            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA));
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA_V1));

            // Problem here is we get the View but no ViewModel

            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA_VM1));

            // Enable view Injection

            var vm = containerProvider.Resolve<IContentAViewModel>();
            //vm.Message = "Prism Rocks!";

            //Now inject in a region the view that viewmodel created. (16)
            regionManager.Regions[RegionNames.ContentRegionM].Add(vm.View);

            // If don't deactivate view the second view is covered up.

            regionManager.Regions[RegionNames.ContentRegionM].Deactivate(vm.View);

            var vm2 = containerProvider.Resolve<IContentAViewModel>();
            vm2.Message = "Prism Rocks! Second ViewModel";

            regionManager.Regions[RegionNames.ContentRegionM].Add(vm2.View);

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
