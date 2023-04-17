namespace CoL.Service.DataProvider;

public interface IJsonDataProvider
{
    bool HasImportData();
    Task<JsonData?> GetImportDataAsync();
}