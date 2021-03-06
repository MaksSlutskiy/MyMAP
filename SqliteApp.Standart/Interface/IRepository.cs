using System;
using System.Collections.Generic;

namespace SqliteApp.Standart.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Create(TEntity entity);
        void Update(TEntity entity,int id);

        void UpdateRange(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);
        void Save();


    }
}
