using System.Threading.Tasks;

namespace CoL.Service
{
    public interface IJsonDataProvider
    {
        bool HasImportData();
        Task<JsonImportData> GetImportDataAsync();
    }
}
