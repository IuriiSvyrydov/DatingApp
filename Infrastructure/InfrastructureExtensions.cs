using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbConnection(configuration);
            return services;
        }

        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
