using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> AddAsync(Profile entity);
        Task<Profile?> GetByIdAsync(Guid id);
        Task<IEnumerable<Profile>> GetAllByUserIdAsync(Guid userId);
        Task<Profile?> UpdateAsync(Profile entity);
        Task<bool> DeleteAsync(Guid id);
    }



}
