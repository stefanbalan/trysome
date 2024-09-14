using DuplicateFileFind;

namespace DFF;

public class Item
{
    public required string OriginalName { get; set; }
    public string? OriginalPath { get; set; }

    public required string FileName { get; set; }
    public string? IndexedPath { get; set; }

    public string? Path { get; set; }

    public string? Hash128 { get; set; }

    public string? Hash { get; set; }

    // 2 properties, one for the date and one for the time
    public DateOnly? Date { get; set; }
    public TimeSpan? Time { get; set; }


    public IndexedFile? IndexedFile { get; set; }
}