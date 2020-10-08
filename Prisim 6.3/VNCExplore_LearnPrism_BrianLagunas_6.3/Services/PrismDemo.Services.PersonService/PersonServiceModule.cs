using Microsoft.Practices.Unity;
using Prism.Modularity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace PrismDemo.Services.PersonService
{
    public class PersonServiceModule : IModule
    {
        readonly IUnityContainer _container;

        public PersonServiceModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IPersonService, PersonService>(new ContainerControlledLifetimeManager());
        }
    }
}
