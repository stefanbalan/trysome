namespace DFF;

public class Item
{
    public required FileInfo FileInfo { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Hash { get; set; }

    /// <summary>
    /// DateTime when the file was produced (not created, not modified) = EXIF DateTimeOriginal
    /// </summary>
    public DateTime? ProducedDateTime { get; set; }
}