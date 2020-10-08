using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using Microsoft.Practices.Unity;

using Prism.Modularity;
using Prism.Unity;

namespace LiveDemo
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Quick and Dirty

        public Bootstrapper()
        {
            // we need to watch our folder for newly added modules
            //FileSystemWatcher fileWatcher = new FileSystemWatcher(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Modules"), "*.dll");
            //fileWatcher.Created += fileWatcher_Created;
            //fileWatcher.EnableRaisingEvents = true;
        }

        void fileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                //get the Prism assembly that IModule is defined in
                Assembly moduleAssembly = AppDomain.CurrentDomain.GetAssemblies().First(asm => asm.FullName == typeof(IModule).Assembly.FullName);
                Type IModuleType = moduleAssembly.GetType(typeof(IModule).FullName);

                //load our newly added assembly
                Assembly assembly = Assembly.LoadFile(e.FullPath);

                //look for all the classes that implement IModule in our assembly and create a ModuleInfo class from it
                var moduleInfos = assembly.GetExportedTypes()
                    .Where(IModuleType.IsAssignableFrom)
                    .Where(t => t != IModuleType)
                    .Where(t => !t.IsAbstract).Select(t => CreateModuleInfo(t));


                //create an instance of our module manager
                var moduleManager = Container.Resolve<IModuleManager>();

                foreach (var moduleInfo in moduleInfos)
                {
                    //add the ModuleInfo to the catalog so it can be loaded
                    ModuleCatalog.AddModule(moduleInfo);

                    //now load the module using the Dispatcher because the FileSystemWatcher.Created even occurs on a separate thread
                    //and we need to load our module into the main thread.
                    var d = Application.Current.Dispatcher;
                    if (d.CheckAccess())
                        moduleManager.LoadModule(moduleInfo.ModuleName);
                    else
                        d.BeginInvoke((Action)delegate { moduleManager.LoadModule(moduleInfo.ModuleName); });
                }
            }
        }

        private static ModuleInfo CreateModuleInfo(Type type)
        {
            string moduleName = type.Name;

            var moduleAttribute = CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);

            if (moduleAttribute != null)
            {
                foreach (CustomAttributeNamedArgument argument in moduleAttribute.NamedArguments)
                {
                    string argumentName = argument.MemberInfo.Name;
                    if (argumentName == "ModuleName")
                    {
                        moduleName = (string)argument.TypedValue.Value;
                        break;
                    }
                }
            }

            ModuleInfo moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = type.Assembly.CodeBase,
            };

            return moduleInfo;
        }

        #endregion //Quick and Dirty

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell(); 

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            DynamicDirectoryModuleCatalog catalog = new DynamicDirectoryModuleCatalog(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Modules"));
            return (IModuleCatalog)catalog;
        }
    }
}
