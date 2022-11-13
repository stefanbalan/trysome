using System.Threading.Tasks;

namespace CoL.Service
{
    public interface IJsonDataProvider
    {
        bool HasImportData();
        Task<JsonData> GetImportDataAsync();
        bool SetImported(bool success);
    }
}
