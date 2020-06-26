using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GraduationIdeasRegistration.Startup))]
namespace GraduationIdeasRegistration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
