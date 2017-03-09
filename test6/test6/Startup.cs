using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test6.Startup))]
namespace test6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
