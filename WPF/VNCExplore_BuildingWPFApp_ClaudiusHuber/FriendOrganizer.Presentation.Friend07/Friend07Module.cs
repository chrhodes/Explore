using FriendOrganizer.DomainServices;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend07
{
    public class Friend07Module : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        // 01

        public Friend07Module(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO(crhodes)
            // Should we be registering stuff here and not in App.Xaml.cs
            _container.RegisterType<ViewModels.IFriend07DetailViewModel, ViewModels.Friend07DetailViewModel>();
            _container.RegisterType<Views.IFriend07Detail, Views.Friend07Detail>();

            _container.RegisterType<IFriendDataService06, FriendDataService06>();

            _container.RegisterType<ViewModels.IFriend07ViewModel, ViewModels.Friend07ViewModel>();
            _container.RegisterType<Views.IFriend07, Views.Friend07>();

            _container.RegisterType<IFriendLookupDataService06, FriendLookupDataService06>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Region07, typeof(Views.Friend07));
            _regionManager.RegisterViewWithRegion(RegionNames.Region07Detail, typeof(Views.Friend07Detail));
        }
    }
}