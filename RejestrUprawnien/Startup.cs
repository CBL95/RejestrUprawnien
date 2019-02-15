using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RejestrUprawnien.Startup))]
namespace RejestrUprawnien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
