using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DivishRam_PassionProject.Startup))]
namespace DivishRam_PassionProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
