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
    public class EfNotebookRepository : EfGenericRepository<NotebookEntity>
    {
        public EfNotebookRepository(TwoNoteContext dbContext) : base(dbContext)
        {
        }
    }
}
