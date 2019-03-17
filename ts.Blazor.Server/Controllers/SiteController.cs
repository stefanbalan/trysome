using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ts.Domain.Entities;

namespace ts.Blazor.Api.Controllers
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {

        private readonly Domain.Entities.Server[] _servers =
        {
            new Domain.Entities.Server
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a1"),
                Name = "WNPARSRV00001"
            },
            new Domain.Entities.Server
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a2"),
                Name = "WNPARSRV00002"
            },
            new Domain.Entities.Server
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000a3"),
                Name = "WNPARSRV00003"
            }
        };

        private readonly Site[] _sites;

        public SiteController()
        {
            _sites = new[]
            {
                new Site
                {
                    Id = 1,
                    Name = "Site01",
                    Servers =
                    {
                        _servers[0],
                        _servers[1],
                        _servers[2],

                    },
                },
                new Site
                {

                    Id = 2,
                    Name = "Site02",
                    Servers =
                    {
                        _servers[0],
                        _servers[2]
                    }
                },
                new Site()
                {
                    Id = 3,
                    Name = "Site03"
                }
            };

        }


        [HttpGet("[action]")]
        public IEnumerable<Site> GetSites()
        {

            return _sites;
        }
    }
}
