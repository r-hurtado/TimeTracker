using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test7.Startup))]
namespace test7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
