using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(Guid id);
        int Add(T Entity);
        int Update(T Entity);
        int Remove(T Entity);
    }
}
