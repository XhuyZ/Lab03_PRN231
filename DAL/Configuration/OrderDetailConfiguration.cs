using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);
            builder.Property(od => od.Price).HasColumnType("decimal(18,2)");
            builder.Property(od => od.Quantity).IsRequired();

            builder.HasOne(od => od.Orchid)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrchidId);

            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId);

            builder.HasData(
                new OrderDetail
                {
                    Id = 1,
                    OrchidId = 1,
                    Price = 100.00m,
                    Quantity = 2,
                    OrderId = 1
                },
                new OrderDetail
                {
                    Id = 2,
                    OrchidId = 2,
                    Price = 150.00m,
                    Quantity = 1,
                    OrderId = 2
                }
            );
        }
    }
}

