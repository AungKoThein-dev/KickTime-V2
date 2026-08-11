using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KickTime.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}