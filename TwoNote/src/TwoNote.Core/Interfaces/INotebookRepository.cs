using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoNote.Core.Entities;

namespace TwoNote.Core.Interfaces
{
    public interface INotebookRepository : IRepository<NotebookEntity>
    {
        Task<List<NotebookEntity>> ListNotebooksForUserAsync(Guid userId, params string[] includeProperties);
    }
}
