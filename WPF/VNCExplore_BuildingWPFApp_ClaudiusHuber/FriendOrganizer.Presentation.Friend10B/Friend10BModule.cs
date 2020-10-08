using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend10B
{
    public class Friend10BModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        //public static Friend10BDetail friend10Detail;

        // 01

        public Friend10BModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend10BDetailViewModel, ViewModels.Friend10BDetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10BDetail, Views.Friend10BDetail>();

            containerRegistry.Register<IFriendRepository10, FriendRepository10>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend10BViewModel, ViewModels.Friend10BViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend10B, Views.Friend10B>();

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService10>();

            containerRegistry.Register<Views.Friend10BMain>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            //friend10Detail = (Friend10BDetail)Friend10BModule._containerProvider.Resolve(typeof(IFriend10BDetail));

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.Region10BCombined, typeof(Views.Friend10BMain));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region10B, typeof(Views.Friend10B));
            _regionManager.RegisterViewWithRegion(RegionNames.Region10BDetail, typeof(Views.Friend10BDetail));
        }
    }
}