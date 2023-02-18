using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Infra;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ILogger<ApiController> Logger;

    public ApiController(ILogger<ApiController> logger)
    {
        Logger = logger;
    }
}