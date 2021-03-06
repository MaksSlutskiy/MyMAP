using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMap.CustomViews;
using MyMap.Interface;
using MyMap.IRepository;
using MyMap.Model;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.UnitOfWork;

namespace MyMap.Service
{
    public class MapObjectService : IService<MapObjectPin>
    {
        private MapObjectUnitOfWork uow;
        IMapper mapper;


        public MapObjectService(MapObjectUnitOfWork uow)
        {
            this.uow = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg
                .CreateMap<MapObject, MapObjectPin>()
                .ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public Task<bool> Save()
        {
            return Task.Run(() =>
            {
                try
                {
                    uow.MapObjectRepository.Save();
                    uow.Save();
                    return true;
                }
                catch
                {
                    return false;
                }

            });

        }

        public Task<IEnumerable<MapObjectPin>> GetAll()
        {
            return Task.Run(() =>
            {
                var res = uow.MapObjectRepository
                .GetAll()
                .ToList()
                .Select(entity => mapper.Map<MapObjectPin>(entity));
                return res;
            });
        }

        public Task<MapObjectPin> Get(int id)
        {
            return Task.Run(() =>
            {
                var entity = uow.MapObjectRepository.Get(id);
                return mapper.Map<MapObjectPin>(entity);
            });
        }

        public Task<bool> UpdateRange(IEnumerable<MapObjectPin> dto)
        {
            return Task.Run(() =>
            {
            try
            {
                var entity = mapper.Map<IEnumerable<MapObject>>(dto);
                uow.MapObjectRepository.UpdateRange(entity);

                return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<MapObjectPin> Create(MapObjectPin dto)
        {
            return Task.Run(() =>
            {

                var entity = mapper.Map<MapObject>(dto);
                var newEntity = uow.MapObjectRepository.Create(entity);
                return mapper.Map<MapObjectPin>(newEntity);

            });
        }

        public Task<bool> Update(MapObjectPin dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = mapper.Map<MapObject>(dto);
                    uow.MapObjectRepository.Update(entity, dto.Id);
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            });
        }

        public Task<bool> Delete(MapObjectPin dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = uow.MapObjectRepository.Get(dto.Id);
                    uow.MapObjectRepository.Delete(entity);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
