namespace WoN.DataProvider;

public interface IDataProvider<T>
{
    Task<T> GetDataAsync(dynamic? criteria = null);
}
