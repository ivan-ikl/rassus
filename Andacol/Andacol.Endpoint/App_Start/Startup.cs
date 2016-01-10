using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Andacol.Endpoint.App_Start.Startup))]

namespace Andacol.Endpoint.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration { EnableJSONP = true });
        }
    }
}
