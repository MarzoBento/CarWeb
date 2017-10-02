using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarWeb.Startup))]
namespace CarWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
