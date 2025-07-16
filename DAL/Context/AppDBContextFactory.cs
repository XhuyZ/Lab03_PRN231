using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace DAL.Context
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseMySql(
                "Server=localhost;Database=Application;Port=3306;User Id=root;Password=Wenhui35@;",
                new MySqlServerVersion(new Version(8, 0, 2)) 
            );

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
