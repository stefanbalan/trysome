namespace DuplicateFileFind;

public class IndexedFile
{
    public int Id { get; set; }
    public int DirectoryId { get; set; }
    public required IndexedFolder Folder { get; set; }
    public required string Name { get; set; }
    public long Length { get; set; }
    public DateTime CreationDateTime { get; set; }

    /// <summary>
    /// File hash for the first 128 kb of the file
    /// </summary>
    public string? Hash128 { get; set; }

    /// <summary>
    /// File hash for the whole file
    /// </summary>
    public string? Hash { get; set; }
}