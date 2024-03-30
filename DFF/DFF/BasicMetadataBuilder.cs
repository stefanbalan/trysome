using System.Buffers;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
            logger.LogError("Cannot read file {File} : {Message}", item.FileInfo.FullName, e.Message);
        }


        return item;
    }

    private const int Size128Kb = 131072;

    public async ValueTask<(string? Hash, int hashSize)> ComputeHash128Async(FileInfo fileInfo)
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

    public async ValueTask<string?> ComputeHashAsync(FileInfo fileInfo)
    {
        try
        {
            await using var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);

            var hashBytes = await md5.ComputeHashAsync(stream);
            var hash = hashBytes.Aggregate("", (s, b) => s + b.ToString("X2"));

            stream.Close();

            return hash;
        }
        catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
        {
            logger.LogError("Cannot read file {File} : {Message}", fileInfo.FullName, e.Message);
            return null;
        }
    }

    private readonly (string Format, string Regex)[] patterns = [
        //2019-11-28 20.43.25
        ("yyyy-MM-dd HH.mm.ss", @"\d{4}-\d{2}-\d{2}\s\d{2}.\d{2}.\d{2}"),
        // 20190602_200549(0).jpg
        ("yyyyMMdd_HHmmss", @"\d{8}_\d{6}")
    ];


    public ValueTask<(DateTime? Date, string Extra)> ComputeCreationDateAsync(FileInfo fileInfo)
    {
        var fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
        var creationDate = default(DateTime?);

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(fileName, pattern.Regex);
            if (match.Success)
            {
                var extra = Regex.Replace(fileName, pattern.Regex, "");

                if (DateTime.TryParseExact(match.Value, pattern.Format, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var date))
                {
                    return ValueTask.FromResult<(DateTime?, string)>((date, extra));
                }
            }
        }

        return ValueTask.FromResult<(DateTime?, string)>((null, string.Empty));


        string RegexPattern(string dateFormat) => dateFormat
            .Replace("y", "\\d")
            .Replace("M", "\\d")
            .Replace("d", "\\d")
            .Replace("H", "\\d")
            .Replace("m", "\\d")
            .Replace("s", "\\d");
    }
}