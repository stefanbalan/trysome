using System.Web.Http;

namespace ts.OData.Server.Net
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            IocContainer.Setup(config);

            // Web API configuration and services


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
