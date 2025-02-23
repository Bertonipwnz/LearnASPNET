using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnASPNETMVC.Startup))]
namespace LearnASPNETMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
