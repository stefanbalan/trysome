namespace DuplicateFileFind;

public class Folder
{
    public int Id { get; set; }
    public string Path { get; set; }
    public ICollection<File> Files { get; set; }
}