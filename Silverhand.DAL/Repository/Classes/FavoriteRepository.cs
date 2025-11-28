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
    public class FavoriteRepository :  IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Favorite> AddAsync(Favorite entity)
        {
            await _context.Favorites.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Favorite?> GetByIdAsync(Guid id)
        {
            return await _context.Favorites
                .Include(f => f.Title)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Favorite?> GetByProfileAndTitleAsync(Guid profileId, Guid titleId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f =>
                    f.ProfileId == profileId &&
                    f.TitleId == titleId);
        }

        public async Task<List<Favorite>> GetAllByProfileAsync(Guid profileId)
        {
            return await _context.Favorites
                .Include(f => f.Title)
                .Where(f => f.ProfileId == profileId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var fav = await _context.Favorites.FindAsync(id);
            if (fav == null) return false;

            _context.Favorites.Remove(fav);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
