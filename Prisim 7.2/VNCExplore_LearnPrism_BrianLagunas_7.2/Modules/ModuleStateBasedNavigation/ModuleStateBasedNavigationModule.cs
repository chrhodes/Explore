//using Microsoft.Practices.Unity;
using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleStateBasedNavigation
{
    public class ModuleStateBasedNavigationModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _manager;

        public ModuleStateBasedNavigationModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            _container.RegisterType<IContentSBN, ContentSBN>();
            _container.RegisterType<IContentSBNViewModel, ContentSBNViewModel>();

            _manager.RegisterViewWithRegion(RegionNames.ContentRegionN_SB, typeof(ContentSBN));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
    }
}
