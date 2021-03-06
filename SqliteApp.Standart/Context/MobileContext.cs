using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.Interface;
using Xamarin.Forms;

namespace SqliteApp.Standart.Context
{
    public partial class MobileContext : DbContext
    {
        public DbSet<MapObject> MapObjects { get; set; }

        public DbSet<CategoryDb> CategoryDb { get; set; }

        //public DbSet<PurchaseHistory> History { get; set; }
        public MobileContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = DependencyService.Get<IDatabaseService>().GetDatabasePath();
            optionsBuilder.EnableSensitiveDataLogging().UseSqlite($"Filename={dbPath}");

        }


    }
}
