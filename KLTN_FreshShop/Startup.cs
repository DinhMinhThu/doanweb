using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KLTN_FreshShop.Startup))]
namespace KLTN_FreshShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
