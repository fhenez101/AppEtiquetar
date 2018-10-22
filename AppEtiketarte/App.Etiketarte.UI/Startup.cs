using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App.Etiketarte.UI.Startup))]
namespace App.Etiketarte.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
