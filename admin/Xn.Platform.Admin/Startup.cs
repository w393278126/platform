using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Xn.Platform.Admin.Startup))]
namespace Xn.Platform.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
