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
    public class GenericService<TRequest, TResponse, TEntity>
        : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : Base
    {
        private readonly IGenericRepository<TEntity> _repo;

        public GenericService(IGenericRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public async Task<TResponse> CreateAsync(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            await _repo.AddAsync(entity);
            return entity.Adapt<TResponse>();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return 0;

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
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public async Task<int> UpdateAsync(Guid id, TRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return 0;

            var updated = request.Adapt(entity);
            return await _repo.UpdateAsync(updated);
        }
    }

}
