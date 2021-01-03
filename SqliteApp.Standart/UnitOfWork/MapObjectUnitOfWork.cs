using System;
using Microsoft.EntityFrameworkCore;
using SqliteApp.Standart.Abstructions;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.Interface;

namespace SqliteApp.Standart.UnitOfWork
{
    public class MapObjectUnitOfWork : BaseUnitOfWork
    {
        public IRepository<MapObject> PaymentRepository { get; }
        
        public MapObjectUnitOfWork(DbContext db,
                                 IRepository<MapObject> paymentRepository) : base(db)
        {
            this.PaymentRepository = paymentRepository;
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
