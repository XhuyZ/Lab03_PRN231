using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.OrderStatus).IsRequired().HasMaxLength(50);
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Account)
                   .WithMany(a => a.Orders)
                   .HasForeignKey(o => o.AccountId);

            builder.HasData(
                new Order
                {
                    Id = 1,
                    AccountId = 2,
                    OrderDate = new DateTime(2025, 7, 10),
                    OrderStatus = "Pending",
                    TotalAmount = 300.00m
                },
                new Order
                {
                    Id = 2,
                    AccountId = 2,
                    OrderDate = new DateTime(2025, 7, 15),
                    OrderStatus = "Completed",
                    TotalAmount = 150.00m
                }
            );
        }
    }
}

