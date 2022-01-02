using System;

namespace CoL.Service
{

    class JsonImporter
    {
        void Import(IImportProvider importProvider)
        {

        }

    }

    class JsonImport<T>
    {

        T Import { get; set; }
        DateTime Date { get; set; }
    }

    internal interface IImportProvider
    {
        JsonImport<T> GetImport<T>();
    }


    internal class FileImportProvider : IImportProvider
    {

        public FileImportProvider(string path)
        {

        }


        public JsonImport<T> GetImport<T>()
        {
            throw new NotImplementedException();
        }
    }
}
