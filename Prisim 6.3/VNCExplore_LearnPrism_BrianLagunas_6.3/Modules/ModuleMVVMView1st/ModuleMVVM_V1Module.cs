using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using ModuleInterfaces;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleMVVM_V1
{
    public class ModuleMVVM_V1Module : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        // Need a container so we can register our views
        // and a RegionManager so we can compose views and perform View discovery.
        // Prism will pass in when this module is created.

        public ModuleMVVM_V1Module(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        public void Initialize()
        {
            // 1. Register Views and ViewModels

            // ViewModel first approach.

            // ViewModel is responsible for instantiating the View
            // When container creates ViewModel is sees a View is required in constructor.
            // Container resolves the View and passes it into the ViewModel's constructor.

            // Register a View as type IContentA_VM1_View.  Container will return ContentA_VM1
            // because ContentA_VM1 implements IContentA_VM1_View
            // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            _container.RegisterType<ToolBarA>();
            _container.RegisterType<IContentA, ContentA_V1>();
            _container.RegisterType<IContentAViewModel, ContentA_V1_ViewModel>();

            // 2. Compose Application views using registered Views and ViewModels

            // Enable view discovery for ToolBar

            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_V1, typeof(ToolBarA));

            // Enable view discovery for content

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_V1, typeof(ContentA_V1));
        }
    }
}
