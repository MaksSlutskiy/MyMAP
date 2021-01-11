using System;
using System.IO;
using MyMap.Droid.Service.MyWalletProject.Droid.Services;
using SqliteApp.Standart.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace MyMap.Droid.Service
{
    
namespace MyWalletProject.Droid.Services
    {

        public class DatabaseService : IDatabaseService
        {
            public string GetDatabasePath()
            {
                return Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    AppModelInfo.DatabaseName);
            }
        }
    }
}
