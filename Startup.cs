using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FortuneTellerMVC2.Startup))]
namespace FortuneTellerMVC2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
