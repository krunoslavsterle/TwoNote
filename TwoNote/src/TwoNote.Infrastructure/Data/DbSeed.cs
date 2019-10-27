using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;

namespace TwoNote.Infrastructure.Data
{
    public class DbSeed
    {
        public static async Task SeedAsync(TwoNoteContext context)
        {
            Guid notebookId = Guid.Empty;

            var hasNotebooks = await context.Notebooks.AnyAsync();
            if (!hasNotebooks)
            {
                var notebooks = GetPreconfiguredNotebooks();
                notebookId = notebooks.First().Id;
                context.Notebooks.AddRange(notebooks);
                await context.SaveChangesAsync();
            }           

            var hasPages = await context.Pages.AnyAsync();
            if (!hasPages)
            {
                if (notebookId == Guid.Empty)
                {
                    var notebook = await context.Notebooks.FirstAsync();
                    notebookId = notebook.Id;
                }

                context.Pages.AddRange(GetPreconfiguredPages(notebookId));
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<NotebookEntity> GetPreconfiguredNotebooks()
        {
            return new List<NotebookEntity>
            {
                new NotebookEntity
                {
                    Id = Guid.NewGuid(),                    
                    Name = "Krunoslav's Notebook",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                }
            };
        }

        private static IEnumerable<PageEntity> GetPreconfiguredPages(Guid notebookId)
        {
            return new List<PageEntity>
            {
                new PageEntity
                {
                    Id = Guid.NewGuid(),
                    NotebookId = notebookId,
                    Name = "First Page",
                    Content = "Blank",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                }
            };
        }
    }
}
