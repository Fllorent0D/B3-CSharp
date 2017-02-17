using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartWeb.Startup))]
namespace SmartWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
