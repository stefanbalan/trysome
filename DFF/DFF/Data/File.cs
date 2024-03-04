namespace DuplicateFileFind;

public class File
{
    public int Id { get; set; }
    public int DirectoryId { get; set; }
    public Folder Folder { get; set; }
    public string Name { get; set; }
    public long Length { get; set; }
    public DateTime CreationTime { get; set; }
    public string Hash { get; set; }
}