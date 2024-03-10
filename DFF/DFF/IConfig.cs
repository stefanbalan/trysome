namespace DFF;

public interface IConfig
{
    public string SourcePath { get; }
    public string DestinationPath { get; }
    public bool KeepSource { get; set; }
}