using Prism.Ioc;
using Prism.Modularity;

using Unity;
using Unity.Lifetime;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace PrismDemo.DomainServices.PersonService
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
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
    }
}
