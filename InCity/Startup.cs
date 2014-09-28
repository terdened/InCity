using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InCity.Startup))]
namespace InCity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
