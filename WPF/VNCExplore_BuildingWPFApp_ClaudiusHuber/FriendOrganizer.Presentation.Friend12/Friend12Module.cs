using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend12
{
    public class Friend12Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        //public static Friend12Detail friend10Detail;

        // 01

        public Friend12Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend12DetailViewModel, ViewModels.Friend12DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend12Detail, Views.Friend12Detail>();

            containerRegistry.Register<IFriendRepository12, FriendRepository12>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend12ViewModel, ViewModels.Friend12ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend12, Views.Friend12>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService12>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService12>();

            containerRegistry.Register<Views.Friend12Main>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            //friend10Detail = (Friend12Detail)Friend12Module._containerProvider.Resolve(typeof(IFriend12Detail));

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion12, typeof(Views.Friend12Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region12, typeof(Views.Friend12));
            _regionManager.RegisterViewWithRegion(RegionNames.RegionDetail12, typeof(Views.Friend12Detail));
        }
    }
}