namespace DFF;

public class FileSource(ILogger<FileSource> logger, IConfig config)
{
    private readonly string path = config.SourcePath;
    private readonly string extensionFilter = config.ExtensionFilter;

    public IEnumerable<FileInfo> GetFiles()
    {
        var dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            logger.LogError("Source directory {Path} does not exist", path);
            return Enumerable.Empty<FileInfo>();
        }

        var extensions = extensionFilter.Split(',');

        return dir.EnumerateFileSystemInfos("*",
                new EnumerationOptions { RecurseSubdirectories = true })
            .Where(fse => fse is FileInfo)
            .Where(fse => Array.Exists(
                extensions,
                ext => Path.GetExtension(fse.Name).Equals(ext, StringComparison.OrdinalIgnoreCase)))
            .Select(fsi => (FileInfo)fsi);
    }
}