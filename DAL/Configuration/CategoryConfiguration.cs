using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);

            builder.HasData(
                new Category { Id = 1, CategoryName = "Natural" },
                new Category { Id = 2, CategoryName = "Artificial" }
            );
        }
    }
}

