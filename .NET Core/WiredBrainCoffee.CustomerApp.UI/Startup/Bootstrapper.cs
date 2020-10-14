using Autofac;
using Prism.Events;
using WiredBrainCoffee.CustomerApp.DataAccess;
using WiredBrainCoffee.CustomerApp.UI.DataProvider;
using WiredBrainCoffee.CustomerApp.UI.Dialogs;
using WiredBrainCoffee.CustomerApp.UI.ViewModel;

namespace WiredBrainCoffee.CustomerApp.UI.Startup
{
  public class Bootstrapper
  {
    public IContainer Bootstrap()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

      builder.RegisterType<CustomerDbContext>().AsSelf();

      builder.RegisterType<MainWindow>().AsSelf();

      builder.RegisterType<MainViewModel>().AsSelf();
      builder.RegisterType<NavigationViewModel>().AsSelf();
      builder.RegisterType<CustomerDetailViewModel>().AsSelf();

      // Don't want to set up a database? You can use the 
      // CustomerInMemoryDataProvider like this:
      // 
      // builder.RegisterType<CustomerInMemoryDataProvider>().As<ICustomerDataProvider>();
      // 
      // instead of the following code line with the CustomerDbDataProvider:
      builder.RegisterType<CustomerDbDataProvider>().As<ICustomerDataProvider>();

      builder.RegisterType<SendShirtDataProvider>().As<ISendShirtDataProvider>();
      builder.RegisterType<RegistryDataProvider>().As<IRegistryDataProvider>();
      builder.RegisterType<ColorDialogService>().As<IColorDialogService>();
      builder.RegisterType<MessageBoxService>().As<IMessageBoxService>();

      return builder.Build();
    }
  }
}
