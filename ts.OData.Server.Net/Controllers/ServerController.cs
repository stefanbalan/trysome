using System.Web.Http;
using Microsoft.AspNet.OData;
using ts.OData.Data.Net;

namespace ts.OData.Server.Net.Controllers
{
    public class ServerController : ApiController
    {
        private TsODataContext _context;

        public ServerController()
        {
            _context = new TsODataContext();
        }

        public IHttpActionResult Get()
        {

            return Ok();
        }
    }
}
