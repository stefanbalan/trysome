using Microsoft.Extensions.Logging.Abstractions;

namespace DFF.Tests;

public class BasicMetadataBuilderTests
{
    [Theory]
    [InlineData("2019-11-28 20.43.25.jpg", 2019, 11, 28, 20, 43, 25)]
    [InlineData("20190602_200549(0).jpg", 2019, 6, 2, 20, 5, 49)]
    public async void Test1(string filename, int year, int month, int day, int hour, int minute, int second )
    {
        var sut = new BasicMetadataBuilder(NullLogger<BasicMetadataBuilder>.Instance);
        var res = await sut.ComputeCreationDateAsync(new FileInfo(filename));

        var expected = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
        

        Assert.NotNull(res.Date);
        Assert.Equal(expected, res.Date.Value);
    }
}