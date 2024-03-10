namespace DFF;

public class FileSource(ILogger<FileSource> logger, IConfig config) : IFileSource
{
    private readonly string path = config.SourcePath;

    public IEnumerable<Item> GetFiles()
    {
        var dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            logger.LogError("Source directory {Path} does not exist", path);
            return Enumerable.Empty<Item>();
        }
        return dir.EnumerateFileSystemInfos(
                "*",
                new EnumerationOptions { RecurseSubdirectories = true })
            .Where(fse => fse is FileInfo)
            .Select(fse => new Item { FileInfo =  (FileInfo)fse});
    }

    public IAsyncEnumerable<Item> GetFilesAsync() => throw new NotImplementedException();
}