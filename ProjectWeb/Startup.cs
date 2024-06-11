using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectWeb.Startup))]
namespace ProjectWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
