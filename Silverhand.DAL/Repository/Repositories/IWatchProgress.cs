using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IWatchProgressRepository
    {
        Task<WatchProgress?> GetAsync(Guid profileId, Guid? episodeId);
        Task<List<WatchProgress>> GetForProfileAsync(Guid profileId);
        Task AddAsync(WatchProgress entity);
        Task UpdateAsync(WatchProgress entity);
    }

}
