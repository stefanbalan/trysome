using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ts.Domain;
using ts.Domain.Entities;

namespace ts.Blazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : Controller
    {

        private readonly IdentityServer[] _servers =
        {
            new IdentityServer
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a1"),
                Name = "WNPARSRV00001"
            },
            new IdentityServer
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a2"),
                Name = "WNPARSRV00002"
            },
            new IdentityServer
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a3"),
                Name = "WNPARSRV00003"
            }
        };

        private readonly LocalizedSite[] _sites;

        public SiteController()
        {
            _sites = new[]
            {
                new LocalizedSite {Id = 1, Name = "Site01"},
                new LocalizedSite {Id = 2, Name = "Site02"},
                new LocalizedSite {Id = 3, Name = "Site03"}
            };

            _sites[0].LocalizedSiteServers = new List<LocalizedSiteServer>
            {
                new LocalizedSiteServer {Server = _servers[0], Site = _sites[0]},
                new LocalizedSiteServer {Server = _servers[1], Site = _sites[0]},
                new LocalizedSiteServer {Server = _servers[2], Site = _sites[0]},
            };

            _sites[1].LocalizedSiteServers = new List<LocalizedSiteServer>
            {
                new LocalizedSiteServer {Server = _servers[0], Site = _sites[1]},
                new LocalizedSiteServer {Server = _servers[2], Site = _sites[1]},
            };

        }


        [HttpGet("[action]")]
        public IEnumerable<LocalizedSite> GetSites()
        {
            return _sites;
        }


        [HttpGet("servers")]
        public IEnumerable<IdentityServer> GetServers()
        {
            return _servers;
        }
    }
}
