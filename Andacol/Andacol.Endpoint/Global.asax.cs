using IKLSoftware.ODataHelpers;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.OData.Extensions;

namespace Andacol.Endpoint
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                config.MapODataServiceRoute("odata", "odata", ODataConfig.GetModel(Assembly.GetExecutingAssembly()));
            });
        }

        public Global()
        {
        }

    }
}