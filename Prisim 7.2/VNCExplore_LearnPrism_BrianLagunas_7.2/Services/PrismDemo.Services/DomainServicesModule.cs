using Prism.Ioc;
using Prism.Modularity;
using PrismDemo.DomainServices.Repositories;
using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace PrismDemo.DomainServices
{
    public class DomainServicesModule : IModule
    {
        // 01

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPersonRepository, PersonRepository>();
        }

        // 02

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
    }
}
