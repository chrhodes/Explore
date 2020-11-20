using System;
using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;
using VNC;
using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend
{
    public class FriendModule : IModule
    {
        private readonly IRegionManager _regionManager;
        //public static IContainerProvider _containerProvider;

        // 01

        public FriendModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            containerRegistry.Register<ViewModels.IFriendDetailViewModel, ViewModels.FriendDetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriendDetail, Views.FriendDetail>();

            containerRegistry.Register<ViewModels.IMeetingDetailViewModel, ViewModels.MeetingDetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeetingDetail, Views.MeetingDetail>();

            containerRegistry.Register<ViewModels.IProgrammingLanguageDetailViewModel, ViewModels.ProgrammingLanguageDetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IProgrammingLanguageDetail, Views.ProgrammingLanguageDetail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigationViewModel, ViewModels.NavigationViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation, Views.Navigation>();

            containerRegistry.Register<Views.FriendMain>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService, LookupDataService>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService, LookupDataService>();
            containerRegistry.Register<IMeetingLookupDataService, LookupDataService>();

            containerRegistry.Register<IFriendRepository, FriendRepository>();
            containerRegistry.Register<IMeetingRepository, MeetingRepository>();
            containerRegistry.Register<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            //_containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.FriendMain));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.RegionNavigation, typeof(Views.Navigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region, typeof(Views.Friend));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail, typeof(Views.FriendDetail));

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }
    }
}