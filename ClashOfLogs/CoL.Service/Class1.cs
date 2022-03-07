using System;

namespace CoL.Service
{

    class JsonImporter
    {
        void Import(IDataProvider importProvider)
        {

        }

    }

    class JsonImport<T>
    {
        T Import { get; set; }
        DateTime Date { get; set; }
    }
}
