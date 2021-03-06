using System;
using System.IO;

namespace MyMap.Interface
{
    public interface IFileService
    {
        string SavePicture(string name, Stream data, string location = "temp");
    }
}
