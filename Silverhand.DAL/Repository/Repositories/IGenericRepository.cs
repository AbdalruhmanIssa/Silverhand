using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        Task<T> AddAsync(T entity);
        Task<int> RemoveAsync(T entity);
        Task<int> UpdateAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

    }
}
