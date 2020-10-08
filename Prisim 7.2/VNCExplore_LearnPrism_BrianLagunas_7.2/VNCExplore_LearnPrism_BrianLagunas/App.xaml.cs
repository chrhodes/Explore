using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using ModuleA;
using ModuleCommunicationPeopleSharedService;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using VNC.Core.Mvvm.Prism;


namespace VNCExplore_LearnPrism_BrianLagunas
{
    public partial class App : PrismApplication
    {
        #region // 00 - On Startup

        // This returns after App.Initialize

        protected override void OnStartup(StartupEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter (00)", Common.LOG_APPNAME, 0);

            base.OnStartup(e);

            VNC.Log.Trace("Exit (00)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 01 - Configure ViewModel Locator

        protected override void ConfigureViewModelLocator()
        {
            long startTicks = VNC.Log.Trace("Enter (01)", Common.LOG_APPNAME, 0);

            base.ConfigureViewModelLocator();

            VNC.Log.Trace("Exit (01)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 02 - Initialize

        // This returns after App.InitializeModules

        public override void Initialize()
        {
            long startTicks = VNC.Log.Trace("Enter (02)", Common.LOG_APPNAME, 0);

            base.Initialize();

            VNC.Log.Trace("Exit (02)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 03 - Create Container Extension

        protected override IContainerExtension CreateContainerExtension()
        {
            long startTicks = VNC.Log.Trace("Enter (03)", Common.LOG_APPNAME, 0);

            VNC.Log.Trace("Exit (03)", Common.LOG_APPNAME, 0, startTicks);

            return base.CreateContainerExtension();
        }

        #endregion

        #region // 04 - Create Module Catalog

        //  Default implementation creates empty ModuleCatalog
        // This is called when you do not directly reference the Assembly
        // containing the module.

        // TODO(crhodes)
        // Figure out how you can do more than one of these creates at a time
        // e.g. App.Config and look in Directory

        // To load modules from directory
        //
        // NB. ModuleB.dll and ModuleD.dll have not been referenced
        // but appears in .\bin\Modules folder

        // This works

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    var directoryModuleCatalog = new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //    directoryModuleCatalog.Load();

        //    var configurationModuleCatalog = new ConfigurationModuleCatalog();
        //    configurationModuleCatalog.Load();

        //    var xamlModuleCatalog = ModuleCatalog.CreateFromXaml(
        //        new Uri("/VNCExplore_LearnPrism_BrianLagunas;component/XamlCatalog.xaml", UriKind.Relative));

        //    return new ModuleCatalog(
        //        directoryModuleCatalog.Modules.OfType<ModuleInfo>()
        //        .Concat(configurationModuleCatalog.Modules.OfType<ModuleInfo>())
        //        .Concat(xamlModuleCatalog.Modules.OfType<ModuleInfo>())
        //        );
        //}

        // This works.  Files in folder and files dropped in after application starts will be loaded.

        // 04 - Create Module Catalog

        protected override IModuleCatalog CreateModuleCatalog()
        {
            long startTicks = VNC.Log.Trace("Enter (04)", Common.LOG_APPNAME, 0);

            var dynamicDirectoryModuleCatalog = new DynamicDirectoryModuleCatalog(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModulesDynamic"));

            VNC.Log.Trace("Exit (04)", Common.LOG_APPNAME, 0, startTicks);

            return dynamicDirectoryModuleCatalog;
        }

        // This does not.  It works to load assemblies from folder like DirectoryModuleCatalog
        // but will not loaded new assemblies dropped into folder.

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    var dynamicDirectoryModuleCatalog = new DynamicDirectoryModuleCatalog(
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModulesDynamic"));
        //    dynamicDirectoryModuleCatalog.Load();

        //    var configurationModuleCatalog = new ConfigurationModuleCatalog();
        //    configurationModuleCatalog.Load();

        //    var xamlModuleCatalog = ModuleCatalog.CreateFromXaml(
        //        new Uri("/VNCExplore_LearnPrism_BrianLagunas;component/XamlCatalog.xaml", UriKind.Relative));
        //    xamlModuleCatalog.Load();

        //    return new ModuleCatalog(
        //        dynamicDirectoryModuleCatalog.Modules.OfType<ModuleInfo>()
        //        .Concat(configurationModuleCatalog.Modules.OfType<ModuleInfo>())
        //        .Concat(xamlModuleCatalog.Modules.OfType<ModuleInfo>())
        //        );
        //}

        // To load modules from Xaml
        //
        // NB. ModuleC.dll has not been referenced
        // but appear in .\bin folder

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return Prism.Modularity.ModuleCatalog.CreateFromXaml(
        //        new Uri("/VNCExploreConsole;component/XamlCatalog.xaml", UriKind.Relative));
        //}

        // To load from an App.Config file
        //
        // NB. ModuleD.dll has not been referenced
        // but appears in .\bin\Modules folder

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new ConfigurationModuleCatalog();
        //}

        #endregion

        #region // 05 - Register Required Types

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = VNC.Log.Trace("Enter (05)", Common.LOG_APPNAME, 0);

            base.RegisterRequiredTypes(containerRegistry);

            VNC.Log.Trace("Exit (05)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 06 - Register Types

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = VNC.Log.Trace("Enter (06)", Common.LOG_APPNAME, 0);

            // base is Abstract.

            VNC.Log.Trace("Exit (06)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 07 - Configure Service Locator

        protected override void ConfigureServiceLocator()
        {
            long startTicks = VNC.Log.Trace("Enter (07)", Common.LOG_APPNAME, 0);

            base.ConfigureServiceLocator();

            VNC.Log.Trace("Exit (07)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 08 - Configure Module Catalog

        // Use this to load modules in code.  Requires reference to assembly

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            long startTicks = VNC.Log.Trace("Enter (08)", Common.LOG_APPNAME, 0);

            // NOTE(crhodes)
            // Loading here doesn't work.
            //((ModuleCatalog)moduleCatalog).Load();

            Type moduleAType = typeof(ModuleAModule);

            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleAType.Name,
                ModuleType = moduleAType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
                // InitializationMode = InitializationMode.OnDemand
            });

            //var moduleCatalog = (ModuleCatalog)ModuleCatalog;

            //moduleCatalog.AddModule(typeof(ModuleAModule));

            // View Examples

            // Now loading from ModulesDynamic
            //moduleCatalog.AddModule(typeof(ModuleSimpleViewModule));

            // Now loading from ModulesDynamic
            //moduleCatalog.AddModule(typeof(ModuleMVVM_V1Module));

            //moduleCatalog.AddModule(typeof(ModuleMVVM_VM1Module));

            //moduleCatalog.AddModule(typeof(ModuleMVVM_V1_ViewInjectionModule));

            //moduleCatalog.AddModule(typeof(ModuleMVVM_VM1_ViewInjectionModule));

            // Commanding Examples

            // By default Prism uses just the class name to distinguish modules
            // Even though these are from different namespaces they collide

            //AddModuleToCatalog(typeof(ModuleStatusBar.ModuleStatusBarModule), moduleCatalog);
            //AddModuleToCatalog(typeof(StatusBarEA.StatusBarModule), moduleCatalog);

            //moduleCatalog.AddModule(typeof(ModulePeopleModule));
            //moduleCatalog.AddModule(typeof(ModulePeopleDelegateCommandModule));
            //moduleCatalog.AddModule(typeof(ModulePeopleCompositeCommandModule));

            //AddModuleToCatalog(typeof(ModulePeopleDC.ModulePeopleModule), moduleCatalog);
            //AddModuleToCatalog(typeof(ModulePeopleCC.ModulePeopleModule), moduleCatalog);

            //moduleCatalog.AddModule(typeof(ModuleStatusBarModule));
            //moduleCatalog.AddModule(typeof(ModuleToolBarModule));

            // EventAggregation Examples

            //moduleCatalog.AddModule(typeof(ModulePeopleEventAggregationModule));
            //moduleCatalog.AddModule(typeof(PrismDemo.Services.ServicesModule));

            moduleCatalog.AddModule(typeof(PrismDemo.DomainServices.DomainServicesModule));

            // SharedService Examples

            moduleCatalog.AddModule(typeof(ModuleCommunicationPeopleSharedServiceModule));

            // RegionContext Examples

            //moduleCatalog.AddModule(typeof(ModulePeopleRegionContextModule));

            // Navigation Examples

            // State-Based Navigation

            //moduleCatalog.AddModule(typeof(PrismDemo.Services.PersonService.PersonServiceModule));
            //moduleCatalog.AddModule(typeof(ModuleStateBasedNavigationModule));

            // View-Based Navigation

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationABasicRegionNavigationModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBBasicRegionNavigationModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationANavigationParticipationModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBNavigationParticipationModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAPassingParametersModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBPassingParametersModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAExistingViewsModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBExistingViewsModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAConfirmCancelModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBConfirmCancelModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationANavigationJournalModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBNavigationJournalModule));

            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAModule));
            //moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBModule));

            // Shells

            //moduleCatalog.AddModule(typeof(ModuleShellsDuplicateRegionsExceptionModule));
            //moduleCatalog.AddModule(typeof(ModuleShellsScopedRegionsModule));
            //moduleCatalog.AddModule(typeof(ModuleShellsDialogServiceModule));
            //moduleCatalog.AddModule(typeof(ModuleShellsViewCompositionModule));

            base.ConfigureModuleCatalog(moduleCatalog);

            VNC.Log.Trace("Exit (08)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 09 - Configure Region Adapter Mappings

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            long startTicks = VNC.Log.Trace("Enter (09)", Common.LOG_APPNAME, 0);

            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());

            VNC.Log.Trace("Exit (09)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 10 - Configure Default Region Behaviors

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            long startTicks = VNC.Log.Trace("Enter (10)", Common.LOG_APPNAME, 0);

            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            VNC.Log.Trace("Exit (10)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 11 - Register FrameworkException Types

        protected override void RegisterFrameworkExceptionTypes()
        {
            long startTicks = VNC.Log.Trace("Enter (11)", Common.LOG_APPNAME, 0);

            base.RegisterFrameworkExceptionTypes();

            VNC.Log.Trace("Exit (11)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 12 - Create Shell


        protected override Window CreateShell()
        {
            long startTicks = VNC.Log.Trace("Enter (12)", Common.LOG_APPNAME, 0);
            
            VNC.Log.Trace("Exit (12)", Common.LOG_APPNAME, 0, startTicks);

            return Container.Resolve<Views.MainWindow>();
        }

        #endregion

        #region // 13 - Initialize Shell

        protected override void InitializeShell(Window shell)
        {
            long startTicks = VNC.Log.Trace("Enter (13)", Common.LOG_APPNAME, 0);

            base.InitializeShell(shell);

            VNC.Log.Trace("Exit (13)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 14 - Initialize Modules

        protected override void InitializeModules()
        {
            long startTicks = VNC.Log.Trace("Enter (14)", Common.LOG_APPNAME, 0);

            // Don't let exceptions crash program.

            try
            {
                base.InitializeModules();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            VNC.Log.Trace("Exit (14)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 15 - On Initialized

        // NOTE(crhodes)
        // Module Constructors, RegisterTypes(), OnIntialized() called

        protected override void OnInitialized()
        {
            long startTicks = VNC.Log.Trace("Enter (15)", Common.LOG_APPNAME, 0);

            base.OnInitialized();

            VNC.Log.Trace("Exit (15)", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // 16 - On Activated

        protected override void OnActivated(EventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnActivated(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // On Deactivated - 00

        protected override void OnDeactivated(EventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnDeactivated(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // Exit - 01

        protected override void OnExit(ExitEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnExit(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region
        
        protected override void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnFragmentNavigation(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Load Completed

        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnLoadCompleted(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Navigated

        protected override void OnNavigated(NavigationEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnNavigated(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Navigating

        protected override void OnNavigating(NavigatingCancelEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnNavigating(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Navigation Failed

        protected override void OnNavigationFailed(NavigationFailedEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnNavigationFailed(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Navigation Progress

        protected override void OnNavigationProgress(NavigationProgressEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnNavigationProgress(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region On Navigation Stopped

        protected override void OnNavigationStopped(NavigationEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnNavigationStopped(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        #region // On Session Ending

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            base.OnSessionEnding(e);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        #endregion

        // TODO(crhodes)
        // Should this be in VNC.Core

        void AddModuleToCatalog(Type moduleType, ModuleCatalog catalog, InitializationMode initializationMode = InitializationMode.OnDemand)
        {
            long startTicks = VNC.Log.Trace("Enter", Common.LOG_APPNAME, 0);

            ModuleInfo moduleInfo = new ModuleInfo();

            // Use the fully qualified name to distinguish the ModuleName
            moduleInfo.ModuleName = moduleType.Name;
            //moduleInfo.ModuleName = moduleType.AssemblyQualifiedName;
            moduleInfo.ModuleType = moduleType.AssemblyQualifiedName;
            moduleInfo.InitializationMode = initializationMode;

            catalog.AddModule(moduleInfo);

            VNC.Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}
