using System;

using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;

using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;
using System.Linq;
using VNC.Core.Mvvm;

namespace ModuleMVVM_VM1
{
    public class ModuleMVVM_VM1Module : IModule
    {
        // 01 Register Views and ViewModels
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            //    // ViewModel first approach.

            //    // ViewModel is responsible for instantiating the View
            //    // When container creates ViewModel is sees a View is required in constructor.
            //    // Container resolves the View and passes it into the ViewModel's constructor.

            //    // Register a View as type IContentA_VM1_View.  Container will return ContentA_VM1
            //    // because ContentA_VM1 implements IContentA_VM1_View
            //    // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            containerRegistry.Register<ToolBarA>();
            containerRegistry.Register<IContentA, ContentA_VM1>();
            containerRegistry.Register<IContentAViewModel_VM1, ContentA_VM1_ViewModel>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            // 2. Compose Application using registered Views and ViewModels

            // Enable view discovery for ToolBar

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_VM1, typeof(ToolBarA));

            // Enable view discovery for content

            // This creates the View directly without resolving the ViewModel
            // Problem is new View does not know about ViewModel.
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_VM1, typeof(ContentA_VM1));
            // Can get the list of views.
            var views = regionManager.Regions[RegionNames.ContentRegionV_VM1].Views;
            IView view = (IView)views.First();
            // Create a ViewModel
            var vm = containerProvider.Resolve<IContentAViewModel_VM1>();
            var views2 = regionManager.Regions[RegionNames.ContentRegionV_VM1].Views;
            vm.Message = $"Create the View First views.Count:{views.Count()} and hook it up to the ViewModel.  views2.Count:{views2.Count()}";
            // And connect the View to the ViewModel
            ((IContentA)view).ViewModel = vm;
            // And tell the ViewModel which View it is associated with
            //vm.View = view;


            //// But if we resolve the ViewModel first, it will create the view (Constructor needs)
            //var vm = containerProvider.Resolve<IContentAViewModel_VM1>();
            //var views3 = regionManager.Regions[RegionNames.ContentRegionV_VM1].Views;
            //vm.Message = $"Create the ViewModel First views3.Count:{views3.Count()}";


            ////Now inject (Regions[].Add() the view the ViewModel created. (17)
            //regionManager.Regions[RegionNames.ContentRegionV_VM1].Add(vm.View);

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
