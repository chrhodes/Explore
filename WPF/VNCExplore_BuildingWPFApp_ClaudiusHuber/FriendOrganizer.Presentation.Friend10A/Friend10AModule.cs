using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend10A
{
    public class Friend10AModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        //public static Friend10ADetail friend10Detail;

        // 01

        public Friend10AModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend10ADetailViewModel, ViewModels.Friend10ADetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10ADetail, Views.Friend10ADetail>();

            containerRegistry.Register<IFriendRepository10, FriendRepository10>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend10AViewModel, ViewModels.Friend10AViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10A, Views.Friend10A>();

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService10>();

            containerRegistry.Register<Views.Friend10AMain>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            //friend10Detail = (Friend10ADetail)Friend10AModule._containerProvider.Resolve(typeof(IFriend10ADetail));

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.Region10ACombined, typeof(Views.Friend10AMain));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region10A, typeof(Views.Friend10A));
            _regionManager.RegisterViewWithRegion(RegionNames.Region10ADetail, typeof(Views.Friend10ADetail));
        }
    }
}