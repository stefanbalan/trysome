using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoL.Service
{
    internal class FileDataProvider : IDataProvider
    {
        private readonly DirectoryInfo directory;

        public FileDataProvider(DirectoryInfo di)
        {
            directory = di;
        }
        public Dictionary<string,object> GetImport<T>()
        {
            if (!(directory?.Exists ?? false)) return null;
            var result = new Dictionary<string, object>();

            directory.EnumerateDirectories()
                .Where(d => DateTime.TryParse(d.Name, out var _))
                .Where(d => !d.Name.Contains("imported"))
                .First();


            return result;
        }

        JsonImport<T> IDataProvider.GetImport<T>()
        {
            throw new NotImplementedException();
        }
    }
}
