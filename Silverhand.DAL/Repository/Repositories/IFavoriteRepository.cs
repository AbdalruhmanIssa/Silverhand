using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> AddAsync(Favorite entity);
        Task<Favorite?> GetByIdAsync(Guid id);
        Task<Favorite?> GetByProfileAndTitleAsync(Guid profileId, Guid titleId);
        Task<List<Favorite>> GetAllByProfileAsync(Guid profileId);
        Task<bool> DeleteAsync(Guid id);
    }
    }
