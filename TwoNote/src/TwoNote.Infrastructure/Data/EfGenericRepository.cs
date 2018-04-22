using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;
using TwoNote.Core.Interfaces;

namespace TwoNote.Infrastructure.Data
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

        public Task<T> GetByIdAsync(Guid id)
        {
            return dbContext.Set<T>().SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<T>> ListAsync()
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        #endregion Methods
    }
}
