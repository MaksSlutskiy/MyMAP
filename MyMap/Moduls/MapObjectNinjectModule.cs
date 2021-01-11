using System;
using Microsoft.EntityFrameworkCore;
using MyMap.Interface;
using MyMap.IRepository;
using MyMap.Model;
using MyMap.Service;

namespace MyMap.Moduls
{
    //public class MapObjectNinjectModule : NinjectModule
    //{
    //    public override void Load()
    //    {
    //        Bind<IRepository<MapObject>>().To<MapObjectRepository>();
    //        Bind<IService<MapObjectPin>>().To<MapObjectService>();
    //        Bind<DbContext>().To<MobileContext>();

    //        Builder.RegisterType<IRepository<MapObject>>().As<MapObjectRepository>().SingleInstance();
    //    }
    //}
}
