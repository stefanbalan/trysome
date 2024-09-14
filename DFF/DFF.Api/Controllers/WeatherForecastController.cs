using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DirectoryController : ControllerBase
{
    

    private readonly ILogger<DirectoryController> logger;

    public DirectoryController(ILogger<DirectoryController> logger)
    {
        this.logger = logger;
    }

    [HttpPost(Name = "GetWeatherForecast")]
    public Job Add(string path)
    {
        return new Job();
    }
}

public class Job
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Path { get; set; }
    public string Status { get; set; } = "Pending";
    public int? ItemCount { get; set; }
    public int? Progress { get; set; } = 0;
}
