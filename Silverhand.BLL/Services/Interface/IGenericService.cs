using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IGenericService<TRequest, TResponse, TEntity>
    {
        IEnumerable<TResponse> GetAll(bool a = false);
        TResponse? GetById(Guid id);
        int Create(TRequest request);
        int Delete(Guid id);
        int Update(Guid id, TRequest request);
    
    }
}
