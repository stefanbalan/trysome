using AdventOfCode2022;

namespace Tests;

public class Day3Test
{
    private readonly Day3 day3 = new();

    [Theory]
    [MemberData(nameof(PriorityTestData))]
    public void GetPriorityTest1(char c, int priority)
    {
        Assert.Equal(day3.GetPriority(c), priority);
    }

    public static IEnumerable<object[]> PriorityTestData()
    {
        const string source = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (var i = 0; i < source.Length; i++)
            yield return new object[] { source[i], i + 1 };
    }


    [Theory]
    [InlineData("a", "A", "")]
    [InlineData("a", "a", "a")]
    [InlineData("ab", "aB", "a")]
    [InlineData("abc", "aBC", "a")]
    public void FindMatchTest(string str1, string str2, string exResult)
    {
        Assert.Equal(day3.FindCommon(str1, str2), exResult);
    }

    [Theory]
    [InlineData("a", "A", "B", "")]
    [InlineData("a", "a", "a", "a")]
    [InlineData("ab", "aB", "aC", "a")]
    [InlineData("abc", "aBC", "aDE", "a")]
    [InlineData("abc", "ABc", "cDE", "c")]
    [InlineData("abcd", "ABcd", "cdEF", "cd")]
    public void FindCommonTest(string str1, string str2, string str3, string exResult)
    {
        Assert.Equal(day3.FindCommon(str1, str2, str3), exResult);
    }
}
