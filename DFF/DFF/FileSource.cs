namespace DFF;

public class FileSource(IConfig config) : IFileSource
{
    private readonly string path = config.FileSourcePath;


    public IEnumerable<Item> GetFiles()
    {
        var dir = new DirectoryInfo(path);
        return dir.EnumerateFileSystemInfos(
                "*",
                new EnumerationOptions() { RecurseSubdirectories = true })
            .Where(fse => fse is FileInfo)
            .Select(fse => new Item() { FileInfo =  (FileInfo)fse});
    }

    public IAsyncEnumerable<Item> GetFilesAsync() => throw new NotImplementedException();
}