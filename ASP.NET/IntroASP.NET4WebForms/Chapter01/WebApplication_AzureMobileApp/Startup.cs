using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication_AzureMobileApp.Startup))]

namespace WebApplication_AzureMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}