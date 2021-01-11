using System;
using MyMap.Extensions;
using MyMap.Interface;
using MyMap.Model;
using Prism.Mvvm;

namespace MyMap.Moduls
{
    public class DbManagerModule : BindableBase
    {
        public IService<MapObjectPin> MapObjectService { get; set; }
        public DbManagerModule(IService<MapObjectPin> mapObjectService)
        {
            MapObjectService = mapObjectService;
        }
       
    }
}
