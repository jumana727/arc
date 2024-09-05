using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.Infrastructure
{

    public static class InfraCommon
    {

        /// <summary>
        /// DbContect register for environment.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            //DatabaseContextFactory factory = new DatabaseContextFactory();
            //factory.CreateDbContext(new string[] { connectionString });
            services.AddDbContext<DatabaseContext>(item => item.UseNpgsql(connectionString));
            services.AddDbContext<DatabaseContext>(options =>options.UseNpgsql(connectionString));

            return services;
        }

    }

}