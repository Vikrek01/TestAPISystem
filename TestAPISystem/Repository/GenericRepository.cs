using System.Collections.Generic;
using System;
using TestAPISystem.Models;
using Microsoft.EntityFrameworkCore;

namespace TestAPISystem.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TestDbContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericRepository(TestDbContext context)
        {
            this._context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<T> Get(int id)
        {
            return await entities.FindAsync(id);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            _context.SaveChanges();
        }
        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            var res = await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await entities.FindAsync(id);
            int res = 0;
            if (entity != null)
            {
                entities.Remove(entity);
                res = await _context.SaveChangesAsync();
            }
            return res > 0;
        }
    }
}
