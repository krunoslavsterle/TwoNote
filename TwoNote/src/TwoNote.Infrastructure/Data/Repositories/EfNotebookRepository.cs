using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;
using TwoNote.Core.Interfaces;

namespace TwoNote.Infrastructure.Data.Repositories
{
    public class EfNotebookRepository : EfGenericRepository<NotebookEntity>, INotebookRepository
    {
        public EfNotebookRepository(TwoNoteContext dbContext) : base(dbContext)
        {
            
        }

        public Task<List<NotebookEntity>> ListNotebooksForUserAsync(Guid userId, params string[] includeProperties)
        {
            var query = dbContext.Set<NotebookEntity>().AsQueryable();
            query = query.Where(p => p.UserId == userId);
            OnIncludeProperties(ref query, includeProperties);

            return query.ToListAsync();
        }
    }
}
