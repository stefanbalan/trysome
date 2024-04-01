namespace DFF;

public interface IConfig
{
    public string SourcePath { get; }
    string ExtensionFilter { get; }
    public string DestinationPath { get; }
    public bool KeepSource { get; }
}

public class Config(IConfiguration configuration, string[] args) : IConfig
{
    private string? sourcePath;
    private string? destinationPath;
    private bool? keepSource;
    private string? extensionFilter;

    public string SourcePath => sourcePath ??= GetRequiredConfigOrArgument<string>(nameof(SourcePath));
    public string ExtensionFilter => extensionFilter ??= GetConfigOrArgumentWithDefault(nameof(ExtensionFilter), "gif,jpg,jpeg,png,3gp,mp4");

    public string DestinationPath => destinationPath ??= GetRequiredConfigOrArgument<string>(nameof(DestinationPath));
    public bool KeepSource => keepSource ??= GetConfigOrArgumentWithDefault(nameof(KeepSource), true);




    private T GetRequiredConfigOrArgument<T>(string key)
        => GetConfigOrArgument<T>(key) ?? throw new MissingConfigurationException(nameof(key));

    private T GetConfigOrArgumentWithDefault<T>(string key, T defaultValue)
        => GetConfigOrArgument<T>(key) ?? defaultValue;

    private T? GetConfigOrArgument<T>(string key)
    {
        var x = Array.Find(args, a => a.StartsWith(key));
        if (!string.IsNullOrWhiteSpace(x))
        {
            var a = x.Split("=");
            if (a.Length > 1 && !string.IsNullOrWhiteSpace(a[1])) return (T)Convert.ChangeType(a[1], typeof(T));
        }

        x = configuration.GetValue<string>(key);
        if (string.IsNullOrWhiteSpace(x))
            return default;

        return (T)Convert.ChangeType(x, typeof(T));
    }

    public class MissingConfigurationException(string name) : Exception
    {
    }
}