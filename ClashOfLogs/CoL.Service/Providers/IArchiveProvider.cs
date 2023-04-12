using ILogger = Serilog.ILogger;

namespace CoL.Service.Providers;

public interface IArchiveProvider
{
    public Task<bool> ArchiveAsync(DateTime dateTime, string objectContent, string objectName, bool? success);
}
