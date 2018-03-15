using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DataServices.Startup))]

namespace DataServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}