using System;
using System.IO;
using MyMap.Droid.Service;
using MyMap.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace MyMap.Droid.Service
{
    public class FileService : IFileService
    {
        public string SavePicture(string name, Stream data, string location = "temp")
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Orders", location);
            Directory.CreateDirectory(documentsPath);
            string filePath = Path.Combine(documentsPath, name);
            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
            return filePath;
        }
        public string GetPath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = System.IO.Path.Combine(documentsPath, fileName);
            try
            {
                if (!File.Exists(path))
                {
                    var context = Android.App.Application.Context;
                    var dbAssetStream = context.Assets.Open(fileName);

                    var dbFileStream = new FileStream(path, FileMode.OpenOrCreate);
                    var buffer = new byte[1024];

                    int b = buffer.Length;
                    int length;

                    while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                    {
                        dbFileStream.Write(buffer, 0, length);
                    }

                    dbFileStream.Flush();
                    dbFileStream.Close();
                    dbAssetStream.Close();
                }
            }
            catch { path = null; }
            return path;
        }
    }
}
