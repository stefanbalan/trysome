namespace DuplicateFileFind;

public class IndexedFolder
{
    public int Id { get; set; }
    public string Path { get; set; }
    public ICollection<IndexedFile> Files { get; set; }
}