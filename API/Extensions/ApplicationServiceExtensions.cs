
using Application.Settings;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddSwaggerGen();

            return services;
        }
    }
}
