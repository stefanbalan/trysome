using Lazy.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Infra
{
    [ApiController]
    public class PagingController : ControllerBase
    {
        protected readonly ILogger<PagingController> Logger;
        private readonly UserSettingsService _userSettingsService;

        public PagingController(ILogger<PagingController> logger, UserSettingsService userSettingsService)
        {
            Logger = logger;
            _userSettingsService = userSettingsService;
        }

        protected (int ps, int pn) ValidatePaging(int? pageSize, int? pageNumber)
        {
            var ps = pageSize is null or < 0 ? _userSettingsService.PageSize : pageSize.Value;
            var pn = pageNumber is null or < 0 ? 0 : pageNumber.Value;

            return (ps, pn);
        }
    }
}