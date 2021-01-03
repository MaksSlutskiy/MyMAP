using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Abstructions;
using SqliteApp.Standart.Entites;

namespace SqliteApp.Standart.Repositories
{
    public class MapObjectRepository : BaseRepository<MapObject>
    {

        public MapObjectRepository(DbContext db) : base(db)
        {

        }
    }
}
