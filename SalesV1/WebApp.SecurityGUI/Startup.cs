using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp.SecurityGUI.Startup))]
namespace WebApp.SecurityGUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
