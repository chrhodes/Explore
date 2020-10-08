using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;
using FriendOrganizer.Presentation.Friend10.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend10
{
    public class Friend10Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        public static Friend10Detail friend10Detail;

        // 01

        public Friend10Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend10DetailViewModel, ViewModels.Friend10DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10Detail, Views.Friend10Detail>();

            containerRegistry.Register<IFriendRepository10, FriendRepository10>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend10ViewModel, ViewModels.Friend10ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10, Views.Friend10>();

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService10>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            //friend10Detail = (Friend10Detail)Friend10Module._containerProvider.Resolve(typeof(IFriend10Detail));

            _regionManager.RegisterViewWithRegion(RegionNames.Region10, typeof(Views.Friend10));
            _regionManager.RegisterViewWithRegion(RegionNames.Region10Detail, typeof(Views.Friend10Detail));
        }
    }
}