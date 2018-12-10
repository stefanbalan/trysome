using System.Web.Http;
using Microsoft.AspNet.OData;
using ts.OData.Data.Net;

namespace ts.OData.Server.Net.Controllers
{
    public class SiteController : ApiController
    {
        private TsODataContext _context;

        public SiteController(TsODataContext context)
        {
            _context = context;
        }

        public IHttpActionResult Get()
        {

            return Ok();
        }
    }
}
