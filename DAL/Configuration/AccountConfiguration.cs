using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AccountName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Password).IsRequired();

            builder.HasOne(a => a.Role)
                   .WithMany(r => r.Accounts)
                   .HasForeignKey(a => a.RoleId);

            builder.HasData(
                new Account
                {
                    Id = 1,
                    AccountName = "admin",
                    Email = "admin@orchid.com",
                    Password = "1@", 
                    RoleId = 1
                },
                new Account
                {
                    Id = 2,
                    AccountName = "john_doe",
                    Email = "john@example.com",
                    Password = "1@",
                    RoleId = 2
                }
            );
        }
    }
}

