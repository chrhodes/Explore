using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstWebSite.Startup))]
namespace FirstWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
