using Lazy.Client.Services;

namespace Lazy.Server.Infra
{
    public class ApiControllerWithPaging : ApiController
    {
        private readonly UserSettingsService _userSettingsService;

        public ApiControllerWithPaging(ILogger<ApiControllerWithPaging> logger, UserSettingsService userSettingsService) : base(logger)
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