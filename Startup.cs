using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineLibrary1.Startup))]
namespace OnlineLibrary1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
