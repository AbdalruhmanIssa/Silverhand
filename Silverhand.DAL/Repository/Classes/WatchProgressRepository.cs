using Microsoft.EntityFrameworkCore;
using Silverhand.DAL.Data;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Classes
{
    public class WatchProgressRepository : IWatchProgressRepository
    {
        private readonly ApplicationDbContext _db;

        public WatchProgressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<WatchProgress?> GetAsync(Guid profileId, Guid? episodeId)
        {
            return await _db.WatchProgresses
                .FirstOrDefaultAsync(x =>
                    x.ProfileId == profileId &&
                    x.EpisodeId == episodeId);
        }

        public async Task<List<WatchProgress>> GetForProfileAsync(Guid profileId)
        {
            return await _db.WatchProgresses
                .Where(x => x.ProfileId == profileId)
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(WatchProgress entity)
        {
            await _db.WatchProgresses.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(WatchProgress entity)
        {
            _db.WatchProgresses.Update(entity);
            await _db.SaveChangesAsync();
        }
    }

}
