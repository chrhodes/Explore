//using System;
//using System.Windows;
//using System.Windows.Controls;

//using ModuleA;

//using ModulePeopleCompositeCommand;

//using ModulePeopleDelegateCommand;

//using ModulePeopleEventAggregation;

//using ModulePeopleRegionContext;

//using ModulePeopleSharedService;

//using ModuleShellsDialogService;

//using ModuleShellsDuplicateRegionsException;

//using ModuleShellsScopedRegions;

//using ModuleShellsViewComposition;

//using ModuleSimpleView;

//using ModuleStateBasedNavigation;

//using ModuleStatusBar;

//using ModuleToolBar;

//using ModuleViewBasedNavigationA;

//using ModuleViewBasedNavigationABasicRegionNavigation;

//using ModuleViewBasedNavigationAConfirmCancel;

//using ModuleViewBasedNavigationAExistingViews;

//using ModuleViewBasedNavigationANavigationJournal;

//using ModuleViewBasedNavigationANavigationParticipation;

//using ModuleViewBasedNavigationAPassingParameters;

//using ModuleViewBasedNavigationB;

//using ModuleViewBasedNavigationBBasicRegionNavigation;

//using ModuleViewBasedNavigationBConfirmCancel;

//using ModuleViewBasedNavigationBExistingViews;

//using ModuleViewBasedNavigationBNavigationJournal;

//using ModuleViewBasedNavigationBNavigationParticipation;

//using ModuleViewBasedNavigationBPassingParameters;

//using Prism.Modularity;
//using Prism.Regions;
////using Microsoft.Practices.Unity;
//using Prism.Unity;

//using VNC.Core.Mvvm.Prism;

//using VNCExplore_LearnPrism_BrianLagunas.ViewModels;

//namespace VNCExplore_LearnPrism_BrianLagunas
//{
//    class Bootstrapper : UnityBootstrapper
//    {
//        // Step 1a - Create the catalog of Modules
//        //
//        // This is called when you do not directly reference the Assembly
//        // containing the module.

//        // TODO(crhodes)
//        // Figure out how you can do more than one of these creates at a time
//        // e.g. App.Config and look in Directory

//        // To load modules from directory
//        //
//        // NB. ModuleB.dll and ModuleD.dll have not been referenced
//        // but appears in .\bin\Modules folder

//        //protected override IModuleCatalog CreateModuleCatalog()
//        //{
//        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
//        //}

//        // To load modules from Xaml
//        //
//        // NB. ModuleC.dll has not been referenced
//        // but appear in .\bin folder

//        //protected override IModuleCatalog CreateModuleCatalog()
//        //{
//        //    return Prism.Modularity.ModuleCatalog.CreateFromXaml(
//        //        new Uri("/VNCExploreConsole;component/XamlCatalog.xaml", UriKind.Relative));
//        //}

//        // To load from an App.Config file
//        //
//        // NB. ModuleD.dll has not been referenced
//        // but appears in .\bin\Modules folder

//        protected override IModuleCatalog CreateModuleCatalog()
//        {
//            return new ConfigurationModuleCatalog();
//        }

//        // Step 1b - Configure the catalog of modules
//        // Modules are loaded at Startup and must be a project reference

//        protected override void ConfigureModuleCatalog()
//        {
//            Type moduleAType = typeof(ModuleAModule);
//            ModuleCatalog.AddModule(new ModuleInfo()
//            {
//                ModuleName = moduleAType.Name,
//                ModuleType = moduleAType.AssemblyQualifiedName,
//                InitializationMode = InitializationMode.WhenAvailable
//                //    InitializationMode = InitializationMode.OnDemand
//            });

//            var moduleCatalog = (ModuleCatalog)ModuleCatalog;

//            //moduleCatalog.AddModule(typeof(ModuleAModule));

//            // View Examples

//            moduleCatalog.AddModule(typeof(ModuleSimpleViewModule));

//            moduleCatalog.AddModule(typeof(ModuleMVVM_V1Module));

//            moduleCatalog.AddModule(typeof(ModuleMVVM_VM1Module));

//            moduleCatalog.AddModule(typeof(ModuleMVVM_V1_ViewInjectionModule));

//            moduleCatalog.AddModule(typeof(ModuleMVVM_VM1_ViewInjectionModule));

//            // Commanding Examples

//            // By default Prism uses just the class name to distinguish modules
//            // Even though these are from different namespaces they collide

//            //AddModuleToCatalog(typeof(ModuleStatusBar.ModuleStatusBarModule), moduleCatalog);
//            //AddModuleToCatalog(typeof(StatusBarEA.StatusBarModule), moduleCatalog);

//            //moduleCatalog.AddModule(typeof(ModulePeopleModule));
//            moduleCatalog.AddModule(typeof(ModulePeopleDelegateCommandModule));
//            moduleCatalog.AddModule(typeof(ModulePeopleCompositeCommandModule));

//            //AddModuleToCatalog(typeof(ModulePeopleDC.ModulePeopleModule), moduleCatalog);
//            //AddModuleToCatalog(typeof(ModulePeopleCC.ModulePeopleModule), moduleCatalog);

//            moduleCatalog.AddModule(typeof(ModuleStatusBarModule));
//            moduleCatalog.AddModule(typeof(ModuleToolBarModule));

//            // EventAggregation Examples

//            moduleCatalog.AddModule(typeof(ModulePeopleEventAggregationModule));
//            moduleCatalog.AddModule(typeof(PrismDemo.Services.ServicesModule));

//            // SharedService Examples

//            moduleCatalog.AddModule(typeof(ModulePeopleSharedServiceModule));

//            // RegionContext Examples

//            moduleCatalog.AddModule(typeof(ModulePeopleRegionContextModule));

//            // Navigation Examples

//            // State-Based Navigation

//            moduleCatalog.AddModule(typeof(PrismDemo.Services.PersonService.PersonServiceModule));
//            moduleCatalog.AddModule(typeof(ModuleStateBasedNavigationModule));

//            // View-Based Navigation

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationABasicRegionNavigationModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBBasicRegionNavigationModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationANavigationParticipationModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBNavigationParticipationModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAPassingParametersModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBPassingParametersModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAExistingViewsModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBExistingViewsModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAConfirmCancelModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBConfirmCancelModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationANavigationJournalModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBNavigationJournalModule));

//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationAModule));
//            moduleCatalog.AddModule(typeof(ModuleViewBasedNavigationBModule));

//            moduleCatalog.AddModule(typeof(ModuleShellsDuplicateRegionsExceptionModule));
//            moduleCatalog.AddModule(typeof(ModuleShellsScopedRegionsModule));
//            moduleCatalog.AddModule(typeof(ModuleShellsDialogServiceModule));
//            moduleCatalog.AddModule(typeof(ModuleShellsViewCompositionModule));
//            //moduleCatalog.AddModule(typeof(ModuleMultipleShellsModule));
//        }

//        void AddModuleToCatalog(Type moduleType, ModuleCatalog catalog)
//        {
//            ModuleInfo moduleInfo = new ModuleInfo();

//            // Use the fully qualified name to distinguish the ModuleName
//            moduleInfo.ModuleName = moduleType.AssemblyQualifiedName;
//            moduleInfo.ModuleType = moduleType.AssemblyQualifiedName;

//            catalog.AddModule(moduleInfo);
//        }
//        // Step 2 - Configure the container

//        protected override void ConfigureContainer()
//        {
//            base.ConfigureContainer();

//            // This got added for the ViewBased Navigation Examples
//            //Container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
//            Container.RegisterTypeForNavigation<IMainWindowViewModel, MainWindowViewModel>();

//            // Create a Singleton ShellService (DialogService)
//            //Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());

//            // Use the ServiceRepository
//            //Container.RegisterType<PersonRepository.Interface.IPersonRepository, ServiceRepository>();
//            // Use the CSVRepository
//            //Container.RegisterType<PersonRepository.Interface.IPersonRepository, CSVRepository>();
//            // Use the SQLRepository
//            //Container.RegisterType<PersonRepository.Interface.IPersonRepository, SQLRepository>();
//        }

//        // Step 3 - Configure the RegionAdapters if any custom ones have been created

//        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
//        {
//            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

//            //mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
//            mappings.RegisterMapping(typeof(StackPanel), Container.TryResolve<StackPanelRegionAdapter>());
//            return mappings;
//        }

//        // Step 4 - Create the Shell that will hold the modules in designated regions.

//        protected override DependencyObject CreateShell()
//        {
//            //return Container.Resolve<Views.MainWindow>();
//            return Container.TryResolve<Views.MainWindow>();
//        }

//        // Step 5 - Show the MainWindow

//        protected override void InitializeShell()
//        {
//            // Added for RegionManagerAware examples
//            var regionManager = RegionManager.GetRegionManager(Shell);
//            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);

//            Application.Current.MainWindow.Show();
            
//        }
//    }
//}
