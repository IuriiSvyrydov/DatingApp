
using Application.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class AddApplicationExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

        }
    }
}
