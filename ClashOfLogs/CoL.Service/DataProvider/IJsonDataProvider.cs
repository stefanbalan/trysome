namespace CoL.Service.DataProvider;

public interface IJsonDataProvider
{
    Task<JsonData?> GetImportDataAsync();
    TimeSpan GetNextImportDelay();
}