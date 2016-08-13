using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PageParse.Startup))]
namespace PageParse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
