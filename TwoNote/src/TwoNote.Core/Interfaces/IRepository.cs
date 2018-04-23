using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoNote.Core.Entities;

namespace TwoNote.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, params string[] includeProperties);
        Task<List<T>> ListAsync(params string[] includeProperties);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
