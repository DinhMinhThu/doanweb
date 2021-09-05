using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Doan_ChuyenMon.Startup))]
namespace Doan_ChuyenMon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
