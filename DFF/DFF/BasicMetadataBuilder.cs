using System.Buffers;
using System.Security.Cryptography;

namespace DFF;

public class BasicMetadataBuilder(ILogger<BasicMetadataBuilder> logger)
    : IBasicMetadataBuilder
{
    private readonly MD5 md5 = MD5.Create();

    public async Task<Item> BuildMetadataAsync(Item item)
    {
        try
        {
            await using var stream = item.FileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);

            var hash = await md5.ComputeHashAsync(stream);
            item.Hash = hash.Aggregate("", (s, b) => s + b.ToString("X2"));

            stream.Close();
        }
        catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
        {
            logger.LogError("Cannot read file {File} : {Message}", item.FileInfo.FullName,  e.Message);
        }


        return item;
    }

    private const int Size128Kb = 131072;

    public async Task<(string? Hash, int hashSize)> ComputeHash128Async(FileInfo fileInfo)
    {
        try
        {
            await using var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);

            var rented = ArrayPool<byte>.Shared.Rent(Size128Kb);

            var bSize = await stream.ReadAsync(rented);

            var hashBytes = md5.ComputeHash(rented, 0, bSize);
            var hash = hashBytes.Aggregate("", (s, b) => s + b.ToString("X2"));

            stream.Close();

            return (hash, bSize);
        }
        catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
        {
            logger.LogError("Cannot read file {File} : {Message}", fileInfo.FullName, e.Message);
            return (null, 0);
        }
    }

    public async Task<string?> ComputeHashAsync(FileInfo fileInfo)
    {
        try
        {
            await using var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);

            var hashBytes = await md5.ComputeHashAsync(stream);
            var hash = hashBytes.Aggregate("", (s, b) => s + b.ToString("X2"));

            stream.Close();

            return hash ;
        }
        catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
        {
            logger.LogError("Cannot read file {File} : {Message}", fileInfo.FullName, e.Message);
            return null;
        }
    }

    public Task<DateTime?> ComputeCreationDateAsync(FileInfo fileInfo) => throw new NotImplementedException();
}