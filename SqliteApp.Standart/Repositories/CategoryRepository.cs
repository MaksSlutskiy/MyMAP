using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Abstructions;
using SqliteApp.Standart.Entites;

namespace SqliteApp.Standart.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryDb>
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }
    }
}
