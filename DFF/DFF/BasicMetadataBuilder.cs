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
}