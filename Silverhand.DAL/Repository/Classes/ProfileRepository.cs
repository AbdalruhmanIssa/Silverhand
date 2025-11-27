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
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> AddAsync(Profile entity)
        {
            await _context.Profiles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // ------------------------------------
        // GET BY ID
        // ------------------------------------
        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // ------------------------------------
        // GET ALL PROFILES FOR USER
        // ------------------------------------
        public async Task<IEnumerable<Profile>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Profiles
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        // ------------------------------------
        // UPDATE
        // ------------------------------------
        public async Task<Profile?> UpdateAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        // ------------------------------------
        // DELETE
        // ------------------------------------
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Profiles.FindAsync(id);
            if (entity == null)
                return false;

            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
