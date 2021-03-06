using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMap.Interface;
using MyMap.Model;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.UnitOfWork;

namespace MyMap.Service
{
    public class CategoryService : IService<Category>
    {
        private MapObjectUnitOfWork uow;
        IMapper mapper;


        public CategoryService(MapObjectUnitOfWork uow)
        {
            this.uow = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg
                .CreateMap<CategoryDb, Category>()
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
                    uow.CategoryRepository.Save();
                    uow.Save();
                    return true;
                }
                catch
                {
                    return false;
                }

            });

        }

        public Task<IEnumerable<Category>> GetAll()
        {
            return Task.Run(() =>
            {
                var res = uow.CategoryRepository
                .GetAll()
                .ToList()
                .Select(entity => mapper.Map<Category>(entity));
                return res;
            });
        }

        public Task<Category> Get(int id)
        {
            return Task.Run(() =>
            {
                var entity = uow.CategoryRepository.Get(id);
                return mapper.Map<Category>(entity);
            });
        }

        public Task<bool> UpdateRange(IEnumerable<Category> dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = mapper.Map<IEnumerable<CategoryDb>>(dto);
                    uow.CategoryRepository.UpdateRange(entity);

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<Category> Create(Category dto)
        {
            return Task.Run(() =>
            {

                var entity = mapper.Map<CategoryDb>(dto);
                var newEntity = uow.CategoryRepository.Create(entity);
                return mapper.Map<Category>(newEntity);

            });
        }

        public Task<bool> Update(Category dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = mapper.Map<CategoryDb>(dto);
                    uow.CategoryRepository.Update(entity, dto.Id);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }

        public Task<bool> Delete(Category dto)
        {
            return Task.Run(() =>
            {
                try
                {
                    var entity = uow.CategoryRepository.Get(dto.Id);
                    uow.CategoryRepository.Delete(entity);
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
