using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client.WEb.Startup))]
namespace Client.WEb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
