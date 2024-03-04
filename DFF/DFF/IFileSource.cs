namespace DFF;

public interface IFileSource 
{
    IEnumerable<Item> GetFiles();
    IAsyncEnumerable<Item> GetFilesAsync();
}