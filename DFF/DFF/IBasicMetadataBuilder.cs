namespace DFF;

public interface IBasicMetadataBuilder
{
    Task<Item> BuildMetadataAsync(Item item);

    ValueTask<(string? Hash, int hashSize)> ComputeHash128Async(FileInfo fileInfo);
    ValueTask<string?> ComputeHashAsync(FileInfo fileInfo);
    ValueTask<(DateTime? Date, string Extra)> ComputeCreationDateAsync(FileInfo fileInfo);
}