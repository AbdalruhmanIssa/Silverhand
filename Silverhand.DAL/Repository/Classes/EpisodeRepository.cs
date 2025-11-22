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
    public class EpisodeRepository:GenericRepository<Episode>,IEpisodeRepository
    {
        private readonly ApplicationDbContext _context;

        public EpisodeRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        // Override GetById
        public new Episode? GetById(Guid id)
        {
            return _context.Episodes
                .Include(e => e.Title)
                .FirstOrDefault(e => e.Id == id);
        }

        // Override GetAll
        public new IEnumerable<Episode> GetAll(bool withTracking = false)
        {
            var query = _context.Episodes
                .Include(e => e.Title)
                .AsQueryable();

            return withTracking
                ? query.ToList()
                : query.AsNoTracking().ToList();
        }
    }
}
