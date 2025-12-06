using Microsoft.EntityFrameworkCore;
using Silverhand.DAL.Data;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base//has base properties
    {
        //object of dbcontext
        private readonly ApplicationDbContext _context;
        //constructor injection
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //method to get entities based on a condition
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()//DbSet of T
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync(bool withTracking = false)
        {
            if (withTracking)//track changes
                return await _context.Set<T>().ToListAsync();

            return await _context.Set<T>().AsNoTracking().ToListAsync();//no tracking
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }
    }

}
