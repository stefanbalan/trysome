using Lazy.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Infra
{
    [ApiController]
    public class LazyPagingController : ControllerBase
    {
        private readonly UserSettingsService _userSettingsService;

        public LazyPagingController(UserSettingsService userSettingsService)
        {
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