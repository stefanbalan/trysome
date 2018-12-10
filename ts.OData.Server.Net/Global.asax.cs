using System.Web.Http;
using ts.OData.Data.Net;

namespace ts.OData.Server.Net
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            

            GlobalConfiguration.Configure(WebApiConfig.Register);

            IocContainer.Setup();


            var ctx = new TsODataContext();
            ctx.Database.Initialize(false);
        }
    }
}
