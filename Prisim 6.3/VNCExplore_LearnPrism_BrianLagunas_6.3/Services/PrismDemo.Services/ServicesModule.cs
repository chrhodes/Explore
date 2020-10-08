using Microsoft.Practices.Unity;
using Prism.Modularity;
using Services;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace PrismDemo.Services
{
    public class ServicesModule : IModule
    {
        IUnityContainer _container;

        public ServicesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            // Register with container as a shared service
            // Using ContainerControlledLifetimeManager turns it into a singleton

            _container.RegisterType<IPersonRepository, PersonRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
