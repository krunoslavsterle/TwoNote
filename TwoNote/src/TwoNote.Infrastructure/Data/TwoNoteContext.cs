using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;

namespace TwoNote.Infrastructure.Data
{
    public class TwoNoteContext : DbContext
    {
        #region Constructors
        
        public TwoNoteContext(DbContextOptions<TwoNoteContext> options)
            : base(options)
        {
        }

        #endregion Constructors

        #region Properties

        public DbSet<NotebookEntity> Notebooks { get; set; }
        public DbSet<PageEntity> Pages { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotebookEntity>(DbConfigurations.ConfigureNotebookEntity);
            modelBuilder.Entity<PageEntity>(DbConfigurations.ConfigurePageEntity);
        }

        #endregion Methods

    }
}
