using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class OrchidConfiguration : IEntityTypeConfiguration<Orchid>
    {
        public void Configure(EntityTypeBuilder<Orchid> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Description).HasMaxLength(300);
            builder.Property(o => o.Url).HasMaxLength(200);
            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Category)
                   .WithMany(c => c.Orchids)
                   .HasForeignKey(o => o.CategoryId);

            builder.HasData(
                new Orchid
                {
                    Id = 1,
                    Name = "Orchid A",
                    Description = "Beautiful natural orchid",
                    Url = "http://example.com/orchid-a.jpg",
                    Price = 100.00m,
                    IsNatural = true,
                    CategoryId = 1
                },
                new Orchid
                {
                    Id = 2,
                    Name = "Orchid B",
                    Description = "Artificial orchid for decoration",
                    Url = "http://example.com/orchid-b.jpg",
                    Price = 150.00m,
                    IsNatural = false,
                    CategoryId = 2
                }
            );
        }
    }
}

