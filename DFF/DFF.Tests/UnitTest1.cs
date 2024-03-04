namespace DFF.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var files = Directory.EnumerateFileSystemEntries("c:\\Windows\\WinSxS",
            "*",
            new EnumerationOptions() { RecurseSubdirectories = true });
        var fs = files.ToList();
    }
}