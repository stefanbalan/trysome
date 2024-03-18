namespace DFF;

public interface IBasicMetadataBuilder
{
    Task<Item> BuildMetadataAsync(Item item);

    Task<(string? Hash, int hashSize)> ComputeHash128Async(FileInfo fileInfo);
    Task<string?> ComputeHashAsync(FileInfo fileInfo);
    Task<DateTime?> ComputeCreationDateAsync(FileInfo fileInfo);
}