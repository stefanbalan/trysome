using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace DuplicateFileFind;

public class IndexedFile
{
    public long Id { get; set; }
    public int FolderId { get; set; }
    public required IndexedFolder Folder { get; set; }
    
    public required string Name { get; set; }
    public long Length { get; set; }

    /// <summary>
    /// Media creation date
    /// </summary>
    public DateOnly? Date { get; set; }
    public TimeSpan? Time { get; set; }


    public string? OriginalPath { get; set; }
    public string OriginalName { get; set; }
    public DateTime OriginalFileDate { get; set; }


    /// <summary>
    /// File hash for the first 128 kb of the file
    /// </summary>
    public string? Hash128 { get; set; }

    /// <summary>
    /// File hash for the whole file
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// json with extended metadata
    /// </summary>
    public string ExtendedMetadata { get; set; }
}