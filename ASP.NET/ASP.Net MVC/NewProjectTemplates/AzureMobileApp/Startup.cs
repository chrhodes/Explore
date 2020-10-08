using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AzureMobileApp.Startup))]

namespace AzureMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}