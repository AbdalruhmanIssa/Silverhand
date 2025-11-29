using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Classes
{
    using Microsoft.EntityFrameworkCore;
    using Silverhand.DAL.Data;
    using Silverhand.DAL.Models;
    using Silverhand.DAL.Repository.Repositories;

    public class AvailabilityWindowRepository
        : GenericRepository<AvailabilityWindow>, IAvailabilityWindowRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityWindowRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<AvailabilityWindow>> GetForTitleAsync(Guid titleId)
        {
            return await _context.AvailabilityWindows
                .Where(x => x.TitleId == titleId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<AvailabilityWindow>> GetForEpisodeAsync(Guid episodeId)
        {
            return await _context.AvailabilityWindows
                .Where(x => x.EpisodeId == episodeId && !x.IsDeleted)
                .ToListAsync();
        }
    }

}
