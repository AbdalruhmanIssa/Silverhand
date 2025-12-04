using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IGenericService<TRequest, TResponse, TEntity>
       where TEntity : Base
    {
        Task<TResponse> CreateAsync(TRequest request);

        Task<int> UpdateAsync(Guid id, TRequest request);

        Task<int> DeleteAsync(Guid id);

        Task<TResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<TResponse>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<TResponse>> GetWhere(Expression<Func<TEntity, bool>> predicate);


    }
}
