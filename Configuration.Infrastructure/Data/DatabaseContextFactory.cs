using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Infrastructure.Data
{

    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {

        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ConfigDb;Username=postgres;Password=admin;");
            return new DatabaseContext(optionsBuilder.Options);
        }

    }

}