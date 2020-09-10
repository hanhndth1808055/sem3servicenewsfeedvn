using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsFeedVn.Startup))]
namespace NewsFeedVn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
