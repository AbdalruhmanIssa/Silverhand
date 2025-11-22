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
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Set<T>().ToList();

            return _context.Set<T>().AsNoTracking().ToList();
        }


        public T? GetById(Guid id) => _context.Set<T>().Find(id);


        public int Remove(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            return _context.SaveChanges();
        }

        public int Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            return _context.SaveChanges();
        }
    }
}
