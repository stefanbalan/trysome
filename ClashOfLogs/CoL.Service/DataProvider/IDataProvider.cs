namespace CoL.Service
{
    internal interface IDataProvider
   {
        JsonImport<T> GetImport<T>();
    }
}
