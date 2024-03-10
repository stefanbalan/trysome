namespace DFF;

public class NamingProvider(ILogger<NamingProvider> logger)
{
    private const string UnknownPath = "_unknown";

    public bool Presorted { get; set; }

    public string GetRelativePath(Item file)
    {
        try
        {
            if (Presorted)
                // the last directory from the file path
                return file.FileInfo.Directory?.Name ?? UnknownPath;
            
            return file.ProducedDateTime.HasValue
                ? file.ProducedDateTime.Value.ToString("yyyy\\MM")
                : UnknownPath;
        }
        catch (Exception e)
        {
            logger.LogError("Naming error {Message}", e.Message);
            return UnknownPath;
        }
    }
}