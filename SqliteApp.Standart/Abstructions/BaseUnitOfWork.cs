using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Interface;

namespace SqliteApp.Standart.Abstructions
{
    public abstract class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        protected DbContext db;

        public BaseUnitOfWork(DbContext db)
        {
            this.db = db;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
