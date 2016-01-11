using IKLSoftware.ODataHelpers;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData.Extensions;
using System.Web.Routing;

namespace Andacol.Endpoint
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                config.MapODataServiceRoute("odata", "odata", ODataConfig.GetModel(Assembly.GetExecutingAssembly()));
            });
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}