using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TwoNote.Core.Entities;

namespace TwoNote.Infrastructure.Data
{
    public class DbConfigurations
    {
        public static void ConfigureNotebookEntity(EntityTypeBuilder<NotebookEntity> builder)
        {
            builder.ToTable("Notebook");
            builder.HasKey(p => p.Id);
            
            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.DateCreated)
                .IsRequired();

            builder.Property(p => p.DateUpdated)
                .IsRequired();
        }

        public static void ConfigurePageEntity(EntityTypeBuilder<PageEntity> builder)
        {
            builder.ToTable("Page");
            builder.HasKey(p => p.Id);
                       
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.DateCreated)
                .IsRequired();

            builder.Property(p => p.DateUpdated)
                .IsRequired();
        }
    }
}
