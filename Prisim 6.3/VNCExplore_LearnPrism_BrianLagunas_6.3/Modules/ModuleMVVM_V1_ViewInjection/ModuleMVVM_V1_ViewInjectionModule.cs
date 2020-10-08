using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using ModuleInterfaces;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleMVVM_V1_ViewInjection
{
    public class ModuleMVVM_V1_ViewInjectionModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        // Need a container so we can register our views
        // and a RegionManager so we can compose views and perform View discovery.
        // Prism will pass in when this module is created.

        public ModuleMVVM_V1_ViewInjectionModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        public void Initialize()
        {
            // 1. Register Views and ViewModels

            // View first approach.  This is more common

            // View is responsible for instantiating the ViewModel
            // When container creates View, it sees a ViewModel is required in constructor.
            // Container resolves the ViewModel and passes it into the View's constructor.

            // Register a View as type IContentA.  Container will return ContentA
            // because ContentA implements IContentA
            // Do same for IContentAViewModel, ContentAViewModel

            _container.RegisterType<ToolBarA>();
            //_container.RegisterType<ContentA>();   // Don't have to use interface but recommended.
            _container.RegisterType<IContentA, ContentA_V1>();
            _container.RegisterType<IContentAViewModel, ContentA_V1_ViewModel>();

            // 2. Compose Application views using registered Views and View Models

            // Enable view discovery for ToolBar
            // Not clear if need to RegisterType with container, supra, if using region manager

            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_V1_VI, typeof(ToolBarA));

            // Enable view discovery for content

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_V1_VI, typeof(ContentA_V1));

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
        }
    }
}
