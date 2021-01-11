using System;
using System.IO;
using MyMap.iOS.Service;
using SqliteApp.Standart.Interface;
using Xamarin.Forms;

[assembly: Xamarin.Forms.DependencyAttribute(typeof(MyMap.iOS.Service.DatabaseService))]
namespace MyMap.iOS.Service
{
    public class DatabaseService : IDatabaseService
    {
        public string GetDatabasePath()
        {
                var res = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments),
                "..",
                "Library",
                AppModelInfo.DatabaseName);
            return res;
        }
    }
}
