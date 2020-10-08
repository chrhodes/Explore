using System.Web.Mvc;
using Ninject;
using Ninject.Mvc3;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Models.AppStart_NinjectMVC3), "Start")]

namespace Models
{
    public static class AppStart_NinjectMVC3
    {
        public static void Start()
        {
            IKernel kernel = new StandardKernel();
            
            DependencyResolver.SetResolver(new NinjectServiceLocator(kernel));
        }
    }
}
