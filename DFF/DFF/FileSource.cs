using System.Collections.Immutable;

namespace DFF;

public class FileSource(ILogger<FileSource>? logger, string path) : ISource
{
    public IList<FileInfo> GetFiles()
    {
        var dir = new DirectoryInfo(path);
        if (dir.Exists)
            return dir
                .EnumerateFileSystemInfos("*", new EnumerationOptions { RecurseSubdirectories = true })
                .OfType<FileInfo>()
                .ToImmutableList();
        
        logger?.LogError("Source directory {Path} does not exist", path);
        return [];
    }
}