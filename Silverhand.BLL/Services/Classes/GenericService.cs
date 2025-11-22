using Mapster;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : Base
    {
        private readonly IGenericRepository<TEntity> _geniricService;
        public GenericService(IGenericRepository<TEntity> geniricService)
        {
            _geniricService = geniricService;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _geniricService.Add(entity);
        }

        public int Delete(Guid id)
        {
            var entity = _geniricService.GetById(id);
            if (entity is null) return 0;

            return _geniricService.Remove(entity);
        }

  

        public IEnumerable<TResponse> GetAll(bool a = false)
        {
            var entities = _geniricService.GetAll();
            
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(Guid id)
        {
            var entity = _geniricService.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

       

        public int Update(Guid id, TRequest request)
        {
            var entity = _geniricService.GetById(id);
            if (entity is null) return 0;
            var updated = request.Adapt(entity);
            return _geniricService.Update(updated);
        }

    }
}
