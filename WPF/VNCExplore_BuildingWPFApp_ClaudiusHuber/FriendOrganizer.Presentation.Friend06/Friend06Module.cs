using FriendOrganizer.DomainServices;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend06
{
    public class Friend06Module : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _containerRegistry;

        // 01

        public Friend06Module(IUnityContainer container, IRegionManager regionManager)
        {
            _containerRegistry = container;
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _containerRegistry.RegisterType<ViewModels.IFriend06ViewModel, ViewModels.Friend06ViewModel>();
            _containerRegistry.RegisterType<Views.IFriend06, Views.Friend06>();

            _containerRegistry.RegisterType<IFriendLookupDataService06, FriendLookupDataService06>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Region06, typeof(Views.Friend06));
        }
    }
}