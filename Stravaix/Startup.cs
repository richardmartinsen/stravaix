using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stravaix.Startup))]
namespace Stravaix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
