using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelPortal.web.Startup))]
namespace TravelPortal.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
