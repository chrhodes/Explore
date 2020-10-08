using FriendOrganizer.DomainServices;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend08
{
    public class Friend08Module : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        // 01

        public Friend08Module(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO(crhodes)
            // Should we be registering stuff here and not in App.Xaml.cs
            _container.RegisterType<ViewModels.IFriend08DetailViewModel, ViewModels.Friend08DetailViewModel>();
            _container.RegisterType<Views.IFriend08Detail, Views.Friend08Detail>();

            _container.RegisterType<IFriendDataService08, FriendDataService08>();

            _container.RegisterType<ViewModels.IFriend08ViewModel, ViewModels.Friend08ViewModel>();
            _container.RegisterType<Views.IFriend08, Views.Friend08>();

            _container.RegisterType<IFriendLookupDataService06, FriendLookupDataService06>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Region08, typeof(Views.Friend08));
            _regionManager.RegisterViewWithRegion(RegionNames.Region08Detail, typeof(Views.Friend08Detail));
        }
    }
}