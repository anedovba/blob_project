using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlobProject.Startup))]
namespace BlobProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
