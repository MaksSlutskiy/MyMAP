using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Abstructions;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.Interface;

namespace SqliteApp.Standart.UnitOfWork
{
    public class MapObjectUnitOfWork : BaseUnitOfWork
    {
        public IRepository<MapObject> MapObjectRepository { get; }

        public IRepository<CategoryDb> CategoryRepository { get; }

        public MapObjectUnitOfWork(DbContext db,
                                 IRepository<MapObject> mapObjectRepository, IRepository<CategoryDb> categoryRepository) : base(db)
        {
            this.MapObjectRepository = mapObjectRepository;
            this.CategoryRepository = categoryRepository;
        }


        //public IRepository<PurchaseHistory> PurchaseHistory { get; }
        //public PaymentUnitOfWork(DbContext db,
        //                         IRepository<MapObject> paymentRepository, IRepository<PurchaseHistory> purchaseHistory) : base(db)
        //{
        //    this.PaymentRepository = paymentRepository;
        //    this.PurchaseHistory = purchaseHistory;
        //}
    }
}
