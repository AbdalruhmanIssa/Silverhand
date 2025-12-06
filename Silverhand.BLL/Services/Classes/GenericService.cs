using Mapster;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity>// multi-purpose service
        : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : Base
    {
        private readonly IGenericRepository<TEntity> _repo;

        public GenericService(IGenericRepository<TEntity> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<TResponse>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entities =await _repo.GetWhere(predicate);
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public async Task<TResponse> CreateAsync(TRequest request)
        {
            var entity = request.Adapt<TEntity>();// map to entity
            await _repo.AddAsync(entity);
            return entity.Adapt<TResponse>();// map to response
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return 0;// not found

            return await _repo.RemoveAsync(entity);
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync(bool withTracking = false)
        {
            var entities = await _repo.GetAllAsync(withTracking);
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public async Task<TResponse?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? default : entity.Adapt<TResponse>();// map to response
        }

        public async Task<int> UpdateAsync(Guid id, TRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return 0;

            var updated = request.Adapt(entity);// map request to existing entity
            return await _repo.UpdateAsync(updated);
        }
    }

}
