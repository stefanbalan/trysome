using System.Security.Cryptography;

namespace DFF;

public class BasicMetadataBuilder : IBasicMetadataBuilder
{
    private readonly MD5 md5 = MD5.Create();

    public async Task<Item> BuildMetadataAsync(Item item)
    {
        if (IsFileLocked(item.FileInfo))
            return item;

        byte[] hash;
        await using (var stream = item.FileInfo.OpenRead())
        {
            hash = await md5.ComputeHashAsync(stream);
        }

        var hashString = hash.Aggregate("", (s, b) => s + b.ToString("X2"));
        item.Hash = hashString;
        return item;
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
        try
        {
            using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            stream.Close();
        }
        catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
        {
            return true;
        }

        //file is not locked
        return false;
    }
}