using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFF;
internal class FileDestination(string baseStorePath)
{
    private readonly string baseStorePath = baseStorePath;

    bool AddFile(FileInfo f, string path, string name )
    {
        return true;
    }
}
