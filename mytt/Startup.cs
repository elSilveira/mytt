using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mytt.Startup))]
namespace mytt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
