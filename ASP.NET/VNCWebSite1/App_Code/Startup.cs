using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VNCWebSite1.Startup))]
namespace VNCWebSite1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
