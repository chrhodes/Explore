using FriendOrganizer.DomainServices;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend06Detail
{
    public class Friend06DetailModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _containerRegistry;

        // 01

        public Friend06DetailModule(IUnityContainer container, IRegionManager regionManager)
        {
            _containerRegistry = container;
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO(crhodes)
            // Should we be registering stuff here and not in App.Xaml.cs
            _containerRegistry.RegisterType<ViewModels.IFriend06DetailViewModel, ViewModels.Friend06DetailViewModel>();
            _containerRegistry.RegisterType<Views.IFriend06Detail, Views.Friend06Detail>();

            _containerRegistry.RegisterType<IFriendDataService06, FriendDataService06>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Region06Detail, typeof(Views.Friend06Detail));
        }
    }
}