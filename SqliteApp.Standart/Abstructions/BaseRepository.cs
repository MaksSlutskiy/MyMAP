using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Interface;
using System.Reflection;
using System.Linq;

namespace SqliteApp.Standart.Abstructions
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext db;

        public BaseRepository(DbContext db)
        {
            this.db = db;
        }


        public TEntity Create(TEntity entity)
        {
            var res = db.Add(entity).Entity;
            Save();
            var local = db.Set<TEntity>().ToList().Last();
            Save();
            return local;
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            Save();
        }


        public TEntity Get(int id)
        {
            var res = db.Set<TEntity>().Find(id);
            return db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var res = db.Set<TEntity>();
            return db.Set<TEntity>();
        }

        public void Save()
        {
            db.SaveChangesAsync();
        }


        public void Update(TEntity entity,int id)
        {
            var local = Get(id);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }
            db.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            if(entity != null)
            {
                //foreach (var item in entity)
                //{
                //    db.Entry(item).State = EntityState.Detached;
                //}
                db.Set<TEntity>().AsNoTracking();
                db.Set<TEntity>().UpdateRange(entity);
                Save();

            }
           
        }
    }
}
