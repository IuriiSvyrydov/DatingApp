
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class AddApplicationExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();

        }
    }
}
