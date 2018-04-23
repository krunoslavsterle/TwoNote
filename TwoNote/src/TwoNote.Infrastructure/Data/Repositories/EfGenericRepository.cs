using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;
using TwoNote.Core.Interfaces;

namespace TwoNote.Infrastructure.Data.Repositories
{
    public class EfGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly TwoNoteContext dbContext;

        #endregion Fields

        #region Consturctors

        public EfGenericRepository(TwoNoteContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        #endregion Consturctors

        #region Methods
        
        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task<T> GetByIdAsync(Guid id, params string[] includeProperties)
        {
            var query = dbContext.Set<T>().Where(p => p.Id == id);
            OnIncludeProperties(ref query, includeProperties);

            return query.FirstOrDefaultAsync();
        }

        public Task<List<T>> ListAsync(params string[] includeProperties)
        {
            var query = dbContext.Set<T>().AsQueryable();
            OnIncludeProperties(ref query, includeProperties);

            return query.ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        private void OnIncludeProperties(ref IQueryable<T> query, params string[] includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                    query = query.Include(property);
            }
        }

        #endregion Methods
    }
}
