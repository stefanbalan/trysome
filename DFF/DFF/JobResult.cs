namespace DFF;

public class JobResult
{
    public string Error { get; set; }

    public List<FileResult> FileResults { get; set; } = [];


    public record FileResult(string Path, Result Result);


    public enum Result
    {
        Failed = 0,
        Filtered = 1,
        ExistingRemoved = 2,
        ExistedNotRemoved = 3,
        ExistingKept = 4,    
        NewRemoved = 5,
        NewNotRemoved = 6,
        NewKept = 7,

    }
}