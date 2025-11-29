using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IAvailabilityWindowRepository : IGenericRepository<AvailabilityWindow>
    {
        Task<List<AvailabilityWindow>> GetForTitleAsync(Guid titleId);
        Task<List<AvailabilityWindow>> GetForEpisodeAsync(Guid episodeId);
    }

}
