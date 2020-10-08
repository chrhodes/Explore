using System;
using System.Windows;

using FriendOrganizer.Presentation.Friend;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using VNC;

using Common = VNCExplore.FriendOrganizer.Common;

namespace VNCExplore_FriendOrganizer
{
    public partial class App : PrismApplication
    {
        // 01

        protected override void ConfigureViewModelLocator()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.ConfigureViewModelLocator();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 02

        protected override IContainerExtension CreateContainerExtension()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return base.CreateContainerExtension();
        }

        // 03 - Create the catalog of Modules

        protected override IModuleCatalog CreateModuleCatalog()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return base.CreateModuleCatalog();
        }

        // 04

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.RegisterRequiredTypes(containerRegistry);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 05

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);
            // These are used by MainWindowDxLayout

            //containerRegistry.Register<IFriendDataService05, FriendDataService05>();
            //containerRegistry.Register<IFriendDataService04, FriendDataServiceMock04>();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 06

        protected override void ConfigureServiceLocator()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.ConfigureServiceLocator();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 07 - Configure the catalog of modules
        // Modules are loaded at Startup and must be a project reference

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            moduleCatalog.AddModule(typeof(FriendModule));

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 08

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 09

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 10

        protected override void RegisterFrameworkExceptionTypes()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.RegisterFrameworkExceptionTypes();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 11 

        protected override Window CreateShell()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return Container.Resolve<Views.MainWindowDxLayout>();
        }

        // 12

        protected override void InitializeShell(Window shell)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.InitializeShell(shell);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // 13 

        protected override void InitializeModules()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            base.InitializeModules();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occurred. Please inform the admin."
              + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
