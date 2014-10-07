using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SongHaven.Startup))]
namespace SongHaven
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
